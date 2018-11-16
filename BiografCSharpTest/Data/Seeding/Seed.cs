using System.Collections.Generic;
using System.Linq;
using BiografCSharpTest.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data.Seeding
{
    public class Seed
    {
        private readonly BioContext _context;

        public Seed(BioContext context) {
            this._context = context;
        }

        public void SeedDiscounts() {
            if (!_context.Discounts.Any()) {
                var discounts = new List<Discount>();

                int standardAmount = 5;
                for (int i = 0; i <= 50; i = i + 10) {
                    var stepDiscount = new Discount {
                        RequiredBookings = i,
                        Amount = standardAmount
                    };
                    discounts.Add(stepDiscount);
                    standardAmount++;
                }

                foreach (var discount in discounts)
                {
                    _context.Discounts.Add(discount);
                }

                _context.SaveChanges();
            }
        }

        public void SeedRoles() {
            if (!_context.Roles.Any()) {
                var roles = new List<Role>
                {
                    new Role{Name = "Standard"},
                    new Role{Name = "Personale"},
                    new Role{Name = "Admin"}
                };

                foreach (var role in roles)
                {
                    _context.Roles.Add(role);
                }

                _context.SaveChanges();
            }
        }

        public void SeedMovies() {
            if (!_context.Movies.Any()) {
                var movieData = System.IO.File.ReadAllText("Data/Seeding/MovieSeedData.json");
                var movies = JsonConvert.DeserializeObject<List<Movie>>(movieData);

                foreach (var movie in movies)
                {
                    string cutToOnlyOneGenre = movie.Genre.Split("|")[0];
                    movie.Genre = cutToOnlyOneGenre;
                    _context.Movies.Add(movie);
                }

                _context.SaveChanges();
            }
        }

        public void SeedUsers() {
            if (!_context.Users.Any()) {
                var userData = System.IO.File.ReadAllText("Data/Seeding/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                Role standardRole = _context.Roles.FirstOrDefault(r => r.Id == 1);
                Role personaleRole = _context.Roles.FirstOrDefault(r => r.Id == 2);
                Role adminRole = _context.Roles.FirstOrDefault(r => r.Id == 3);

                foreach (var user in users)
                {
                    if (user.Username == "Admin") {
                        user.Role = adminRole;
                    } else if (user.Username == "Personale") {
                        user.Role = personaleRole;
                    } else {
                        user.Role = standardRole;
                    }
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    _context.Users.Add(user);
                }

                _context.SaveChanges();
            }

            // Udkommenter for at fjerne alle brugere fra databasen
            //_context.Users.ToList().ForEach(u => {_context.Users.Remove(u); _context.SaveChanges();});
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}