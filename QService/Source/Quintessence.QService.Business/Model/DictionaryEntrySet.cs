using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Quintessence.QService.Business.Model
{

    public class DictionaryEntrySet : IEnumerable<DictionaryEntry>
    {
        private readonly IEnumerable<DictionaryEntry> _entries;

        public DictionaryEntrySet(DataTable dataTable)
        {
            _entries = dataTable.Rows.OfType<DataRow>().Select(dr => new DictionaryEntry(dr, int.Parse(dataTable.TableName)));
        }

        public IEnumerator<DictionaryEntry> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
