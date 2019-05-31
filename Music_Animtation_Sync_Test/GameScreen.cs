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
            //var pp = new PresentationParameters();
            //pp.BackBufferWidth = 320;
            //pp.BackBufferHeight = 180;
            //gDevice = new GraphicsDevice(gD.Adapter, GraphicsProfile.HiDef, pp);
            nativeScreen = nS;

            gDevice = gD;
            spriteBatch = new SpriteBatch(gDevice);
            renderTarget = new RenderTarget2D(gDevice, nativeScreen.X, nativeScreen.Y);

            rand = new Random();
            clarpies = new List<Clarpy>();
            var clarpy = content.Load<Texture2D>("Clarpies/Clarpy Did It");
            var pos = new Vector2(0, 0);
            clarpies.Add(new Clarpy(clarpy, pos, DanceSpeed.Normal, Beat.On));
            
        }
        public static void Update(float gameTime)
        {
            foreach (var clarpy in clarpies)
                clarpy.Update(gameTime);
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