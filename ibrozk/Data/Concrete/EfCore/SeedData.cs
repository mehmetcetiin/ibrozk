using Microsoft.EntityFrameworkCore;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;
using System;

namespace ibrozk.Data.Concrete.EfCore
{

    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ImageContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Tagname = "Nesneler" },
                        new Tag { Tagname = "Modeller" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "ibrozk", Password = "ibrozkadmin20" }
                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Urlname = "1--Yüzük.jpg",
                            Column = 1,
                            Tags = context.Tags.Take(3).ToList()
                        }, new Post
                        {
                            Urlname = "1-E-S.jpg",
                            Column = 2,
                            Tags = context.Tags.Take(3).ToList()
                        }, new Post
                        {
                            Urlname = "1-Gözlük.jpg",
                            Column = 3,
                            Tags = context.Tags.Take(3).ToList()
                        }, new Post
                        {
                            Urlname = "1-K-S.jpg",
                            Column = 1,
                            Tags = context.Tags.Take(3).ToList()
                        }, new Post
                        {
                            Urlname = "1-Y-copy.jpg",
                            Column = 2,
                            Tags = context.Tags.Take(3).ToList()
                        }, new Post
                        {
                            Urlname = "15 copy.jpg",
                            Column = 3,
                            Tags = context.Tags.Take(3).ToList()
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

