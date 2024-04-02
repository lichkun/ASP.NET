using RequestProcessingPipeline;

namespace Number_Interpreter
{
    public static class FromThousandToTenThousandMiddlewareExtensions
    {
        public static IApplicationBuilder UseFromThousandToTenThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromThousandToTenThousandMiddleware>();
        }
    }
}
