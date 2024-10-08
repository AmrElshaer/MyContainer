using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MyContainer.Controllers;

public class HomeController : Controller
{
    public IActionResult SetLanguage(string culture)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return RedirectToPage("/Pages/Index");
    }
}