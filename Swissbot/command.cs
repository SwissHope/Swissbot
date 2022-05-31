using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Swissbot
{ 
     
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo) => ReplyAsync(echo);
    }

    [Group("sample")]
    public class SampleModule : ModuleBase<SocketCommandContext>
    {
        [Command("square")]
        [Summary("Squares a number.")]

        public async Task SquareAsync([Summary("The number to square.")] int num)
        {
            await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        }

        [Command("userinfo")]
        [Summary("Gets user info.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("The (optional) user to get info from.")] SocketUser user = null)
        {
            var info = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{info.Username}#{info.Discriminator}");
        }
    }

    [Group("Admin")]
    public class AdminModule : ModuleBase<SocketCommandContext>
    {

    }
}