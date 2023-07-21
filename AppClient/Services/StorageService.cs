﻿using Blazored.LocalStorage;

namespace MyAppClient.Services
{
    public class StorageService
    {
        private readonly ILocalStorageService _storage;

        public StorageService(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task SaveUserAsync(string jwt)
        {
              await _storage.SetItemAsStringAsync("Auth", jwt);
        }

        public async Task<string> GetUserAsync()
        {
            return await _storage.GetItemAsStringAsync("Auth");
        }

    }
}
