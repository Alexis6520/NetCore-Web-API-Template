namespace WebAPI.Extensions
{
    public static class WebHostEnvironmentExtension
    {
        public static bool IsTesting(this IWebHostEnvironment environment)
        {
            return environment.EnvironmentName
                .Equals("Testing", StringComparison.OrdinalIgnoreCase);
        }
    }
}
