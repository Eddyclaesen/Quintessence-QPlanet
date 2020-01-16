namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class NeopirScoreModel
    {
        public string Description { get; set; }
        public string Scale { get; set; }
        public int Score { get; set; }
        public int NormScore { get; set; }
        public string Label { get { return string.Format("{0} {1}", Scale, Description); } }
    }
}