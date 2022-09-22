namespace CatchMeUp.API.Extensions;

public static class HttpContextExtensions
{
    public static string? UserId(this HttpContext context)
    {
        return context.User.Claims.FirstOrDefault(x => x.Type == "sid")?.Value;
    }

    public static string? PreferredUserName(this HttpContext context)
    {
        return context.User.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
    }
}