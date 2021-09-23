using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

#if NETFRAMEWORK
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#else
using System.Text.Json;
#endif

namespace Timer
{
    public class TimerMode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsCountDown { get; set; }
        public TimeSpan TimeToAlert { get; set; }
        public string AlertSoundFilePath { get; set; }

        public static TimerMode[] ReadFromConfig()
        {
            if (!File.Exists("TimerConfig.json")) return new TimerMode[] { };
#if NETFRAMEWORK
            var Config = JObject.Parse(File.ReadAllText("TimerConfig.json"));
            var version = Config.Value<string>("Version");
            if(version != "1") { throw new NotSupportedException("TimerConfig version is not supported."); }

            var modes = Config["TimerModes"] as JArray;
            var timerModes = modes.Select(m => m.ToObject<TimerMode>()).ToArray();
#else


            var config = JsonSerializer.Deserialize<TimerModeConfig>(File.ReadAllText("TimerConfig.json").Trim(), new JsonSerializerOptions() { AllowTrailingCommas = true });
            var timerModes = config.TimerModes.Select(x => (TimerMode)x).ToArray();
#endif

            return timerModes;
        }
    }

#if !NETFRAMEWORK
    public class TimerModeConfig
    {
        public string Version { get; set; }
        public List<TimerModeLiteral> TimerModes { get; set; }
    }

    public class TimerModeLiteral
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public bool IsCountDown { get; set; }
        public string TimeToAlert { get; set; }
        public string AlertSoundFilePath { get; set; }

        public static explicit operator TimerMode(TimerModeLiteral timerModeLiteral)
        {
            return new TimerMode
            {
                Name = timerModeLiteral.Name,
                Description = timerModeLiteral.Description,
                IsCountDown = timerModeLiteral.IsCountDown,
                TimeToAlert = TimeSpan.Parse(timerModeLiteral.TimeToAlert),
                Duration = TimeSpan.Parse(timerModeLiteral.Duration),
                AlertSoundFilePath = timerModeLiteral.AlertSoundFilePath
            };
        }
    }
#endif
}
