using Common.Application.Abstractions.Messaging;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Designer.Queries.GetListDesigners
{
    public class GetListDesignersQuery : IQuery<IReadOnlyList<DesignerDto>>
    {
        public string? Name { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public UserStatus? Status { get; set; }
    }
}
