using Currency.DB.Models;
using Currency.Domain;
using Currency.Domain.Dtos;
using Currency.Domain.IDtos;
using Currency.Domain.IHttpClients;
using Currency.Domain.IServices;
using Currency.Infrastructure.IRepositories;
using Currency.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Currency.Tests.Infrastructure.Services
{
    public class CurrencyServiceTest
    {
        private Mock<CurrencyService> serviceMock;
        private Mock<ICurrencyRepository> repositoryMock;
        private Mock<IRatesClient> httpClientMock;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<ICurrencyRepository>();
            var appSettings = new Mock<IAppSettingsDto>();
            appSettings.Setup(s => s.EcbEuropaCurrenciesXML).Returns("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            httpClientMock = new Mock<IRatesClient>();
            serviceMock = new Mock<CurrencyService>( repositoryMock.Object, appSettings.Object, httpClientMock.Object);
            serviceMock.CallBase = true;
        }

        [Test]
        public async Task GetRatesNoCurrenciesYetTest()
        {
            repositoryMock.Setup(m => m.GetCurrencyRates()).ReturnsAsync(new List<CurrencyRate>());
            serviceMock.Setup(s => s.SaveEcbEuropaCurrencyRates()).ReturnsAsync(new List<CurrencyRateDto>() { new CurrencyRateDto { Code = "USD", Rate = 0.83m } });
            var rates = await serviceMock.Object.GetRates();
            serviceMock.Verify(m => m.SaveEcbEuropaCurrencyRates(), Times.Once);
            Assert.IsTrue(rates.Count == 1);
        }

        [Test]
        public async Task GetRate()
        {
            repositoryMock.Setup(m => m.GetCurrencyRates()).ReturnsAsync(new List<CurrencyRate>() { new CurrencyRate { Code = "USD", Rate = 0.83m } });
            serviceMock.Setup(s => s.SaveEcbEuropaCurrencyRates());
            var rates = await serviceMock.Object.GetRates();
            serviceMock.Verify(m => m.SaveEcbEuropaCurrencyRates(), Times.Never);
            Assert.IsTrue(rates.Count == 1);
        }

        [Test]
        public async Task SaveEcbEuropaCurrencyRatesTest()
        {
            #region xml string
            string xmlString = @"<gesmes:Envelope xmlns:gesmes=
                    ""http://www.gesmes.org/xml/2002-08-01"" xmlns=""http://www.ecb.int/vocabulary/2002-08-01/eurofxref"">
                        <gesmes:subject > Reference rates </gesmes:subject >
                         <gesmes:Sender >
                          <gesmes:name > European Central Bank</gesmes:name >
                              </gesmes:Sender >
                                <Cube >
                                <Cube time = ""2021-02-02"" >
                                <Cube currency=""USD"" rate = ""1.2044"" />
                                <Cube currency=""JPY"" rate = ""126.46"" />
                                <Cube currency=""BGN"" rate = ""1.9558"" />
                                <Cube currency=""CZK"" rate = ""25.900"" />
                                <Cube currency=""DKK"" rate = ""7.4373"" />
                                <Cube currency=""GBP"" rate = ""0.88075"" />
                                <Cube currency=""HUF"" rate = ""355.70"" />
                                <Cube currency=""PLN"" rate = ""4.4953"" />
                                <Cube currency=""RON"" rate = ""4.8743"" />
                                <Cube currency=""SEK"" rate = ""10.1593"" />
                                <Cube currency=""CHF"" rate = ""1.0808""/>
                                <Cube currency=""ISK"" rate = ""156.10"" />
                                <Cube currency=""NOK"" rate = ""10.3463"" />
                                <Cube currency=""HRK"" rate = ""7.5810"" />
                                <Cube currency=""RUB"" rate = ""90.9307"" />
                                <Cube currency=""TRY"" rate = ""8.6629"" />
                                <Cube currency=""AUD"" rate = ""1.5847"" />
                                <Cube currency=""BRL"" rate = ""6.4904"" />
                                <Cube currency=""CAD"" rate = ""1.5422"" />
                                <Cube currency=""CNY"" rate = ""7.7756"" />
                                <Cube currency=""HKD"" rate = ""9.3361"" />
                                <Cube currency=""IDR"" rate = ""16878.40"" />
                                <Cube currency=""ILS"" rate = ""3.9727"" />
                                <Cube currency=""INR"" rate = ""87.9060"" />
                                <Cube currency=""KRW"" rate = ""1344.07"" />
                                <Cube currency=""MXN"" rate = ""24.3492"" />
                                <Cube currency=""MYR"" rate = ""4.8712"" />
                                <Cube currency=""NZD"" rate = ""1.6793"" />
                                <Cube currency=""PHP"" rate = ""57.708"" />
                                <Cube currency=""SGD"" rate = ""1.6041"" />
                                <Cube currency=""THB"" rate = ""36.120"" />
                                <Cube currency=""ZAR"" rate = ""18.0067"" />
                            </Cube>
                        </Cube>
                    </gesmes:Envelope> ";
            #endregion

            httpClientMock.Setup(m => m.GetEcbEuropaCurrenciesXML()).Returns(XDocument.Parse(xmlString));
            var rates = await serviceMock.Object.SaveEcbEuropaCurrencyRates();
            httpClientMock.Verify(m => m.GetEcbEuropaCurrenciesXML(), Times.Once);
            Assert.IsTrue(rates.Count == 32);
        }

        
    }
}
