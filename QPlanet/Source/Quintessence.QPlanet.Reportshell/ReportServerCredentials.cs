using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

namespace Quintessence.QPlanet.Reportshell
{
    public class ReportServerCredentials : IReportServerCredentials
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _domain;

        public ReportServerCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = _userName;
            password = _password;
            authority = null;
            return false;
        }

        public WindowsIdentity ImpersonationUser { get; private set; }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_userName, _password, _domain); }
        }
    }
}