using Unimake.Primitives.Security.Credentials;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Security.OAuth;

public class UserCredentialsTests
{
    #region Public Methods

    [Fact]
    public void Password_ShouldReturnAssignedValue()
    {
        var credentials = new UserCredentials
        {
            Password = "secret-123"
        };

        Assert.Equal("secret-123", credentials.Password);
    }

    [Fact]
    public void Password_ShouldReturnEmptyString_WhenAssignedNull()
    {
        var credentials = new UserCredentials
        {
            Password = null
        };

        Assert.Equal(string.Empty, credentials.Password);
    }

    [Fact]
    public void UserCredentials_ShouldReturnEmptyStrings_WhenNotInitialized()
    {
        var credentials = new UserCredentials();

        Assert.Equal(string.Empty, credentials.Username);
        Assert.Equal(string.Empty, credentials.Password);
    }

    [Fact]
    public void Username_ShouldReturnAssignedValue()
    {
        var credentials = new UserCredentials
        {
            Username = "user@test.com"
        };

        Assert.Equal("user@test.com", credentials.Username);
    }

    [Fact]
    public void Username_ShouldReturnEmptyString_WhenAssignedNull()
    {
        var credentials = new UserCredentials
        {
            Username = null
        };

        Assert.Equal(string.Empty, credentials.Username);
    }

    #endregion Public Methods
}