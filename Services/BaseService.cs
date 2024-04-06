namespace DevsTutorialCenterMVC.Services
{
    public class BaseService : IDisposable
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public BaseService(HttpClient client, IConfiguration config)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _baseUrl = config.GetValue<string>("ApiUrls:BaseUrl") ?? throw new ArgumentNullException("API base URL is not configured.");
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<TResult?> MakeRequest<TResult, TData>(string address, string methodType, TData data, string? token = null)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentNullException(nameof(address));

            var fullAddress = $"{_baseUrl}{address}";
            Console.WriteLine($"Making {methodType} request to: {fullAddress}");

            var apiResult = new HttpResponseMessage();

            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            switch (methodType)
            {
                case "POST":
                    apiResult = await _client.PostAsJsonAsync(fullAddress, data);
                    break;

                case "PUT":
                    apiResult = await _client.PutAsJsonAsync(fullAddress, data);
                    break;

                case "DELETE":
                    apiResult = await _client.DeleteAsync(fullAddress);
                    break;

                default:
                    apiResult = await _client.GetAsync(fullAddress);
                    break;
            }

            if (apiResult.IsSuccessStatusCode)
            {
                var result = await apiResult.Content.ReadFromJsonAsync<TResult>();
                return result;
            }
            else
            {
                // Handle error responses
                throw new HttpRequestException($"Failed to make {methodType} request to {fullAddress}. Status code: {(int)apiResult.StatusCode}.");
            }
        }

    }
}