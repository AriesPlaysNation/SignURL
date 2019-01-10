using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Plugins;
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
        
        public static SignURL Instance { get; set; }

        protected override void Load()
        {

            //logger message upon loading server
            base.Load();

            Instance = this;

            UnturnedPlayerEvents.OnPlayerUpdateGesture += OnUpdatedGesture;

            Logger.LogWarning("\n Loading SignURL, made by AriesPlaysNation, supported and supplied by Mr.Kwabs!");
            Logger.LogWarning($"\n Default URL Description: {Instance.Configuration.Instance.DefaultDesc}");
            Logger.LogWarning("\n Successfully loaded SignURL, made by AriesPlaysNation, supported and supplied by Mr.Kwabs!");
        }

        protected override void Unload()
        {

            //Upon shutdown closes instance
            
            UnturnedPlayerEvents.OnPlayerUpdateGesture -= OnUpdatedGesture;
            Instance = null;
            base.Unload();

        }

        //assigns uCaller and Gester
        public void OnUpdatedGesture(UnturnedPlayer uCaller, UnturnedPlayerEvents.PlayerGesture Gesture)
        {

            //assigns website upon punching the sign
            //attempting to convert text from sign into link to website! **Having Issues**
            if (Gesture == UnturnedPlayerEvents.PlayerGesture.PunchLeft)
            {

                Transform Raycast = GetRaycast(uCaller);

                if (Raycast == null)
                {
                    Logger.LogError("RayCast returned null");
                    return;
                }

                if (Raycast.GetComponent<InteractableSign>() != null)
                {
                    ManageSign(uCaller, Raycast.GetComponent<InteractableSign>());
                }
                else
                {
                    Logger.LogError("No sign in front of user. Please Contact me at bradbotteron13@gmail.com or my discord for help!");
                    return;
                }

            }

        }

        private void ManageSign(UnturnedPlayer uPlayer, InteractableSign Sign)
        {

            if (Sign.text == null || Sign.text == "" || !Sign.text.Contains("*") || !uPlayer.HasPermission("signurl")) { return; }

            string URL = Sign.text.Split('*', '*')[1].ToString();

            uPlayer.Player.sendBrowserRequest(Instance.Configuration.Instance.DefaultDesc, URL);
        }
            private Transform GetRaycast(UnturnedPlayer uPlayer)
            {
                if (Physics.Raycast(uPlayer.Player.look.aim.position, uPlayer.Player.look.aim.forward, out RaycastHit RayHit, Instance.Configuration.Instance.MaxDistance, RayMasks.BARRICADE_INTERACT))
                {
                    return RayHit.transform;
                }
                return null;
            }
        

    }
}
