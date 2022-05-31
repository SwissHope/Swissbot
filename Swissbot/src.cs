///This code is that of Katherine Binnie-Ritchie, the coder of Swissbot.
///The bot started life as a project to replace the job of two bots with one, and to add more features that were not present without
///paying money, or suffering the ignomy of dealing with NFTs and the Metaverse, which is being pushed by the cryptobros.
///In all honesty, these people have no idea how the world itself works and they are sold on the idea of "money for nothing."
///As much as one would love for money to have no meaning, short of complete societal collapse or the complete abolition of money
///Such ideas remain a pipe dream.

#define DEBUG_VERBOSE //Forces debugging messages to the build console.

//Set up all the dependencies that are required to get the bot working.


using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

//Put all the code in the same namespace.
//That way, all the code can refer to one and other with ease.

namespace Swissbot
{

    //Define the class that the bot code initialises from.
    public class Swissbot
    {
        public static Task Main(string[] args) => new Swissbot().MainAsync();// GetAwaiter().GetResult();

        public static string dataFileDiscord = "../../api.key";
        public static string content = "(!!NO KEY!!)";

        private DiscordSocketClient _client;

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private string getToken()
        {
            try
            {
                content = File.ReadAllText(dataFileDiscord);
                return content;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to create the api.key file!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                Console.WriteLine("=======================================");
                Console.WriteLine("");
                Console.WriteLine("Please check that you have the Discord API key and that you have saved it in the correct location!");
                Console.WriteLine("Swissbot needs this in order to access Discord.");
                Console.WriteLine("");
                Console.WriteLine("Swissbot will now exit.");
                Console.ReadKey();
                System.Environment.Exit(-1);
                return content;
            }
        }

        public async Task MainAsync()
        {

            Console.Title = "Swissbot Console";

            var _config = new DiscordSocketConfig { MessageCacheSize = 100 };

            _client = new DiscordSocketClient(_config);

            _client.Log += Log;

            var token = "token";

            token = getToken();

            try
            {
                if (token == "(!!NO KEY!!)")
                {
                    throw new NoAPIKeyException();
                }

                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();

                _client.MessageUpdated += MessageUpdated;
                _client.Ready += () =>
                {
                    Console.WriteLine("");
                    Console.WriteLine("Swissbot is alive!");
                    return Task.CompletedTask;
                };

                await Task.Delay(-1);
            }
            catch (NoAPIKeyException)
            {
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No API key has been set!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                Console.WriteLine("=======================================");
                Console.WriteLine("");
                Console.WriteLine("Please check your application files and try again.");
                Console.WriteLine("");
                Console.WriteLine("Swissbot will now exit.");
                Console.ReadLine();
                System.Environment.Exit(-1);
            }
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }
    }
}
