using Microsoft.AspNetCore.Builder;
using VendingMachine.DAL.Data;

namespace VendingMachine.DAL
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureModdlewareDAL(this IApplicationBuilder app)
        {
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
