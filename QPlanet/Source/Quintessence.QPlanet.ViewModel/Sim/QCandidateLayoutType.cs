using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class QCandidateLayoutType : Enumeration
    {
        public static readonly QCandidateLayoutType Hide = new QCandidateLayoutType(1, nameof(Hide));
        public static readonly QCandidateLayoutType Show = new QCandidateLayoutType(2, nameof(Show));
        public static readonly QCandidateLayoutType Pdf = new QCandidateLayoutType(3, nameof(Pdf));
        public static readonly QCandidateLayoutType Memo = new QCandidateLayoutType(4, nameof(Memo));


        public QCandidateLayoutType(int id, string name)
            : base(id, name)
        {
        }

	}
}