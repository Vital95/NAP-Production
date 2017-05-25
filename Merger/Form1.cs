using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Merger.Data_structures;

delegate void LogicChoser();

namespace Merger
{
    public partial class Merge : Form
    {
        public Merge()
        {
            InitializeComponent();
            InitDragAndDrop();
        }

        //relation with excelObjects in threads
        public delegate void UpdateProgressBar();

        #region Drag and Drop

        /// <summary>
        /// Initialize Drag and drop events
        /// </summary>
        private void InitDragAndDrop()
        {
            listFilesWithData.DragEnter += ListFilesWithData_DragEnter;
            listFilesWithStructure.DragEnter += ListFilesWithStructure_DragEnter;
            listFilesWithData.DragDrop += ListFilesWithData_DragDrop;
            listFilesWithStructure.DragDrop += ListFilesWithStructure_DragDrop;
            SetScrollBars(listFilesWithData);
            SetScrollBars(listFilesWithStructure);
        }

        /// <summary>
        /// Set fields and mods for Scroll bars
        /// </summary>
        /// <param name="obj"></param>
        private void SetScrollBars(object obj)
        {
            if(obj is ListBox)
            {
                (obj as ListBox).HorizontalScrollbar = true;
                (obj as ListBox).DrawMode = DrawMode.OwnerDrawFixed;
                (obj as ListBox).DrawItem += Merge_DrawItem;
                (obj as ListBox).RightToLeft = RightToLeft.Yes;
            }
        }

        /// <summary>
        /// Render a nice view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Merge_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            Brush myBrush = Brushes.Black;

            switch (e.Index)
            {
                case 0:
                    myBrush = Brushes.Red;
                    break;
                case 1:
                    myBrush = Brushes.Orange;
                    break;
                case 2:
                    myBrush = Brushes.Purple;
                    break;
            }

            e.Graphics.DrawString((sender as ListBox).Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            e.DrawFocusRectangle();
        }

#region callbacks 

        private void ListFilesWithStructure_DragDrop(object sender, DragEventArgs e)
        {
            GetFiles(sender,e);
        }

        private void ListFilesWithData_DragDrop(object sender, DragEventArgs e)
        {
            GetFiles(sender,e);
        }

        private void ListFilesWithStructure_DragEnter(object sender, DragEventArgs e)
        {
            SetDragDropEffect(e);
        }

        private void ListFilesWithData_DragEnter(object sender, DragEventArgs e)
        {
            SetDragDropEffect(e);
        }

        private void SetDragDropEffect(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }


        /// <summary>
        /// Get Files and view them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetFiles(object sender,DragEventArgs e)
        {
            (sender as ListBox).Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                (sender as ListBox).Items.Add(file);

            int width = 0;
            Graphics g = (sender as ListBox).CreateGraphics();

            foreach (object item in (sender as ListBox).Items)
            {
                string text = item.ToString();
                SizeF s = g.MeasureString(text, (sender as ListBox).Font);
                if (s.Width > width)
                    width = (int)s.Width+1;
            }

            (sender as ListBox).HorizontalExtent = width;
        }

#endregion

        #endregion

        /// <summary>
        /// On botton "OK" click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void go_Click(object sender, EventArgs e)
        {
            View view = new View();
            if (view.ValidateListBoxItems(listFilesWithData, listFilesWithStructure))
            {
                try {
                    Task.Factory.StartNew(
                        () => DoWork()
                    );
                }
                catch(Exception ex)
                {
                    if (ex != null)
                        MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Something Wrong","Error",MessageBoxButtons.OK);
            }
            
        }

        /// <summary>
        /// Main paralell invoking method
        /// </summary>
        private void DoWork()
        {
            List<ExcelObject> excelObjects = new List<ExcelObject>();
            try
            {
                View view = new View();
                excelObjects = view.CreateExcelObjects(listFilesWithData, listFilesWithStructure, modeSelectorBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
            Parallel.ForEach(excelObjects, currentFile =>
            {
                MergeExcelFiles(currentFile);
            }
            );
        }

        /// <summary>
        /// Update Progress bar by 1 in thread-safe manner
        /// </summary>
        public void UpdateProgressBarBy1()
        {
            this.Invoke((Action)delegate
            {
                if (!((totalProgressBar.Value + 1) > totalProgressBar.Maximum))
                    this.totalProgressBar.Value += 1;
            });
        }

        /// <summary>
        /// Distinguish what modes are selected and launch appropriate methods
        /// </summary>
        /// <param name="obj"></param>
        private void MergeExcelFiles(ExcelObject obj)
        {
            DelegateLogic dl = new DelegateLogic();

            LogicChoser logicCh;

            if (obj.mode == 1)
            {
                dl.SetExcelObject(obj);
                dl.Changed += dl_Changed;
                LogicChoser logicStyle = new LogicChoser(dl.FillStyles);
                logicCh = logicStyle;

                this.Invoke((Action)delegate{
                    this.totalProgressBar.Maximum += dl.GetMaximumStepCount() * 2;
                });

                logicCh.Invoke();
            }
            if (obj.mode == 2)
            {
                dl.SetExcelObject(obj);
                dl.Changed += dl_Changed;
                LogicChoser logicStyle = new LogicChoser(dl.FillStyles);
                LogicChoser logicStyleAndPounds = new LogicChoser(dl.FillPounds);
                logicCh = logicStyle;
                logicCh += logicStyleAndPounds;
                
                this.Invoke((Action)delegate
                {
                    this.totalProgressBar.Maximum += dl.GetMaximumStepCount() * 2;
                });

                logicCh.Invoke();
            }

            
            Helper help = new Helper();
            string outputString = help.ListOfStringsToString(dl.GetOutputString());

            this.Invoke((Action)delegate
            {
                this.errorRichTextBox.AppendText(outputString);
            });
            
            UpdateProgressBarBy1();
        }

        /// <summary>
        /// on Change handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dl_Changed(object sender, EventArgs e)
        {
            UpdateProgressBarBy1();
        }

        /// <summary>
        /// Append data to chosen RichTextBox
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="outputString"></param>
        void OutDataIntoTheRichTextBox(object obj, string outputString)
        {
            if (obj is RichTextBox)
                (obj as RichTextBox).AppendText(outputString);
        }
    }
}
