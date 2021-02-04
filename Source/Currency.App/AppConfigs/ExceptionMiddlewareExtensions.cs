using Microsoft.AspNetCore.Builder;

namespace Currency.App.AppConfigs
{
    public static  class ExceptionMiddlewareExtensions {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
    
}
