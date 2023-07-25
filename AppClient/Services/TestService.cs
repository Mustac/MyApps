namespace MyAppClient.Services
{
    public class TestService
    {
        private readonly ApiCallService _apiCallService;

        public TestService(ApiCallService apiCallService)
        {
            _apiCallService = apiCallService;
        }

        public async Task<ServerResponse<object>> TestAsync() =>
            await _apiCallService.ApiGetAsync<object>("test", true);
    }
}
