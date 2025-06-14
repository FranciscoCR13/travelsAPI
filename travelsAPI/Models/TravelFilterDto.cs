namespace travelsAPI.Models
{
    public class TravelFilterDto
    {
        public string? Name { get; set; }
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }
        public int? OperatorId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? Date { get; set; }

        public string? OrderBy { get; set; }
        public string? OrderDir { get; set; }
    }
}
