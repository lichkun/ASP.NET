namespace RequestProcessingPipeline
{
    public static class FromThousandToTenThousandsExtensions
    {
        public static IApplicationBuilder UseFromThousandToTenThousands(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromThousandToTenThousandsMiddleWare>();
        }
    }
}
