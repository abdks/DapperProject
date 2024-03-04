namespace DapperProject.Dtos
{
    public class ResultProjectDto
    {
        public int id {  get; set; }
        public float rms { get; set; }
        public int eventID { get; set; }
        public string location {  get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float magnitude { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public DateTime date { get; set; }
        public string isEventUpdate { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}
