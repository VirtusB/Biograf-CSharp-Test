namespace BiografCSharpTest.Dtos
{
    public class MovieForPopularListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Minutes { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int CountLikedBy { get; set; }
    }
}