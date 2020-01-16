using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace Quintessence.QService.Business.Model
{
    public class DictionaryEntry
    {
        private readonly DataRow _dataRow;
        private readonly int _languageId;

        public DictionaryEntry(DataRow dataRow, int languageId)
        {
            _dataRow = dataRow;
            _languageId = languageId;
        }

        public string Cluster
        {
            get { return GetDataRowValue<string>("Cluster"); }
        }

        public string ClusterDefinition
        {
            get { return GetDataRowValue<string>("ClusterDefinition"); }
        }

        public string Competence
        {
            get { return GetDataRowValue<string>("Competence"); }
        }

        public string CompetenceDefinition
        {
            get { return GetDataRowValue<string>("CompetenceDefinition"); }
        }

        public string Level
        {
            get { return Convert.ToInt32(GetDataRowValue<double?>("Level")).ToString(CultureInfo.InvariantCulture); }
        }

        public string LevelDescription
        {
            get { return GetDataRowValue<string>("LevelDescription"); }
        }

        public string Indicator
        {
            get { return GetDataRowValue<string>("Indicator"); }
        }

        public int LanguageId
        {
            get { return _languageId; }
        }

        private TType GetDataRowValue<TType>(string propertyName)
        {
            //var stacktrace = new StackTrace();
            //var frame = stacktrace.GetFrame(1);
            //var propertyName = frame.GetMethod().Name.Substring(4);
            return (TType)(_dataRow[propertyName] != DBNull.Value ? _dataRow[propertyName] : null);
        }
    }
}