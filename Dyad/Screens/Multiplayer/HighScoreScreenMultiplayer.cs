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
    class HighScoreScreenMultiplayer : GameScreen
    {
        SpriteFont scoreFont;
        ContentManager content;
        List<FileHandler.SaveGameData> list = new List<FileHandler.SaveGameData>();

        public HighScoreScreenMultiplayer()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            //FileHandler.Load();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            list = FileHandler.GetAllData();

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            scoreFont = content.Load<SpriteFont>(@"Misc\scorefont");
        }

        public override void UnloadContent()
        {            
            content.Unload();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            spriteBatch.DrawString(scoreFont, "Level 1     Level2     Level3     Level4",
                    new Vector2(400, 200), Color.DarkBlue);

            foreach (FileHandler.SaveGameData data in list)
            {
                if (data.scores.Count > 4)
                {
                    data.scores.RemoveRange(4, data.scores.Count - 2);                      
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                string name;
                if (list[i].Name.Length > 8)
                    name = list[i].Name.Substring(0, 8);
                else
                    name = list[i].Name;
                spriteBatch.DrawString(scoreFont, name,
                    new Vector2(200, 250 + (50 * i)), Color.DarkBlue);

                for (int k = 0; k < list[i].scores.Count; k++)
                {
                    spriteBatch.DrawString(scoreFont, "" + list[i].scores[k],
                        new Vector2(400 + (160 * k), 250 + (50 * i)), Color.DarkBlue);
                }   
                

            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void HandleInput(InputState input)
        {

            for (int i = 0; i < 4; i++)
            {
                if (input.LastGamePadStates[i].Buttons.B == ButtonState.Released
                    && input.CurrentGamePadStates[i].Buttons.B == ButtonState.Pressed)
                {
                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(@"Background\HomeScreen"),
                                                               new MultiPlayerMenuScreen());
                }
            }
        }
    }
}