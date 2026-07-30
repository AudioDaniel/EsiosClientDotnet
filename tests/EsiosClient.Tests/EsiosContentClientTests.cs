using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace EsiosClient.Tests;

[TestClass]
public class EsiosContentClientTests
{
    private readonly Uri _baseAddress = new(TestConstants.BaseUrl);

    [TestMethod]
    public async Task GetGlossariesAsync_ReturnsTypedResponse()
    {
        // Arrange
        var jsonResponse = @"{
            ""contents"": [
                {
                    ""id"": 886,
                    ""title"": ""Cable""
                }
            ]
        }";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.GlossariesUrl)
                .WithHeaders("x-api-key", TestConstants.Token)
                .Respond("application/json", jsonResponse);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };
        httpClient.DefaultRequestHeaders.Add("x-api-key", TestConstants.Token);

        var client = new EsiosClient(httpClient);

        // Act
        var result = await client.Content.GetGlossariesAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Contents.Count);
        Assert.AreEqual(886, result.Contents[0].Id);
        Assert.AreEqual("Cable", result.Contents[0].Title);
    }
}
