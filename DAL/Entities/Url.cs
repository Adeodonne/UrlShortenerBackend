namespace DAL.Entities;

public class Url
{
    public string Id { get; set; }
    public string FullUrl { get; set; }
    public string ShortUrl { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    
    public User User { get; set; }
}
