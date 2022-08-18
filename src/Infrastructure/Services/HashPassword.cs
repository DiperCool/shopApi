using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
namespace CleanArchitecture.Infrastructure.Services;
public class HashPassword : IHashPassword
{
    public string Hash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())  
        {  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)); 
            StringBuilder builder = new StringBuilder();  
            for (int i = 0; i < bytes.Length; i++)  
            {  
                builder.Append(bytes[i].ToString("x2"));  
            }  
            return builder.ToString();
        }  
    }
}