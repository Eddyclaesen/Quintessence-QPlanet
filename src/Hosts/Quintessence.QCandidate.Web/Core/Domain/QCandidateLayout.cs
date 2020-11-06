using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class QCandidateLayout : Enumeration
    {
        public static readonly QCandidateLayout Unknown = new QCandidateLayout(0, nameof(Unknown));
        public static readonly QCandidateLayout Hide = new QCandidateLayout(1, nameof(Hide));
        public static readonly QCandidateLayout Show = new QCandidateLayout(2, nameof(Show));
        public static readonly QCandidateLayout Pdf = new QCandidateLayout(3, nameof(Pdf));
        public static readonly QCandidateLayout Memo = new QCandidateLayout(4, nameof(Memo));


        public QCandidateLayout(int id, string name)
            : base(id, name)
        {
        }
        
    }
}
