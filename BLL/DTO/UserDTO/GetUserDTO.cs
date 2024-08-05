namespace BLL.DTO.UserDTO;

public class GetUserDTO
{
    public string Id { get; set; }
    public string Login { get; set; }
    public bool IsAdmin { get; set; }
    public string Token { get; set; }
}
