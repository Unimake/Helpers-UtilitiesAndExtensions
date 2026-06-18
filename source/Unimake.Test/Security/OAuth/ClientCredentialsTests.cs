using Unimake.Primitives.Security.Credentials;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Security.OAuth;

public class ClientCredentialsTests
{
    #region Public Methods

    [Fact]
    public void ClientCredentials_ShouldReturnEmptyStrings_WhenNotInitialized()
    {
        var credentials = new ClientCredentials();

        Assert.Equal(string.Empty, credentials.ClientId);
        Assert.Equal(string.Empty, credentials.Secret);
    }

    [Fact]
    public void ClientId_ShouldReturnAssignedValue()
    {
        var credentials = new ClientCredentials
        {
            ClientId = "client-01"
        };

        Assert.Equal("client-01", credentials.ClientId);
    }

    [Fact]
    public void ClientId_ShouldReturnEmptyString_WhenAssignedNull()
    {
        var credentials = new ClientCredentials
        {
            ClientId = null
        };

        Assert.Equal(string.Empty, credentials.ClientId);
    }

    [Fact]
    public void Secret_ShouldReturnAssignedValue()
    {
        var credentials = new ClientCredentials
        {
            Secret = "secret-xyz"
        };

        Assert.Equal("secret-xyz", credentials.Secret);
    }

    [Fact]
    public void Secret_ShouldReturnEmptyString_WhenAssignedNull()
    {
        var credentials = new ClientCredentials
        {
            Secret = null
        };

        Assert.Equal(string.Empty, credentials.Secret);
    }

    #endregion Public Methods
}