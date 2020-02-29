using System;
using System.Windows.Forms;

namespace EasyHelper
{
    public partial class frmSettings : Form
    {

        private Form currentchildForm;

        public frmSettings()
        {
            InitializeComponent();
            currentchildForm = null;
        }

        private void ShowChildForm(Form childForm)
        {
            if (currentchildForm != null)
                currentchildForm.Close();
            currentchildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            RecordsManager.Add(new Record { Id = 1, Command = "cmd", Arguments = "/C ping 217.29.30.220 -t", Modificator = Mdificator.Alt, Key = Keys.Z });
            RecordsManager.Add(new Record { Id = 2, Command = "cmd", Arguments = "/C ping 217.29.30.220 -t", Modificator = Mdificator.Control, Key = Keys.X });
            
            HotKeyRegistratorManager.Registrator = new HotKeyRegistrator();
            HotKeyRegistratorManager.Registrator.RegisterHotKeys();
            
            ShowChildForm(new frmDetales());
        }

        private void btnCommands_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmCommands());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmOtherSettings());
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmDetales());
        }
    }
}
