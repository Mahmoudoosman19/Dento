using Common.Application.Abstractions.Messaging;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Designer.Queries.GetDesignerById
{
    public class GetDesignerQuery : IQuery<DesignerDto>
    {
        public Guid Id { get; init; }
    }
}
