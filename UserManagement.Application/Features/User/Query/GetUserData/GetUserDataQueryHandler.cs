using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.Identity;
using UserManagement.Domain.Entites;
using UserManagement.Domain.Entities;


namespace UserManagement.Application.Features.User.Queries.GetUserData
{
    internal class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, GetUserDataQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly CustomUserManager _userManager;
        private readonly ITokenExtractor _tokenExtractor;

        public GetUserDataQueryHandler(IMapper mapper, CustomUserManager userManager, ITokenExtractor tokenExtractor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel<GetUserDataQueryResponse>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User();
            if (request.Id != Guid.Empty)
                user = await _userManager.GetUserWithRoleAsync(request.Id);
            else
            {
                var userId = _tokenExtractor.GetUserId();
                user = await _userManager.GetUserWithRoleAsync(userId);
            }

            if (user == null)
                return ResponseModel.Failure<GetUserDataQueryResponse>("Customer Not Found");

            var response = _mapper.Map<GetUserDataQueryResponse>(user!);

            return ResponseModel.Success(response);
        }
    }
}
