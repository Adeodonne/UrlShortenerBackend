namespace DAL.Entities;

public class User
{
    public string Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<Url> Urls { get; set; }
}
