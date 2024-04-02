namespace Number_Interpreter
{
    public class FromTenThousandToHundredThousandMiddleware
    {
        private readonly RequestDelegate _next;


        public FromTenThousandToHundredThousandMiddleware(RequestDelegate next)

        {

            _next = next;

        }


        public async Task InvokeAsync(HttpContext context)

        {

            int number;

            if (int.TryParse(context.Request.Query["number"], out number) && number >= 10000 && number <= 99999)

            {

                string[] numbers = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };


                int tenThousandsDigit = number / 10000;

                int remainder = number % 10000;


                await context.Response.WriteAsync($"Your number is {numbers[tenThousandsDigit]} ten thousand");


                if (remainder > 0)

                {

                    await context.Response.WriteAsync(" ");

                    //await FromThousandToNineThousandNineHundredNinetyNineMiddleware.Invoke(context, remainder);

                }

            }

            else

            {

                await _next.Invoke(context);

            }

        }
    }
}
