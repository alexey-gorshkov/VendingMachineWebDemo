using Microsoft.AspNetCore.Builder;
using VendingMachine.DAL.Data;

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
