using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Sharepoint.Integration.Tests.SPListsService;

namespace Quintessence.QService.Sharepoint.Integration.Tests
{
    [TestClass]
    public class ListsSoapClientTests
    {
        /// <summary>
        /// Tests the get list collection.
        /// </summary>
        [TestMethod]
        public void TestGetListCollection()
        {
            //var binding = new BasicHttpBinding("ListsSoap");
            //{
            //    Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.TransportCredentialOnly, Transport = new HttpTransportSecurity { ClientCredentialType = HttpClientCredentialType.Windows } }
            //};
            //var endpoint = new EndpointAddress("http://qshare/_vti_bin/Lists.asmx");

            var client = new ListsSoapClient("ListsSoap");

            if (client.ClientCredentials != null)
            {
#pragma warning disable 612, 618
                client.ClientCredentials.Windows.AllowNtlm = true;
#pragma warning enable 612, 618

                client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;
                client.ClientCredentials.Windows.ClientCredential = new NetworkCredential("SPUser", "$Quint123", "QUINTDOMAIN");
            }

            var result = client.GetListCollection();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests the get list.
        /// </summary>
        [TestMethod]
        public void TestGetList()
        {
            //var binding = new BasicHttpBinding("ListsSoap");
            //{
            //    Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.TransportCredentialOnly, Transport = new HttpTransportSecurity { ClientCredentialType = HttpClientCredentialType.Windows } }
            //};
            //var endpoint = new EndpointAddress("http://qshare/_vti_bin/Lists.asmx");

            var client = new ListsSoapClient("ListsSoap");

            if (client.ClientCredentials != null)
            {
#pragma warning disable 612, 618
                client.ClientCredentials.Windows.AllowNtlm = true;
#pragma warning enable 612, 618

                client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;
                client.ClientCredentials.Windows.ClientCredential = new NetworkCredential("SPUser", "$Quint123", "QUINTDOMAIN");
            }

            var result = client.GetList("{9AFD4F74-F8F8-4D6F-AB31-45C0A44D1ADF}");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetListAndView()
        {
            //var binding = new BasicHttpBinding("ListsSoap");
            //{
            //    Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.TransportCredentialOnly, Transport = new HttpTransportSecurity { ClientCredentialType = HttpClientCredentialType.Windows } }
            //};
            //var endpoint = new EndpointAddress("http://qshare/_vti_bin/Lists.asmx");

            var client = new ListsSoapClient("ListsSoap");

            if (client.ClientCredentials != null)
            {
#pragma warning disable 612, 618
                client.ClientCredentials.Windows.AllowNtlm = true;
#pragma warning enable 612, 618

                client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;
                client.ClientCredentials.Windows.ClientCredential = new NetworkCredential("SPUser", "$Quint123", "QUINTDOMAIN");
            }

            var result = client.GetListAndView("{9AFD4F74-F8F8-4D6F-AB31-45C0A44D1ADF}", string.Empty);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetListItems()
        {
            //var binding = new BasicHttpBinding("ListsSoap");
            //{
            //    Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.TransportCredentialOnly, Transport = new HttpTransportSecurity { ClientCredentialType = HttpClientCredentialType.Windows } }
            //};
            //var endpoint = new EndpointAddress("http://qshare/_vti_bin/Lists.asmx");

            var client = new ListsSoapClient("ListsSoap");

            if (client.ClientCredentials != null)
            {
#pragma warning disable 612, 618
                client.ClientCredentials.Windows.AllowNtlm = true;
#pragma warning enable 612, 618

                client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;
                client.ClientCredentials.Windows.ClientCredential = new NetworkCredential("SPUser", "$Quint123", "QUINTDOMAIN");
            }

            var viewFields = XElement.Parse("<ViewFields><FieldRef Name=\"ID\" /></ViewFields>"); //"<ViewFields><FieldRef Name=\"ID\" /><FieldRef Name=\"LinkFilename\" /></ViewFields>");
            var query = XElement.Parse("<Query/>"); //"<Query><Where><Eq><FieldRef Name=\"contacts_ID\"></FieldRef></Eq></Where></Query>");
            var rowLimit = string.Empty;
            var queryOptions = XElement.Parse("<QueryOptions />");
            var webId = string.Empty;

            var result = client.GetListItems(
                "{85232866-A6D1-42C9-AAB4-8D5A6A0282B7}", //"{9AFD4F74-F8F8-4D6F-AB31-45C0A44D1ADF}", 
                "SO_Contacts",  //string.Empty,
                new XmlDocument().ReadNode(query.CreateReader()) as XmlElement,
                new XmlDocument().ReadNode(viewFields.CreateReader()) as XmlElement,
                rowLimit,
                new XmlDocument().ReadNode(queryOptions.CreateReader()) as XmlElement,
                webId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestClientGetLists()
        {
            using (var clientContext = new ClientContext("http://qshare/ITWeb"))
            {
                clientContext.Credentials = new NetworkCredential("SPDev", "$Quint123", "QUINTDOMAIN");

                ListCollection lists = clientContext.Web.Lists;

                clientContext.Load(lists);
                clientContext.ExecuteQuery();
                Assert.IsNotNull(lists);

                foreach (var list in lists)
                {
                    
                }

                //var list = clientContext.Web.Lists.GetByTitle("TrainingChecklist"); //lists.FirstOrDefault(l => l.Title == "TrainingChecklist");

                //Assert.IsNotNull(list);
            }

            using (var clientContext = new ClientContext("http://qshare/ITWeb"))
            {
                clientContext.Credentials = new NetworkCredential("SPDev", "$Quint123", "QUINTDOMAIN");

                var documentList = clientContext.Web.Lists.GetById(Guid.Parse("{A6F82531-21D2-4551-8E95-9D742C6D676B}"));

                clientContext.Load(documentList);
                clientContext.ExecuteQuery();
                Assert.IsNotNull(documentList);

                var query = new CamlQuery
                    {
                        ViewXml = "<View />"
                        //                            @"<View>
                        //                                <Query>
                        //                                  <Where>
                        //                                    <Eq>
                        //                                      <FieldRef Name='Contact_x003a__x0020_contact_id'/>
                        //                                      <Value Type='Text'>3</Value>
                        //                                    </Eq>
                        //                                  </Where>
                        //                                </Query>
                        //                                <RowLimit>100</RowLimit>
                        //                              </View>"
                    };

                var documents = documentList.GetItems(query);
                clientContext.Load(documents);

                clientContext.ExecuteQuery();
                Assert.IsNotNull(documents);
            }
        }
    }
}
