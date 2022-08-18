using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IBillingService
    {
        string CreateBillingUser(string email);
        string CreateCheckout(User user, Order order);
        string CreatePortal(User user);

    }
}