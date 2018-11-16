using System.Collections.Generic;
using System.Linq;
using BiografCSharpTest.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System;

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

        public void CleanDatabase() {
            var discounts = _context.Discounts.ToList();
            var reservations = _context.Reservations.ToList();
            var shows = _context.Shows.ToList();
            var movies = _context.Movies.ToList();
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();

            discounts.ForEach(discount => {
                _context.Discounts.Remove(discount);
            });
            reservations.ForEach(reservation => {
                _context.Reservations.Remove(reservation);
            });
            shows.ForEach(show => {
                _context.Shows.Remove(show);
            });
            movies.ForEach(movie => {
                _context.Movies.Remove(movie);
            });
            users.ForEach(user => {
                _context.Users.Remove(user);
            });
            roles.ForEach(role => {
                _context.Roles.Remove(role);
            });
            _context.SaveChanges();
        }

        public int RandomNumber(int min, int max)  
        {  
            Random random = new Random();  
            return random.Next(min, max);  
        }  

        public void SeedReservations() {
            if (!_context.Reservations.Any()) {
                var users = _context.Users.ToList();
                var shows = _context.Shows.ToArray();

                var standard = users.Where(u => u.Username != "Admin" && u.Username != "Personale").ToList();
                var personale = users.Where(u => u.Username == "Personale").FirstOrDefault();
                var admin = users.Where(u => u.Username == "Admin").FirstOrDefault();

                users.ForEach(user => {
                    for (int i = 0; i < 5; i++) {
                        int showIndex = RandomNumber(0, 10);
                        var reservation = new Reservation {
                            Created = DateTime.Now,
                            Row = RandomNumber(1, 6),
                            Seat = RandomNumber(1, 21),
                            BookingState = RandomNumber(0, 3),
                            User = user,
                            Show = shows[showIndex]
                        };
                        _context.Reservations.Add(reservation);
                    }
                });
                _context.SaveChanges();
            }
        }

        public void SeedShows() {
            if (!_context.Shows.Any()) {
                for (int i = 0; i < 10; i++) {
                    var movies = _context.Movies.Take(10).ToArray();

                    DateTime startDate = DateTime.Now.AddDays(i);

                    var show = new Show {
                        StartDate = startDate,
                        EndDate = startDate.AddHours(i),
                        TicketPrice = RandomNumber(80, 210),
                        HallNumber = RandomNumber(1, 5),
                        Movie = movies[i]
                    };

                    _context.Shows.Add(show);
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

                Role standardRole = _context.Roles.FirstOrDefault(r => r.Name == "Standard");
                Role personaleRole = _context.Roles.FirstOrDefault(r => r.Name == "Personale");
                Role adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");

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