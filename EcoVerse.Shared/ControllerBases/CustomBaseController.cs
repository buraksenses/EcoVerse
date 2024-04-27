using EcoVerse.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        protected static IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}

