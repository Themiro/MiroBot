using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace _23012021Rewrite.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        

        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("pong");
        }

        [Command("SetStatus")]
        public async Task SetStatus([Remainder] string status = null)
        {
            if(!Program.admins.Contains(Context.User.Id))
            {
                await ReplyAsync("only a bot admin can execute this command");
                return;
            }
            await Context.Client.SetGameAsync(status);
            Program.currentstatus = status;
            await ReplyAsync("set playing status");
            ITextChannel logChannel = Context.Client.GetChannel(802621868560482364) as ITextChannel;
            await logChannel.SendMessageAsync("status was set to <" + status + ">");
            await Task.CompletedTask;
            
        }
        [Command("SetRoundStatus")]
        public async Task SetRoundStatus(char OnlineType = 'O')
        {
            if(Program.admins.Contains(Context.User.Id))
            {
                switch (OnlineType)
                {

                    case 'O':
                        await Program._client.SetStatusAsync(UserStatus.Online);
                        TXTFileIO.Save(Program.CurrentStatusPath, OnlineType.ToString());
                        await (Program._client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Online");
                        await ReplyAsync("Set Status to Online");
                        break;
                    case 'o':
                        await Program._client.SetStatusAsync(UserStatus.Offline);
                        TXTFileIO.Save(Program.CurrentStatusPath, OnlineType.ToString());
                        await (Program._client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Offline");
                        await ReplyAsync("Set Status to Offline");
                        break;
                    case 'I':
                        await Program._client.SetStatusAsync(UserStatus.Invisible);
                        TXTFileIO.Save(Program.CurrentStatusPath, OnlineType.ToString());
                        await (Program._client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Invisible");
                        await ReplyAsync("Set Status to Invisible");
                        break;
                    case 'i':
                        await Program._client.SetStatusAsync(UserStatus.Idle);
                        TXTFileIO.Save(Program.CurrentStatusPath, OnlineType.ToString());
                        await (Program._client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Idle");
                        await ReplyAsync("Set Status to Idle");
                        break;
                    case 'D':
                        await Program._client.SetStatusAsync(UserStatus.DoNotDisturb);
                        TXTFileIO.Save(Program.CurrentStatusPath, OnlineType.ToString());
                        await (Program._client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Do Not Disturb");
                        await ReplyAsync("Set Status to Do Not Disturb");
                        break;
                    case 'A':
                        await Program._client.SetStatusAsync(UserStatus.AFK);
                        TXTFileIO.Save(Program.CurrentStatusPath, OnlineType.ToString());
                        await (Program._client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to AFK");
                        await ReplyAsync("Set Status to AFK");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                await ReplyAsync("only bot admin can execute this command");
            }
            
        }

        /*[Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "You don't have the permission ``ban_member``!")]
        public async Task BanMember(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was banned\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Ban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);

            ITextChannel logChannel = Context.Client.GetChannel(642698444431032330) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"{user.Mention} was banned\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Ban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        */

    }
}
