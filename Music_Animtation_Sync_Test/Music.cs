using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Music_Animtation_Sync_Test.Models;

namespace Music_Animtation_Sync_Test
{
    //Going to handle playing/pausing/etc here
    public static class Music
    {
        //TEMPO = BPM (Beats per minute)
        //or beats per 60 seconds = 60000 ms
        //so 60 BPM = 1 Beat per second = 1 Beat per 1000ms

        //jumper = 160 BPM


        private static SoundEffectInstance musicInstance;
        private static float beat_per_ms; //tempo of music
        private static bool beats_reset;

        public static Dictionary<Beat, BeatData> BeatCollection; //collection of beat data

        public static void Initialize(ContentManager content)
        {
            SoundEffect se = content.Load<SoundEffect>("Music/Jumper");
            musicInstance = se.CreateInstance();
            musicInstance.Volume = 1f;

            var BPM = 185;
            beat_per_ms = 60000f / BPM;
            BeatCollection = new Dictionary<Beat, BeatData>()
            {
                { Beat.On, new BeatData(0) },
                { Beat.Off, new BeatData(beat_per_ms) }
            };
        }

        public static void Play()
        {
            if(musicInstance.State != SoundState.Playing)
            {
                musicInstance.Play();
                beats_reset = false;
            }
        }

        /// <summary>
        /// Used to track the beats
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(float gameTime)
        {
            for(int i = 0; i < BeatCollection.Count(); i++)
            {
                BeatCollection.ElementAt(i).Value.Beat = false;

                if (musicInstance.State == SoundState.Playing)
                {
                    BeatCollection.ElementAt(i).Value.Counter += gameTime;

                    if (BeatCollection.ElementAt(i).Value.Counter >= beat_per_ms)
                    {
                        BeatCollection.ElementAt(i).Value.Counter -= beat_per_ms;
                        BeatCollection.ElementAt(i).Value.Beat = true;
                    }
                }
            }

            if (musicInstance.State == SoundState.Stopped && !beats_reset)
            {
                for (int i = 0; i < BeatCollection.Count(); i++)
                {
                    BeatCollection.ElementAt(i).Value.ResetBeat();
                }
                beats_reset = true;
            }
        }

    }

    public class BeatData
    {
        private float offset;
        public BeatData(float offset)
        {
            this.offset = offset;
            ResetBeat();
        }
        public void ResetBeat()
        {
            Counter = -offset;
            Beat = false;
        }
        public bool Beat { get; set; }
        public float Counter { get; set; }
    }
}