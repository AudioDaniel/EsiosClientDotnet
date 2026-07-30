using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace EsiosClient.Tests;

[TestClass]
public class EsiosArchiveClientTests
{
    private readonly Uri _baseAddress = new(TestConstants.BaseUrl);

    [TestMethod]
    public async Task GetArchivesAsync_ReturnsTypedResponse()
    {
        // Arrange
        var jsonResponse = @"{
            ""archives"": [
                {
                    ""id"": 159,
                    ""name"": ""Resultado_Subasta_Mensual_POR"",
                    ""horizon"": ""M"",
                    ""archive_type"": ""xls"",
                    ""date_times"": [ ""2026-07-01"", ""2026-07-31"" ]
                }
            ]
        }";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.ArchivesUrl)
                .Respond("application/json", jsonResponse);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };
        httpClient.DefaultRequestHeaders.Add("x-api-key", TestConstants.Token);

        var client = new EsiosClient(httpClient);

        // Act
        var result = await client.Archives.GetArchivesAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Archives.Count);
        Assert.AreEqual(159, result.Archives[0].Id);
        Assert.AreEqual("Resultado_Subasta_Mensual_POR", result.Archives[0].Name);
        Assert.AreEqual("M", result.Archives[0].Horizon);
        Assert.AreEqual(new DateTime(2026, 7, 1), result.Archives[0].DateTimes![0]);
    }

    [TestMethod]
    public async Task GetArchivesRawAsync_ReturnsRawString()
    {
        // Arrange
        var jsonResponse = "{ \"test\": \"raw\" }";

        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(TestConstants.ArchivesUrl)
                .Respond("application/json", jsonResponse);

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = _baseAddress
        };

        var client = new EsiosClient(httpClient);

        // Act
        var result = await client.Archives.GetArchivesRawAsync();

        // Assert
        Assert.AreEqual(jsonResponse, result);
    }
}
