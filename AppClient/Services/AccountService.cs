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
            await _apiCallService.ApiPostAsync(userEmail, "account/signin/send-token", true);

        public async Task<ServerResponse<UserSigninResponse>> SignInAsync(UserEmailCode userEmailCode) =>
            await _apiCallService.ApiPostAsync<UserSigninResponse, UserEmailCode>(userEmailCode, "account/signin/verify-token", true);

        public async Task<ServerResponse<object>> UpdateInfoAsync(UserUpdate userRegistration) =>
            await _apiCallService.ApiPutAsync(userRegistration, "account/update", true);
    }
}
