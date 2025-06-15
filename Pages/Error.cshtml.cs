using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DRDOAssignment.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    public string? ErrorMessage { get; set; }

    public string? StackTrace { get; set; }

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        
        if (IsDevelopment)
        {
            var exception = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            if (exception != null)
            {
                ErrorMessage = exception.Error.Message;
                StackTrace = exception.Error.StackTrace;
            }
        }
    }
}

