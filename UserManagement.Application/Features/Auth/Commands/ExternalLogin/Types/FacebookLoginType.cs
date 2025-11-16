using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Abstractions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Types;

internal class FacebookLoginType : BaseExternalLogin
{
    private readonly CustomUserManager _userManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;

    
    public FacebookLoginType(CustomUserManager userManager, IJwtProvider jwtProvider, IMapper mapper, IHttpClientFactory httpClientFactory) : base(userManager, jwtProvider, mapper)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
    }

    public override LoginProvider LoginProvider { get; set; } = LoginProvider.Facebook;
    public override async Task<ResponseModel<LoginCommandResponse>> Login(ExternalLoginCommand command)
    {
        var facebookData = await GetFacebookUserDataAsync(command.FacebookLoginDto!.AccessToken);

        if (facebookData is null)
            return ResponseModel.Failure<LoginCommandResponse>(Messages.FailedToFetchUserData);

        var user = await _userManager.FindByEmailAsync(facebookData.Email);
        
        if (user == null)
        {
            user = CreateCustomerUser(facebookData.Email, facebookData.Name);

            var result = await _userManager.CreateExternalLoginUser(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description));

                return ResponseModel.Failure<LoginCommandResponse>(errors);
            }
        }

        var token = await _jwtProvider.Generate(user);
        
        return new LoginCommandResponse()
        {
            Token = token,
            User = _mapper.Map<UserDto>(user!)
        };
    }

    private async Task<FacebookLoginResponse?> GetFacebookUserDataAsync(string accessToken)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            // Facebook Graph API endpoint to fetch user data
            var requestUrl = $"https://graph.facebook.com/v20.0/me?fields=id,name,email&access_token={accessToken}";
            
            var response = await httpClient.GetAsync(requestUrl);
            
            response.EnsureSuccessStatusCode();
            
            var responseBody = await response.Content.ReadAsStringAsync();
            
            var responseDto = JsonSerializer.Deserialize<FacebookLoginResponse>(responseBody);
            
            return responseDto;
        }
        catch (HttpRequestException e)
        {
            // Console.WriteLine($"Request exception: {e.Message}");
            // throw;
            return null;
        }
    }
}

class FacebookLoginResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}