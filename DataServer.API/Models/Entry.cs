namespace DataServer.API.Models;

public class Entry
{
    public int Id {get; set;}
    public string date = string.Empty;
    public Server? Server;
    public int ServerId {get;set;}
}
