namespace IdentityHelper.Abstraction;
public interface ITokenExtractor
{
    Guid GetUserId();
    string GetUserRole();
    string GetEmail();
    string GetUsername();
    List<string> GetPermissions();
    bool IsUserAuthenticated();
}
