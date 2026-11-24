namespace DataServer.API.Models;

public class Run
{
    public int Id {get;set;}
    public decimal? overall {get; set;}
    public decimal? Cpu {get; set;}
    public decimal? Memory {get; set;}
    public decimal? Io {get; set;}
    public decimal? System {get; set;}
    public decimal? Mt {get; set;}
    public decimal? St {get; set;}
    public decimal? Os {get; set;}
    public decimal? Sys {get; set;}
    public Entry? Entry;    
    public int EntryId {get; set;}
}
