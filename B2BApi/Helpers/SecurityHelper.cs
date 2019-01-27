using System;
using System.Collections.Generic;
using System.Linq;
using B2BApi.Models;
using B2BApi.Providers;

namespace B2BApi.Helpers
{
    internal static class SecurityHelper
    {
        public static void SetCredentials(User user)
        {
            var password = GenerateRandomPassword(out var salt);
            var hasher = new Sha256HashProvider();
            user.Credentials = new Credentials
            {
                Password = hasher.GetHash(password + salt),
                Salt = salt
            };
        }

        public static string HashPassword(string password, string salt)
        {
            var hasher = new Sha256HashProvider();
            return hasher.GetHash(password + salt);
        }

        public static string GenerateRandomPassword(out string salt)
        {
            var opts = new
            {
                RequiredLength = 8,
                RequiredSaltLength = 6,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars =
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",  
                "abcdefghijkmnopqrstuvwxyz", 
                "0123456789", 
                "!@$?_-"
            };
            
            var rand = new Random(Environment.TickCount);
            var passwordChars = new List<char>();
            var saltChars = new List<char> { 'x', 'a' };

            if (opts.RequireUppercase)
            {
                passwordChars.Insert(rand.Next(0, passwordChars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

                saltChars.Insert(rand.Next(0, saltChars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            }

            if (opts.RequireLowercase)
            {
                passwordChars.Insert(rand.Next(0, passwordChars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

                saltChars.Insert(rand.Next(0, saltChars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);
            }

            if (opts.RequireDigit)
            {
                passwordChars.Insert(rand.Next(0, passwordChars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

                saltChars.Insert(rand.Next(0, saltChars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
            }

            if (opts.RequireNonAlphanumeric)
            {
                passwordChars.Insert(rand.Next(0, passwordChars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);
            }

            for (var i = passwordChars.Count;
                i < opts.RequiredLength
                || passwordChars.Distinct().Count() < opts.RequiredUniqueChars;
                i++)
            {
                var rcs = randomChars[rand.Next(0, randomChars.Length)];
                passwordChars.Insert(rand.Next(0, passwordChars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            } 
            
            for (var i = saltChars.Count;
                i < opts.RequiredLength
                || saltChars.Distinct().Count() < opts.RequiredUniqueChars;
                i++)
            {
                var rcs = randomChars[rand.Next(0, randomChars.Length)];
                saltChars.Insert(rand.Next(0, saltChars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            salt = new string(saltChars.ToArray());
            return new string(passwordChars.ToArray());
        }
    }
}