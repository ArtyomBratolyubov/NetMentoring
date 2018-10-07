namespace Task_2
{
	partial class DownloadManagerForm
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
			this.UrlTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.LoadBtn = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.clearBtn = new System.Windows.Forms.LinkLabel();
			this.UrlGridView = new System.Windows.Forms.DataGridView();
			this.RemoveBtn = new System.Windows.Forms.Button();
			this.AddBtn = new System.Windows.Forms.Button();
			this.gridBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.folderBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.UrlGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// UrlTextBox
			// 
			this.UrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UrlTextBox.Location = new System.Drawing.Point(393, 33);
			this.UrlTextBox.Name = "UrlTextBox";
			this.UrlTextBox.Size = new System.Drawing.Size(286, 22);
			this.UrlTextBox.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(347, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "URL:";
			// 
			// LoadBtn
			// 
			this.LoadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LoadBtn.Location = new System.Drawing.Point(350, 130);
			this.LoadBtn.Name = "LoadBtn";
			this.LoadBtn.Size = new System.Drawing.Size(89, 42);
			this.LoadBtn.TabIndex = 6;
			this.LoadBtn.Text = "Load";
			this.LoadBtn.UseVisualStyleBackColor = true;
			this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_ClickAsync);
			// 
			// CancelBtn
			// 
			this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBtn.Location = new System.Drawing.Point(350, 178);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(89, 42);
			this.CancelBtn.TabIndex = 7;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// clearBtn
			// 
			this.clearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.clearBtn.AutoSize = true;
			this.clearBtn.BackColor = System.Drawing.SystemColors.Control;
			this.clearBtn.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.clearBtn.LinkColor = System.Drawing.Color.Red;
			this.clearBtn.Location = new System.Drawing.Point(685, 36);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(17, 17);
			this.clearBtn.TabIndex = 9;
			this.clearBtn.TabStop = true;
			this.clearBtn.Text = "X";
			this.clearBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearBtn_LinkClicked);
			// 
			// UrlGridView
			// 
			this.UrlGridView.AllowUserToAddRows = false;
			this.UrlGridView.AllowUserToDeleteRows = false;
			this.UrlGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UrlGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.UrlGridView.Location = new System.Drawing.Point(12, 12);
			this.UrlGridView.Name = "UrlGridView";
			this.UrlGridView.ReadOnly = true;
			this.UrlGridView.RowTemplate.Height = 24;
			this.UrlGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.UrlGridView.Size = new System.Drawing.Size(332, 463);
			this.UrlGridView.TabIndex = 10;
			this.UrlGridView.SelectionChanged += new System.EventHandler(this.UrlGridView_SelectionChanged);
			// 
			// RemoveBtn
			// 
			this.RemoveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RemoveBtn.Location = new System.Drawing.Point(350, 433);
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(89, 42);
			this.RemoveBtn.TabIndex = 11;
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.UseVisualStyleBackColor = true;
			this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
			// 
			// AddBtn
			// 
			this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AddBtn.Location = new System.Drawing.Point(708, 23);
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(89, 42);
			this.AddBtn.TabIndex = 12;
			this.AddBtn.Text = "Add";
			this.AddBtn.UseVisualStyleBackColor = true;
			this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
			// 
			// gridBindingSource
			// 
			this.gridBindingSource.DataSource = typeof(Task_2.Program);
			// 
			// folderBtn
			// 
			this.folderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.folderBtn.Location = new System.Drawing.Point(708, 433);
			this.folderBtn.Name = "folderBtn";
			this.folderBtn.Size = new System.Drawing.Size(89, 42);
			this.folderBtn.TabIndex = 13;
			this.folderBtn.Text = "Drop folder";
			this.folderBtn.UseVisualStyleBackColor = true;
			this.folderBtn.Click += new System.EventHandler(this.folderBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(809, 487);
			this.Controls.Add(this.folderBtn);
			this.Controls.Add(this.AddBtn);
			this.Controls.Add(this.RemoveBtn);
			this.Controls.Add(this.UrlGridView);
			this.Controls.Add(this.clearBtn);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.LoadBtn);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.UrlTextBox);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Task 2";
			((System.ComponentModel.ISupportInitialize)(this.UrlGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox UrlTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button LoadBtn;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.LinkLabel clearBtn;
		private System.Windows.Forms.DataGridView UrlGridView;
		private System.Windows.Forms.BindingSource gridBindingSource;
		private System.Windows.Forms.Button RemoveBtn;
		private System.Windows.Forms.Button AddBtn;
		private System.Windows.Forms.Button folderBtn;
	}
}

