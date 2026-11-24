using DataServer.API.Data;
using DataServer.API.Entrypoints;
using DataServer.API.Models;

var builder = WebApplication.CreateBuilder(args);
var connString = "Data Source=DataServer.db";
builder.Services.AddSqlite<DataServerContext>(
    connString,
    optionsAction: options => options.UseSeeding((context, _) =>
    {
        if (!context.Set<Server>().Any())
        {
            context.Set<Server>().AddRange(
                new Server {Id=1,Name="oscip05" },
                new Server {Id=2,Name="oslab15" }
            );
            context.SaveChanges();
        }
    })
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.AddDataEntryPoints();
app.Run();
