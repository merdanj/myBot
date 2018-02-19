using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.Entities;
using System.Linq;

//Commands "hi" and "random" were created through tutorial made by emzi0767
//https://dsharpplus.emzi0767.com/articles/intro.html
namespace myBot
{
    public class MyCommands
    {   
        [Command("hi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");
            var interactivity = ctx.Client.GetInteractivityModule();
            var msg = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id && xm.Content.ToLower() == "how are you?", TimeSpan.FromMinutes(1));
            if (msg != null)
                await ctx.RespondAsync($"I'm fine, thank you!");
        }

        //Returns random number between given min and max values.
        [Command("random")]
        public async Task Random(CommandContext ctx, int min, int max)
        {
            var rnd = new Random();
            await ctx.RespondAsync($"🎲 Your random number is: {rnd.Next(min, max)}");
        }

        //Assigns users with asked role when they send command
        /*
            Can't assign roles that are higher than bot role, 
            so make sure that moderator and admin roles are higher than bot role
        */
        /*TODO: 
            Find if user can assign themselves with moderator and admin roles through bot even 
            if bot has lower privilege than admin and mods
         */
        [Command("role")]
        public async Task assignRole(CommandContext ctx, string uRole)
        {
            var user = ctx.User;
            var role = ctx.Guild.Roles.FirstOrDefault(x => x.Name == uRole);
            await ctx.Member.GrantRoleAsync(role);
            await ctx.RespondAsync($"Your have been granted with {role.Name} role");
        }

        //Lets user create new voice channels. User have to give channel name, might change to optional later
        //this can create VOICE channel only.
        [Command("newVoice")]
        public async Task CreateVoiceServer(CommandContext ctx, string chName)
        {
            //put Voice channels category ID or any other categories ID instead of 0123456789.
            await ctx.Guild.CreateChannelAsync(chName, ChannelType.Voice, ctx.Guild.GetChannel(0123456789));
            await ctx.RespondAsync($"{ctx.User.Mention}, your voice channel has been created.");
        }
    }
}
