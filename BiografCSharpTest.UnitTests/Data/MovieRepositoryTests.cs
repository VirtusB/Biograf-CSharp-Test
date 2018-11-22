using System;
using Xunit;
using BiografCSharpTest.Data;
using System.Collections.Generic;
using System.Linq;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.UnitTests.Data
{
    public class MovieRepositoryTests
    {
        [Fact]
        public async void GetMovies_GenreParamIsAction_ReturnsMoviesWithActionGenre() {
            // TODO: fiks MovieRepository, så logikken bag MovieParams ikke sker i selve repository'en
            // TODO: testen bør ikke bruge repository'en 

            // RAM database som kun eksisterer når forbindelsen er åben
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try {
                var options = new DbContextOptionsBuilder<BioContext>()
                    .UseSqlite(connection)
                    .Options;

                    var context = new BioContext(options);
                    context.Database.EnsureCreated();

                    context.AddRange(this.GetListOfTestMovies());
                    context.SaveChanges();

                    var movieRepo = new MovieRepository(context);
                    var movieParams = new MovieParams { Genre = "Action" };

                    var moviesWithActionGenre = await movieRepo.GetMovies(movieParams);

                    Assert.True(moviesWithActionGenre.All(movie => movie.Genre == "Action"));
                    Assert.True(moviesWithActionGenre.Count == 2);
            } finally {
                connection.Close();
            }
        }

        private List<Movie> GetListOfTestMovies() {
            return new List<Movie> {
                new Movie {
                    Name = "Test movie 1",
                    Genre = "Comedy"
                },
                new Movie {
                    Name = "Test movie 2",
                    Genre = "Action"
                },
                new Movie {
                    Name = "Test movie 3",
                    Genre = "Action"
                },
                new Movie {
                    Name = "Test movie 4",
                    Genre = "Adventure"
                }
            };
        }
    }
}