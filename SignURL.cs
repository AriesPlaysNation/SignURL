using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.IO;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SignURL
{
    public class SignURL : RocketPlugin<ConfigSignURL>
    {
        private string defaultdesc;

        public static SignURL Instance { get; set; }

        protected override void Load()
        {

            //logger message upon loading server
            base.Load();

            Instance = this;

            Logger.LogWarning("\n Loading SignURL, made by AriesPlaysNation, supported and supplied by Mr.Kwabs!");
            Logger.LogWarning($"\n Default URL Description: {Instance.Configuration.Instance.DefaultDesc}");
            Logger.LogWarning("\n Successfully loaded SignURL, made by AriesPlaysNation, supported and supplied by Mr.Kwabs!");
        }

        protected override void Unload()
        {

            //Upon shutdown closes instance
            //UnturnedPlayerEvents.OnPlayerUpdateGesture += OnUpdatedGesture; -> Not needed atm
            Instance = null;
            base.Unload();

        }

        //assigns uCaller and Gester
        public void OnUpdatedGesture(UnturnedPlayer uCaller, UnturnedPlayerEvents.PlayerGesture Gesture, InteractableSign Sign, UnturnedPlayer uPlayer, string description)
        {

            //assigns website upon punching the sign
            //attempting to convert text from sign into link to website! **Having Issues**
            if (Gesture == UnturnedPlayerEvents.PlayerGesture.PunchLeft && Sign.text.Contains('*') && uPlayer.HasPermission("signurl"))
            {

                description = defaultdesc;
                //string[] url; ----> FUDGING DURR! WHY AM I DECLARING TWICE :(
                string url = Sign.text.Split('*', '*')[1].ToString();
                uCaller.Player.sendBrowserRequest(description, url);

                return;
                
            }
            else
            {
                Logger.LogError("Something went WAY wrong. Please contact me at bradbotteron13@gmail.com or on my discord!");
                return;
            }
            
        }
        
    }
}
