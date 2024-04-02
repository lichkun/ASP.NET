namespace RequestProcessingPipeline
{
    public  class FromElevenThousandToTwentyThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromElevenThousandToTwentyThousandMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            string[] Hundreds = { "eleven thousand", "twelve thousand", "thirteen thousand", "fourteen thousand", "fifteen thousand", "sixteen thousand", "seventeen thousand", "eighteen thousand", "nineteen thousand" };

            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number > 11000 && number < 19000)
                {
                    await _next.Invoke(context);
                    string? result = string.Empty;
                    result = context.Session.GetString("number");

                    context.Session.SetString("number", result);
                }
                else
                {
                    await _next.Invoke(context);
                } 

            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
