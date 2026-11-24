using DataServer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DataServer.API.Data;

public class DataServerContext : DbContext
{
    public DataServerContext(DbContextOptions<DataServerContext> options) : base(options){}
    public DbSet<User> Users => Set<User>();
    public DbSet<Server> Servers => Set<Server>();
    public DbSet<Entry> Entries => Set<Entry>();
    public DbSet<Run> Runs => Set<Run>();
}