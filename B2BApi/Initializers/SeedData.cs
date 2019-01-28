using System;
using System.IO;
using System.Linq;
using B2BApi.DbContext;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Initializers
{
    public static class SeedData
    {
        private static readonly IHashProvider HashProvider = new Sha256HashProvider();

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<B2BDbContext>();
            context.Database.EnsureCreated();

            #region Users

            var admin = new Admin
            {
                UserName = "b2b",

                Credentials = new Credentials
                {
                    Password = HashProvider.GetHash("passwordsalt1"),
                    Salt = "salt1"
                }
            };
            var user = new Manager()
            {
                UserName = "manager",

                Credentials = new Credentials
                {
                    Password = HashProvider.GetHash("passwordsalt1"),
                    Salt = "salt1"
                }
            };

            #endregion

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    admin,
                    user
                );

                context.SaveChanges();
            }

        }
    }
}