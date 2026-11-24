using DataServer.API.Data;

var builder = WebApplication.CreateBuilder(args);
var connString = "Data Source=DataServer.db";
builder.Services.AddSqlite<DataServerContext>(connString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Hello World";
});

app.Run();
