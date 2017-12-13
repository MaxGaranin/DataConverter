namespace DataConverter
{
    partial class FrmConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConverter));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.formatGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFormatOut = new System.Windows.Forms.ComboBox();
            this.cbxFormatIn = new System.Windows.Forms.ComboBox();
            this.outFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.txtSubFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtSelectedFolder = new System.Windows.Forms.TextBox();
            this.radioSubFolder = new System.Windows.Forms.RadioButton();
            this.radioSelectedFolder = new System.Windows.Forms.RadioButton();
            this.dataTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.radioPoint = new System.Windows.Forms.RadioButton();
            this.radioGrid2D = new System.Windows.Forms.RadioButton();
            this.radioCont = new System.Windows.Forms.RadioButton();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.formatGroupBox.SuspendLayout();
            this.outFolderGroupBox.SuspendLayout();
            this.dataTypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(816, 585);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Location = new System.Drawing.Point(721, 585);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(89, 29);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Обработать";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.formatGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.outFolderGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.dataTypeGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectFiles);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.lstFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtLog);
            this.splitContainer1.Size = new System.Drawing.Size(879, 563);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 3;
            // 
            // formatGroupBox
            // 
            this.formatGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.formatGroupBox.Controls.Add(this.label2);
            this.formatGroupBox.Controls.Add(this.label1);
            this.formatGroupBox.Controls.Add(this.cbxFormatOut);
            this.formatGroupBox.Controls.Add(this.cbxFormatIn);
            this.formatGroupBox.Location = new System.Drawing.Point(497, 150);
            this.formatGroupBox.Name = "formatGroupBox";
            this.formatGroupBox.Size = new System.Drawing.Size(379, 117);
            this.formatGroupBox.TabIndex = 20;
            this.formatGroupBox.TabStop = false;
            this.formatGroupBox.Text = "Форматы файлов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Выходной формат";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Входной формат";
            // 
            // cbxFormatOut
            // 
            this.cbxFormatOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFormatOut.FormattingEnabled = true;
            this.cbxFormatOut.Location = new System.Drawing.Point(125, 73);
            this.cbxFormatOut.Name = "cbxFormatOut";
            this.cbxFormatOut.Size = new System.Drawing.Size(242, 21);
            this.cbxFormatOut.TabIndex = 16;
            // 
            // cbxFormatIn
            // 
            this.cbxFormatIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFormatIn.FormattingEnabled = true;
            this.cbxFormatIn.Location = new System.Drawing.Point(125, 35);
            this.cbxFormatIn.Name = "cbxFormatIn";
            this.cbxFormatIn.Size = new System.Drawing.Size(242, 21);
            this.cbxFormatIn.TabIndex = 15;
            // 
            // outFolderGroupBox
            // 
            this.outFolderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outFolderGroupBox.Controls.Add(this.txtSubFolder);
            this.outFolderGroupBox.Controls.Add(this.btnSelectFolder);
            this.outFolderGroupBox.Controls.Add(this.txtSelectedFolder);
            this.outFolderGroupBox.Controls.Add(this.radioSubFolder);
            this.outFolderGroupBox.Controls.Add(this.radioSelectedFolder);
            this.outFolderGroupBox.Location = new System.Drawing.Point(499, 284);
            this.outFolderGroupBox.Name = "outFolderGroupBox";
            this.outFolderGroupBox.Size = new System.Drawing.Size(377, 142);
            this.outFolderGroupBox.TabIndex = 19;
            this.outFolderGroupBox.TabStop = false;
            this.outFolderGroupBox.Text = "Вывод файлов";
            // 
            // txtSubFolder
            // 
            this.txtSubFolder.Location = new System.Drawing.Point(19, 108);
            this.txtSubFolder.Name = "txtSubFolder";
            this.txtSubFolder.Size = new System.Drawing.Size(100, 20);
            this.txtSubFolder.TabIndex = 4;
            this.txtSubFolder.Text = "Out";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(340, 52);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(25, 20);
            this.btnSelectFolder.TabIndex = 3;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtSelectedFolder
            // 
            this.txtSelectedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectedFolder.Location = new System.Drawing.Point(19, 52);
            this.txtSelectedFolder.Name = "txtSelectedFolder";
            this.txtSelectedFolder.Size = new System.Drawing.Size(320, 20);
            this.txtSelectedFolder.TabIndex = 2;
            // 
            // radioSubFolder
            // 
            this.radioSubFolder.AutoSize = true;
            this.radioSubFolder.Checked = true;
            this.radioSubFolder.Location = new System.Drawing.Point(19, 85);
            this.radioSubFolder.Name = "radioSubFolder";
            this.radioSubFolder.Size = new System.Drawing.Size(308, 17);
            this.radioSubFolder.TabIndex = 1;
            this.radioSubFolder.TabStop = true;
            this.radioSubFolder.Text = "Подпапка относительно папки с выбранными файлами";
            this.radioSubFolder.UseVisualStyleBackColor = true;
            this.radioSubFolder.CheckedChanged += new System.EventHandler(this.radioFolders_CheckedChanged);
            // 
            // radioSelectedFolder
            // 
            this.radioSelectedFolder.AutoSize = true;
            this.radioSelectedFolder.Location = new System.Drawing.Point(19, 29);
            this.radioSelectedFolder.Name = "radioSelectedFolder";
            this.radioSelectedFolder.Size = new System.Drawing.Size(107, 17);
            this.radioSelectedFolder.TabIndex = 0;
            this.radioSelectedFolder.TabStop = true;
            this.radioSelectedFolder.Text = "Заданная папка";
            this.radioSelectedFolder.UseVisualStyleBackColor = true;
            this.radioSelectedFolder.CheckedChanged += new System.EventHandler(this.radioFolders_CheckedChanged);
            // 
            // dataTypeGroupBox
            // 
            this.dataTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTypeGroupBox.Controls.Add(this.radioPoint);
            this.dataTypeGroupBox.Controls.Add(this.radioGrid2D);
            this.dataTypeGroupBox.Controls.Add(this.radioCont);
            this.dataTypeGroupBox.Location = new System.Drawing.Point(497, 13);
            this.dataTypeGroupBox.Name = "dataTypeGroupBox";
            this.dataTypeGroupBox.Size = new System.Drawing.Size(379, 121);
            this.dataTypeGroupBox.TabIndex = 18;
            this.dataTypeGroupBox.TabStop = false;
            this.dataTypeGroupBox.Text = "Тип данных";
            // 
            // radioPoint
            // 
            this.radioPoint.AutoSize = true;
            this.radioPoint.Enabled = false;
            this.radioPoint.Location = new System.Drawing.Point(15, 88);
            this.radioPoint.Name = "radioPoint";
            this.radioPoint.Size = new System.Drawing.Size(55, 17);
            this.radioPoint.TabIndex = 12;
            this.radioPoint.TabStop = true;
            this.radioPoint.Text = "Точки";
            this.radioPoint.UseVisualStyleBackColor = true;
            this.radioPoint.CheckedChanged += new System.EventHandler(this.radioDataType_CheckedChanged);
            // 
            // radioGrid2D
            // 
            this.radioGrid2D.AutoSize = true;
            this.radioGrid2D.Location = new System.Drawing.Point(15, 55);
            this.radioGrid2D.Name = "radioGrid2D";
            this.radioGrid2D.Size = new System.Drawing.Size(91, 17);
            this.radioGrid2D.TabIndex = 11;
            this.radioGrid2D.TabStop = true;
            this.radioGrid2D.Text = "Поверхности";
            this.radioGrid2D.UseVisualStyleBackColor = true;
            this.radioGrid2D.CheckedChanged += new System.EventHandler(this.radioDataType_CheckedChanged);
            // 
            // radioCont
            // 
            this.radioCont.AutoSize = true;
            this.radioCont.Checked = true;
            this.radioCont.Location = new System.Drawing.Point(15, 23);
            this.radioCont.Name = "radioCont";
            this.radioCont.Size = new System.Drawing.Size(68, 17);
            this.radioCont.TabIndex = 10;
            this.radioCont.TabStop = true;
            this.radioCont.Text = "Контуры";
            this.radioCont.UseVisualStyleBackColor = true;
            this.radioCont.CheckedChanged += new System.EventHandler(this.radioDataType_CheckedChanged);
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Location = new System.Drawing.Point(302, 23);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(75, 26);
            this.btnSelectFiles.TabIndex = 17;
            this.btnSelectFiles.Text = "Выбрать";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Файлы для обработки";
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.HorizontalScrollbar = true;
            this.lstFiles.Location = new System.Drawing.Point(0, 23);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(296, 407);
            this.lstFiles.TabIndex = 15;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(879, 122);
            this.txtLog.TabIndex = 0;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveConfig.Location = new System.Drawing.Point(113, 585);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(129, 29);
            this.btnSaveConfig.TabIndex = 4;
            this.btnSaveConfig.Text = "Сохранить настройки";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.Location = new System.Drawing.Point(12, 585);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(95, 29);
            this.btnAbout.TabIndex = 5;
            this.btnAbout.Text = "О программе...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // FrmConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 622);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConverter";
            this.Text = "Конвертор данных";
            this.Load += new System.EventHandler(this.FrmConvertor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.formatGroupBox.ResumeLayout(false);
            this.formatGroupBox.PerformLayout();
            this.outFolderGroupBox.ResumeLayout(false);
            this.outFolderGroupBox.PerformLayout();
            this.dataTypeGroupBox.ResumeLayout(false);
            this.dataTypeGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox dataTypeGroupBox;
        private System.Windows.Forms.RadioButton radioPoint;
        private System.Windows.Forms.RadioButton radioGrid2D;
        private System.Windows.Forms.RadioButton radioCont;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox outFolderGroupBox;
        private System.Windows.Forms.TextBox txtSubFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtSelectedFolder;
        private System.Windows.Forms.RadioButton radioSubFolder;
        private System.Windows.Forms.RadioButton radioSelectedFolder;
        private System.Windows.Forms.GroupBox formatGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFormatOut;
        private System.Windows.Forms.ComboBox cbxFormatIn;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnAbout;
    }
}