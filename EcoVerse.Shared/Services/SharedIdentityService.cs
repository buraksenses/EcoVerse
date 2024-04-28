using Microsoft.AspNetCore.Http;

namespace EcoVerse.Shared.Services;

public class SharedIdentityService : ISharedIdentityService
{
    private IHttpContextAccessor _httpContextAccessor;

    public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId
    {
        get
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available. This property should only be accessed within the context of an HTTP request.");
            }
            var userClaim = _httpContextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (userClaim == null)
            {
                throw new InvalidOperationException("User claim 'sub' is not found. Ensure the user is authenticated and the claim is present.");
            }
            return userClaim.Value;
        }
    }
}