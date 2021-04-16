using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using _23012021Rewrite.Modules;

namespace _23012021Rewrite
{
    class Program
    {
        public static List<ulong> admins = new List<ulong>() { 283311394076753920, 553587721734586368 };
        /*public Task OldBootupLoad()
        {
            SSR = new StateSavingReading();
            if (File.Exists(StatusSpath))
            {
                LoadState = SSR.OldOpen(@".\SavedState.xml");
                 _client.SetGameAsync(LoadState.status);
                (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("status was set to <" + LoadState.status + ">");
            }
            else
            {
                LoadState = new StateAndConfig();
                SSR.OldSave(@".\SavedState.xml", LoadState);
                (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("no preexisting State Data was found, new config was created");
            }
            return Task.CompletedTask;
        }*/
        public void shutdownroutine()
        {
            TXTFileIO.Save(StatusSpath, currentstatus);
        }
        public static Task BootupLoad()
        {
            if (File.Exists(CurrentStatusPath))
            {
                currentroundstatus = TXTFileIO.Open(CurrentStatusPath).First();
                switch (currentroundstatus)
                {

                    case 'O':
                        _client.SetStatusAsync(UserStatus.Online);
                        (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Online");
                        break;
                    case 'o':
                        _client.SetStatusAsync(UserStatus.Offline);
                        (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Offline");
                        break;
                    case 'I':
                        _client.SetStatusAsync(UserStatus.Invisible);
                        (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Invisible");
                        break;
                    case 'i':
                        _client.SetStatusAsync(UserStatus.Idle);
                        (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Idle");
                        break;
                    case 'D':
                        _client.SetStatusAsync(UserStatus.DoNotDisturb);
                        (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to Do Not Disturb");
                        break;
                    case 'A':
                        _client.SetStatusAsync(UserStatus.AFK);
                        (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("Set Status to AFK");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("round status could not be fetched from file.");
            }
            if (File.Exists(StatusSpath))
            {
                _client.SetGameAsync(TXTFileIO.Open(StatusSpath));
                currentstatus = TXTFileIO.Open(StatusSpath);
                (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("status was set to <" + currentstatus + ">");
            }
            else
            {
                (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("status could not be fetched from file.");
            }
            return Task.CompletedTask;
        }


        public static bool firstloadup = true;
        public static string currentstatus = "";
        public static string StatusSpath = @".\Settings\Status.txt";
        public static string CurrentStatusPath = ".\\Settings\\CurrentRoundstatus.txt";
        public static string tokenpath = @".\Settings\Token.txt";
        public static StreamWriter LogSR = new StreamWriter(".\\LogFiles\\"+ (DateTime.Now.Ticks).ToString() +"Log.txt");
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            
            //Retry:
            //try 
            //{
                new Program().RunBotAsync().GetAwaiter().GetResult();
            //}
            /*catch
            {
                File.Delete(@".\SavedState.xml");
                //goto Retry;
            }*/
            
        }

        public static DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public static char currentroundstatus = 'O';
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            LogSR.WriteLine("exiting");
            LogSR.Close();
            (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync("shutting down");
            Console.WriteLine("exiting");
        }


        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            //Loads Token in noted directory
            string token = TXTFileIO.Open(tokenpath);

            _client.Log += _client_Log;

            //await BootupLoad();

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            

            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            if(arg.Message == "Ready" && firstloadup == true)
            {
                BootupLoad();
                firstloadup = false;
            }
            LogSR.WriteLine(arg);
            Console.WriteLine(arg);
            (_client.GetGuild(616333388659556372).GetChannel(802621868560482364) as ITextChannel).SendMessageAsync(arg.Message);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;
            LogSR.WriteLine(message.Timestamp + " "+ context.Guild.Name + message.Channel.Name + " " + message.Author + ":" + message.Content.ToString());
            Console.WriteLine(message.Timestamp + " " + context.Guild.Name + " " + message.Channel.Name + " " + message.Author + ":" + message.Content.ToString());
            int argPos = 0;
            
            if (message.HasStringPrefix("mb!", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
