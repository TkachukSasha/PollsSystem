using System.Security.Claims;

namespace PollsSystem.Shared.Security;

public sealed class JsonWebToken
{
    public JsonWebToken(
        string acessToken,
        long expires,
        string userGid,
        string role,
        IEnumerable<Claim>? claims)
    {
        AccessToken = acessToken;
        Expires = expires;
        UserGid = userGid;
        Role = role;
        Claims = claims;
    }

    public string AccessToken { get; private set; } = default!;

    public long Expires { get; private set; }

    public string UserGid { get; private set; } = default!;

    public string Role { get; private set; } = default!;

    public IEnumerable<Claim>? Claims { get; private set; }
}