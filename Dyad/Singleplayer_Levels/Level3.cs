using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace Dyad
{
    class Level3 : GameplayScreen
    {
        public Level3()
            : base(2, 1)
        {
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D CLC = content.Load<Texture2D>(@"Misc\CatLevelCollision");

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(50, 50), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(110, 590), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(100, 100), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(1200, 600), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\CatLevelBackground");
            backgrounds.Add(background);

            allMoveableObjs.Add(new Terrain(new Vector2(screenRect.Center.X, screenRect.Center.Y)
                , CLC, 0, 1f, true, true));

            forground = new Forground();
            forground.LoadContent(content, @"Forground\CatLevelForeground");

            camera = new Camera(new Vector2(0, 0), 0, 720, 1280, 0);
            camera.UpdateX(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs, Vector2.Zero, Vector2.Zero);
            camera.UpdateY(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs);


            foreach (Moveable obj in allMoveableObjs)
            {
                obj.LoadContent(content);
            }

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (Background background in backgrounds)
            {
                background.Draw(spriteBatch);
            }

            if (gameType == 1)
            {
                spriteBatch.Draw(timerBox, new Vector2(601, 40), Color.White);
                spriteBatch.DrawString(gameFont, "" + (int)timer, new Vector2(640, 50), Color.DarkRed);
            }

            if (goal1 != null)
                goal1.Draw(spriteBatch);
            if (goal2 != null)
                goal2.Draw(spriteBatch);

            // wont draw any terrain objects!!!!!!!!!!!!
            foreach (Moveable obj in allMoveableObjs)
            {
                if (obj.GetRect().Intersects(screenRect) && !(obj is Terrain))
                    obj.Draw(spriteBatch);
            }

            if (forground != null)
                forground.Draw(spriteBatch);

            if (camera.GetCameraType() == 2 && !camera.GetCameraDirection())
            {
                spriteBatch.Draw(spikes, new Vector2(0, 0), Color.White);
            }

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

    }
}
