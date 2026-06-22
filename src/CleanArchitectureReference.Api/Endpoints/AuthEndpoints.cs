using CleanArchitectureReference.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureReference.Api.Endpoints;

public static class AuthEndpoints
{
    public record RegisterRequest(string UserName, string Password);

    public record LoginRequest(string UserName, string Password);

    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Auth");

        group.MapPost("/register", async (
            RegisterRequest request, UserManager<User> userManager) =>
        {
            var user = new User { UserName = request.UserName };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return Results.ValidationProblem(result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray()));
            }

            return Results.Ok();
        })
        .WithName("Register");

        group.MapPost("/login", async (
            LoginRequest request, SignInManager<User> signInManager) =>
        {
            signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(
                request.UserName, request.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return Results.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            // On success the bearer token handler writes the AccessTokenResponse to the body.
            return Results.Empty;
        })
        .WithName("Login");

        return app;
    }
}
