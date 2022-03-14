using VendingMachine.DAL.Data;
using Microsoft.AspNetCore.Builder;

namespace VendingMachine.DAL
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddlewareDAL(this IApplicationBuilder app)
        {
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
