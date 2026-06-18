using Unimake.Primitives.Security.Credentials;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Security.OAuth;

public class RefreshTokenCredentialsTests
{
    #region Public Methods

    [Fact]
    public void ImplicitConversion_FromNullString_ShouldSetEmptyString()
    {
        RefreshTokenCredentials credentials = (string)null;

        Assert.Equal(string.Empty, credentials.RefreshToken);
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldSetRefreshToken()
    {
        RefreshTokenCredentials credentials = "refresh-token-123";

        Assert.Equal("refresh-token-123", credentials.RefreshToken);
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldReturnRefreshToken()
    {
        var credentials = new RefreshTokenCredentials
        {
            RefreshToken = "refresh-token-xyz"
        };

        string value = credentials;

        Assert.Equal("refresh-token-xyz", value);
    }

    [Fact]
    public void RefreshToken_ShouldReturnEmptyString_WhenAssignedNull()
    {
        var credentials = new RefreshTokenCredentials
        {
            RefreshToken = null
        };

        Assert.Equal(string.Empty, credentials.RefreshToken);
    }

    [Fact]
    public void RefreshToken_ShouldReturnEmptyString_WhenNotInitialized()
    {
        var credentials = new RefreshTokenCredentials();

        Assert.Equal(string.Empty, credentials.RefreshToken);
    }

    #endregion Public Methods
}