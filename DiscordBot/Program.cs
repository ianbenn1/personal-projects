using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Discord;
using Discord.Commands;
using System.IO;

namespace SpoderBot
{
    class Program
    {
        
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "Add-token-to-use";

            Console.WriteLine("Welcome to SpoderBot!");


            //event subscriptions
            _client.Log += Log;
            _client.MessageReceived += MessageRecieved;//triggers on every sent message in every guild
            

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.FromResult(0); //he has .CompletedTask
        }
        private Task MessageRecieved(SocketMessage msg) // Block logs incomming dm's and saves all other recieved messages to audit file
        {
            //Console.WriteLine("Channel: " + msg.Channel);
            string msgChannelName = msgChannelToString(msg.Channel);
            bool hashTagFlag = false;
            if (msgChannelName[0] == '@')
            {
                
                for(int i = 0; i < msgChannelName.Length; i++)
                {
                    if(msgChannelName[i] == '#')
                    {
                        hashTagFlag = true;
                        break;
                    }
                }
                if(hashTagFlag == true)
                {
                    if(msg.Source != MessageSource.Bot)
                    {
                        Console.WriteLine("Event!\n\tRecieved Direct Message: " + msg + "\n\tFrom: " + msgChannelName);
                    }
                    
                }
                
            }
            else
            {
                if(hashTagFlag == false) 
                {

                    string pathString = @"D:\Documents\Personal\discordAuditMessages.txt";
                    if (!File.Exists(pathString))
                    {
                        File.Create(pathString);
                    }
                    var chnl = msg.Channel as SocketGuildChannel; 
                    File.AppendAllText(pathString, "Guild: " + chnl.Guild.Name + " Chnl: " + msgChannelName + " usr: " + msg.Source + " - " + msg.Author + " msg: " + msg + Environment.NewLine);


                }
            }
            
            return Task.FromResult(0);
        }

        private string msgChannelToString(ISocketMessageChannel channel)
        {
            return channel + "";
        }
        
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if ((message == null) || (message.Author.IsBot)) return; //he has is in place of ==

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }



        }
    }

    
}
