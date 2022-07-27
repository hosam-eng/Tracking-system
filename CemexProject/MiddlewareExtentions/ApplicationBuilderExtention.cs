using CemexProject.SqlTableDependancy;

namespace CemexProject.MiddlewareExtentions
{
    public static class ApplicationBuilderExtention
    {
        public static void UseSqlTableDependency(this IApplicationBuilder applicationBuilder)
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<SqlTruckDependancy>();
            service.SqlTableDependency();
        }
    }
}
