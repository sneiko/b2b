using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using B2BApi.DbContext;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using B2BApi.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Initializers
{
    public static class FirstData
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
                    Password = HashProvider.GetHash("passwordkafd0"),
                    Salt = "kafd0"
                }
            };
            var admin1 = new Admin
            {
                UserName = "string",

                Credentials = new Credentials
                {
                    Password = HashProvider.GetHash("string9dkf9"),
                    Salt = "9dkf9"
                }
            };
            var user = new Manager()
            {
                UserName = "manager",

                Credentials = new Credentials
                {
                    Password = HashProvider.GetHash("passworddf0os"),
                    Salt = "df0os"
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
                Status = HandlerStatus.Idle,
                Url =
                    "https://www.dropbox.com/s/kv5bx2ncfz8bzhn/%D0%9D%D0%B0%D0%B4%D0%B5%D0%B6%D0%BD%D1%8B%D0%B5%20%D0%B8%D0%BD%D1%81%D1%82%D1%80%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D1%8B.xls?dl=1",
                StartRowData = 1,
                AddNewProduct = true,
                LastUpdate = DateTime.Now,
                Patterns = new List<Pattern>
                {
                    new Pattern {ColumnId = 8, Old = "н", New = "0"},
                    new Pattern {ColumnId = 8, Old = "м", New = "5"},
                    new Pattern {ColumnId = 8, Old = "c", New = "10"},
                    new Pattern {ColumnId = 8, Old = "б", New = "50"}
                },
                GrabColumnItems = new List<GrabColumnItem>
                {
                    new GrabColumnItem {GrabColumn = GrabColumn.Model, Value = 4},
                    new GrabColumnItem {GrabColumn = GrabColumn.Brand, Value = 7},
                    new GrabColumnItem {GrabColumn = GrabColumn.PartNumber, Value = 3},
                    new GrabColumnItem {GrabColumn = GrabColumn.Price, Value = 11},
                    new GrabColumnItem {GrabColumn = GrabColumn.Count, Value = 8}
                }
            };

            var provider = new Provider
            {
                Bic = "1023912",
                Inn = "2039401231",
                Name = "Беларусь опт склад",
                uAddress = "Moscow",
                Bank = "Zenit",
                KorSchet = "1923401923",
                RasSchet = "0234123094923"
            };

            handler.Provider = provider;
            
            #endregion
            
            
            if (!context.Providers.Any())
            {
                context.Providers.AddRange(provider);
                context.SaveChanges();
            }
            
            if (!context.Handlers.Any())
            {
                context.Handlers.AddRange(handler);
                context.SaveChanges();
            }
        }
    }
}