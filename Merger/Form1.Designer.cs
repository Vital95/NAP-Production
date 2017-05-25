namespace Merger
{
    partial class Merge
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.go = new System.Windows.Forms.Button();
            this.modeSelectorBox = new System.Windows.Forms.GroupBox();
            this.styleAndPoundRadioBtn = new System.Windows.Forms.RadioButton();
            this.styleRadioBtn = new System.Windows.Forms.RadioButton();
            this.listFilesWithData = new System.Windows.Forms.ListBox();
            this.listFilesWithStructure = new System.Windows.Forms.ListBox();
            this.totalProgressBar = new System.Windows.Forms.ProgressBar();
            this.errorLogBox = new System.Windows.Forms.GroupBox();
            this.errorRichTextBox = new System.Windows.Forms.RichTextBox();
            this.DataLabel = new System.Windows.Forms.Label();
            this.StructureLabel = new System.Windows.Forms.Label();
            this.modeSelectorBox.SuspendLayout();
            this.errorLogBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(296, 106);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(104, 53);
            this.go.TabIndex = 0;
            this.go.Text = "GO";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // modeSelectorBox
            // 
            this.modeSelectorBox.Controls.Add(this.styleAndPoundRadioBtn);
            this.modeSelectorBox.Controls.Add(this.styleRadioBtn);
            this.modeSelectorBox.Location = new System.Drawing.Point(296, 12);
            this.modeSelectorBox.Name = "modeSelectorBox";
            this.modeSelectorBox.Size = new System.Drawing.Size(104, 88);
            this.modeSelectorBox.TabIndex = 2;
            this.modeSelectorBox.TabStop = false;
            this.modeSelectorBox.Text = "Mode";
            // 
            // styleAndPoundRadioBtn
            // 
            this.styleAndPoundRadioBtn.AutoSize = true;
            this.styleAndPoundRadioBtn.Location = new System.Drawing.Point(7, 57);
            this.styleAndPoundRadioBtn.Name = "styleAndPoundRadioBtn";
            this.styleAndPoundRadioBtn.Size = new System.Drawing.Size(78, 17);
            this.styleAndPoundRadioBtn.TabIndex = 1;
            this.styleAndPoundRadioBtn.TabStop = true;
            this.styleAndPoundRadioBtn.Text = "Style and £";
            this.styleAndPoundRadioBtn.UseVisualStyleBackColor = true;
            // 
            // styleRadioBtn
            // 
            this.styleRadioBtn.AutoSize = true;
            this.styleRadioBtn.Location = new System.Drawing.Point(7, 20);
            this.styleRadioBtn.Name = "styleRadioBtn";
            this.styleRadioBtn.Size = new System.Drawing.Size(48, 17);
            this.styleRadioBtn.TabIndex = 0;
            this.styleRadioBtn.TabStop = true;
            this.styleRadioBtn.Text = "Style";
            this.styleRadioBtn.UseVisualStyleBackColor = true;
            // 
            // listFilesWithData
            // 
            this.listFilesWithData.AllowDrop = true;
            this.listFilesWithData.HorizontalScrollbar = true;
            this.listFilesWithData.Location = new System.Drawing.Point(12, 12);
            this.listFilesWithData.Name = "listFilesWithData";
            this.listFilesWithData.ScrollAlwaysVisible = true;
            this.listFilesWithData.Size = new System.Drawing.Size(278, 147);
            this.listFilesWithData.TabIndex = 3;
            // 
            // listFilesWithStructure
            // 
            this.listFilesWithStructure.AllowDrop = true;
            this.listFilesWithStructure.HorizontalScrollbar = true;
            this.listFilesWithStructure.Location = new System.Drawing.Point(406, 12);
            this.listFilesWithStructure.Name = "listFilesWithStructure";
            this.listFilesWithStructure.ScrollAlwaysVisible = true;
            this.listFilesWithStructure.Size = new System.Drawing.Size(278, 147);
            this.listFilesWithStructure.TabIndex = 4;
            // 
            // totalProgressBar
            // 
            this.totalProgressBar.Location = new System.Drawing.Point(11, 178);
            this.totalProgressBar.Maximum = 1;
            this.totalProgressBar.Name = "totalProgressBar";
            this.totalProgressBar.Size = new System.Drawing.Size(673, 23);
            this.totalProgressBar.Step = 1;
            this.totalProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.totalProgressBar.TabIndex = 5;
            // 
            // errorLogBox
            // 
            this.errorLogBox.Controls.Add(this.errorRichTextBox);
            this.errorLogBox.Location = new System.Drawing.Point(11, 207);
            this.errorLogBox.Name = "errorLogBox";
            this.errorLogBox.Size = new System.Drawing.Size(673, 136);
            this.errorLogBox.TabIndex = 6;
            this.errorLogBox.TabStop = false;
            this.errorLogBox.Text = "Errors log";
            // 
            // errorRichTextBox
            // 
            this.errorRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorRichTextBox.Location = new System.Drawing.Point(6, 18);
            this.errorRichTextBox.Name = "errorRichTextBox";
            this.errorRichTextBox.Size = new System.Drawing.Size(660, 112);
            this.errorRichTextBox.TabIndex = 0;
            this.errorRichTextBox.Text = "";
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.Location = new System.Drawing.Point(12, 162);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(79, 13);
            this.DataLabel.TabIndex = 7;
            this.DataLabel.Text = "Files With Data";
            // 
            // StructureLabel
            // 
            this.StructureLabel.AutoSize = true;
            this.StructureLabel.Location = new System.Drawing.Point(590, 162);
            this.StructureLabel.Name = "StructureLabel";
            this.StructureLabel.Size = new System.Drawing.Size(99, 13);
            this.StructureLabel.TabIndex = 8;
            this.StructureLabel.Text = "Files With Structure";
            // 
            // Merge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 355);
            this.Controls.Add(this.StructureLabel);
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.errorLogBox);
            this.Controls.Add(this.totalProgressBar);
            this.Controls.Add(this.listFilesWithStructure);
            this.Controls.Add(this.listFilesWithData);
            this.Controls.Add(this.modeSelectorBox);
            this.Controls.Add(this.go);
            this.Name = "Merge";
            this.Text = "Merge";
            this.modeSelectorBox.ResumeLayout(false);
            this.modeSelectorBox.PerformLayout();
            this.errorLogBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button go;
        private System.Windows.Forms.GroupBox modeSelectorBox;
        private System.Windows.Forms.RadioButton styleAndPoundRadioBtn;
        private System.Windows.Forms.RadioButton styleRadioBtn;
        private System.Windows.Forms.ListBox listFilesWithData;
        private System.Windows.Forms.ListBox listFilesWithStructure;
        private System.Windows.Forms.ProgressBar totalProgressBar;
        private System.Windows.Forms.GroupBox errorLogBox;
        private System.Windows.Forms.RichTextBox errorRichTextBox;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.Label StructureLabel;
    }
}

