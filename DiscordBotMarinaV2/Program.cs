using DiscordBotMarinaV2;
using System.Configuration;

public class Program
{
    static void Main(string[] args)
    {

            var token = ConfigurationManager.AppSettings.Get("token");
            var role = ConfigurationManager.AppSettings.Get("role");
            var commandsPrefix = ConfigurationManager.AppSettings.Get("commandsPrefix");
            List<string> allowedMessagesList = ConfigurationManager.AppSettings
                                                          .Get("allowedMessages")
                                                          .ToLower()
                                                          .Split(' ')
                                                          .Select(s => $"{commandsPrefix + s}")
                                                          .ToList();
            var allowedChannel = ConfigurationManager.AppSettings.Get("allowedChannel");




        PrintCfg(token, role, commandsPrefix, allowedMessagesList, allowedChannel);

        var bot = new DiscordBot(token, role, allowedMessagesList, allowedChannel);

        while(true)
        {
            Console.WriteLine("Starting...");
            bot.WorkAsync().GetAwaiter().GetResult();
        }
    }

    static void PrintCfg(
        string token, 
        string role, 
        string commandsPrefix, 
        List<string> allowedMessagesList, 
        string allowedChannel
        )
    {
        Console.WriteLine("Token:");
        Console.WriteLine(token);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Role:");
        Console.WriteLine(role);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Commands prefix:");
        Console.WriteLine(commandsPrefix);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Allowed messages:");
        foreach (string m in allowedMessagesList)
        {
            Console.WriteLine(m);
        }
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Allowed channel:");
        Console.WriteLine(allowedChannel);
        Console.WriteLine("----------------------------------------");

    }
}