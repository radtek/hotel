using System;
using DotNetSpeech;
using log4net;

namespace CheckIn.AddDll
{
    public static class Common
    {
        private static readonly ILog Log = LogManager.GetLogger("Common");
        private const SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        private static SpVoice Sp = new SpVoice();

        public static SpVoice GetSpVoice()
        {
            return Sp;
        }

        public static void Speak(string str)
        {
            try
            {
                Sp.Pause();
                Sp = new SpVoiceClass();
                Sp.Speak(str, SpFlags);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
