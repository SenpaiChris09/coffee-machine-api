namespace coffee_machine_api.Services
{
    public class WeatherService
    {
        public WeatherService(HttpClient httpClient)
        {
        }

        public async Task<double> GetCurrentTemperatureAsync()
        {
            // Mocked temperature for demo / test purposes
            return 32.0;
        }
    }
}
