using ReportingSystem.API.DTO.Request.Common;

namespace ReportingSystem.API.DTO.Request
{
    public class SearchPagingRequest:PagingRequest
    {
        public string SearchTerm { get; set; }
    }
}
