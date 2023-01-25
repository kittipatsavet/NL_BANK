using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary_NL_BANK;
using System.Data.SqlClient;
using CustomMessageBox;
using System.Runtime.Remoting.Messaging;

namespace NL_BANK
{
    public partial class LoginForm : Form
    {
        public NL_BankEntities lodbserver = new NL_BankEntities();
        public LoginForm()
        {
            InitializeComponent();
            Center(this);
        }
        private void Center(Form form)
        {
            form.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (form.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (form.Size.Height / 2));
        }
        private void ClearForm()
        {
            txtUser.Text = String.Empty;
            txtPwd.Text = String.Empty;
            txtUser.Focus();
            this.Refresh();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(txtUser.Text == string.Empty))
                {
                    if (!(txtPwd.Text == string.Empty))
                    {
                        string sqlstr = "server = .;database=NL_BANK;user id=sa;password=PearmaiPaimae12;";
                        //SqlConnection sqlcon = new SqlConnection(@"metadata=res://*/Model_DBMainServer.csdl|res://*/Model_DBMainServer.ssdl|res://*/Model_DBMainServer.msl;provider=System.Data.SqlClient;provider connection string='data source=DLT-SERVER;initial catalog=DLT;persist security info=True;user id=sa;password=Ki1tt1p@t;MultipleActiveResultSets=True;App=EntityFramework';");
                        string query = "Select * from [NL_Bank].[dbo].[User] where UserName ='" + txtUser.Text.Trim() + "' and Pwd = '" + txtPwd.Text.Trim() + "'";
                        string user, password;
                        user = txtUser.Text;
                        password = txtPwd.Text;
                        SqlConnection con = new SqlConnection(sqlstr);
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader dbr;
                        con.Open();
                        dbr = cmd.ExecuteReader();
                        int count = 0;
                        while (dbr.Read())
                        {
                            count = count + 1;
                        }
                        if (count == 1)
                        {
                            
                            GlobalVar.UserName = txtUser.Text.Trim();
                            GlobalVar.AuthenPass = true;
                            var userrec  = (from g in lodbserver.Users where (g.UserName.Contains(@GlobalVar.UserName)) orderby g.UserName select g).FirstOrDefault();
                            if (userrec != null)
                            {
                                GlobalVar.eid = userrec.USERID;
                                GlobalVar.User_fname = userrec.USER_FNAME;
                                GlobalVar.AuthenPass = true;
                                GlobalVar.UserRole = userrec.Role;
                                string WelcomeMessage = "Welcome " + GlobalVar.User_fname;
                                RJMessageBox.Show(WelcomeMessage, "Welcome to NL Bank System", MessageBoxButtons.OK);
                            }
                            else
                            {
                                GlobalVar.AuthenPass = false;
                                GlobalVar.User_fname = string.Empty;
                                GlobalVar.UserRole = string.Empty;
                            }

                            this.Close();
                        }
                        else if (count > 1)
                        {
                            RJMessageBox.Show("There are more than one user with the same Account login at the same time", "Error", MessageBoxButtons.OK);
                            ClearForm();
                        }
                        else
                        {
                            RJMessageBox.Show("Wrong User Name or Wrong Password", "Error", MessageBoxButtons.OK);
                            ClearForm();
                        }
                    }
                    else
                    {
                        RJMessageBox.Show("Please Login with your UserName and Password", "Suggestion", MessageBoxButtons.OK);
                        ClearForm();
                    }//End check if txtPwd is empty
                }
                else
                {
                    RJMessageBox.Show("Please Login with your UserName and Password", "Suggestion", MessageBoxButtons.OK);
                    ClearForm();
                } //End check if txtUser is empty       
            }
            catch (Exception es)
            {
                RJMessageBox.Show(es.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void ButtonCANCEL_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPwd.Focus();
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton.Focus();
            }
        }
    }
}
