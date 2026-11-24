using System.ComponentModel.DataAnnotations.Schema;

namespace DataServer.API.Models;

public class Entry
{
    public int Id {get; set;}
    public string date{get;set;} = string.Empty;
    public Server? Server { get; set; }
    [ForeignKey("Server")] public int ServerId {get;set;}
}
