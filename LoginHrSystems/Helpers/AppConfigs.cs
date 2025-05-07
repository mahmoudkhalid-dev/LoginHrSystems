namespace LoginHrSystems.Helpers
{
    public static class AppConfigs
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            // app.UseMiddleware<CustomExceptionMiddleware>();
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.InjectStylesheet("/SwaggerCss3.x/theme-material.css");
            });

        }
    }
}
