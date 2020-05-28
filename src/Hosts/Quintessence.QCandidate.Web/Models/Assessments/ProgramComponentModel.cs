namespace Quintessence.QCandidate.Models.Assessments
{
    public class ProgramComponentModel
    {
        public ProgramComponentModel(string title, string location, string documentName, string documentLink, string assessors, string time, int startPixelOffset, int endPixelOffset)
        {
            Title = title;
            Location = location;
            DocumentName = documentName;
            DocumentLink = documentLink;
            Assessors = assessors;
            Time = time;
            StartPixelOffset = startPixelOffset;
            EndPixelOffset = endPixelOffset;
        }

        public string Title { get; }
        public string Location { get; }
        public string DocumentName { get; }
        public string DocumentLink { get; }
        public string Assessors { get; }
        public string Time { get; }
        public int StartPixelOffset { get; }
        public int EndPixelOffset { get; }
    }
}