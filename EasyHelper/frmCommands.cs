using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHelper
{
    public partial class frmCommands : Form
    {

        Record currentrecord;
        Keys currentkey;

        public frmCommands()
        {
            InitializeComponent();

            dataGridView.DataSource = RecordsManager.Records;


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private Mdificator GetIntBySelectedRadio()
        {
            if (rbAlt.Checked)
                return Mdificator.Alt;
            
            if (rbCtrl.Checked)
            {
                return Mdificator.Control;
            }
            
            if (rbShift.Checked)
            {
                return Mdificator.Shift;
            }
           
            return Mdificator.Alt;
        }

        private void SetSelectToRadio(int number)
        {
            switch (number)
            {
                case 1:
                    rbAlt.Checked = true;
                    break;
                case 2:
                    rbCtrl.Checked = true;
                    break;
                case 4:
                    rbShift.Checked = true;
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(currentrecord!=null)
            {
                currentrecord.Arguments = txtParams.Text;
                currentrecord.Command = txtPrograqmm.Text;
                currentrecord.Key = currentkey == Keys.None?currentrecord.Key:currentkey;
                currentrecord.Modificator = GetIntBySelectedRadio();
                RecordsManager.Edit(currentrecord);
            }
            else
            {
                RecordsManager.Add(new Record { Modificator=GetIntBySelectedRadio(), Arguments=txtParams.Text, Command=txtPrograqmm.Text, Key=currentkey, Id=DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second});
            }
            Reregist();
        }

        private void Reregist()
        {
            HotKeyRegistratorManager.Registrator.Dispose();
            HotKeyRegistratorManager.Registrator = new HotKeyRegistrator();
            HotKeyRegistratorManager.Registrator.RegisterHotKeys();
            dataGridView.EndEdit();
            dataGridView.DataSource = null;
            dataGridView.DataSource = RecordsManager.Records;
            dataGridView.ClearSelection();
            btnClear_Click(this, null);
        }

        private void btnDdelete_Click(object sender, EventArgs e)
        {
            if (currentrecord != null)
                RecordsManager.Remove(currentrecord);
            Reregist();
        }

        private void dataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
        }

        private void dataGridView_RowEnters(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].Selected = true;
            int index = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value);
            currentrecord = RecordsManager.Records.Where(r => r.Id == index).FirstOrDefault();
            SetSelectToRadio((int)currentrecord.Modificator);
            txtPrograqmm.Text = currentrecord.Command;
            txtParams.Text = currentrecord.Arguments;
            txtSymbol.Text = currentrecord.Key.ToString();
        }

        private void frmCommands_Load(object sender, EventArgs e)
        {
            dataGridView.FirstDisplayedCell.Selected = false;
            dataGridView.RowEnter += dataGridView_RowEnters;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtParams.Text = string.Empty;
            txtPrograqmm.Text = string.Empty;
            txtSymbol.Text = string.Empty;
            currentrecord = null;
            dataGridView.ClearSelection();
        }

        private void txtSymbol_KeyUp(object sender, KeyEventArgs e)
        {
            txtSymbol.Text = e.KeyCode.ToString();
            currentkey = e.KeyCode;
        }
    }
}
