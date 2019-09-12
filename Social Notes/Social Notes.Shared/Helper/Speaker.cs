using System;
using System.Collections.Generic;
using System.Text;

using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace Social_Notes.Helper
{
    class Speaker
    {
        private static MediaElement speak;
        SpeechSynthesizer TTs;
        /// <summary>
        /// Crea una instancia para reproducir Texto a voz
        /// </summary>
        public Speaker()
        {
            TTs = new SpeechSynthesizer();
            if (speak == null) speak = new MediaElement();
            // Busca idioma español
            foreach (var voz in SpeechSynthesizer.AllVoices)
            {
                if (voz.Language.Equals("es-MX") && voz.Gender == VoiceGender.Female)
                {
                    this.setVoice(voz);
                }
            }
          
        }
        /// <summary>
        /// Envia Texto a reproducir por voz
        /// </summary>
        /// <param name="Text">Texto a reproducir</param>
        public async void SpeakMessage(string Text)
        {
            if (speak.CurrentState != Windows.UI.Xaml.Media.MediaElementState.Stopped)
                speak.Stop();

            SpeechSynthesisStream stream = await TTs.SynthesizeTextToStreamAsync(Text);
            speak.SetSource(stream, stream.ContentType);
            speak.Play();
        }
        /// <summary>
        /// Cambia de Voz
        /// </summary>
        /// <param name="voz">Nueva voz a poner</param>
        public  void setVoice(VoiceInformation voz){
            TTs.Voice = voz;
        }
        /// <summary>
        /// Get Voice
        /// </summary>
        /// <returns>voice</returns>
        public VoiceInformation GetVoice()
        {
            return TTs.Voice;
        }

        public void Dispose()
        {
            speak = null;
            TTs.Dispose();
            TTs = null;
        }
    }
}
