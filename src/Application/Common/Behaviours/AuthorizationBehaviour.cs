using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Behaviours;
public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            if (_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }
            var mustBeAdmin = authorizeAttributes.Select(x=>x.MustBeAdmin).First();
            User user = await _applicationDbContext.Users.AsNoTracking().Where(x=>x.Id == _currentUserService.UserIdGuid).FirstAsync();
            if(mustBeAdmin&&!(user.Role==Role.Admin))
            {
                throw new UnauthorizedAccessException();
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}