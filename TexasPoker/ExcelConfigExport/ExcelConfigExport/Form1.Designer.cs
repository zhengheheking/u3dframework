namespace ExcelConfigExport
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadClientExcelButton = new System.Windows.Forms.Button();
            this.ExcelListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ExportClientDataButton = new System.Windows.Forms.Button();
            this.ExportCSharpButton = new System.Windows.Forms.Button();
            this.ExportJavaButton = new System.Windows.Forms.Button();
            this.LoadServerExcel = new System.Windows.Forms.Button();
            this.ExportServerDataButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadClientExcelButton
            // 
            this.LoadClientExcelButton.Location = new System.Drawing.Point(42, 26);
            this.LoadClientExcelButton.Name = "LoadClientExcelButton";
            this.LoadClientExcelButton.Size = new System.Drawing.Size(120, 23);
            this.LoadClientExcelButton.TabIndex = 0;
            this.LoadClientExcelButton.Text = "加载客户端Excel";
            this.LoadClientExcelButton.UseVisualStyleBackColor = true;
            this.LoadClientExcelButton.Click += new System.EventHandler(this.LoadExcelButton_Click);
            // 
            // ExcelListBox
            // 
            this.ExcelListBox.FormattingEnabled = true;
            this.ExcelListBox.ItemHeight = 12;
            this.ExcelListBox.Location = new System.Drawing.Point(42, 63);
            this.ExcelListBox.Name = "ExcelListBox";
            this.ExcelListBox.Size = new System.Drawing.Size(400, 460);
            this.ExcelListBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LogRichTextBox);
            this.groupBox1.Location = new System.Drawing.Point(42, 538);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1059, 225);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Location = new System.Drawing.Point(24, 31);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.Size = new System.Drawing.Size(1018, 188);
            this.LogRichTextBox.TabIndex = 0;
            this.LogRichTextBox.Text = "";
            // 
            // ExportClientDataButton
            // 
            this.ExportClientDataButton.Location = new System.Drawing.Point(501, 107);
            this.ExportClientDataButton.Name = "ExportClientDataButton";
            this.ExportClientDataButton.Size = new System.Drawing.Size(142, 23);
            this.ExportClientDataButton.TabIndex = 3;
            this.ExportClientDataButton.Text = "导出客户端数据文件";
            this.ExportClientDataButton.UseVisualStyleBackColor = true;
            this.ExportClientDataButton.Click += new System.EventHandler(this.ExportDataButton_Click);
            // 
            // ExportCSharpButton
            // 
            this.ExportCSharpButton.Location = new System.Drawing.Point(501, 250);
            this.ExportCSharpButton.Name = "ExportCSharpButton";
            this.ExportCSharpButton.Size = new System.Drawing.Size(108, 23);
            this.ExportCSharpButton.TabIndex = 4;
            this.ExportCSharpButton.Text = "导出CSharp代码";
            this.ExportCSharpButton.UseVisualStyleBackColor = true;
            this.ExportCSharpButton.Click += new System.EventHandler(this.ExportCSharpButton_Click);
            // 
            // ExportJavaButton
            // 
            this.ExportJavaButton.Location = new System.Drawing.Point(501, 326);
            this.ExportJavaButton.Name = "ExportJavaButton";
            this.ExportJavaButton.Size = new System.Drawing.Size(108, 23);
            this.ExportJavaButton.TabIndex = 5;
            this.ExportJavaButton.Text = "导出Java代码";
            this.ExportJavaButton.UseVisualStyleBackColor = true;
            this.ExportJavaButton.Click += new System.EventHandler(this.ExportJavaButton_Click);
            // 
            // LoadServerExcel
            // 
            this.LoadServerExcel.Location = new System.Drawing.Point(256, 26);
            this.LoadServerExcel.Name = "LoadServerExcel";
            this.LoadServerExcel.Size = new System.Drawing.Size(128, 23);
            this.LoadServerExcel.TabIndex = 6;
            this.LoadServerExcel.Text = "加载服务端Excel";
            this.LoadServerExcel.UseVisualStyleBackColor = true;
            this.LoadServerExcel.Click += new System.EventHandler(this.LoadServerExcel_Click);
            // 
            // ExportServerDataButton
            // 
            this.ExportServerDataButton.Location = new System.Drawing.Point(501, 176);
            this.ExportServerDataButton.Name = "ExportServerDataButton";
            this.ExportServerDataButton.Size = new System.Drawing.Size(142, 23);
            this.ExportServerDataButton.TabIndex = 7;
            this.ExportServerDataButton.Text = "导出服务端数据文件";
            this.ExportServerDataButton.UseVisualStyleBackColor = true;
            this.ExportServerDataButton.Click += new System.EventHandler(this.ExportServerDataButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 775);
            this.Controls.Add(this.ExportServerDataButton);
            this.Controls.Add(this.LoadServerExcel);
            this.Controls.Add(this.ExportJavaButton);
            this.Controls.Add(this.ExportCSharpButton);
            this.Controls.Add(this.ExportClientDataButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExcelListBox);
            this.Controls.Add(this.LoadClientExcelButton);
            this.Name = "Form1";
            this.Text = "配置导出工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadClientExcelButton;
        private System.Windows.Forms.ListBox ExcelListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private System.Windows.Forms.Button ExportClientDataButton;
        private System.Windows.Forms.Button ExportCSharpButton;
        private System.Windows.Forms.Button ExportJavaButton;
        private System.Windows.Forms.Button LoadServerExcel;
        private System.Windows.Forms.Button ExportServerDataButton;

    }
}

