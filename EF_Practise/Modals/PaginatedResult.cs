
namespace EF_Practise.Modals
{
    [Obsolete]
    public class PaginatedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
        public int Total { get; set; }



    }
}
