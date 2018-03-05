using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Commands.Permissions.Visibility;
using System.Collections.Generic;
using System;
using Discord.API;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace SpoderBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        DateTime spamDate = DateTime.Parse("12/25/2016 12:00:01 AM");

        string[] spiderPics;
        string[] bugPics;
        string[] snekers;
        public MyBot()
        {
            
            spiderPics = new string[]
            {
                "Spiders/spider11.jpg",
                "Spiders/spider12.jpg",
                "Spiders/spider13.jpg",
                "Spiders/spider4.jpg",
                "Spiders/spider5.jpg",
                "Spiders/spider6.jpg",
                "Spiders/spider7.jpg",
                "Spiders/spider8.jpg",
                "Spiders/spider9.jpg",
                "Spiders/spider10.jpg"

            };

            bugPics = new string[]
            {
                "Spiders/centi2.jpg",
                "Spiders/centi1.jpg",
                "Spiders/centi3.jpg",
                "Spiders/centi4.jpg",
                "Spiders/centi5.jpg"
                

            };

            snekers = new string[]
            {
                "Spiders/snek1.jpg",
                "Spiders/Sneker.gif"

            };
            int count = 0;
            int count2 = 0;
            int loopey = 1;

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
           {
               x.PrefixChar = '!';
               //x.AllowMentionPrefix = true;
           });

            commands = discord.GetService<CommandService>();

            commands.CreateCommand("commands")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("!spiderUp - post spider\n!bugOut - post bug\n!superSecretSnek - snek mem\n!fancyspam - spams fancily\n!georgebush\n!sadface - sad snek\n!uptime - bit uptime\n!poke. Follow this command with the user you would like to poke.\n!struct - sing the song of my people");
                    
                });

            commands.CreateCommand("uptime")
                    .Description("Shows how long is bot running for.")
                    .Do(async e => {
                        var time = (DateTime.Now - Process.GetCurrentProcess().StartTime);
                        string str = "I've been online for " + time.Days + " days, " + time.Hours + " hours, and " + time.Minutes + " minutes.";
                        await e.Channel.SendMessage(str);
                    });

            /*commands.CreateCommand("kick")
                .Do(async (e) =>
                {
                    var user = await _client.FindUser(e, e.Args[0], e.Args[1]); //args[0]would be the username, args[1] would be the discriminator (the random number that follows the discord id)
                    if (user == null) return;
                    await user.Kick();
                });*/

            /*commands.CreateCommand("bugOut")
                .Do(async (e) =>
                {
                    string bugToPost = bugPics[count2];
                    //await e.Channel.SendMessage("<@255854515046187019>");
                    await e.Channel.SendFile(bugToPost);
                    if (count2 >= 4)
                    {
                        count2 = 0;
                    }
                    else { count2++; }
                });*/
            commands.CreateCommand("christmas")
                .Do(async (e) =>
                {

                    DateTime daysLeft = DateTime.Parse("12/25/2017 12:00:01 AM");/*11/25/2017 12:00:01 AM*/
                    DateTime startDate = DateTime.Now;
                    /*spam prevention*/
                    TimeSpan sp = startDate - spamDate;
                    //await e.Channel.SendMessage("SECS: " + sp.Seconds);
                    if(sp.Seconds > 30 || sp.Minutes > 1)
                    {
                        //Calculate countdown timer.
                        TimeSpan t = daysLeft - startDate;
                        string countDown = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds til launch.", t.Days, t.Hours, t.Minutes, t.Seconds);
                        await e.Channel.SendMessage("The current exact time is: " + startDate + " \nThere's only " + t.Days + " Days, " + t.Hours + " Hours, " + t.Minutes + " Minutes, " + t.Seconds + " Seconds til Christmas!");
                        await e.Channel.SendMessage("Thanks for the idea Jonas!");
                        spamDate = DateTime.Now;
                    }


                    

                    
                    //Thread.Sleep(10000);
                });

            commands.CreateCommand("poke")
             .Parameter("target", ParameterType.Required)
             .Do(async (e) =>
             {
                 ulong id;
                 User u = null;
                 string findUser = e.Args[0];

                 if (!string.IsNullOrWhiteSpace(findUser))
                 {
                     if (e.Message.MentionedUsers.Count() == 1)
                         u = e.Message.MentionedUsers.FirstOrDefault();
                     else if (e.Server.FindUsers(findUser).Any())
                         u = e.Server.FindUsers(findUser).FirstOrDefault();
                     else if (ulong.TryParse(findUser, out id))
                         u = e.Server.GetUser(id);
                 }
                 Console.WriteLine("[" + e.Server.Name + "]" + e.User.Name + " just poked " + u);
                 await u.SendMessage("Hey, you just got poked by " + e.User.Name);
             });

            commands.CreateCommand("superSecretSnek")
                .Do(async (e) =>
                {
                    string snekToPost = snekers[0];
                    //await e.Channel.SendMessage("<@255854515046187019>");
                    await e.Channel.SendFile(snekToPost);
                    
                });

            
            /*commands.CreateCommand("kick")
                    .Description("Kicks a user from this server.")
                    .Parameter("user")
                    .Parameter("discriminator", ParameterType.Optional)
                    .MinPermissions((int)PermissionLevel.ServerModerator)
                    .Do(async e =>
                    {
                        var user = await _client.FindUser(e, e.Args[0], e.Args[1]);
                        if (user == null) return;

                        await user.Kick();
                        await _client.Reply(e, $"Kicked user {user.Name}.");
                    });*/

            /*commands.CreateCommand("lucas")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("<@182960069669814272>\n");
                    await e.Channel.SendMessage("<@182960069669814272>\n");
                    await e.Channel.SendMessage("<@182960069669814272>\n");
                    await e.Channel.SendMessage("<@182960069669814272>\n");
                    await e.Channel.SendMessage("<@182960069669814272>\n");
                   
                })*/

            /*commands.CreateCommand("rolo")
                .Do(async (e) =>
                {
                    
                    await e.Channel.SendMessage("<@203219356186837002>\n<@203219356186837002>\n<@203219356186837002>\n<@203219356186837002>\n");
                    await e.Channel.SendMessage("<@203219356186837002>\n<@203219356186837002>\n<@203219356186837002>\n<@203219356186837002>\n");
                    await e.Channel.SendMessage("<@203219356186837002>\n<@203219356186837002>\n<@203219356186837002>\n<@203219356186837002>\n");
                
                });*/


            commands.CreateCommand("struct")
                .Do(async (e) =>
                {
                    if (loopey == 1)
                    {
                        await e.Channel.SendMessage("you take the struct\n");
                        //Thread.Sleep(1000);
                        loopey++;
                    }
                    else if (loopey == 2)
                    {
                        await e.Channel.SendMessage("you fill the struct\n");
                        //Thread.Sleep(1000);
                        loopey++;
                    }
                    else if (loopey == 3)
                    {
                        await e.Channel.SendMessage("you write the struct\n");
                        //Thread.Sleep(1000);
                        loopey++;
                    }
                    else if (loopey <= 4)
                    {
                        await e.Channel.SendMessage("you empty the struct\n");
                        /*Thread.Sleep(2000);*/
                        loopey = 1;
                    }

                       
                });

            commands.CreateCommand("spoop")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("<@255854515046187019>");
                    //Message[] messagesToObtain;

                    //messagesToObtain = await e.Channel.DownloadMessages(25);

                });

            commands.CreateCommand("sadface")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile("Spiders/sadsnek.png");
                });

            /*commands.CreateCommand("loop")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Oh shit waddup");
                });*/

            

            

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjczMTU4NTU0MDkxNzgyMTQ1.C2fjdA.7qLjDSk0fPbEo3D1Kr2EJUV9XhU", TokenType.Bot);
            });
        }


        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
