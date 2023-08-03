using MyAppClient.Shared.Components.Menu;

namespace MyAppClient.Services
{
    public class SidebarService
    {
        public event Action<bool>? OnNavbarChange;

        public void SideBarChanged(bool state)
        {
            if (OnNavbarChange is null)
                return;

            OnNavbarChange.Invoke(state);
        }
    }
}
