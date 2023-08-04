using Blazored.Toast.Services;
using Shared;

namespace MyAppClient.Services
{
    public class NotificationService
    {
        private readonly IToastService _toastService;

        public NotificationService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public void Show(HttpStatusCode statusCode, string message)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    _toastService.ShowInfo(message);
                    break;

                case HttpStatusCode.Unauthorized:
                    _toastService.ShowWarning(message);
                    break;

                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.Conflict:
                    _toastService.ShowError(message);
                    break;
            }
        }
    }
}
