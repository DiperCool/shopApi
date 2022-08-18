using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs;

public class UserDTO: IMapFrom<User>
{
    public Guid Id { get; set;}
    public string Email { get; set; }= string.Empty;
    public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; }= string.Empty;
}
