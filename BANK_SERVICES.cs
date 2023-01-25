
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Net.WebRequestMethods;
using ClassLibrary_NL_BANK;
using CustomMessageBox;
using System.Globalization;

namespace NL_BANK
{
    public partial class BANK_SERVICES : Form
    {
        private static BANK_SERVICES _instance;
        public NL_BankEntities lodbserver = new NL_BankEntities();
        public bool DepositFlag,DepositDoneFlag=false, TransferFlag, SaveFlag,ConfirmTransferFlag =false,ConfirmDepositFlag = false;
        public string id, CurrentAccountNo, SaveCustAccountNo, SearchText, SearchTextF, SearchTextN, FirstlyId, LastlyId, FirstID, LastID;
        public string SaveTransferAccount;
        public int SaveID, bcYear;
        public DateTime NowDate = DateTime.Now;
        public DateTime TransactionDate,Todaydate;
        public decimal SaveBalance = 0.00m,SaveDeposit =0.00m,SaveRemainDeposit =0.00m,SaveFee=0.00m, SaveNewBalance=0.00m;
        public decimal SaveTransferAMT=0.00m,SaveTransferTargetBalance = 0.00m;
        
        public BANK_SERVICES()
        {           
            InitializeComponent();
            //DateThaiYearToGregorian();
            Todaydate = NowDate;
            txtTransactionDate.Text = string.Format("{0:dd/MM/yyyy}", Todaydate);
            if (GlobalVar.UserRole == "ADMIN")
            {
                RJMessageBox.Show("Please enter Bank Account Number for Bank Services. If new account, please Add new Customer Bank Account in Master Account,First.", "Suggestion", MessageBoxButtons.OK);
                SearchButton.Visible = true;
                txt_AccountNO.Enabled= true;
                txt_AccountNO.ReadOnly = false;
                txt_AccountNO.Focus();
            }
            else
               if (GlobalVar.UserRole == "CUSTOMER")
            {
                SearchText = txt_AccountNO.Text.Trim();
                var lcs = (from c in lodbserver.BankAccountMasters where c.IBANAccountNo.Contains(SearchText) orderby c.IBANAccountNo select c).FirstOrDefault();
                if (lcs != null)
                {
                    SaveID = lcs.AccountID;
                    SaveCustAccountNo = lcs.IBANAccountNo;
                    txt_AccountNO.Text = lcs.IBANAccountNo;
                    txt_customer_Account.Text = lcs.IBANAccountNo;
                    txt_customer_first_name.Text = lcs.Customer_FirstName;
                    txt_TopCust_Fname.Text = lcs.Customer_FirstName;
                    txt_customer_last_name.Text = lcs.Customer_LastName;
                    txt_TopCust_Lname.Text = lcs.Customer_LastName;
                    txt_address_line1.Text = lcs.CAddress1;
                    txt_address_line1.Text = lcs.CAddress2;
                    txt_city.Text = lcs.CCity;
                    txt_zipcode.Text = lcs.CZipcode;
                    txt_country.Text = lcs.CCountry;
                    txt_phone.Text = lcs.CPhone;
                    txt_email.Text = lcs.Cemail;
                    SaveBalance = (decimal) lcs.Balance;
                    if (SaveBalance > 0.00m )
                    {
                        txtBalance.Text = lcs.Balance?.ToString("###,###.##");
                        txt_TopBalance.Text = lcs.Balance?.ToString("###,###.##");
                    }
                    else
                    {
                        txtBalance.Text = "0.00";
                        txt_TopBalance.Text = "0.00";

                    }

                    
                    this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
                    this.Refresh();
                }
            }
        }
        private void DateThaiYearToGregorian()
        {
            Todaydate = NowDate;

            txtTransactionDate.Text = String.Empty;
            //
            //txtTransactionDate.Text = uk;
            //int day = new DateTime(Todaydate).Day;
            int day =  Todaydate.Day;
            int month = Todaydate.Month;
            int year = Todaydate.Year;
            if (year > 2023)
            {
                bcYear = year - 543;
            }
            else
            {
                bcYear = year;
            }
            DateTime bcDate = new DateTime(bcYear, month, day);
            String bcDateFormat = bcDate.ToString("dd/MM/yyyy");
            txtTransactionDate.Text = bcDateFormat;
            

        }
        private void txtTransferAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveTransferAccount = txtTransferAccNo.Text.Trim();
                SearchText = txtTransferAccNo.Text.Trim();
                var lcs = (from c in lodbserver.BankAccountMasters where c.IBANAccountNo.Contains(SearchText) orderby c.IBANAccountNo select c).FirstOrDefault();
                if (lcs != null)
                {
                    SaveID = lcs.AccountID;                   
                    //txt_customer_first_name.Text = lcs.Customer_FirstName;
                    //txt_TopCust_Fname.Text = lcs.Customer_FirstName;
                    //txt_customer_last_name.Text = lcs.Customer_LastName;
                    //txt_TopCust_Lname.Text = lcs.Customer_LastName;
                    txtTransferAccountName.Text = lcs.Customer_LastName + "   " + lcs.Customer_LastName;
                    //txt_address_line1.Text = lcs.CAddress1;
                    //txt_address_line1.Text = lcs.CAddress2;
                    //txt_city.Text = lcs.CCity;
                    //txt_zipcode.Text = lcs.CZipcode;
                    //txt_country.Text = lcs.CCountry;
                    //txt_phone.Text = lcs.CPhone;
                    //txt_email.Text = lcs.Cemail;
                    //SaveBalance = (decimal)lcs.Balance;
                    SaveTransferTargetBalance = (decimal)lcs.Balance;
                    //txtBalance.Text = lcs.Balance?.ToString("###,###.##");
                    //this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
                    this.Refresh();
                }
                txtTransferAMT.Focus();
            }
        }

        private void TransferTabPage_Click(object sender, EventArgs e)
        {

        }
        private bool CheckCustomerDepositData()
        {
            if ((txt_Deposit.Text.Trim() == ""))
            {
                RJMessageBox.Show("Please enter Deposit Amount !!!", "Warning", MessageBoxButtons.OK);
                txt_Deposit.Focus();
                return false;
            }           
            else
            {
                return true;
            }
        }

        private void TransferCancelButton_Click(object sender, EventArgs e)
        {
            this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
            DepositButton.Focus();
        }

        private void DepositOkButton_Click(object sender, EventArgs e)
        {
            ConfirmDepositFlag = true;
            var result = CheckCustomerDepositData();
            if ((result == true) && (ConfirmDepositFlag))
            {
                try
                {
                    if (RJMessageBox.Show("Do you confirm to deposit money to your Bank Account? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {


                        ClassLibrary_NL_BANK.BankAccountMaster ccDeposit = new ClassLibrary_NL_BANK.BankAccountMaster();

                        var qcsN = (from c in lodbserver.BankAccountMasters where (c.IBANAccountNo == SaveCustAccountNo) select c).FirstOrDefault();
                        if (qcsN != null)
                        {
                            qcsN.IBANAccountNo = txt_AccountNO.Text;
                            qcsN.Balance = SaveNewBalance;
                            if (SaveNewBalance > 0.00m)
                            {
                                txtBalance.Text = SaveNewBalance.ToString("###,###.##");
                                txt_TopBalance.Text = SaveNewBalance.ToString("###,###.##");
                            }
                            else
                            {
                                txtBalance.Text = "0.00";
                                txt_TopBalance.Text = "0.00";
                            }

                        }
                        using (var tr = lodbserver.Database.BeginTransaction())
                        {
                            //lodbserver.BankAccountMasters.Add(cc);
                            lodbserver.SaveChanges();
                            tr.Commit();
                            //ClearForm();
                            RJMessageBox.Show("Deposit Process is saved to your Bank Account.", "Save Completed", MessageBoxButtons.OK);

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

        private void DepositCancelButton_Click(object sender, EventArgs e)
        {
            this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
        }

        private bool CheckCustomerData()
        {
            if ((txtTransferAccNo.Text.Trim() == "") || (txtTransferAccountName.Text.Trim() == ""))
            {
                RJMessageBox.Show("Please enter Transfer Account Number !!!", "Warning", MessageBoxButtons.OK);
                txtTransferAccNo.Focus();
                return false;
            }
            if ((txtTransferAMT.Text.Trim() == ""))
            {
                RJMessageBox.Show("Please enter Transfer Amount ", "Warning", MessageBoxButtons.OK);
                txtTransferAMT.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private decimal GetBalance(string KeyAccountNO)
        {
            decimal result = 0;
            var Owner = (from c in lodbserver.BankAccountMasters where (c.IBANAccountNo == KeyAccountNO) select c).FirstOrDefault();
            if (Owner != null)
            {
                 result = (decimal) Owner?.Balance;
            }
            return result;
        }
        private void TransferOKButton_Click(object sender, EventArgs e)
        {
            ConfirmTransferFlag = true;
            var result = CheckCustomerData();
            if ((result == true) && (ConfirmTransferFlag))
            {
                try
                {
                    if (RJMessageBox.Show("Do you confirm to transfer money to this Bank Account? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {


                        ClassLibrary_NL_BANK.BankAccountMaster cct = new ClassLibrary_NL_BANK.BankAccountMaster();

                        var qcsTransfer = (from c in lodbserver.BankAccountMasters where (c.IBANAccountNo == txtTransferAccNo.Text) select c).FirstOrDefault();
                        if (qcsTransfer != null)
                        {
                            qcsTransfer.IBANAccountNo = txtTransferAccNo.Text;
                            qcsTransfer.Balance = SaveTransferTargetBalance;
                        }
                        using (var tr = lodbserver.Database.BeginTransaction())
                        {
                            //lodbserver.BankAccountMasters.Add(cc);
                            lodbserver.SaveChanges();
                            tr.Commit();
                            //ClearForm();
                            RJMessageBox.Show("Transfer Money To Transfer Bank Account is saved", "Save Completed", MessageBoxButtons.OK);

                        }
                        ClassLibrary_NL_BANK.BankAccountMaster ccOwner = new ClassLibrary_NL_BANK.BankAccountMaster();
                        var qcsOwner = (from c in lodbserver.BankAccountMasters where (c.IBANAccountNo == SaveCustAccountNo) select c).FirstOrDefault();
                        if (qcsOwner != null)
                        {
                            qcsOwner.IBANAccountNo = txtTransferAccNo.Text;
                            qcsOwner.Balance = SaveNewBalance;
                            if (SaveNewBalance > 0)
                            {
                                txt_TopBalance.Text = SaveNewBalance.ToString("###,###.##");
                                txtBalance.Text = SaveNewBalance.ToString("###,###.##");
                            }
                            else
                            {
                                txtBalance.Text = "0.00";
                                txt_TopBalance.Text = "0.00";
                            }


                        }
                        using (var tr = lodbserver.Database.BeginTransaction())
                        {
                            //lodbserver.BankAccountMasters.Add(cc);
                            lodbserver.SaveChanges();
                            tr.Commit();
                            //ClearForm();
                            RJMessageBox.Show("Your Bank Account is saved", "Save Completed", MessageBoxButtons.OK);
                            ExitButton.Focus();
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

        private void txtTransferAMT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveBalance = GetBalance(txt_AccountNO.Text);
                SaveTransferAMT = Convert.ToDecimal(txtTransferAMT.Text);
                SaveTransferTargetBalance = SaveTransferTargetBalance + SaveTransferAMT;
                SaveNewBalance = SaveBalance - SaveTransferAMT;               
                txt_TransferNewBalance.Text = SaveNewBalance.ToString("###,###.##");
                TransferOKButton.Focus();
            }
        }

        private void txt_AccountNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchButton.Focus();
            }
            
        }

        private void txt_Deposit_KeyDown(object sender, KeyEventArgs e)
        {
            if (txt_Deposit.Text.Length > 0)
            {
                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        txt_NewBalance.Text = txt_TopBalance.Text;
                        SaveDeposit = Convert.ToDecimal(txt_Deposit.Text);
                        SaveFee = SaveDeposit * 0.001m;
                        SaveRemainDeposit = SaveDeposit - SaveFee;
                        SaveNewBalance = SaveBalance + SaveRemainDeposit;
                        txt_Fee.Text = SaveFee.ToString("###,###.##");
                        txt_RemainDeposit.Text = SaveRemainDeposit.ToString("###,###.##");
                        txt_NewBalance.Text = SaveNewBalance.ToString("###,###.##");
                        DepositDoneFlag = true;
                        txt_Fee.Focus();
                    }
                }
                catch (Exception h)
                {
                    RJMessageBox.Show("Please provide number only", "Warning", MessageBoxButtons.OK);
                }
            }
                

        }

        private void txt_Fee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_RemainDeposit.Focus();
            }
        }

        private void txt_Deposit_TextChanged(object sender, EventArgs e)
        {          
                //if (DepositDoneFlag == false)
                //{
                //    SaveDeposit = Convert.ToDecimal(txt_Deposit.Text);
                //    SaveFee = SaveDeposit * 0.001m;
                //    SaveRemainDeposit = SaveDeposit - SaveFee;
                //    SaveNewBalance = SaveBalance - SaveFee;
                //    txt_Fee.Text = SaveFee.ToString("###,###.##");
                //    txt_RemainDeposit.Text = SaveRemainDeposit.ToString("###,###.##");
                //    txt_NewBalance.Text = SaveNewBalance.ToString("###,###.##");
                //}     
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            SetEnableObject();
            DepositFlag = true;
            TransferFlag = false;
            SaveFlag = false;
            this.ClearForm();
            //LastID = GetLastID();
            //setvariablesForIBAN();
            RJMessageBox.Show("Please complete all information marked with * !!!", "Suggestion", MessageBoxButtons.OK);
            CreateCustomerAccount p = new CreateCustomerAccount();
            var shared = new SharedClass();
            //RJMessageBox.Show("Please wait for new IBAN number being generated from website.", "Processing...");

            var w = new Form() { Size = new Size(0, 0) };
            Task.Delay(TimeSpan.FromSeconds(10))
            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            RJMessageBox.Show(w, "Please wait for new IBAN number being generated from website.", "Processing...");
            //AutoClosingMessageBox.Show("Please wait for new IBAN number being generated from website.", "Processing...", 5000);
            txt_customer_Account.Text = shared.GetIBANNumber();
            txt_customer_first_name.Focus();
        }

        public static BANK_SERVICES Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BANK_SERVICES();
                else
                    _instance = new BANK_SERVICES();
                return _instance;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchText = txt_AccountNO.Text.Trim();
            var lcs = (from c in lodbserver.BankAccountMasters where c.IBANAccountNo.Contains(SearchText) orderby c.IBANAccountNo select c).FirstOrDefault();
            if (lcs != null)
            {
                SaveCustAccountNo = lcs.IBANAccountNo;
                SaveID = lcs.AccountID;
                txt_AccountNO.Text = lcs.IBANAccountNo;
                txt_customer_Account.Text = lcs.IBANAccountNo;
                txt_customer_first_name.Text = lcs.Customer_FirstName;
                txt_TopCust_Fname.Text = lcs.Customer_FirstName;
                txt_customer_last_name.Text = lcs.Customer_LastName;
                txt_TopCust_Lname.Text = lcs.Customer_LastName;
                txt_address_line1.Text = lcs.CAddress1;
                txt_address_line2.Text = lcs.CAddress2;
                txt_city.Text = lcs.CCity;
                txt_zipcode.Text = lcs.CZipcode;
                txt_country.Text = lcs.CCountry;
                txt_phone.Text = lcs.CPhone;
                txt_email.Text = lcs.Cemail;
                SaveBalance = (decimal)lcs.Balance;
                if (SaveBalance > 0.00m)
                {
                    txtBalance.Text = lcs.Balance?.ToString("###,###.##");
                    txt_TopBalance.Text = lcs.Balance?.ToString("###,###.##");
                }
                else
                {
                    txtBalance.Text = "0.00";
                    txt_TopBalance.Text = "0.00";
                }

                this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DetailsTabPage"];
                this.Refresh();
            }
        }

        private void DepositButton_Click(object sender, EventArgs e)
        {
            this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["DepositTabPage"];
            //SetEnableObject();
            DepositFlag = true;
            TransferFlag = false;
            SaveFlag = false;
            //this.ClearForm();
            //LastID = GetLastID();
            
            //RJMessageBox.Show("Please complete all information marked with * !!!", "Suggestion", MessageBoxButtons.OK);

            txt_Deposit.Focus();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Transfer_Button_Click(object sender, EventArgs e)
        {
            this.tabControl_BankAccount.SelectedTab = this.tabControl_BankAccount.TabPages["TransferTabPage"];
            //SetEnableObject();
            DepositFlag = false;
            TransferFlag = true;
            SaveFlag = false;
            //this.ClearForm();
            //LastID = GetLastID();

            //RJMessageBox.Show("Please complete all information marked with * !!!", "Suggestion", MessageBoxButtons.OK);

            txtTransferAccNo.Focus();
        }

       

        
        private void txt_Balance_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void tabControl_BankAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
