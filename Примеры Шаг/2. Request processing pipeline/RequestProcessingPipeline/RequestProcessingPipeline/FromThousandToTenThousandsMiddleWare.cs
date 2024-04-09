namespace RequestProcessingPipeline
{
    public class FromThousandToTenThousandsMiddleWare
    {
        private readonly RequestDelegate _next;

        public FromThousandToTenThousandsMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            string[] Hundreds = { "one thousand", "two thousand", "three thousand", "four thousand", "five thousand", "six thousand", "seven thousand", "eight thousand", "nine thousand" };

            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number == 10000)
                {
                    await context.Response.WriteAsync("Your number is ten thousand");
                }
                else if (number >= 11000 && number < 20000)
                {
                    if (number % 1000 == 0)
                    {
                        context.Session.SetString("number", "");
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        
                        context.Session.SetString("number", " " + result);
                    }
                }
                else if (number > 10000)
                {
                    if (number % 1000 == 0)
                    {
                        context.Session.SetString("number", Hundreds[number / 1000 % 10 - 1]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        if (number / 1000 % 10 - 1 >= 0)
                        {

                            context.Session.SetString("number", Hundreds[number / 1000 % 10 - 1] + " " + result);
                        }
                        else
                        {
                            context.Session.SetString("number", result);

                        }
                    }
                }
                else if (number > 1000)
                {
                    if (number % 1000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Hundreds[number / 1000 - 1]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");

                        await context.Response.WriteAsync("Your number is " + Hundreds[number / 1000 - 1] + " " + result);
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
