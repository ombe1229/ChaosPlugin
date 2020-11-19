using Exiled.API.Interfaces;
using System.ComponentModel;
using Log = Exiled.API.Features.Log;

namespace ChaosPlugin
{
    public class Configs : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        [Description("Should welcome message be shwon?")]
        public bool WelcomeMessageEnable { get; private set; } = true;
        
        [Description("Should SCP Leave message be shown?")]
        public bool ScpLeftMessageEnable { get; private set; } = true;
        
        [Description("Should Cuffedkill message be shown?")]
        public bool CuffedkillMessageEnable { get; private set; } = true;
        
        [Description("Should last survivor message be shown?")]
        public bool LastSurvivorMessageEnable { get; private set; } = true;

        [Description("Should SCP list message be shown?")]
        public bool ScpListMessageEnable { get; private set; } = true;

        [Description("Use SCP Healing?")]
        public bool ScpHealingEnable { get; private set; } = true;
    }
}