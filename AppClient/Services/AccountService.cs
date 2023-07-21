using MyAppClient.Helpers;
using Shared;
using Shared.DataTransferModels.User;

namespace MyAppClient.Services
{
    public class AccountService
    {
        private readonly ApiCallService _apiCallService;

        public AccountService(ApiCallService apiCallService)
        {
            _apiCallService = apiCallService;
        }


        public async Task<ServerResponse<object>> SendVerificationCodeAsync(UserEmail userEmail) =>
            await _apiCallService.ApiPostAsync(userEmail, "account/send-verification-code", true);

        public async Task<ServerResponse<string>> SignInAsync(UserEmailCode userEmailCode) =>
            await _apiCallService.ApiPostAsync<string, UserEmailCode>(userEmailCode, "account/signin", true);


    }
}
