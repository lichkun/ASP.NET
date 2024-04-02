using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();  
var app = builder.Build();

app.UseSession();   


app.UseFromOneToTen();
app.UseFromElevenToNineteen();
app.UseFromTwentyToHundred();
app.UseFromHundredToThousand();

app.Run();
