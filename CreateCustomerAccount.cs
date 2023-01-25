using CustomMessageBox;
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
using System.Runtime.Remoting.Messaging;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Net.WebRequestMethods;

namespace NL_BANK
{
    public partial class CreateCustomerAccount : Form
    {
        private static CreateCustomerAccount _instance;
        public NL_BankEntities lodbserver = new NL_BankEntities();
        public bool AddFlag, EditFlag, SaveFlag;
        public string id, CurrentAccountNo, SaveCustAccountNo, SearchText, SearchTextF, SearchTextN;
        public string SaveUserName, SavePassword1, SavePassword2, SaveFirstAccountNO, SaveLastAccountNo;
        public int SaveID, FirstlyId, FirstID, LastID, LastlyId;
        public DateTime NowDate = DateTime.Now;
        public DateTime TransactionDate, Todaydate, SaveCurrentDateTime;
        public bool DepositFlag, DepositDoneFlag = false, TransferFlag, ConfirmTransferFlag = false, ConfirmDepositFlag = false;
        public string SaveTransferAccount;
        public int bcYear;
        public decimal SaveBalance = 0.00m, SaveDeposit = 0.00m, SaveNewBalance = 0.00m, SaveFee = 0.00m;


        public static CreateCustomerAccount Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CreateCustomerAccount();
                else
                    _instance = new CreateCustomerAccount();
                return _instance;
            }
        }
        private void Center(Form form)
        {
            form.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (form.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (form.Size.Height / 2));
        }

        private void AccountradioButton_CheckedChanged(object sender, EventArgs e)
        {
            txtAccountSearch.Focus();
        }

        private void txt_customer_Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_customer_first_name.Focus();
            }
        }

        private void txt_customer_first_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_customer_last_name.Focus();
            }
        }

        private void txt_customer_last_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_address_line1.Focus();
            }
        }

        private void txt_address_line1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_address_line2.Focus();
            }
        }

        private void txt_address_line2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_city.Focus();
            }

        }

        private void txt_city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_country.Focus();
            }
        }

        private void txt_country_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_zipcode.Focus();
            }
        }

        private void txt_mobile_phone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_email.Focus();
            }
        }

        private void txt_email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Save_Button.Focus();
            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            RJMessageBox.Show("Please complete all information marked with * !!!", "Suggestion", MessageBoxButtons.OK);
            SetEnableObject();
            txt_customer_first_name.Focus();
            EditFlag = true;
            AddFlag = false;
            SaveFlag = false;
        } // Edit_Button

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            if (txt_customer_Account.Text.Trim() == "")
            {
                return;
            }
            try
            {
                if (RJMessageBox.Show("Do you want to delete this Customer Bank Account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (var tr = lodbserver.Database.BeginTransaction())
                    {
                        var os = (from id in lodbserver.BankAccountMasters where id.IBANAccountNo.ToString() == txt_customer_Account.Text.Trim() select id).FirstOrDefault();
                        if (os != null)
                        {
                            lodbserver.BankAccountMasters.Remove(os);
                        }
                        lodbserver.SaveChanges();
                        tr.Commit();
                    }
                    RJMessageBox.Show("Deletion of this Customer Bank Account has been completed.", "Delete Operation", MessageBoxButtons.OK);
                    ClearForm();
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                RJMessageBox.Show("ไม่สามารถลบข้อมูลได้ เนื่องจาก : " + dbEx.Message, "ERROR", MessageBoxButtons.OK);
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        private void Search_Button_Click(object sender, EventArgs e)
        {
            SetEnableObject();
            this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["SearchTabPage"];
            AccountradioButton.Focus();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Top_Button_Click(object sender, EventArgs e)
        {
            var result = (from cc in lodbserver.BankAccountMasters where cc.AccountID > 0 select cc).ToList();

            if (result.Count == 0) {
                RJMessageBox.Show("Database is Empty, Please enter Customer BANK Account First.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                SetDisableObject();

                var fcs = (from dc in lodbserver.BankAccountMasters select dc).First();
                SaveID = fcs.AccountID;
                txt_customer_Account.Text = fcs.IBANAccountNo;
                txt_customer_first_name.Text = fcs.Customer_FirstName;
                txt_customer_last_name.Text = fcs.Customer_LastName;
                txt_address_line1.Text = fcs.CAddress1;
                txt_address_line1.Text = fcs.CAddress2;
                txt_city.Text = fcs.CCity;
                txt_zipcode.Text = fcs.CZipcode;
                txt_country.Text = fcs.CCountry;
                txt_phone.Text = fcs.CPhone;
                txt_email.Text = fcs.Cemail;
                var CustUser = (from c in lodbserver.Users where (c.IBANAccountNo == CurrentAccountNo) orderby c.IBANAccountNo ascending select c).FirstOrDefault();
                if (CustUser != null)
                {
                    txtUserName.Text = CustUser.UserName;
                    txt_PWD1.Text = CustUser.Pwd;
                    txt_PWD2.Text = CustUser.Pwd;
                }
                this.Refresh();
            }
        }

        private void NameradioButton_CheckedChanged(object sender, EventArgs e)
        {
            txtFNameSearch.Focus();
        }

        private void txt_PWD1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PWD2.Focus();
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PWD1.Focus();
            }
        }

        private void txt_PWD2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_PWD1.Text == txt_PWD2.Text)
                {
                    RJMessageBox.Show("Your Passwords match.", "Set Password Completed", MessageBoxButtons.OK);
                    Save_Button.Focus();
                }
                else
                {
                    RJMessageBox.Show("Your Passwords do not match.", "Please Set Password Again", MessageBoxButtons.OK);
                    txt_PWD1.Focus();
                }
            }
        }

        private void txt_InitialBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (txt_InitialBalance.Text.Length > 0)
            {
                try
                {

                    int temp = Convert.ToInt32(txt_InitialBalance.Text);
                    if (e.KeyCode == Keys.Enter)
                    {

                        SaveDeposit = Convert.ToDecimal(txt_InitialBalance.Text);
                        SaveFee = SaveDeposit * 0.001m;
                        SaveNewBalance = SaveDeposit - SaveFee;
                        txtBalance.Text = SaveNewBalance.ToString("###,###.##");
                        DepositDoneFlag = true;
                        txtUserName.Focus();
                    }
                }
                catch (Exception h)
                {
                    RJMessageBox.Show("Please provide number only", "Warning", MessageBoxButtons.OK);
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (AccountradioButton.Checked == true)
            {
                SearchText = AccountradioButton.Text.Trim();
                var lcs = (from c in lodbserver.BankAccountMasters where c.IBANAccountNo.Contains(SearchText) orderby c.IBANAccountNo select c).FirstOrDefault();
                if (lcs != null)
                {
                    SaveID = lcs.AccountID;
                    txt_customer_Account.Text = lcs.IBANAccountNo;
                    txt_customer_first_name.Text = lcs.Customer_FirstName;
                    txt_customer_last_name.Text = lcs.Customer_LastName;
                    txt_address_line1.Text = lcs.CAddress1;
                    txt_address_line2.Text = lcs.CAddress2;
                    txt_city.Text = lcs.CCity;
                    txt_zipcode.Text = lcs.CZipcode;
                    txt_country.Text = lcs.CCountry;
                    txt_phone.Text = lcs.CPhone;
                    txt_email.Text = lcs.Cemail;
                    txtBalance.Text = lcs.Balance.Value.ToString("###,###.##");
                    var CustUser = (from c in lodbserver.Users where (c.IBANAccountNo == CurrentAccountNo) orderby c.IBANAccountNo ascending select c).FirstOrDefault();
                    if (CustUser != null)
                    {
                        txtUserName.Text = CustUser.UserName;
                        txt_PWD1.Text = CustUser.Pwd;
                        txt_PWD2.Text = CustUser.Pwd;
                    }
                    this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
                    this.Refresh();
                }
                else
                {
                    RJMessageBox.Show("There is no record about Customer you have just entered, Please make sure that you enter them correctly.", "Warning!!!", MessageBoxButtons.OK);
                }
            }
            else
             if (NameradioButton.Checked == true)
            {
                SearchTextF = txtFNameSearch.Text;
                SearchTextN = txtLastNameSearch.Text;
                var lcs = (from c in lodbserver.BankAccountMasters where (c.Customer_FirstName == SearchTextF && c.Customer_LastName == SearchTextN) orderby c.IBANAccountNo select c).FirstOrDefault();
                if (lcs != null)
                {
                    SaveID = lcs.AccountID;
                    txt_customer_Account.Text = lcs.IBANAccountNo;
                    txt_customer_first_name.Text = lcs.Customer_FirstName;
                    txt_customer_last_name.Text = lcs.Customer_LastName;
                    txt_address_line1.Text = lcs.CAddress1;
                    txt_address_line2.Text = lcs.CAddress2;
                    txt_city.Text = lcs.CCity;
                    txt_zipcode.Text = lcs.CZipcode;
                    txt_country.Text = lcs.CCountry;
                    txt_phone.Text = lcs.CPhone;
                    txt_email.Text = lcs.Cemail;
                    txtBalance.Text = lcs.Balance.Value.ToString("###,###.##");
                    var CustUser = (from c in lodbserver.Users where (c.IBANAccountNo == CurrentAccountNo) orderby c.IBANAccountNo ascending select c).FirstOrDefault();
                    if (CustUser != null)
                    {
                        txtUserName.Text = CustUser.UserName;
                        txt_PWD1.Text = CustUser.Pwd;
                        txt_PWD2.Text = CustUser.Pwd;
                    }
                    this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
                    this.Refresh();
                }
                else
                {
                    RJMessageBox.Show("There is no record about Customer you have just entered, Please make sure that you enter them correctly.", "Warning!!!", MessageBoxButtons.OK);
                }
            }


        }

        private void ClearForm()
        {
            txt_customer_Account.Text = String.Empty;
            txt_customer_first_name.Text = String.Empty;
            txt_customer_last_name.Text = String.Empty;
            txt_address_line1.Text = String.Empty;
            txt_address_line2.Text = String.Empty;
            txt_city.Text = String.Empty;
            txt_country.Text = String.Empty;
            txt_zipcode.Text = String.Empty;
            txt_phone.Text = String.Empty;
            txt_email.Text = String.Empty;
            this.Refresh();
        }

        private void Previous_Button_Click(object sender, EventArgs e)
        {
            var result = (from cc in lodbserver.BankAccountMasters where cc.AccountID > 0 select cc).ToList();

            if (result.Count == 0)
            {
                RJMessageBox.Show("Database is Empty, Please enter Customer BANK Account First.", "Error", MessageBoxButtons.OK);
            }
            else
            {

                var items = (from cc in lodbserver.BankAccountMasters where (cc.AccountID < SaveID) orderby cc.AccountID ascending select cc).ToList();

                SetDisableObject();

                SaveCustAccountNo = txt_customer_Account.Text.Trim();
                FirstID = GetFirstID(ref SaveFirstAccountNO);
                if (SaveFirstAccountNO != null)
                {
                    if (SaveFirstAccountNO == SaveCustAccountNo)
                    {

                    }
                    else
                    {
                        var previous = (from c in lodbserver.BankAccountMasters
                                        where c.AccountID < SaveID
                                        orderby c.AccountID descending
                                        select c).First();
                        
                        if (previous != null)
                        {
                            SaveID = previous.AccountID;
                            CurrentAccountNo = previous.IBANAccountNo;
                            txt_customer_Account.Text = previous.IBANAccountNo;
                            txt_customer_first_name.Text = previous.Customer_FirstName;
                            txt_customer_last_name.Text = previous.Customer_LastName;
                            txt_address_line1.Text = previous.CAddress1;
                            txt_address_line1.Text = previous.CAddress2;
                            txt_city.Text = previous.CCity;
                            txt_zipcode.Text = previous.CZipcode;
                            txt_country.Text = previous.CCountry;
                            txt_phone.Text = previous.CPhone;
                            txt_email.Text = previous.Cemail;
                            txtBalance.Text = previous.Balance.Value.ToString("###,###.##");
                            var CustUser = (from c in lodbserver.Users where (c.IBANAccountNo == CurrentAccountNo) orderby c.IBANAccountNo ascending select c).FirstOrDefault();
                            if (CustUser != null)
                            {
                                txtUserName.Text = CustUser.UserName;
                                txt_PWD1.Text = CustUser.Pwd;
                                txt_PWD2.Text = CustUser.Pwd;
                            }
                        }
                    }
                }


                this.Refresh();
            }
        }

        private void CButtonCANCEL_Click(object sender, EventArgs e)
        {
            this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
        }

        private void Next_Button_Click(object sender, EventArgs e)
        {
            var result = (from cc in lodbserver.BankAccountMasters where cc.AccountID > 0 orderby cc.AccountID ascending select cc).ToList();

            if (result.Count == 0)
            {
                RJMessageBox.Show("Database is Empty, Please enter Customer BANK Account First.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                {
                    var items = (from cc in lodbserver.BankAccountMasters where (cc.AccountID > SaveID) orderby cc.AccountID ascending select cc).ToList();
                    SetDisableObject();
                    SaveCustAccountNo = txt_customer_Account.Text.Trim();
                    LastID = GetLastID(ref SaveLastAccountNo);
                    if (SaveLastAccountNo == SaveCustAccountNo)
                    {

                    }
                    else
                    {
                        var next = (from c in lodbserver.BankAccountMasters where (c.AccountID > SaveID) orderby c.AccountID ascending select c).FirstOrDefault();


                        if (next != null)
                        {
                            SaveID = next.AccountID;
                            CurrentAccountNo = next.IBANAccountNo;
                            txt_customer_Account.Text = next.IBANAccountNo;
                            txt_customer_first_name.Text = next.Customer_FirstName;
                            txt_customer_last_name.Text = next.Customer_LastName;
                            txt_address_line1.Text = next.CAddress1;
                            txt_address_line1.Text = next.CAddress2;
                            txt_city.Text = next.CCity;
                            txt_zipcode.Text = next.CZipcode;
                            txt_country.Text = next.CCountry;
                            txt_phone.Text = next.CPhone;
                            txt_email.Text = next.Cemail;
                            var CustUser = (from c in lodbserver.Users where (c.IBANAccountNo == CurrentAccountNo) orderby c.IBANAccountNo ascending select c).First();
                            if (CustUser != null)
                            {
                                txtUserName.Text= CustUser.UserName;
                                txt_PWD1.Text = CustUser.Pwd;
                                txt_PWD2.Text = CustUser.Pwd;
                            }
                        }
                        this.Refresh();
                    }
                }
            }
        }
    


        private void Bottom_Button_Click(object sender, EventArgs e)
        {
            var result = (from cc in lodbserver.BankAccountMasters where cc.AccountID > 0  orderby cc.AccountID select cc).ToList();

            if (result.Count == 0)
            {
                RJMessageBox.Show("Database is Empty, Please enter Customer BANK Account First.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                SetDisableObject();
                var lcs = (from c in lodbserver.BankAccountMasters
                           orderby c.AccountID descending
                           select c).First();
                SaveID = lcs.AccountID;
                CurrentAccountNo = lcs.IBANAccountNo;
                txt_customer_Account.Text = lcs.IBANAccountNo;
                txt_customer_first_name.Text = lcs.Customer_FirstName;
                txt_customer_last_name.Text = lcs.Customer_LastName;
                txt_address_line1.Text = lcs.CAddress1;
                txt_address_line1.Text = lcs.CAddress2;
                txt_city.Text = lcs.CCity;
                txt_zipcode.Text = lcs.CZipcode;
                txt_country.Text = lcs.CCountry;
                txt_phone.Text = lcs.CPhone;
                txt_email.Text = lcs.Cemail;
                var CustUser = (from c in lodbserver.Users where (c.IBANAccountNo == CurrentAccountNo) orderby c.IBANAccountNo ascending select c).First();
                if (CustUser != null)
                {
                    txtUserName.Text = CustUser.UserName;
                    txt_PWD1.Text = CustUser.Pwd;
                    txt_PWD2.Text = CustUser.Pwd;
                }
                this.Refresh();
            }
        }

        public CreateCustomerAccount()
        {
            InitializeComponent();
            Todaydate = NowDate;
            SaveCurrentDateTime = Todaydate;
            txtTransactionDate.Text = string.Format("{0:dd/MM/yyyy}", Todaydate);
            Center(this);
        }
        private bool CheckCustomerData()
        {
            if (txt_customer_first_name.Text.Trim() == "") 
            {
                RJMessageBox.Show("Please enter First Name !!!", "Suggestion", MessageBoxButtons.OK);
                txt_customer_first_name.Focus();
                return false;
            }
            else
            if (txt_customer_last_name.Text.Trim() == "") 
            {
                RJMessageBox.Show("Please enter Last Name !!!", "Suggestion", MessageBoxButtons.OK);
                txt_customer_last_name.Focus();
                return false;
            }
            else
            if ((txt_customer_first_name.Text.Trim() == "") || (txt_customer_last_name.Text.Trim() == "") || (txt_address_line1.Text.Trim() == "") || (txt_city.Text.Trim() == "") || (txt_zipcode.Text.Trim() == "") || (txt_phone.Text.Trim() == "") || (txt_email.Text.Trim() == ""))
            {
                RJMessageBox.Show("Please complete all information marked with * !!!", "Suggestion", MessageBoxButtons.OK);
                txt_customer_first_name.Focus();
                return false;
            }
            else         
            {
                return true;
            }
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveFlag = true;
            var result = CheckCustomerData();
            if ((result == true) && (AddFlag))
            {
                try
                {
                    if (RJMessageBox.Show("Do you want to add new Customer Bank Account? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {

                        
                        ClassLibrary_NL_BANK.BankAccountMaster cc = new ClassLibrary_NL_BANK.BankAccountMaster();      
                        
                        cc.IBANAccountNo = txt_customer_Account.Text;
                        cc.Customer_FirstName = txt_customer_first_name.Text;
                        cc.Customer_LastName = txt_customer_last_name.Text;
                        cc.CAddress1 = txt_address_line1.Text;
                        cc.CAddress2 = txt_address_line1.Text;
                        cc.CCity = txt_city.Text;
                        cc.CZipcode = txt_zipcode.Text;
                        cc.CCountry = txt_country.Text;
                        cc.CPhone = txt_phone.Text;
                        cc.Cemail = txt_email.Text;
                        cc.Balance = Convert.ToDecimal(txtBalance.Text);
                        cc.AccDateCreated = SaveCurrentDateTime;
                        SaveUserName = txtUserName.Text;
                        SavePassword1= txt_PWD1.Text;
                        SavePassword2= txt_PWD2.Text;
                        using (var tr = lodbserver.Database.BeginTransaction())
                        {
                            lodbserver.BankAccountMasters.Add(cc);
                            lodbserver.SaveChanges();
                            tr.Commit();                           
                            RJMessageBox.Show("New Customer Bank Account is saved", "Save Completed", MessageBoxButtons.OK);
                            txt_customer_Account.Focus();
                        }
                        LastID = GetLastID(ref SaveLastAccountNo);
                        ClassLibrary_NL_BANK.User ucc = new ClassLibrary_NL_BANK.User();
                        ucc.USER_FNAME = txt_customer_first_name.Text;
                        ucc.USER_LNAME = txt_customer_last_name.Text;
                        ucc.UserName = SaveUserName;
                        ucc.Pwd = SavePassword1;
                        ucc.Role = "CUSTOMER";
                        ucc.IBANAccountNo = txt_customer_Account.Text;
                        using (var tr = lodbserver.Database.BeginTransaction())
                        {
                            lodbserver.Users.Add(ucc);
                            lodbserver.SaveChanges();
                            tr.Commit();

                        }
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    RJMessageBox.Show("System can not save because : " + dbEx.Message, "ERROR", MessageBoxButtons.OK);
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                
            }
            if ((result == true) && (EditFlag))
            {
                try
                {
                    if (RJMessageBox.Show("Do you want to edit Customer Bank Account ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (AccountradioButton.Checked == true)
                        {
                            SearchText = AccountradioButton.Text;
                            var qcsA = (from c in lodbserver.BankAccountMasters where c.IBANAccountNo == SearchText select c).FirstOrDefault();
                            if (qcsA != null)
                            {
                                qcsA.IBANAccountNo = txt_customer_Account.Text;
                                qcsA.Customer_FirstName = txt_customer_first_name.Text;
                                qcsA.Customer_LastName = txt_customer_last_name.Text;
                                qcsA.CAddress1 = txt_address_line1.Text;
                                qcsA.CAddress2 = txt_address_line1.Text;
                                qcsA.CCity = txt_city.Text;
                                qcsA.CZipcode = txt_zipcode.Text;
                                qcsA.CCountry = txt_country.Text;
                                qcsA.CPhone = txt_phone.Text;
                                qcsA.Cemail = txt_email.Text;
                                
                            }
                            else
                            {

                            }
                        }
                        else
                            if (NameradioButton.Checked == true)
                        {
                            SearchTextF = txtFNameSearch.Text.Trim();
                            SearchTextN = txtLastNameSearch.Text.Trim();
                            var qcsN = (from c in lodbserver.BankAccountMasters where (c.Customer_FirstName == SearchTextF && c.Customer_LastName == SearchTextN) select c).FirstOrDefault();
                            if (qcsN != null)
                            {
                                qcsN.IBANAccountNo = txt_customer_Account.Text;
                                qcsN.Customer_FirstName = txt_customer_first_name.Text;
                                qcsN.Customer_LastName = txt_customer_last_name.Text;
                                qcsN.CAddress1 = txt_address_line1.Text;
                                qcsN.CAddress2 = txt_address_line1.Text;
                                qcsN.CCity = txt_city.Text;
                                qcsN.CZipcode = txt_zipcode.Text;
                                qcsN.CCountry = txt_country.Text;
                                qcsN.CPhone = txt_phone.Text;
                                qcsN.Cemail = txt_email.Text;
                            }
                            else
                            {

                            }
                        }

                            using (var tr = lodbserver.Database.BeginTransaction())
                            {
                                lodbserver.SaveChanges();
                                tr.Commit();

                                //ClearForm();
                                RJMessageBox.Show("Customer Bank Account has been edited and saved.", "Save Completed", MessageBoxButtons.OK);
                                txt_customer_first_name.Focus();
                            }
                         
                    }
                }

                catch (DbEntityValidationException dbEx)
                {
                    RJMessageBox.Show("System can not save because : " + dbEx.Message, "ERROR", MessageBoxButtons.OK);
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
    }

        private void AddButton_Click(object sender, EventArgs e)
        {
            SetEnableObject();
            AddFlag = true;
            EditFlag = false;
            SaveFlag = false;
            this.ClearForm();            
            RJMessageBox.Show("Please complete all information marked with * !!!", "Suggestion", MessageBoxButtons.OK);
            CreateCustomerAccount p = new CreateCustomerAccount();
             var shared = new SharedClass();
            RJMessageBox.Show("Please wait for new IBAN number being generated from website for a few seconds.", "Please click OK to start generation process...");
            txt_customer_Account.Text = shared.GetIBANNumber();
            
            txt_customer_first_name.Focus();
 
        }
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
                
                RJMessageBox.Show(text, caption);

            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);

            }

            void OnTimerElapsed(object state)

            {

                IntPtr mbWnd = FindWindow(null, _caption);

                if (mbWnd != IntPtr.Zero)

                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                _timeoutTimer.Dispose();

            }

            const int WM_CLOSE = 0x0010;

            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]

            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]

            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        }
        private void SetEnableObject()
        {          
            txt_customer_Account.ReadOnly = true;
            txt_customer_first_name.ReadOnly = false;
            txt_customer_last_name.ReadOnly = false;
            txt_address_line1.ReadOnly = false;
            txt_address_line2.ReadOnly = false;
            txt_city.ReadOnly = false;
            txt_country.ReadOnly = false;
            txt_zipcode.ReadOnly = false;
            txt_phone.ReadOnly = false;
            txt_email.ReadOnly = false;
        }
        private void SetDisableObject()
        {
            txt_customer_Account.ReadOnly = true;
            txt_customer_first_name.ReadOnly = true;
            txt_customer_last_name.ReadOnly = true;
            txt_address_line1.ReadOnly = true;
            txt_address_line2.ReadOnly = true;
            txt_city.ReadOnly = true;
            txt_country.ReadOnly = true;
            txt_zipcode.ReadOnly = true;
            txt_phone.ReadOnly = true;
            txt_email.ReadOnly = true;
        }
        private int GetFirstID(ref string KeyAccount)
        {
            var pcs = (from c in lodbserver.BankAccountMasters orderby c.AccountID ascending select c).First();             
                FirstlyId = pcs.AccountID;
                KeyAccount = pcs.IBANAccountNo;                 
            return FirstlyId;
        }
        private int GetLastID(ref string KeyAccountNO)
        {
            var lcs = (from c in lodbserver.BankAccountMasters orderby c.AccountID descending select c).First();      
            KeyAccountNO = lcs.IBANAccountNo;
            LastlyId = lcs.AccountID;
            return LastlyId;
        }
        
    }
}
