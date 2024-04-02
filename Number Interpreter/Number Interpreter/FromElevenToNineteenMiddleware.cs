namespace RequestProcessingPipeline
{
    public class FromElevenToNineteenMiddleware
    {

        private readonly RequestDelegate _next;


        public FromElevenToNineteenMiddleware(RequestDelegate next)

        {

            _next = next;

        }


        public async Task InvokeAsync(HttpContext context)

        {

            int number;

            if (int.TryParse(context.Request.Query["number"], out number) && number >= 11 && number <= 19)

            {

                string[] numbers = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                await context.Response.WriteAsync($"Your number is {numbers[number]}");

            }

            else

            {

                await _next.Invoke(context);

            }

        }
    }
}
