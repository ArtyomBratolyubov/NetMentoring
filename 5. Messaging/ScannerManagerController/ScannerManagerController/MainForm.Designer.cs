namespace ScannerManagerController
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
			this.components = new System.ComponentModel.Container();
			this.readScannersStatusTimer = new System.Windows.Forms.Timer(this.components);
			this.StatusGridView = new System.Windows.Forms.DataGridView();
			this.readPdfDocumentsTimer = new System.Windows.Forms.Timer(this.components);
			this.SendBtn = new System.Windows.Forms.Button();
			this.TimeOutUD = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.StatusGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TimeOutUD)).BeginInit();
			this.SuspendLayout();
			// 
			// readScannersStatusTimer
			// 
			this.readScannersStatusTimer.Interval = 1000;
			// 
			// StatusGridView
			// 
			this.StatusGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.StatusGridView.Location = new System.Drawing.Point(12, 12);
			this.StatusGridView.Name = "StatusGridView";
			this.StatusGridView.ReadOnly = true;
			this.StatusGridView.RowTemplate.Height = 24;
			this.StatusGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.StatusGridView.Size = new System.Drawing.Size(530, 408);
			this.StatusGridView.TabIndex = 0;
			// 
			// readPdfDocumentsTimer
			// 
			this.readPdfDocumentsTimer.Interval = 1000;
			// 
			// SendBtn
			// 
			this.SendBtn.Location = new System.Drawing.Point(645, 40);
			this.SendBtn.Name = "SendBtn";
			this.SendBtn.Size = new System.Drawing.Size(87, 37);
			this.SendBtn.TabIndex = 1;
			this.SendBtn.Text = "Send";
			this.SendBtn.UseVisualStyleBackColor = true;
			this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
			// 
			// TimeOutUD
			// 
			this.TimeOutUD.Location = new System.Drawing.Point(612, 14);
			this.TimeOutUD.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
			this.TimeOutUD.Name = "TimeOutUD";
			this.TimeOutUD.Size = new System.Drawing.Size(120, 22);
			this.TimeOutUD.TabIndex = 2;
			this.TimeOutUD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(548, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Time out";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(744, 432);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TimeOutUD);
			this.Controls.Add(this.SendBtn);
			this.Controls.Add(this.StatusGridView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.StatusGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TimeOutUD)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer readScannersStatusTimer;
		private System.Windows.Forms.DataGridView StatusGridView;
		private System.Windows.Forms.Timer readPdfDocumentsTimer;
		private System.Windows.Forms.Button SendBtn;
		private System.Windows.Forms.NumericUpDown TimeOutUD;
		private System.Windows.Forms.Label label1;
	}
}