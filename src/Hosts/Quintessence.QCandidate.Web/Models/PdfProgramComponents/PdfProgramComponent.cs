using System;

namespace Quintessence.QCandidate.Models.PdfProgramComponents
{
    public class PdfProgramComponent
    {
        private DateTime _start;

        public PdfProgramComponent(string title, DateTime start, bool pdfExists, string pdfUrl)
        {
            Title = title;
            _start = start;
            PdfExists = pdfExists;
            PdfUrl = pdfUrl;
        }

        public string Title { get; set; }

        public bool PdfExists { get; set; }

        public bool CanShowPdf => DateTime.Now.Date == _start.Date.Date && DateTime.Now > _start;

        public string PdfUrl { get; set; }
    }
}