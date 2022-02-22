var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost*/")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/parse/{numerals}", (string numerals) => 
{
    try
    {
        var parser = new RomanNumerals.Parser();
        return Results.Ok(parser.Parse(numerals));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    
})
.WithName("ParseRomanNumerals")
.RequireCors(MyAllowSpecificOrigins);

app.Run();