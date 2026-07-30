using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace EsiosClient.Tests;

[TestClass]
public class EsiosClientBaseTests
{
    private readonly Uri _baseAddress = new(TestConstants.BaseUrl);

    [TestMethod]
    public async Task CheckHealthAsync_ReturnsTrue_OnSuccess()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.esios.ree.es/indicators")
                .Respond(HttpStatusCode.OK);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };

        var esiosClient = new EsiosClient(httpClient);

        // Act
        var isHealthy = await esiosClient.CheckHealthAsync();

        // Assert
        Assert.IsTrue(isHealthy);
    }

    [TestMethod]
    public async Task CheckHealthAsync_ReturnsFalse_OnFailure()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.IndicatorsUrl)
                .Respond(HttpStatusCode.InternalServerError);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };

        var esiosClient = new EsiosClient(httpClient);

        // Act
        var isHealthy = await esiosClient.CheckHealthAsync();

        // Assert
        Assert.IsFalse(isHealthy);
    }

    [TestMethod]
    public async Task VerifyTokenAsync_ReturnsTrue_WhenAuthorized()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.IndicatorsUrl)
                .Respond(HttpStatusCode.OK);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };

        var esiosClient = new EsiosClient(httpClient);

        // Act
        var isValid = await esiosClient.VerifyTokenAsync();

        // Assert
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public async Task VerifyTokenAsync_ReturnsFalse_WhenUnauthorized()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.IndicatorsUrl)
                .Respond(HttpStatusCode.Unauthorized);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };

        var esiosClient = new EsiosClient(httpClient);

        // Act
        var isValid = await esiosClient.VerifyTokenAsync();

        // Assert
        Assert.IsFalse(isValid);
    }
}
