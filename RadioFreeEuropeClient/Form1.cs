using RadioFreeEuropeClient.Models;
using System;
using System.Windows.Forms;

namespace RadioFreeEuropeClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            HttpClient client = new();
            ResponseFromServer res;

            if (tbId.Value <= 0) 
                MessageBox.Show("Id should be greater than 0");
            else
            {
                
                if (rbLeft.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(tbInput.Text))
                    {
                        res = client.SendRequest(new Diff((int)tbId.Value, tbInput.Text, DiffType.Left));
                        MessageBox.Show(res.Message);
                    }
                    else
                        MessageBox.Show("Input shoudn't be empty");
                }
                else if (rbRight.Checked)
                {
                    if(!string.IsNullOrWhiteSpace(tbInput.Text))
                        MessageBox.Show(client.SendRequest(new Diff((int)tbId.Value, tbInput.Text, DiffType.Right)).Message);
                    else
                        MessageBox.Show("Input shoudn't be empty");
                }
                else
                {
                    res = client.SendRequest(new Diff((int)tbId.Value));
                    MessageBox.Show(res.Message);
                    if (res.Result != null)
                        foreach (IsDiff item in res.Result)
                            MessageBox.Show($"offset: {item.Offset} length: {item.Length}");
                }
                
            tbInput.Text = "";
            tbId.Value = 0;
            rbLeft.Checked = true;

            }
        }

        private void rbCheck_CheckedChanged(object sender, EventArgs e)
        {
            tbInput.Enabled = !rbCheck.Checked;
        }
    }
}
