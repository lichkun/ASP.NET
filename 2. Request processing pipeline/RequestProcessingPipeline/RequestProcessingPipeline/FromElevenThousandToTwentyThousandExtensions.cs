namespace RequestProcessingPipeline
{
    public static class FromElevenThousandToTwentyThousandExtensions
    {
        public static IApplicationBuilder FromElevenThousandToTwentyThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromElevenThousandToTwentyThousandMiddleware>();
        }
    }
}
