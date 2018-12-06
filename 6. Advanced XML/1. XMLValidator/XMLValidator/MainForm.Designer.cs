namespace XMLValidator
{
	partial class MainForm
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
			this.BrowseXmlBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.XmlTB = new System.Windows.Forms.TextBox();
			this.XsdTB = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.BrowseXsdBtn = new System.Windows.Forms.Button();
			this.ValidateBtn = new System.Windows.Forms.Button();
			this.OutputTB = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.XsltTB = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.BrowseXsltBtn = new System.Windows.Forms.Button();
			this.ConvertBtn = new System.Windows.Forms.Button();
			this.FileNameTB = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BrowseXmlBtn
			// 
			this.BrowseXmlBtn.Location = new System.Drawing.Point(461, 13);
			this.BrowseXmlBtn.Name = "BrowseXmlBtn";
			this.BrowseXmlBtn.Size = new System.Drawing.Size(77, 32);
			this.BrowseXmlBtn.TabIndex = 0;
			this.BrowseXmlBtn.Text = "Browse";
			this.BrowseXmlBtn.UseVisualStyleBackColor = true;
			this.BrowseXmlBtn.Click += new System.EventHandler(this.BrowseXmlBtn_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "XML file";
			// 
			// XmlTB
			// 
			this.XmlTB.Location = new System.Drawing.Point(85, 18);
			this.XmlTB.Name = "XmlTB";
			this.XmlTB.ReadOnly = true;
			this.XmlTB.Size = new System.Drawing.Size(360, 22);
			this.XmlTB.TabIndex = 2;
			// 
			// XsdTB
			// 
			this.XsdTB.Location = new System.Drawing.Point(85, 68);
			this.XsdTB.Name = "XsdTB";
			this.XsdTB.ReadOnly = true;
			this.XsdTB.Size = new System.Drawing.Size(360, 22);
			this.XsdTB.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "XSD file";
			// 
			// BrowseXsdBtn
			// 
			this.BrowseXsdBtn.Location = new System.Drawing.Point(461, 63);
			this.BrowseXsdBtn.Name = "BrowseXsdBtn";
			this.BrowseXsdBtn.Size = new System.Drawing.Size(77, 32);
			this.BrowseXsdBtn.TabIndex = 3;
			this.BrowseXsdBtn.Text = "Browse";
			this.BrowseXsdBtn.UseVisualStyleBackColor = true;
			this.BrowseXsdBtn.Click += new System.EventHandler(this.BrowseXsdBtn_Click);
			// 
			// ValidateBtn
			// 
			this.ValidateBtn.Location = new System.Drawing.Point(461, 407);
			this.ValidateBtn.Name = "ValidateBtn";
			this.ValidateBtn.Size = new System.Drawing.Size(77, 32);
			this.ValidateBtn.TabIndex = 6;
			this.ValidateBtn.Text = "Validate";
			this.ValidateBtn.UseVisualStyleBackColor = true;
			this.ValidateBtn.Click += new System.EventHandler(this.ValidateBtn_Click);
			// 
			// OutputTB
			// 
			this.OutputTB.Location = new System.Drawing.Point(24, 247);
			this.OutputTB.Multiline = true;
			this.OutputTB.Name = "OutputTB";
			this.OutputTB.ReadOnly = true;
			this.OutputTB.Size = new System.Drawing.Size(421, 192);
			this.OutputTB.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 227);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Output:";
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// XsltTB
			// 
			this.XsltTB.Location = new System.Drawing.Point(85, 132);
			this.XsltTB.Name = "XsltTB";
			this.XsltTB.ReadOnly = true;
			this.XsltTB.Size = new System.Drawing.Size(360, 22);
			this.XsltTB.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 135);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 17);
			this.label4.TabIndex = 10;
			this.label4.Text = "XSLT file";
			// 
			// BrowseXsltBtn
			// 
			this.BrowseXsltBtn.Location = new System.Drawing.Point(461, 127);
			this.BrowseXsltBtn.Name = "BrowseXsltBtn";
			this.BrowseXsltBtn.Size = new System.Drawing.Size(77, 32);
			this.BrowseXsltBtn.TabIndex = 9;
			this.BrowseXsltBtn.Text = "Browse";
			this.BrowseXsltBtn.UseVisualStyleBackColor = true;
			this.BrowseXsltBtn.Click += new System.EventHandler(this.BrowseXsltBtn_Click);
			// 
			// ConvertBtn
			// 
			this.ConvertBtn.Location = new System.Drawing.Point(461, 169);
			this.ConvertBtn.Name = "ConvertBtn";
			this.ConvertBtn.Size = new System.Drawing.Size(77, 32);
			this.ConvertBtn.TabIndex = 12;
			this.ConvertBtn.Text = "Convert";
			this.ConvertBtn.UseVisualStyleBackColor = true;
			this.ConvertBtn.Click += new System.EventHandler(this.ConvertBtn_Click);
			// 
			// FileNameTB
			// 
			this.FileNameTB.Location = new System.Drawing.Point(85, 174);
			this.FileNameTB.Name = "FileNameTB";
			this.FileNameTB.Size = new System.Drawing.Size(360, 22);
			this.FileNameTB.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 177);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69, 17);
			this.label5.TabIndex = 13;
			this.label5.Text = "File name";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(552, 451);
			this.Controls.Add(this.FileNameTB);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ConvertBtn);
			this.Controls.Add(this.XsltTB);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.BrowseXsltBtn);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.OutputTB);
			this.Controls.Add(this.ValidateBtn);
			this.Controls.Add(this.XsdTB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.BrowseXsdBtn);
			this.Controls.Add(this.XmlTB);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BrowseXmlBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BrowseXmlBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox XmlTB;
		private System.Windows.Forms.TextBox XsdTB;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BrowseXsdBtn;
		private System.Windows.Forms.Button ValidateBtn;
		private System.Windows.Forms.TextBox OutputTB;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TextBox XsltTB;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button BrowseXsltBtn;
		private System.Windows.Forms.Button ConvertBtn;
		private System.Windows.Forms.TextBox FileNameTB;
		private System.Windows.Forms.Label label5;
	}
}