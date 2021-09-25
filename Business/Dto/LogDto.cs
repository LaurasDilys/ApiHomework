namespace Business.Dto
{
    public class LogDto
    {
        public string Timestamp { get; set; }
        public string Level { get; set; }
        public string MessageTemplate { get; set; }
        public string RenderedMessage { get; set; }
    }
}
