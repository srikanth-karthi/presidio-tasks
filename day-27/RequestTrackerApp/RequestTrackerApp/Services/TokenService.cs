﻿using Microsoft.IdentityModel.Tokens;
using RequestTrackerApp.Interfaces;
using RequestTrackerApp.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RequestTrackerApp.Services
{


        public class TokenService : ITokenService
        {
            private readonly string _secretKey;
            private readonly SymmetricSecurityKey _key;

            public TokenService(IConfiguration configuration)
            {
                _secretKey = configuration.GetSection("TokenKey").GetSection("JWT").Value.ToString();
                _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            }
            public string GenerateToken(Employee user)
            {
                string token = string.Empty;
                var claims = new List<Claim>(){
                new Claim("eid",user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

                var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var myToken = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
                token = new JwtSecurityTokenHandler().WriteToken(myToken);
                return token;
            }
        }
    }

