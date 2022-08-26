using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pipe_Locator
{
    public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void OnChanged(object source, FileSystemEventArgs e)
		{
			base.Invoke(new MethodInvoker(delegate ()
			{
				string item = e.FullPath.Replace("\\\\.\\pipe\\", string.Empty);
				this.listBox1.Items.Add(item);
				this.listBox1.Refresh();
				this.label1.Text = "Total Pipes: " + this.listBox1.Items.Count;
			}));
		}

		private void OnDeleted(object source, FileSystemEventArgs e)
		{
			base.Invoke(new MethodInvoker(delegate ()
			{
				string value = e.FullPath.Replace("\\\\.\\pipe\\", string.Empty);
				this.listBox1.Items.Remove(value);
				this.listBox1.Refresh();
				this.label1.Text = "Total Pipes: " + this.listBox1.Items.Count;
			}));
		}

		private FileSystemWatcher watcher = new FileSystemWatcher();

		private string[] listOfPipes = Directory.GetFiles("\\\\.\\pipe\\");

        private void button1_Click(object sender, EventArgs e)
        {
			this.listBox1.Items.Clear();
			this.listBox1.Refresh();
			string[] array = this.listOfPipes;
			for (int i = 0; i < array.Length; i++)
			{
				string item = array[i].Replace("\\\\.\\pipe\\", string.Empty);
				this.listBox1.Items.Add(item);
			}
			this.watcher.Path = "\\\\.\\pipe\\";
			this.watcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.LastAccess);
			this.watcher.Created += this.OnChanged;
			this.watcher.Deleted += this.OnDeleted;
			this.watcher.EnableRaisingEvents = true;
			this.label1.Text = "Total Pipes: " + this.listBox1.Items.Count;
		}

        private void button2_Click(object sender, EventArgs e)
        {
			this.label1.Text = "Total Pipes: ";
			this.button2.Enabled = false;
			this.button3.Enabled = true;
			this.watcher.Path = "\\\\.\\pipe\\";
			this.watcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.LastAccess);
			this.watcher.Created += this.OnChanged;
			this.watcher.Deleted += this.OnDeleted;
			this.watcher.EnableRaisingEvents = true;
		}

        private void button3_Click(object sender, EventArgs e)
        {
			this.label1.Text = "Total Pipes: ";
			this.button3.Enabled = false;
			this.button2.Enabled = true;
			this.watcher.EnableRaisingEvents = false;
		}

        private void textBox1_Leave(object sender, EventArgs e)
        {
			if (this.textBox1.Text == "")
			{
				this.textBox1.Text = "Search";
			}
		}

        private void textBox1_Enter(object sender, EventArgs e)
        {
			if (this.textBox1.Text == "Search")
			{
				this.textBox1.Text = "";
			}
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
			string text = this.textBox1.Text;
			int num = this.listBox1.FindString(text, -1);
			if (num != -1)
			{
				this.listBox1.SetSelected(num, true);
			}
		}

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Clipboard.SetText(this.listBox1.SelectedItem.ToString());
		}

        private void clearListboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
			this.listBox1.Items.Clear();
		}
    }
}
