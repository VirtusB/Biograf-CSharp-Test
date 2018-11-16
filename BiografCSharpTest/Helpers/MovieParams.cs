namespace BiografCSharpTest.Helpers
{
    public class MovieParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1; // default to 1
        private int pageSize = 10; // default 10
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public int MovieId { get; set; }
        public string Genre { get; set; }
        public int MinYear { get; set; } = 1920; // default year of 1920
        public int MaxYear { get; set; } = 2018;

    }
}