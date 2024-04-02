namespace Number_Interpreter
{
    public class FromThousandToTenThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromThousandToTenThousandMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int number;
            if (int.TryParse(context.Request.Query["number"], out number) && number >= 1000 && number <= 9999)
            {
                string[] numbers = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                int thousandsDigit = number / 1000;
                int remainder = number % 1000;

                await context.Response.WriteAsync($"Your number is {numbers[thousandsDigit]} thousand");

                if (remainder > 0)
                {
                    await context.Response.WriteAsync(" ");
                   // await FromHundredToNineHundredNinetyNineMiddleware.Invoke(context, remainder);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
