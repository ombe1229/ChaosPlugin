using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace ChaosPlugin
{
    public class ConsoleCommands
    {
        private readonly chaosPlugin _pluginInstance;
        public ConsoleCommands(chaosPlugin pluginInstance) => this._pluginInstance = pluginInstance;

        public void OnConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
            switch (ev.Name)
            {
                case "c":
                {
                    ev.IsAllowed = false;
                    ev.Color = "red";
                    if (ev.Player.Team != Team.SCP)
                    {
                        ev.ReturnMessage = "SCP진영만 사용 가능한 명령어입니다!";
                        return;
                    }
                    string content = $"<color=red>[SCPCHAT]</color>{ev.Player.Nickname}({ev.Player.Role.ToString()}):";
                    for (int i = 0; i < ev.Arguments.Count; i++)
                    {
                        content += ev.Arguments[i] + " ";
                    }

                    foreach (var player in Player.List)
                    {
                        if (player.Team == Team.SCP) player.Broadcast(10,content);
                        Log.Info($"{ev.Player.UserId}'s message : {content}");
                    }
                    break;
                }
            }
        }
    }
}