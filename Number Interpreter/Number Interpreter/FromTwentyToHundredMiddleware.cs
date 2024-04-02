using Microsoft.AspNetCore.Http;

namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredMiddleware
    {
        

            private readonly RequestDelegate _next;


            public FromTwentyToHundredMiddleware(RequestDelegate next)

            {

                _next = next;

            }


            public async Task InvokeAsync(HttpContext context)

            {

                int number;

                if (int.TryParse(context.Request.Query["number"], out number) && number >= 20 && number <= 99)

                {

                    string[] numbers = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                    string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };


                    int tensDigit = number / 10;

                    int unitsDigit = number % 10;


                    await context.Response.WriteAsync($"Your number is {tens[tensDigit]} {(unitsDigit > 0 ? numbers[unitsDigit] : "")}");

                }

                else

                {

                    await _next.Invoke(context);

                }

            }

    }
}
