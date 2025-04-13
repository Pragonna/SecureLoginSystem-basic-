
using DotNetEnv;
using Newtonsoft.Json;

public class EmailConfig

{
    public string Server { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }

    public static EmailConfig LoadFromEnv()
    {
        Env.Load("config.env");

        var config = new EmailConfig
        {
            Server = Environment.GetEnvironmentVariable("SMTP_HOST"),
            Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0"),
            User = Environment.GetEnvironmentVariable("SMTP_USER"),
            Pass = Environment.GetEnvironmentVariable("SMTP_PASS"),
        }; 
        return config;
    }
}
