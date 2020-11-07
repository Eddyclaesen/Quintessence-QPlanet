using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class QCandidateLayout : Enumeration
    {
        public QCandidateLayout(int id, string name)
            : base(id, name)
        {
        }

        public static readonly QCandidateLayout Hide = new QCandidateLayout(1, nameof(Hide));
        public static readonly QCandidateLayout Pdf = new QCandidateLayout(2, nameof(Pdf));
        public static readonly QCandidateLayout Memo = new QCandidateLayout(3, nameof(Memo));
    }
}