namespace BLL.DTO.UrlDTO;

public class UrlDto
{
    public string Id { get; set; }
    public string FullUrl { get; set; }
    public string ShortUrl { get; set; }
    public DateTime Date { get; set; }
    public bool Existed { get; set; }
}