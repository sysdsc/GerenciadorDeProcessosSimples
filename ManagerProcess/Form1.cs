using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerProcess
{
    public partial class Form1 : Form
    {
        private string selectedProcessName;

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.ItemSelectionChanged += ListView1_ItemSelectionChanged;
        }

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                selectedProcessName = e.Item.Text;
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            Process[] processos = Process.GetProcesses();
            foreach (Process process in processos)
            {
                ListViewItem item = new ListViewItem(process.ProcessName);
                item.SubItems.Add(process.Id.ToString());
                listView1.Items.Add(item);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Process processo = Process.GetProcessesByName(selectedProcessName).FirstOrDefault();
            if (processo != null)
            {
                try
                {
                    processo.Kill();
                    processo.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
