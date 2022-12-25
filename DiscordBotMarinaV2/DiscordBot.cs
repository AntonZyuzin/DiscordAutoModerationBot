using Discord;
using Discord.WebSocket;

namespace DiscordBotMarinaV2
{
    public class DiscordBot
    {
        private DiscordSocketClient _client;
        private readonly string _token;
        private readonly string _unwantedRole;
        private readonly List<string> _allowedMessagesList;
        private readonly string _allowedChannel;

        public DiscordBot(
            string token, 
            string role, 
            List<string> allowedMessagesList, 
            string allowedChannel
            )
        {
            _token = token;
            _unwantedRole = role;
            _allowedMessagesList = allowedMessagesList;
            _allowedChannel = allowedChannel;
        }

        public async Task WorkAsync()
        {

            var socketConfig = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            _client = new DiscordSocketClient(socketConfig);
            _client.MessageReceived += DoModeration;
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }


        private Task Log(LogMessage logMsg)
        {
            Console.WriteLine(logMsg);
            return Task.CompletedTask;
        }

        private Task DoModeration(SocketMessage message)
        {
            if (!message.Author.IsBot)
            {
                if (_allowedChannel == message.Channel.ToString())
                {
                    var authorRolesList = ((SocketGuildUser)message.Author)
                        .Roles
                        .Select(authorRole => authorRole.Name)
                        .ToList();

                    if (authorRolesList.Contains(_unwantedRole))
                    {
                        if (!_allowedMessagesList.Contains(message.Content.ToLower()))
                        {
                            message.Channel.DeleteMessageAsync(message);
                            Console.WriteLine("Я удалила сообщение пользователя "
                                + message.Author.ToString()
                                + ", потому что он петух (⌒‿⌒)");
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
