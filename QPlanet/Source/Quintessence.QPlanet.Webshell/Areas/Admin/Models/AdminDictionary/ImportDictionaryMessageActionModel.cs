namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminDictionary
{
    public class ImportDictionaryMessageActionModel
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public ImportDictionaryMessageSeverity Severity { get; set; }
    }

    public enum ImportDictionaryMessageSeverity
    {
        Info,
        Warning,
        Error
    }
}