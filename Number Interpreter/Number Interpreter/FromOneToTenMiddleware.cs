namespace RequestProcessingPipeline
{
    public class FromOneToTenMiddleware
    {
        private readonly RequestDelegate _next;


        public FromOneToTenMiddleware(RequestDelegate next)

        {

            _next = next;

        }


        public async Task InvokeAsync(HttpContext context)

        {

            int number;

            if (int.TryParse(context.Request.Query["number"], out number) && number >= 1 && number <= 10)

            {

                string[] numbers = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

                await context.Response.WriteAsync($"Your number is {numbers[number]}");

            }

            else

            {

                await _next.Invoke(context);

            }

        }
    }
}
