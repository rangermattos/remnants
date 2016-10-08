//using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Remnants
{
    class AudioController
    {
        public List<SoundEffect> soundEffects = new List<SoundEffect>();
        private static AudioController instance;

        public static AudioController Instance
        {
            get
            {
                if (instance == null)
                    instance = new AudioController();

                return instance;
            }
        }
        public AudioController()
        {
        }

        public void LoadContent(ContentManager Content)
        {
            //song = Content.Load<Song>("In_Light_Of_Darkness");
            soundEffects.Add(Content.Load<SoundEffect>("gates_ofmydian"));
            //soundEffects.Add(Content.Load<SoundEffect>("drone_pulsing_darkness"));
        }

        public void UnloadContent()
        {

        }

        public void Update()
        {

        }

        public void Play()
        {
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(song);

            // Fire and forget play
            
            // Play that can be manipulated after the fact
            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = true;
            instance.Play();

        }
    }
}
