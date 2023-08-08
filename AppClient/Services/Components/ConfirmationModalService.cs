
namespace MyAppClient.Services.Components
{
    public class ConfirmationModalService
    {
        private ConfirmationModal? _confirmationModal;
        public ConfirmationModal? ConfirmationModal { get
            {
                return _confirmationModal;
            }
            set
            {
                if (_confirmationModal is null)
                    _confirmationModal = value;
            }
        }

        private TaskCompletionSource<bool>? _completionSource;

        public async Task<bool> Show(string title, string description)
        {
            _completionSource = new TaskCompletionSource<bool>();

            _confirmationModal?.SetInfo(title, description);
            _confirmationModal?.ShowModal(true);

            return await _completionSource.Task;
        }

        public void OnButtonClick(bool status)
        {
            _completionSource?.SetResult(status);
            _confirmationModal?.ShowModal(false);
        }

    }
}
