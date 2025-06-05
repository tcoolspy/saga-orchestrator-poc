namespace AwesomeWebServiceTwo.Models.Dtos;

public class AddUserRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}