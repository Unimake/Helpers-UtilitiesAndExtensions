using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Unimake.Primitives.Security.OAuth;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Security.OAuth;
public class TokenDataTests
{
    #region Private Methods

    private string GenerateJwtToken(string issuer = "issuer", int expireSeconds = 60)
    {
        var keyBytes = new byte[32];
        RandomNumberGenerator.Fill(keyBytes);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, "testuser") },
            expires: DateTime.UtcNow.AddSeconds(expireSeconds),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion Private Methods

    #region Public Methods

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        var tokenData = new TokenData
        {
            TokenType = "Bearer",
            AccessToken = "def456"
        };

        string result = tokenData;

        Assert.Equal("Bearer def456", result);
    }

    [Fact]
    public void IsExpired_ShouldReturnTrue_ForExpiredToken()
    {
        var tokenString = GenerateJwtToken(expireSeconds: -10);
        var tokenData = new TokenData
        {
            TokenType = "Bearer",
            AccessToken = tokenString
        };

        var result = tokenData.IsExpired();

        Assert.True(result);
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_ForValidToken()
    {
        var tokenString = GenerateJwtToken();
        var tokenData = new TokenData
        {
            TokenType = "Bearer",
            AccessToken = tokenString
        };

        var result = tokenData.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var tokenData = new TokenData
        {
            TokenType = "Bearer",
            AccessToken = "abc123"
        };

        var result = tokenData.ToString();

        Assert.Equal("Bearer abc123", result);
    }

    #endregion Public Methods
}