namespace Stats.Api.Models
{
    public class Period : BaseModel
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public short HomeScore { get; set; }
        public short VisitorScore { get; set; }
    }
}
