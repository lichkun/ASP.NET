namespace RequestProcessingPipeline
{
    public class FromHundredToThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromHundredToThousandMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int number;
            if (int.TryParse(context.Request.Query["number"], out number) && number >= 100 && number <= 999)
            {
                string[] numbers = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                int hundredsDigit = number / 100;
                int remainder = number % 100;

                await context.Response.WriteAsync($"Your number is {numbers[hundredsDigit]} hundred");

                if (remainder > 0)
                {
                    await context.Response.WriteAsync(" and ");
                    await FromTwentyToNinetyNineMiddleware.Invoke(context, remainder);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
