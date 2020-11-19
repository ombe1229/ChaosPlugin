using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace ChaosPlugin
{
    public class EventHandlers
    {
        private readonly chaosPlugin _pluginInstance;
        public EventHandlers(chaosPlugin pluginInstance) => this._pluginInstance = pluginInstance;


        internal void OnJoined(JoinedEventArgs ev)
        {
            if (!_pluginInstance.Config.WelcomeMessageEnable) return;
            ev.Player.Broadcast(10,$"<color=green>카오스 서버</color>에 오신 것을 환영합니다.\n<color=blue>디스코드</color>에 참여하여 <color=orange>서버 규칙을 반드시 확인</color>해주시기 바랍니다.\n라운드 시간:<color=green>{Round.ElapsedTime.Minutes}</color>분 / 현재 유저 수: <color=green>{Player.List.Count().ToString()}</color>명");
        }
        
        internal void OnSpawning(SpawningEventArgs ev)
        {
            if (!_pluginInstance.Config.ScpListMessageEnable) return;
            string scpList = "";
            int count = 0;
            
            foreach (var player in Player.List)
            {
                if (player.Team == Team.SCP)
                {
                    if (count != 0) scpList += " | ";
                    if (player.Team == Team.SCP)
                    {
                        scpList += $"<color=red>{player.Role.ToString()}</color>";
                        count++;
                    }
                }
            }
            if (ev.Player.Team == Team.SCP)
            {
                ev.Player.Broadcast(10,$"<color=red>이번 라운드 SCP 목록</color>\n{scpList}");
            }
        }
        
        internal void OnScpLeave(LeftEventArgs ev)
        {
            if (ev.Player.Team != Team.SCP || !_pluginInstance.Config.ScpLeftMessageEnable) return;
            Map.Broadcast(10,$"<color=red>{ev.Player.Nickname}</color>(이)가 <color=red>SCP</color>진영에서 게임을 중도 퇴장했습니다.\n신고용 URL:<color=green>{ev.Player.UserId}</color>");
        }
        
        internal void OnPlayerDeath(DiedEventArgs ev)
        {
            if (_pluginInstance.Config.CuffedkillMessageEnable && ev.Target.IsCuffed)
            {
                Map.Broadcast(10,$"<size=30><color=red>{ev.Killer.Nickname}님이 {ev.Target.Nickname}님을 체포킬 하셨습니다.\n죽인 사람의 URL(신고용):{ev.Killer.UserId}</color></size>");
            }

            if (_pluginInstance.Config.ScpHealingEnable && ev.Killer != ev.Target && ev.Killer.Team == Team.SCP)
            {
                var heal = 0;
                switch (ev.Killer.Role)
                {
                    case RoleType.Scp173 :
                        heal = 57;
                        break;
                    case RoleType.Scp096 :
                        heal = 57;
                        break;
                    case RoleType.Scp049 :
                        heal = 57;
                        break;
                    case RoleType.Scp0492 :
                        heal = 57;
                        break;
                    case RoleType.Scp106 :
                        heal = 57;
                        break;
                    case RoleType.Scp93953 :
                        heal = 57;
                        break;
                    case RoleType.Scp93989 :
                        heal = 57;
                        break;
                }
                if (ev.Killer.MaxHealth <= ev.Killer.Health + heal) ev.Killer.Health = ev.Killer.MaxHealth;
                else ev.Killer.Health += heal;
                ev.Killer.Broadcast(10,$"당신은 인간을 처치하여 체력을 <color=red>{heal}</color>만큼 회복했습니다.\n현재 HP:<color=red>{ev.Killer.Health}</color>");
            }
            
            if (_pluginInstance.Config.LastSurvivorMessageEnable)
            {
                Team playerTeam = ev.Target.Team;
                int teamLeft = 0;
                Player lastplayer = null;

                if (playerTeam == Team.CDP || playerTeam == Team.CHI)
                {
                    foreach (var player in Player.List)
                    {
                        if (player.Team == Team.CDP || player.Team == Team.CHI && player != ev.Target)
                        {
                            teamLeft++;
                            lastplayer = player;
                        }
                    }
                } 
                else if (playerTeam == Team.MTF || playerTeam == Team.RSC)
                {
                    foreach (var player in Player.List)
                    {
                        if (player.Team == Team.MTF || player.Team == Team.RSC && player != ev.Target)
                        {
                            teamLeft++;
                            lastplayer = player;
                        }
                    }
                } 
                else if (playerTeam == Team.SCP)
                {
                    foreach (var player in Player.List)
                    {
                        if (player.Team == Team.SCP && player != ev.Target)
                        {
                            teamLeft++;
                            lastplayer = player;
                        }
                    }
                }

                if (teamLeft == 1)
                    if (lastplayer != null)
                        lastplayer.Broadcast(10, "당신이 현재 진영의 <color=red>마지막 생존자</color>입니다!");
            }
        }
    }
}