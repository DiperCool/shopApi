using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set;}
        public string Email { get; set; }= string.Empty;
        public string BillingId { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string Phone { get; set; }= string.Empty;
        public Role Role { get; set; }
    }
}