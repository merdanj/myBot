using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using System.Linq;

//This code was created through tutorial made by emzi0767
//https://dsharpplus.emzi0767.com/articles/intro.html
namespace myBot
{
    class Program
    {
        //discordClient instance which is used to interract with API
        static DiscordClient discord;


        static CommandsNextModule commands;

        static InteractivityModule interactivity;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            // Initializing discord client  
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "TToken here",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            interactivity = discord.UseInteractivity(new InteractivityConfiguration{});

            //Listener which waits for message starting with ping to respond with pong!
            discord.MessageCreated += async e =>
            {
                if(e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong!"); 
            };

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = ";;"
            });

            commands.RegisterCommands<MyCommands>();

            // Connects to the gateway
            await discord.ConnectAsync();
            //Infinite wait to stop bot from turning off.
            await Task.Delay(-1);
        }
    }
}
