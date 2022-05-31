using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Swissbot
{
    public class commandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private ulong guildId;

        public commandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            _commands = commands;
            _client = client;
            _services = services;

            client.Ready += Client_Ready;
            client.SlashCommandExecuted += SlashCommandHandler;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandsAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        public async Task HandleCommandsAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null)
            {
                return;
            }
            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }

        public async Task Client_Ready()
        {
            var guild = _client.GetGuild(guildId);
            if (guild == null)
            {
                return;
            }
            
            var guildCommand = new SlashCommandBuilder();

            guildCommand.WithName("first-command");
            guildCommand.WithDescription("The first command created");

            var globalComand = new SlashCommandBuilder();
            globalComand.WithName("first-global-command");
            globalComand.WithDescription("First global command created");
            
            try
            {
                await guild.CreateApplicationCommandAsync(guildCommand.Build());
                await _client.CreateGlobalApplicationCommandAsync(guildCommand.Build());
            }
            catch (HttpException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(json);
            }
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            await command.RespondAsync($"You Executed {command.Data.Name}");
        }
    }
}