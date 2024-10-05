namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductSpecification
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Sort { get; set; }
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value> MAXPAGESIZE)? MAXPAGESIZE :value;
        }
        private string? _Search ;

        public string? Search
        {
            get => _Search;
            set => _Search = value?.Trim().ToLower();
        }

    }
}
