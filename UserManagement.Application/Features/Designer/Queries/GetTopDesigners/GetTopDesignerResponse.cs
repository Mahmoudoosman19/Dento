using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Designer.Queries.GetTopDesigners
{
    public class GetTopDesignerResponse
    {
        public int TotalDesignersCount { get; set; }
        public List<VendorCountDto> TopDesigners { get; set; }
    }
}
