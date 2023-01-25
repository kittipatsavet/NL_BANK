using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary_NL_BANK;
using CustomMessageBox;

namespace NL_BANK
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);           
            Application.Run(new LoginForm());
            if (GlobalVar.AuthenPass == true)
            {
                if (GlobalVar.UserRole == "ADMIN")
                {
                    Application.Run(new MDIParent_ADMIN());
                }
                else
                    if ((GlobalVar.UserRole == "CUSTOMER"))
                {
                    Application.Run(new MDIParent_CUSTOMER());
                }
                else
                {
                    Application.Run(new MDIParent_CUSTOMER());
                }

            }
        }
    }
}
