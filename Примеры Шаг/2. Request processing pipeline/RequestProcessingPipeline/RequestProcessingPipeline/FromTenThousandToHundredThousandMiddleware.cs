namespace RequestProcessingPipeline
{
    public class FromTenThousandToHundredThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTenThousandToHundredThousandMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            string[] Hundreds = { "ten thousand", "twenty thousand", "thirty thousand", "fourty thousand", "fifty thousand", "sixty thousand", "seventy thousand", "eighty thousand", "ninety thousand", 
                "ten thousand", "eleven thousand","twelve thousand","therteen thousand","fourteen thousand","fifteen thousand","sixteen thousand","seventeen thousand","eighteen thousand","nineteen thousand", };

            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number == 100000)
                {
                    await context.Response.WriteAsync("Your number is one hundred thousand");
                }
                else if (number>=11000 && number < 20000){

                    await _next.Invoke(context);
                    string? result = string.Empty;
                    result = context.Session.GetString("number");
                    if (number / 1000 - 1 >= 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Hundreds[number / 1000 - 1] + " " + result);
                    }
                    else
                    {
                        await context.Response.WriteAsync("Your number is " + result);
                    }
                }
                else if (number > 10000)
                {
                    if (number % 10000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Hundreds[number / 10000 - 1]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        if(number / 10000 - 1 >= 0)
                        {
                            await context.Response.WriteAsync("Your number is " + Hundreds[number / 10000 - 1] + " " + result);
                        }
                        else
                        {
                            await context.Response.WriteAsync("Your number is "+  result); 
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
