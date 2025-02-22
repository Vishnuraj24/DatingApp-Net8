using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using API.Entities;
using DatingApp.API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{

    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from the appsetting configuration");
            if (tokenKey.Length < 64) throw new Exception("Your tokenkey needs to be longer");

            //just like ssl -> public key and private key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.UserName);
            }


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }


}