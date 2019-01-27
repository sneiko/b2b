using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using B2BApi.Enums;
using B2BApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace B2BApi.Helpers
{
    internal static class JwtHelper
    {
        public static CompleteToken BuildCompleteToken(User user, JwtConfiguration configuration, DateTime creationDate)
        {
            var accessExpires = creationDate.AddMinutes(configuration.AccessExpiresMinutes);
            var accessToken = BuildToken(user, configuration, accessExpires, JwtType.Access);
            var refreshExpires = creationDate.AddMinutes(configuration.RefreshExpiresMinutes);
            var refreshToken = BuildToken(user, configuration, refreshExpires, JwtType.Refresh);
            var completeToken = new CompleteToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpirationTime = accessExpires
            };
            return completeToken;
        }
        
                
        public static bool TryGetUserId(this HttpRequest request, out int userId)
        {
            try
            {
                var jwt = request.GetJwt();
                userId = GetUserId(jwt);
                return true;
            }
            catch
            {
                userId = 0;
                return false;
            }
        }
        
        public static bool TryGetJwt(this HttpRequest request, out string jwtToken)
        {
            try
            {
                var jwt = request.GetJwt();
                jwtToken = jwt;
                return true;
            }
            catch
            {
                jwtToken = null;
                return false;
            }
        }
        
        private static string BuildToken(User user, JwtConfiguration configuration, DateTime expires, JwtType type)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            
            if (type == JwtType.Access)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));   
            }

            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecurityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(configuration.Issuer, 
                configuration.Issuer, 
                claims,
                expires: expires, 
                signingCredentials: signingCredentials);
            
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static int GetUserId(string jwt)
        {
            var jwtHandler = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            var nameIdentifier = jwtHandler.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            if (int.TryParse(nameIdentifier.Value, out var userId))
            {
                return userId;
            } 
            throw new InvalidOperationException($"Id пользователя не найден в токене {jwt}");
        }
        
        private static string GetJwt(this HttpRequest request)
        {
            string authorizationHeader = request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new InvalidOperationException("JWT не найден");
            }

            const string toBeSearched = "Bearer ";
            var index = authorizationHeader.IndexOf(toBeSearched, StringComparison.OrdinalIgnoreCase);
            return index != -1 
                ? authorizationHeader.Substring(index + toBeSearched.Length) 
                : throw new InvalidOperationException("JWT задан неверно");
        }
    }
}