using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Authenticate.Register.Command
{
    public class RegisterUserCommand: IRequest<TokenResult>
    {
        public string Email { get ;set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public string ConfirmPassword { get ;set; } = string.Empty;
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenResult>
    {
        IJWTService _iJWTService;
        IApplicationDbContext _context;
        IHashPassword _hashPassword;

        IBillingService _billingService;

        public RegisterUserCommandHandler(IJWTService iJWTService, IApplicationDbContext context, IHashPassword hashPassword, IBillingService billingService)
        {
            _iJWTService = iJWTService;
            _context = context;
            _hashPassword = hashPassword;
            _billingService = billingService;
        }

        public async Task<TokenResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(await _context.Users.AnyAsync(x=>x.Email==request.Email))
            {
                throw new BadRequestException("This email exists");
            }
            User user = new(){Email = request.Email, Password= _hashPassword.Hash(request.Password), Role = Role.Customer};
            user.BillingId=_billingService.CreateBillingUser(user.Email);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new(){AccessToken=_iJWTService.GenerateJWT(user)};
        }
    }
}