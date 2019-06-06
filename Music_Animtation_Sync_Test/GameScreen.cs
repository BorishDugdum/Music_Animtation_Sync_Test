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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Music_Animtation_Sync_Test.Models;

namespace Music_Animtation_Sync_Test
{
    //handle all game logic here
    public static class GameScreen
    {
        private static GraphicsDevice gDevice;
        private static SpriteBatch spriteBatch;
        private static RenderTarget2D renderTarget;
        private static Random rand;
        private static List<Clarpy> clarpies;
        private static Point nativeScreen;

        public static void Initialize(ContentManager content, GraphicsDevice gD, Point nS)
        {
            //want to set a new render target based on the native size of the screen where we want to draw
            nativeScreen = nS;
            gDevice = gD;
            spriteBatch = new SpriteBatch(gDevice);
            renderTarget = new RenderTarget2D(gDevice, nativeScreen.X, nativeScreen.Y);

            rand = new Random();
            clarpies = new List<Clarpy>();
            var clarpy = content.Load<Texture2D>("Clarpies/Clarpy Did It");

            Vector2 pos;


            //left clarpy
            pos = new Vector2(16, nativeScreen.Y - 64); //manually center clarpy for now
            clarpies.Add(new Clarpy(clarpy, pos, DanceSpeed.Normal, Beat.Off));


            //speedy left clarpy
            pos = new Vector2(64, nativeScreen.Y - 64); //manually center clarpy for now
            clarpies.Add(new Clarpy(clarpy, pos, DanceSpeed.Fast, Beat.On, 64));


            //center clarpy
            pos = new Vector2(nativeScreen.X / 2 - 32, nativeScreen.Y - 64); //manually center clarpy for now
            clarpies.Add(new Clarpy(clarpy, pos, DanceSpeed.Normal, Beat.On));


            //speedy right clarpy
            pos = new Vector2(nativeScreen.X - 16 - 128, nativeScreen.Y - 64); //manually center clarpy for now
            clarpies.Add(new Clarpy(clarpy, pos, DanceSpeed.Fast, Beat.On, 64));


            //right clarpy
            pos = new Vector2(nativeScreen.X  - 16 - 64, nativeScreen.Y - 64); //manually center clarpy for now
            clarpies.Add(new Clarpy(clarpy, pos, DanceSpeed.Normal, Beat.Off));


            //set up the music
            Music.Initialize(content);
        }
        public static void Update(float gameTime)
        {
            //update inputs
            InputManager.Update();

            //if key pressed - play music
            if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space) ||
                InputManager.IsTouched())
            {
                foreach (var clarpy in clarpies)
                    clarpy.Reset();
                Music.Play();
            }

            //Update Music
            Music.Update(gameTime);

            foreach (var clarpy in clarpies)
                clarpy.Update(gameTime, Music.BeatCollection);
        }

        public static Texture2D Draw(float gameTime)
        {
            gDevice.SetRenderTarget(renderTarget);
            gDevice.Clear(Color.Red);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            foreach(var clarpy in clarpies)
            {
                clarpy.Draw(spriteBatch);
            }
            spriteBatch.End();

            gDevice.SetRenderTarget(null); //allow us to remove the target from the device and pass it

            return renderTarget;
        }
    }
}