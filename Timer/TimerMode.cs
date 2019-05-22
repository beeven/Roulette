using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Timer
{
    public class TimerMode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsCountDown { get; set; }
        public TimeSpan TimeToAlert { get; set; }
        public string AlertSoundFilePath {get;set;}
        
        public static TimerMode[] ReadFromConfig()
        {
            if (!File.Exists("TimerConfig.json")) return new TimerMode[] { };
            var Config = JObject.Parse(File.ReadAllText("TimerConfig.json"));
            var version = Config.Value<string>("Version");
            if(version != "1") { throw new NotSupportedException("TimerConfig version is not supported."); }

            var modes = Config["TimerModes"] as JArray;
            var timerModes = modes.Select(m => m.ToObject<TimerMode>()).ToArray();
            return timerModes;
        }
    }
}
