namespace TC.DVDCentral.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TC.DVDCentral.Data.DVDCentralContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TC.DVDCentral.Data.DVDCentralContext";
        }

        protected override void Seed(TC.DVDCentral.Data.DVDCentralContext context)
        {
            //  This method will be called after migrating to the latest version.

            Rating r1 = new Rating { Description = "G" };
            Rating r2 = new Rating { Description = "PG" };
            Rating r3 = new Rating { Description = "PG-13" };
            Rating r4 = new Rating { Description = "R" };
            Rating r5 = new Rating { Description = "NC-17" };
            context.Ratings.AddOrUpdate(r => r.Description, r1);
            context.Ratings.AddOrUpdate(r => r.Description, r2);
            context.Ratings.AddOrUpdate(r => r.Description, r3);
            context.Ratings.AddOrUpdate(r => r.Description, r4);
            context.Ratings.AddOrUpdate(r => r.Description, r5);



            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Horror" });
            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Drama" });
            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Thriller" });
            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Action" });
            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Romance" });
            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Adventure" });
            context.Genres.AddOrUpdate(g => g.Description, new Genre { Description = "Science Fiction" });

            context.Formats.AddOrUpdate(f => f.Description, new Format { Description = "Blu Ray" });
            context.Formats.AddOrUpdate(f => f.Description, new Format { Description = "DVD" });


            context.Directors.AddOrUpdate(d => d.LastName, new Director
            {
                FirstName = "Steven",
                LastName = "Spielberg",
            });
            context.Directors.AddOrUpdate(d => d.LastName, new Director
            {
                FirstName = "George",
                LastName = "Lucas",
            });
            context.Directors.AddOrUpdate(d => d.LastName, new Director
            {
                FirstName = "James",
                LastName = "Cameron",
            });

            /*List<Movie> defaultMovies = new List<Movie>();

            defaultMovies.Add(new Movie
            {
                Title = "Star Wars: Episode IV - A New Hope",
                Description = "The Imperial Forces -- under orders from cruel Darth Vader (David Prowse) -- hold Princess Leia (Carrie Fisher) hostage, in their efforts to quell the rebellion against the Galactic Empire.",
                Cost = (decimal)5.99,
                Rating = 1,

            })*/


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
