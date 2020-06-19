using System;

namespace Quintessence.QCandidate.Models
{
    public class ProgramComponent
    {
        private DateTime _start;

        public ProgramComponent(string title, DateTime start, bool pdfExists, string pdfUrl)
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