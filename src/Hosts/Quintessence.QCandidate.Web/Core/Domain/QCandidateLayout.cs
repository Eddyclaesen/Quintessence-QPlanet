using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class QCandidateLayout : Enumeration
    {
        public static readonly QCandidateLayout None = new QCandidateLayout(0, nameof(None));
        public static readonly QCandidateLayout Pdf = new QCandidateLayout(1, nameof(Pdf));
        public static readonly QCandidateLayout Memo = new QCandidateLayout(2, nameof(Memo));


        public QCandidateLayout(int id, string name)
            : base(id, name)
        {
        }
        
    }
}
