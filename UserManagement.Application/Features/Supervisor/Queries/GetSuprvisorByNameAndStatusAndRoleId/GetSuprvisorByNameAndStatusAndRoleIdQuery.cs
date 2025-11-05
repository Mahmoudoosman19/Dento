using Common.Application.Abstractions.Messaging;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Supervisor.Queries.GetSupervisorByNameAndStatusAndRoleId
{
    public class GetSupervisorByNameAndStatusAndRoleIdQuery : IQuery<IReadOnlyList<UserDto>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public string? Name { get; set; }
        public UserStatus? Status { get; set; }
    }
}
