using Npgsql;

namespace Ticketer.Business;

public class DbConfig
{
    public const string SectionName = "Database";
    public string Host { get; set; }
    public int Port { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Name,
            Username = UserName,
            Password = Password
        };

        return builder.ConnectionString;
    }
}