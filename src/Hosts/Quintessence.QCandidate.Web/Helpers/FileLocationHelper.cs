﻿using System;
using System.IO;

namespace Quintessence.QCandidate.Helpers
{
    public static class FileLocationHelper
    {
        public static string GetPdfFileLocation(string pdfFolder, Guid simulationCombinationId, string language)
        {
            if(string.IsNullOrWhiteSpace(pdfFolder)
               || string.IsNullOrWhiteSpace(language))
            {
                return null;
            }

            var filename = $"{simulationCombinationId}.pdf";

            return Path.Combine(pdfFolder, language, filename);
        }

        public static string GetNeoFileLocation(string pdfFolder, Guid candidateId)
        {
            if (string.IsNullOrWhiteSpace(pdfFolder))
            {
                return null;
            }

            var filename = $"{candidateId}.pdf";

            return Path.Combine(pdfFolder, filename);
        }
    }
}