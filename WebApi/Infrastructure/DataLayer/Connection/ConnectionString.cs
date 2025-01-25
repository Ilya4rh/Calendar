namespace Infrastructure.Connection;

public class ConnectionString
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DatabaseName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public string Build()
    {
        return $"Host={Host};Port={Port};Database={DatabaseName};Username={UserName};Password={Password}";
    }
}