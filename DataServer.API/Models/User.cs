namespace DataServer.API.Models;

public class User
{
    public User(int id, string name, string salt, string password)
    {
        this.Id = id;
        this.Name = name;
        this.Salt = salt;
        this.Password = password;
    }
    
    public int Id {get;set;}
    public string Name = string.Empty;
    public string Salt = string.Empty;
    public string Password = string.Empty;
}