using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Helper
{
    public class TokenGenerateRL
    {
        
        public static string GenerateTokenRL(UserEntity userEntity)
        {
            var authClaims = new[]
            {
               
               new Claim(JwtRegisteredClaimNames.Email,userEntity.Email),
               new Claim("UserName",userEntity.FirstName),
               new Claim("UserID",userEntity.UserId.ToString()),
               
              // new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
           };
            
            var Securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwtSecretKey")));
           
            var creds = new SigningCredentials(Securitykey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("jwtValidIssuer"),
                audience: Environment.GetEnvironmentVariable("jwtValidAudience"),
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials:creds); 
            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
    }
}
