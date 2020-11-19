using System;
using System.Security.Permissions;
using UnityEngine;
using Log = Exiled.API.Features.Log;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;
using MapEvents = Exiled.Events.Handlers.Map;
using WarheadEvents = Exiled.Events.Handlers.Warhead;
using Features = Exiled.API.Features;

namespace ChaosPlugin
{
    public class chaosPlugin : Features.Plugin<Configs>
    {
        public static bool IsStarted { get; set; }
        public EventHandlers EventHandlers { get; private set; }
        
        public ConsoleCommands PlayerConsolecommands { get; private set; }

        public void LoadEvents()
        {
            PlayerEvents.Joined += EventHandlers.OnJoined;
            PlayerEvents.Spawning += EventHandlers.OnSpawning;
            PlayerEvents.Died += EventHandlers.OnPlayerDeath;
            PlayerEvents.Left += EventHandlers.OnScpLeave;
        }

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;
            EventHandlers = new EventHandlers(this);
            PlayerConsolecommands = new ConsoleCommands(this);
            LoadEvents();
            ServerEvents.SendingConsoleCommand += PlayerConsolecommands.OnConsoleCommand;
            Log.Info("ChaosPlugin Enabled.");
        }

        public override void OnDisabled()
        {
            PlayerEvents.Joined -= EventHandlers.OnJoined;
            PlayerEvents.Spawning -= EventHandlers.OnSpawning;
            PlayerEvents.Died -= EventHandlers.OnPlayerDeath;
            PlayerEvents.Left -= EventHandlers.OnScpLeave;
        }

        public override void OnReloaded()
        {
            
        }
    }
}