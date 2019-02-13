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
            
            var handler1 = new Handler
            {
                Status = HandlerStatus.Idle,
                Url = "http://opt.nasklade.by/sitefiles/1/11/common_pricelist.xls",
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
                    new GrabColumnItem {GrabColumn = GrabColumn.Price, Value = 9},
                    new GrabColumnItem {GrabColumn = GrabColumn.Count, Value = 8}
                }
            };
            
            var provider1 = new Provider
            {
                Bic = "PJCBBY2X",
                Inn = "191890972",
                Name = "ООО \"Надежные инструменты\"",
                uAddress = "220140, г. Минск, ул. Притыцкого 62, корп.12, комната 203",
                Bank = "ОАО \"Приорбанк\"",
                KorSchet = "1923401923",
                RasSchet = "BY34PJCB30120294611000000933"
            };
            
            var handler2 = new Handler
            {
                Status = HandlerStatus.Idle,
                Url = "http://www.tools.by/ftpdata/mg/2019-02-09_691619708_tools.csv",
                StartRowData = 1,
                AddNewProduct = true,
                LastUpdate = DateTime.Now,
                GrabColumnItems = new List<GrabColumnItem>
                {
                    new GrabColumnItem {GrabColumn = GrabColumn.Model, Value = 4},
                    new GrabColumnItem {GrabColumn = GrabColumn.Brand, Value = 19},
                    new GrabColumnItem {GrabColumn = GrabColumn.Gtin, Value = 16},
                    new GrabColumnItem {GrabColumn = GrabColumn.PartNumber, Value = 5},
                    new GrabColumnItem {GrabColumn = GrabColumn.Price, Value = 8},
                    new GrabColumnItem {GrabColumn = GrabColumn.Count, Value = 12}
                }
            };
            
            var provider2 = new Provider
            {
                Bic = "PJCBBY2X",
                Inn = "192775574",
                Name = "ООО «ТД Комплект»",
                uAddress = " г.Минск, ул. Брикета 31",
                Bank = "ОАО «Белгазпромбанк»",
                KorSchet = "1923401923",
                RasSchet = "BY46OLMP30120001041450000933 "
            };

            handler.Provider = provider;
            
            handler1.Provider = provider1;
            
            handler2.Provider = provider2;
            
            #endregion
            
            if (!context.Handlers.Any())
            {
                context.Handlers.AddRange(handler);
                context.Handlers.AddRange(handler1);
                context.Handlers.AddRange(handler2);
                context.SaveChanges();
            }
        }
    }
}