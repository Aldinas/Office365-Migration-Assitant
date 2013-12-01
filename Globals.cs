using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _365_Migration_Prep_Tool
{
    public class Globals
    {
        // Client primary web domain, e.g. GB3.co.uk.
        private static string _clientDomain;
        // Client account name, as listed in AD e.g. Jason.Newport
        private static string _accountName;
        // Clients primary email address, as listed in AD, e.g. Jason.Newport@gb3.co.uk
        private static string _emailAddress;

        // Required in case of a powershell connection, the admin account details.
        private static string _adminCredentials365;

        public static string ClientDomain
        {
            get { return _clientDomain; }
            set { _clientDomain = value; }
        }

        public static string AccountName
        {
            get { return _accountName; }
            set { _accountName = value; }
        }

        public static string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        // Powershell may need some work. Looks like the
        // System.Management.Automation namespace isn't
        // available in VS 2010 Express. Need to 
        // investigate further. 
        public static string AdminCredentials365
        {
            get { return _adminCredentials365; }
            set { _adminCredentials365 = value; }
        }
    }
}
