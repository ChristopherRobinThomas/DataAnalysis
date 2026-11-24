using DataServer.API.Data;
using DataServer.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DataServer.API.Entrypoints;

public static class DataEntrypoint
{
    public static decimal? extractDecimal(JsonElement elem)
    {
        if(elem.ValueKind == JsonValueKind.Null) return null;
        else return elem.GetDecimal();
    }
    public static void AddDataEntryPoints(this WebApplication app)
    {
        app.MapPost("/loadData/{serverId}", async(int serverId, DataServerContext context) =>
        {
            var res = await context.Servers.FindAsync(serverId);
            if(res == null){
                return Results.NotFound();
            }
            string json = File.ReadAllText("./jsons/" + res.Name + ".json");
            var rawData = JsonSerializer.Deserialize<Dictionary<string, List<List<JsonElement>>>>(json);
            if(rawData != null)
            {
                foreach (var (dateKey, rows) in rawData)
                {
                    var entry = new Entry
                    {
                        date = dateKey,
                        ServerId = serverId,
                    }; 
                    context.Entries.Add(entry);
                    foreach (var row in rows)
                    {
                        var run = new Run{
                            name = row[0].GetString() ?? "",
                            overall = extractDecimal(row[1]),
                            Cpu = extractDecimal(row[2]),
                            Memory = extractDecimal(row[3]),
                            Io = extractDecimal(row[4]),
                            Sys = extractDecimal(row[5]),
                            Mt = extractDecimal(row[6]),
                            St = extractDecimal(row[7]),
                            Os = extractDecimal(row[8]),
                            Sleep = extractDecimal(row[9]),
                            Entry = entry,
                        };
                        context.Runs.Add(run);
                    }
                }

            }
            int writes = await context.SaveChangesAsync(); 
            return Results.Ok(writes);
        });

        app.MapGet("/servers", async( DataServerContext context) =>
        {
           return await context.Servers.ToListAsync<Server>();
        });
        app.MapGet("/entries/{serverId}", async( int serverId, DataServerContext context) =>
        {
           return await context.Entries.Where(e => e.ServerId == serverId).AsNoTracking().ToListAsync();
        });
        app.MapGet("/runs/{entryId}", async( int entryId, DataServerContext context) =>
        {
           return await context.Runs.Where(e =>e.EntryId == entryId).AsNoTracking().ToListAsync();
        });
    }
}