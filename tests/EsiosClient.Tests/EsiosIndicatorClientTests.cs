using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace EsiosClient.Tests;

[TestClass]
public class EsiosIndicatorClientTests
{
    private readonly Uri _baseAddress = new(TestConstants.BaseUrl);

    [TestMethod]
    public async Task GetIndicatorsAsync_ReturnsTypedResponse()
    {
        // Arrange
        var jsonResponse = @"{
            ""indicators"": [
                {
                    ""id"": 1001,
                    ""name"": ""Demanda"",
                    ""short_name"": ""Demanda Real"",
                    ""description"": ""Test description""
                }
            ]
        }";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.IndicatorsUrl)
                .WithHeaders("x-api-key", TestConstants.Token)
                .Respond("application/json", jsonResponse);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };
        httpClient.DefaultRequestHeaders.Add("x-api-key", TestConstants.Token);

        var client = new EsiosClient(httpClient);

        // Act
        var result = await client.Indicators.GetIndicatorsAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Indicators.Count);
        Assert.AreEqual(1001, result.Indicators[0].Id);
        Assert.AreEqual("Demanda", result.Indicators[0].Name);
        Assert.AreEqual("Demanda Real", result.Indicators[0].ShortName);
    }
}
