using Microsoft.AspNetCore.Components;
using Shared;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace MyAppClient.Helpers
{
    public class ApiCallService
    {
        private readonly HttpClient _httpClient;
        private readonly NotificationService _notificationService;
        private readonly NavigationManager _navigationManager;

        public ApiCallService(HttpClient httpClient, NotificationService notificationService, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
            _navigationManager = navigationManager;
        }

        public async Task<ServerResponse<T>> ApiGetAsync<T>(string url, bool notification = false)
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(url);

                var jsonBodyResponse = await httpResponse.Content.ReadAsStringAsync();

                ServerResponse<T> serviceResponse = JsonConvert.DeserializeObject<ServerResponse<T>>(jsonBodyResponse);

                if(notification)
                    _notificationService.Show(serviceResponse.HttpStatusCode, serviceResponse.Message);

                return serviceResponse;
                
            }
            catch{

                return await TestConnectionAsync<T>();
                
            }
        }


        public async Task<ServerResponse<T>> ApiPostAsync<T, E>(E data, string url, bool notification = false)
        {
            try
            {

                var jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync(url, content);

                var jsonBodyResponse = await httpResponse.Content.ReadAsStringAsync();

                ServerResponse<T> serviceResponse = JsonConvert.DeserializeObject<ServerResponse<T>>(jsonBodyResponse);

                if (notification)
                    _notificationService.Show(serviceResponse.HttpStatusCode, serviceResponse.Message);

                return serviceResponse;
            }
            catch
            {

                return await TestConnectionAsync<T>();

            }
        }

        public async Task<ServerResponse<object>> ApiPostAsync<T>(T data, string url, bool notification = false)
        {
            try
            {

                var jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync(url, content);

                var jsonBodyResponse = await httpResponse.Content.ReadAsStringAsync();

                ServerResponse<object> serviceResponse = JsonConvert.DeserializeObject<ServerResponse<object>>(jsonBodyResponse);

                if (notification)
                    _notificationService.Show(serviceResponse.HttpStatusCode, serviceResponse.Message);

                return serviceResponse;
            }
            catch
            {

                return await TestConnectionAsync<object>();

            }
        }

  
        public async Task<ServerResponse<object>> ApiPutAsync<T>(T data, string url, bool notification = false)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var Httpresponse = await _httpClient.PutAsync(url, content);

                var jsonBodyResponse = await Httpresponse.Content.ReadAsStringAsync();

                ServerResponse<object> serverResponse = JsonConvert.DeserializeObject<ServerResponse<object>>(jsonBodyResponse);

                if (notification)
                    _notificationService.Show(serverResponse.HttpStatusCode, serverResponse.Message);

                return serverResponse;
            }
            catch
            {

                return await TestConnectionAsync();

            }
        }

        public async Task<ServerResponse<object>> ApiDeleteAsync(string url, bool notification = false)
        {
            try
            {
                var httpResponse = await _httpClient.DeleteAsync(url);

                //if (!response.IsSuccessStatusCode)
                //    return FailResponse(response);

                var jsonBody = await httpResponse.Content.ReadAsStringAsync();

                ServerResponse<object> serverResponse = JsonConvert.DeserializeObject<ServerResponse<object>>(jsonBody);

                if (notification)
                    _notificationService.Show(serverResponse.HttpStatusCode, serverResponse.Message);

                return serverResponse;
            }
            catch
            {

                return await TestConnectionAsync();

            }
        }

        async Task<ServerResponse<object>> TestConnectionAsync()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync("test");

                if (!httpResponse.IsSuccessStatusCode)
                    return new ServerResponse<object>(HttpStatusCode.InternalServerError, "Server is currently not available");

                var jsonBodyResponse = await httpResponse.Content.ReadAsStringAsync();

                ServerResponse<object> serverResponse = JsonConvert.DeserializeObject<ServerResponse<object>>(jsonBodyResponse);

                _notificationService.Show(serverResponse.HttpStatusCode, serverResponse.Message);

                return serverResponse;
            }
            catch
            {
                _navigationManager.NavigateTo("/service-unavailable");
                return new ServerResponse<object>(HttpStatusCode.ServiceUnavailable, "Server Error", new { });
            }
        }

        async Task<ServerResponse<T>> TestConnectionAsync<T>()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync("test");

                if (!httpResponse.IsSuccessStatusCode)
                    return new ServerResponse<T>(HttpStatusCode.InternalServerError, "Server is currently not available");

                var jsonBodyResponse = await httpResponse.Content.ReadAsStringAsync();

                ServerResponse<T> serverResponse = JsonConvert.DeserializeObject<ServerResponse<T>>(jsonBodyResponse);

                _notificationService.Show(serverResponse.HttpStatusCode, serverResponse.Message);

                return serverResponse;
            }
            catch
            {
                _navigationManager.NavigateTo("/service-unavailable");
                return new ServerResponse<T>(HttpStatusCode.ServiceUnavailable, "Server Error", default);
            }
        }

    }
}
