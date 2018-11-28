namespace BiografCSharpTest.Helpers
{
    public class ShowParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1; // default to 1
        private int pageSize = 10; // default 10
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public int ShowId { get; set; }
        public int Stars { get; set; } = 0;
        public float MaxTicketPrice { get; set; } = 500; // maks 500 som standard sortering
        public int HallNumber { get; set; } = 0;
        public int MovieId { get; set; } = 0;
    }
}