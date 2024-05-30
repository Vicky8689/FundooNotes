using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusineesLayer.Services
{
    public class HashPasswordBL
    {
        public static string HashPsaaword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashPassword);
        }
    }
}
