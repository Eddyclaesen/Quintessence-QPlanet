using System.Linq;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class Language: Enumeration
    {
        public Language(int id, string name, string code)
            : base(id, name)
        {
            Code = code;
        }

        public string Code { get; }

        public static readonly Language Dutch = new Language(1, nameof(Dutch), "NL");
        public static readonly Language French = new Language(2, nameof(French), "FR");
        public static readonly Language English = new Language(3, nameof(English), "EN");


        public static Language FromCode(string code)
        {
            return GetAll<Language>().Single(l => l.Code == code.ToUpperInvariant());
        }
    }
}