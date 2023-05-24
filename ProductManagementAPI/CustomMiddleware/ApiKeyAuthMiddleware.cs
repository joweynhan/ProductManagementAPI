namespace ProductManagementAPI.CustomMiddleware
{
    //for authenticating API requests based on an API key
    public class ApiKeyAuthMiddleware //program.cs
    {
        private readonly RequestDelegate _next;
        private const string ApiKey = "ApiKey";

        // request object
        public ApiKeyAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // context container request response pipeline objects 
        //main entry point for the middleware
        public async Task Invoke(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(ApiKey, out var extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key not given in the request!");
                return;
            }

            // validate the API Key
            // compares the extracted API key with the actual API key value
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var key = appSettings.GetValue<string>(ApiKey);
            if (!key.Equals(extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key is not valid/unauthorized!");
                return;
            }

            await _next(context); // allows the request to proceed to the next component
        }
    }
}
