using System;
using System.IO;

namespace Quintessence.QCandidate.Helpers
{
    public static class FileLocationHelper
    {
        public static string GetPdfFileLocation(string pdfBasePath, Guid simulationCombinationId, string language)
        {
            if(string.IsNullOrWhiteSpace(pdfBasePath)
               || string.IsNullOrWhiteSpace(language))
            {
                return null;
            }

            var filename = $"{simulationCombinationId}.pdf";

            return Path.Combine(pdfBasePath, language, filename);
        }
    }
}