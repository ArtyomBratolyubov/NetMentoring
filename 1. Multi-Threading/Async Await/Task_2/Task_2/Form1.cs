using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task_2
{
	public partial class DownloadManagerForm : Form
	{
		private BindingList<Download> bindingList;
		private string Folder = @"Downloaded\";

		public DownloadManagerForm()
		{
			InitializeComponent();
			this.bindingList = new BindingList<Download>();

			this.UrlGridView.DataSource = this.bindingList;

			this.UrlGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Url column.
			this.UrlGridView.Columns[1].Width = 130; // Status column.

			this.LoadBtn.Enabled = false;
			this.CancelBtn.Enabled = false;
			this.RemoveBtn.Enabled = false;
		}

		private async void LoadBtn_ClickAsync(object sender, EventArgs e)
		{
			if (this.UrlGridView.SelectedRows.Count != 0)
			{
				var selected = this.UrlGridView.SelectedRows[0].DataBoundItem as Download;

				if (selected.Status == Download.StateLoading)
				{
					return;
				}

				string result = await selected.LoadWebSiteAsync();

				if (!string.IsNullOrEmpty(result))
				{
					string name = this.CleanFileName(selected.URL);
					Directory.CreateDirectory(Folder);
					File.WriteAllText(Folder + name + ".html", result);
				}
			}
		}

		private void clearBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.UrlTextBox.Text = string.Empty;
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			string url = this.UrlTextBox.Text;

			if (string.IsNullOrEmpty(url))
			{
				return;
			}

			this.bindingList.Add(new Download(url));

			this.UrlTextBox.Text = string.Empty;
		}

		private void UrlGridView_SelectionChanged(object sender, EventArgs e)
		{
			if (this.UrlGridView.SelectedRows.Count != 0)
			{
				this.LoadBtn.Enabled = true;
				this.CancelBtn.Enabled = true;
				this.RemoveBtn.Enabled = true;
			}
			else
			{
				this.LoadBtn.Enabled = false;
				this.CancelBtn.Enabled = false;
				this.RemoveBtn.Enabled = false;
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.UrlGridView.SelectedRows.Count != 0)
			{
				var selected = this.UrlGridView.SelectedRows[0].DataBoundItem as Download;

				if (selected.Status == Download.StateLoading)
				{
					selected.CancelLoading();
				}

				this.bindingList.Remove(selected);
			}
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			if (this.UrlGridView.SelectedRows.Count != 0)
			{
				var selected = this.UrlGridView.SelectedRows[0].DataBoundItem as Download;

				if (selected.Status != Download.StateLoading)
				{
					return;
				}

				selected.CancelLoading();
			}
		}

		private void folderBtn_Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", Folder);
		}

		private string CleanFileName(string fileName)
		{
			string str = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
			if (str.Length > 100)
			{
				str = str.Substring(0, 100);
			}
			return str;
		}
	}
}
