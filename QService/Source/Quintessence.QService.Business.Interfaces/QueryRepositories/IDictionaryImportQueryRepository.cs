using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IDictionaryImportQueryRepository
    {
        DictionaryImportView ProcessDictionaryFile(string filename);
    }
}