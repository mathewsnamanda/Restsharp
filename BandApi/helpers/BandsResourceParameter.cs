namespace BandApi.helpers{
    public class BandsResourceParameter{
        public string   mainGenre { get; set; } 
        public string searchquery { get; set; } 

        const int maxPageSize=10;
        public int PageNumber { get; set; } = 1;
        private int _pageSize=10;

        public int PageSize{
            get=>_pageSize;
             set => _pageSize=(value>maxPageSize)?maxPageSize:value;
        }
        public string OrderBy {get;set; } = "Name";
        public string Fields { get; set; }
    }
}