using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Authenticate.Command.Login
{
    public class LoginUserCommand: IRequest<TokenResult>
    {
        public string Login { get; set; }= string.Empty;
        public string Password { get ;set; }=string.Empty;
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResult>
    {
        IJWTService _IJWTService;
        IApplicationDbContext _context;
        IHashPassword _hashPassword;

        public LoginUserCommandHandler(IJWTService iJWTService, IApplicationDbContext context, IHashPassword hashPassword)
        {
            _IJWTService = iJWTService;
            _context = context;
            _hashPassword = hashPassword;
        }

        public async Task<TokenResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x=>x.Email==request.Login&&x.Password== _hashPassword.Hash(request.Password))?? throw new BadRequestException("Login or password is incorrect");
            return new(){AccessToken=_IJWTService.GenerateJWT(user)};
        }
    }
}