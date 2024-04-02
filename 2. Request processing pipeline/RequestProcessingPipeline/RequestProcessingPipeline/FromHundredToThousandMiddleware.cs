namespace RequestProcessingPipeline
{
    public class FromHundredToThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromHundredToThousandMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            string[] Hundreds = { "one hundred", "two hundred", "three hundred", "four hundred", "five hundred", "six hundred", "seven hundred", "eight hundred", "nine hundred" };

            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number == 1000)
                {
                    await context.Response.WriteAsync("Your number is one thousand");
                }
                else if (number > 1000)
                {
                    if (number % 100 == 0)
                    {
                        context.Session.SetString("number", Hundreds[number / 100 % 10 - 1]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        if(number / 100 % 10 - 1 >= 0)
                        {

                            context.Session.SetString("number", Hundreds[number / 100 % 10 - 1] + " " + result);
                        }
                        else
                        {
                            context.Session.SetString("number", result);

                        }
                    }
                }
                else if (number > 100)
                {
                    if (number % 100 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Hundreds[number / 100 - 1]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        if(result == "ten")
                        {
                            await context.Response.WriteAsync("Your number is "  + result);

                        }
                        else
                        {
                            await context.Response.WriteAsync("Your number is " + Hundreds[number / 100 - 1] + " " + result);
                        }
                    }
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
