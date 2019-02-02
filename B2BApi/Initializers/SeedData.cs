using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using B2BApi.DbContext;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
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
            var admin1 = new Admin
            {
                UserName = "string",

                Credentials = new Credentials
                {
                    Password = HashProvider.GetHash("stringsalt1"),
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
                    admin1,
                    user
                );

                context.SaveChanges();
            }

            #region handlers
            
            var handler = new Handler
            {
                Name = "Sosiska",
                Url =
                    "https://www.dropbox.com/s/kv5bx2ncfz8bzhn/%D0%9D%D0%B0%D0%B4%D0%B5%D0%B6%D0%BD%D1%8B%D0%B5%20%D0%B8%D0%BD%D1%81%D1%82%D1%80%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D1%8B.xls?dl=1",
                SaveFileName = "pochinki",
                StartRowData = 1,
                LastUpdate = DateTime.Now,
                Patterns = new List<Pattern>
                {
                    new Pattern {Id = 1, ColumnId = 3, Old = "н", New = "0"},
                    new Pattern {Id = 2, ColumnId = 3, Old = "м", New = "5"},
                    new Pattern {Id = 3, ColumnId = 3, Old = "c", New = "10"},
                    new Pattern {Id = 4, ColumnId = 3, Old = "б", New = "50"}
                },
                GrabColumnItems = new List<GrabColumnItem>
                {
                    new GrabColumnItem {Id = 1, GrabColumn = GrabColumn.Model, Value = 4},
                    new GrabColumnItem {Id = 2, GrabColumn = GrabColumn.Brand, Value = 7},
                    new GrabColumnItem {Id = 3, GrabColumn = GrabColumn.PartNumber, Value = 3},
                    new GrabColumnItem {Id = 4, GrabColumn = GrabColumn.Price, Value = 11},
                    new GrabColumnItem {Id = 5, GrabColumn = GrabColumn.Count, Value = 8}
                }
            };

            #endregion
            
            
            if (!context.Handlers.Any())
            {
                context.Handlers.AddRange(handler);
                context.SaveChanges();
            }
        }
    }
}