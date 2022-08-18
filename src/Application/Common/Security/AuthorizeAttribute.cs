using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute() { }
    
    public bool MustBeAdmin { get; set; }=false;
}
