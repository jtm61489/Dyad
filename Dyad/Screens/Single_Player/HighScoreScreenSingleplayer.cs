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
    class HighScoreScreenSingleplayer : GameScreen
    {
        SpriteFont scoreFont;
        ContentManager content;        
        List<FileHandler.SaveGameData> list = new List<FileHandler.SaveGameData>();
        float sliding = 0;

        public HighScoreScreenSingleplayer()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);            
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

            spriteBatch.DrawString(scoreFont, "Level 1     Level2     Level3     Level4     Level5     Level7      Level9",
                    new Vector2(400 - sliding, 200), Color.DarkBlue);            
            

            for(int i = 0; i < list.Count; i++)
            {
                string name;
                if (list[i].Name.Length > 8)
                    name = list[i].Name.Substring(0, 8);
                else
                    name = list[i].Name;
                spriteBatch.DrawString(scoreFont, name,
                    new Vector2(200 - sliding, 250 + (50 * i)), Color.DarkBlue);

                int skipper = 0;

                for (int k = 0; k < list[i].times.Count; k++)
                {
                    if (list[i].times[k] != 9999 && k != 5 && k != 7 && k!= 9)
                    {
                        spriteBatch.DrawString(scoreFont, "" + list[i].times[k],
                            new Vector2(400 + (160 * (k - skipper)) - sliding, 250 + (50 * i)), Color.DarkBlue);
                    }
                    else if (k != 5 && k != 7 && k != 9)
                    {
                        spriteBatch.DrawString(scoreFont, "" + "????",
                            new Vector2(400 + (160 * (k - skipper)) - sliding, 250 + (50 * i)), Color.DarkBlue);
                    }
                    else
                    {
                        skipper++;
                    }
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
                                                               new SinglePlayerMenuScreen());
                }

                float thumbstick = input.CurrentGamePadStates[i].ThumbSticks.Left.X;

                if (thumbstick != 0)
                {
                    sliding += thumbstick * 10;
                    sliding = MathHelper.Clamp(sliding, 0, 1000);
                }
            }
        }
    }
}