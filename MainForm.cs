using System;
using System.IO;
using System.Windows.Forms;
using JsonViewer.Common;

namespace JsonHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.JsonViewer.ShowTab(Tabs.Text);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.Equals("/c", StringComparison.OrdinalIgnoreCase))
                {
                    this.LoadFromClipboard();
                }
                else if (File.Exists(arg))
                {
                    this.LoadFromFile(arg);
                }
            }
        }

        private void LoadFromFile(string fileName)
        {
            string json = File.ReadAllText(fileName);
            this.JsonViewer.ShowTab(Tabs.Viewer);
            this.JsonViewer.Json = json;
        }

        private void LoadFromClipboard()
        {
            string json = Clipboard.GetText();
            if (!string.IsNullOrEmpty(json))
            {
                this.JsonViewer.ShowTab(Tabs.Viewer);
                this.JsonViewer.Json = json;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"Yahoo! Pipe files (*.run)|*.run|json files (*.json)|*.json|All files (*.*)|*.*";
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Title = @"Select a JSON file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.LoadFromFile(dialog.FileName);
            }
        }

        private void aboutJSONViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutJsonViewer().ShowDialog();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("txtJson", true)[0];
            ((TextBoxBase)c).SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("txtJson", true)[0];
            if (((TextBoxBase)c).SelectionLength > 0)
            {
                string selectedText = ((TextBoxBase)c).SelectedText;
            }
            else
            {
                string text = ((TextBoxBase)c).Text;
            }
            ((TextBoxBase)c).SelectedText = "";
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("txtJson", true)[0];
            ((TextBoxBase)c).Paste();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("txtJson", true)[0];
            ((TextBoxBase)c).Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("txtJson", true)[0];
            ((TextBoxBase)c).Cut();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("txtJson", true)[0];
            ((TextBoxBase)c).Undo();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("pnlFind", true)[0];
            ((Panel)c).Visible = true;
            Control t = this.JsonViewer.Controls.Find("txtFind", true)[0];
            ((TextBoxBase)t).Focus();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("tvJson", true)[0];
            ((TreeView)c).BeginUpdate();
            try
            {
                if (((TreeView)c).SelectedNode != null)
                {
                    TreeNode topNode = ((TreeView)c).TopNode;
                    ((TreeView)c).SelectedNode.ExpandAll();
                    ((TreeView)c).TopNode = topNode;
                }
            }
            finally
            {
                ((TreeView)c).EndUpdate();
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("tvJson", true)[0];
            TreeNode node = ((TreeView)c).SelectedNode;
            if (node != null)
            {
                Clipboard.SetText(node.Text);
            }
        }

        private void copyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c = this.JsonViewer.Controls.Find("tvJson", true)[0];
            JsonViewerTreeNode node = (JsonViewerTreeNode)((TreeView)c).SelectedNode;
            if (node != null && node.JsonObject.Value != null)
            {
                Clipboard.SetText(node.JsonObject.Value.ToString());
            }
        }
    }
}
