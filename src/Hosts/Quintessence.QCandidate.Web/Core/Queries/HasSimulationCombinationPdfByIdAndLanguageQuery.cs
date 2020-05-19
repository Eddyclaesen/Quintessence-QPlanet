using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class HasSimulationCombinationPdfByIdAndLanguageQuery : IRequest<bool>
    {
        public HasSimulationCombinationPdfByIdAndLanguageQuery(string filename)
        {
            Filename = filename;
        }

        public string Filename { get; }
    }
}