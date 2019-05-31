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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Music_Animtation_Sync_Test.Models
{
    public enum DanceSpeed
    {
        Slow = 0,
        Normal = 1,
        Fast = 2 //both on and off beat (hits every beat)
    }
    public enum Beat
    {
        Off = 0,
        On = 1
    }

    public class Clarpy
    {
        Texture2D img;
        Vector2 pos;
        Dictionary<int, float> ms_per_frame;
        float animation_counter = 0;
        int frames_per_animation = 4;

        Point currentFrame;
        Rectangle sourceRect;

        public int Width { get { return sourceRect.Width; } }
        public int Height { get { return sourceRect.Height; } }

        public Clarpy(Texture2D image, Vector2 position, DanceSpeed danceSpeed, Beat beat)
        {
            img = image;
            pos = position;

            //set up key frames here
            ms_per_frame = new Dictionary<int, float>()
            {
                //current frame, ms_per_frame
                { 0, 90 },
                { 1, 90 },
                { 2, 120 },
                { 3, 140 }
            };

            //we use sourcerect to animate the spritesheet
            currentFrame = new Point(0, 0);
            sourceRect = new Rectangle(0, 0, img.Width / frames_per_animation, img.Height);
        }

        public void Update(float gameTime)
        {
            animation_counter += gameTime;
            if(animation_counter >= ms_per_frame[currentFrame.X]) //then we go to next frame!
            {
                animation_counter -= ms_per_frame[currentFrame.X];
                if (animation_counter < 0)
                    animation_counter = 0;

                NextFrame();
            }

            //will use this later for screen adjustments//
            //new Vector2(MathHelper.Clamp(rand.Next(nativeScreen.X - clarpy.Width), 0, nativeScreen.X - clarpy.Width), nativeScreen.Y - clarpy.Height), Color.White);
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(img, pos, sourceRect, Color.White); 
        }

        private void NextFrame()
        {
            currentFrame.X++;
            sourceRect.X = sourceRect.Width * currentFrame.X;
            
            //let's check to make sure we aren't going out of bounds - if so, then start over
            if(sourceRect.X >= img.Width)
            {
                currentFrame.X = 0;
                sourceRect.X = 0;
            }
        }
    }
}