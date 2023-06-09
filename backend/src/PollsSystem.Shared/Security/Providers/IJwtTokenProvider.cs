﻿using PollsSystem.Shared.Abstractions.Time;
using PollsSystem.Shared.Security.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PollsSystem.Shared.Security.Providers;

public interface IJwtTokenProvider
{
    JsonWebToken CreateToken(string userGid, string role, string firstName, string lastName, IDictionary<string, string>? claims);
}

public sealed class JwtTokenProvider : IJwtTokenProvider
{
    private readonly IUtcClock _clock;
    private readonly JwtOptions _jwtOptions;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public JwtTokenProvider(
        IUtcClock clock,
        JwtOptions jwtOptions,
        IJwtTokenGenerator tokenGenerator)
    {
        _clock = clock;
        _jwtOptions = jwtOptions;
        _tokenGenerator = tokenGenerator;
    }

    public JsonWebToken CreateToken(string userGid, string role, string firstName, string lastName, IDictionary<string, string>? claims)
    {
        var now = _clock.GetCurrentUtc();

        var jwtClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userGid),
            new Claim(JwtRegisteredClaimNames.Sub, userGid),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString())
        };

        if (!string.IsNullOrWhiteSpace(role))
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));

        if (!string.IsNullOrWhiteSpace(firstName))
            jwtClaims.Add(new Claim(ClaimTypes.Name, firstName));

        if (!string.IsNullOrWhiteSpace(lastName))
            jwtClaims.Add(new Claim(ClaimTypes.Surname, lastName));

        var expires = now.AddMinutes(_jwtOptions.ExpiryMinutes);

        var jwt = _tokenGenerator.GenerateToken(
            _jwtOptions.SecretKey,
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            expires,
            jwtClaims);

        return new JsonWebToken(
            jwt,
            expires.ToTimestamp(),
            userGid,
            role ?? string.Empty,
            jwtClaims);
    }
}