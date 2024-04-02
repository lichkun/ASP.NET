using RequestProcessingPipeline;

namespace Number_Interpreter
{
    public static class FromTenThousandToHundredThousandMiddlewareExtensions
    {
        public static IApplicationBuilder UseFromTenThousandToHundredThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTenThousandToHundredThousandMiddleware>();
        }
    }
}
