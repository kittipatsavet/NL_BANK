//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary_NL_BANK
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_BankAccountMaster
    {
        public int AccountID { get; set; }
        public string IBANAccountNo { get; set; }
        public string Customer_FirstName { get; set; }
        public string Customer_LastName { get; set; }
        public string CAddress1 { get; set; }
        public string CAddress2 { get; set; }
        public string CCity { get; set; }
        public string CZipcode { get; set; }
        public string CCountry { get; set; }
        public string Cemail { get; set; }
        public string CPhone { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<System.DateTime> AccDateCreated { get; set; }
    }
}
