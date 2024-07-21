using Discord;
using Discord.WebSocket;

namespace DiscordPlayingAPI
{
    public class DiscordBotService(ILogger<DiscordBotService> logger) : BackgroundService
    {

        private readonly DiscordSocketClient _client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.Guilds | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences
        });
        private readonly ILogger<DiscordBotService> _logger = logger;

        private SocketGuildUser _ourUser;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.Log += Log;
            _client.Ready += OnClientReady;

            var token = Environment.GetEnvironmentVariable("TOKEN");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private Task OnClientReady()
        {
            // Console.WriteLine(Convert.ToUInt64(Environment.GetEnvironmentVariable("GUILD_ID")));
            // Console.WriteLine(Convert.ToUInt64(Environment.GetEnvironmentVariable("USER_ID")));
            var guild = _client.GetGuild(Convert.ToUInt64(Environment.GetEnvironmentVariable("GUILD_ID")));
            _ourUser = guild.GetUser(Convert.ToUInt64(Environment.GetEnvironmentVariable("USER_ID")));
            // Console.WriteLine(guild.Name);
            // Console.WriteLine(_ourUser.DisplayName);

            // TODO: create and then we'll set cached status info here
            return Task.CompletedTask;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync();
            _client.Dispose();
            _logger.LogInformation("Discord bot stopped.");
            await base.StopAsync(cancellationToken);
        }

        public Task<IActivity> GetStatusAsync()
        {
            Console.WriteLine("ourUser", _ourUser.Nickname);
            return Task.FromResult(_ourUser.Activities.ElementAt(0));
        }
    }
}
