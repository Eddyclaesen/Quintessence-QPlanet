using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Host.Tests.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QPlanetService.Host.Tests
{
    [TestClass]
    public class DictionaryManagementCommandServiceTests : ServiceTestBase
    {
        [TestMethod]
        public void TestAnalyseExcelDictionary()
        {
            var dictionaryFiles = Execute<IDictionaryManagementQueryService, List<DictionaryImportFileInfoView>>(service => service.ListImportDictionaries());

            Assert.IsNotNull(dictionaryFiles);
            Assert.IsTrue(dictionaryFiles.Any());

            var dictionaryFile = dictionaryFiles.FirstOrDefault();

            Assert.IsNotNull(dictionaryFile);

            var dictionary = Execute<IDictionaryManagementQueryService, DictionaryImportView>(service => service.AnalyseDictionary(dictionaryFile.Name));

            Assert.IsNotNull(dictionary);
        }
    }
}
