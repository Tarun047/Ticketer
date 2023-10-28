namespace Ticketer.WebApp.Extensions;

public static class WebHostEnvironmentExtensions
{
    const string DockerDevelopmentEnvironment = "DockerDev";

    public static bool IsDockerDevelopmentEnvironment(this IWebHostEnvironment environment)
    {
        return environment.IsEnvironment(DockerDevelopmentEnvironment);
    }
}