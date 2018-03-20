using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace SpoderBot
{

    public class MyBot : ModuleBase<SocketCommandContext>
    {
        //public int memecount = 0; //this stupid thing is for meme dispenser

        string[] memes = new string[]
            {
                @"D:\Saved Pictures\Spiders\animalmemes\snek1.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\drpupper.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\ducks.jpeg",
                @"D:\Saved Pictures\Spiders\animalmemes\footsnek.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\forgotdog.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\ineedthis.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\longboy.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\monkey1.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\newsone.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\sealdog.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\snek2.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\snekmic.jpg",
                @"D:\Saved Pictures\Spiders\animalmemes\ventsnek.jpg",


            };
        



        [Command("dlm")]
        public async Task PingAsync(int numMessages)
        {
            if(numMessages > 0 && numMessages < 31)
            {
                await ReplyAsync("Sending scraped chat data for last " + numMessages + " messages, if that many messages have been sent in this channel since bot startup");

                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(numMessages).Flatten();

                foreach (IMessage message in messages)
                {
                    await Context.User.SendMessageAsync(message + "");
                    System.Threading.Thread.Sleep(100);
                }
            }
            else
            {
                await ReplyAsync("Scraping only works for numbers 30 or less");
            }
            

        }

        [Command("poke")]
        public async Task PokeAsync(SocketGuildUser user)
        {
            await user.SendMessageAsync($"Sup home skillet. You've done been poked by " + Context.User.Mention);
            Console.WriteLine("Event!\n\t" + Context.User + " just poked " + user + " from channel '" + Context.Channel + "' in guild '" + Context.Guild + "'");
        }

        [Command("uptime")]
        public async Task UTimeAsync()
        {
            var time = (DateTime.Now - Process.GetCurrentProcess().StartTime);
            string str = "I've been online for " + time.Days + " days, " + time.Hours + " hours, and " + time.Minutes + " minutes.";
            await ReplyAsync(str);
            Console.WriteLine("Event!\n\tUser requested uptime: " + str);
        }

        [Command("createvote")]
        public async Task CreateVote()
        {
            //await Context.Channel.SendFileAsync(@"D:\Saved Pictures\Spiders\animalmemes\snek1.jpg");
        }

        [Command("badmeme")]
        public async Task PostMeme()
        {
            await Context.Channel.SendFileAsync(memes[Globals.memecount]);
            Console.WriteLine("Event!\n\tUser requested badmeme #" + Globals.memecount);
            Globals.memecount++;
            if(Globals.memecount >= 13)
            {
                Globals.memecount = 0;
            }
            
        }

        [Command("countdown")]
        public async Task Countdown(float time)
        {
            if(time < 1200 && time > 0)
            {
                Timer timeout = new Timer()
                {
                    AutoReset = false,
                    Enabled = true,
                    Interval = (time * 1000)

                };
                timeout.Elapsed += (e, a) => TimeoutF(Context.Channel, Context.User);
                timeout.Start();
                await ReplyAsync(Context.User.Mention + " " + time + " second timer started.");
            }
            else
            {
                await ReplyAsync("Timer values are restricted to positive numbers less than 1200 seconds (20 minutes)");
            }
            
            
        }

        private async void TimeoutF(ISocketMessageChannel channel, SocketUser user)
        {
            await ReplyAsync(user.Mention + " Timer is up!");
        }

        [Command("commands")]
        public async Task ShowCommands()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("SpoderBot Commands")
                .WithDescription("**!poke** - Usage '!poke @2smexy' - DM's user, and informs them of poke\n**!uptime** - Displays current uptime of SpoderBot\n**!countdown** - Usage '!countdown 120' - Starts timer for user defined number of seconds\n**!badmeme** - posts a terrible animal meme\n\nBy <@225428808130363392>. Pm for info/comments")
                .WithColor(Color.Orange);

            await ReplyAsync("", false, builder.Build());
        }
    }
    public class Globals
    {
        public static int memecount = 0;
    }
}
