using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OAuthService;
using OAuthService.Options;
using System.Threading.Tasks;

namespace XUnitTestProject
{
    public class OAuthTest1
    {
        private readonly IOptions<GoogleOAuthOptions> _options;

        public OAuthTest1()
        {
            //  ÷∂Øº”‘ÿ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _options = Options.Create(configuration.GetSection("OAuth:Google").Get<GoogleOAuthOptions>()!);
        }
        [Fact]
        public async Task Test1()
        {
            Assert.NotNull(_options);
            Assert.NotNull(_options.Value);
            var service = new GoogleOAuthService(new ApiClientHelper(new HttpClient()), _options);
            var url = service.GetAuthorizationUrl(Guid.NewGuid().ToString());
            Assert.NotNull(url);
            var info = await service.OAuthCallBack(Guid.NewGuid().ToString(), "4/0Ab32j92UJACABQGbD5E4WvIUu4Ue_MBC5p-yx3epM9EHPqlS0OuKzJW6ot17cDfK0_cm5g");

            Assert.NotNull(info);
        }

        [Fact]
        public async Task TestFakeHttp()
        {
            Assert.NotNull(_options);
            Assert.NotNull(_options.Value);
            var fakeHandler = new FakeHttpMessageHandler();
            var httpClient = new HttpClient(fakeHandler);
            var service = new GoogleOAuthService(new ApiClientHelper(httpClient), _options);
            var url = service.GetAuthorizationUrl(Guid.NewGuid().ToString());
            Assert.NotNull(url);
            var info = await service.OAuthCallBack("state123", "fake-code");

            Assert.NotNull(info);
        }
    }
}