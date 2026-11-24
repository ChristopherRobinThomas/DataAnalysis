using System.ComponentModel.DataAnnotations.Schema;

namespace DataServer.API.Models;

public class Run
{
    public int Id {get;set;}
    public string name {get; set;} = string.Empty; 
    public decimal? overall {get; set;}
    public decimal? Cpu {get; set;}
    public decimal? Memory {get; set;}
    public decimal? Io {get; set;}
    public decimal? System {get; set;}
    public decimal? Mt {get; set;}
    public decimal? St {get; set;}
    public decimal? Os {get; set;}
    public decimal? Sys {get; set;}
    public decimal? Sleep {get; set;}
    public Entry? Entry{get;set;}
    [ForeignKey("Entry")] public int EntryId {get; set;}
}
