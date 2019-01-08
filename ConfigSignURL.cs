using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;


namespace SignURL
{
    public class ConfigSignURL : IRocketPluginConfiguration
    {

        public string DefaultDesc;
        public int MaxDistance;

        public void LoadDefaults()
        {
            DefaultDesc = "Open the url to visit the webpage";
            MaxDistance = 5;
        }

    }

}
