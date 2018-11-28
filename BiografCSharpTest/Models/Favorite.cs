namespace BiografCSharpTest.Models
{
    public class Favorite
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public User Liker { get; set; }
        public Movie Likee { get; set; }
    }
}