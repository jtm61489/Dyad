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
    public class SinglePlayerSignInScreen : GameScreen
    {
        SpriteFont scoreFont;
        ContentManager content;        
        static int MaxGamers = 4;
        List<SignedInGamer> gamers = new List<SignedInGamer>();
        Texture2D isReadyTexture;
        bool[] ready = new bool[4];        
        bool SignedIn = false;        
        bool signMoreIn = false;
        Background background;

        
        // first round
        public SinglePlayerSignInScreen()
        {   
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {           

            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            scoreFont = content.Load<SpriteFont>(@"Misc\scorefont");
            isReadyTexture = content.Load<Texture2D>(@"Misc\chat_ready");
            background = new Background();
            background.LoadContent(content, @"Background\UrbanBackground");

        }

        public override void HandleInput(InputState input)
        {

            for (int i = 0; i < 4; i++)
            {                
                if ((input.LastGamePadStates[i].Buttons.X == ButtonState.Released
                    && input.CurrentGamePadStates[i].Buttons.X == ButtonState.Pressed))
                {
                    signMoreIn = true;
                }

                if (input.LastGamePadStates[i].Buttons.B == ButtonState.Released
                    && input.CurrentGamePadStates[i].Buttons.B == ButtonState.Pressed
                    )//&& !ready[i] ) && !oneReady)
                {
                    MessageBoxScreen confirmQuitMessageBox =
                        new MessageBoxScreen("Go back to Main Menu?");

                    confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

                    ScreenManager.AddScreen(confirmQuitMessageBox, null);
                }

            }

            

            foreach (SignedInGamer gamer in gamers)
            {                          

                int index2 = (int)gamer.PlayerIndex;


                if (input.LastGamePadStates[index2].Buttons.A == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.A == ButtonState.Pressed)
                {
                    ready[index2] = true;
                }
                

                if (input.LastGamePadStates[index2].Buttons.A == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.A == ButtonState.Pressed
                    && ready[index2])
                {
                    LoadingScreen.Load(ScreenManager, true, null, gamers, gamer.PlayerIndex,
                                     new BackgroundScreen(@"Background\UrbanBackground"), new SingleplayerLevelScreen(ScreenManager.GetNormalLevels()));
                    break;
                }
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            SignIn();

            if (SignedIn)
            {
                gamers = new List<SignedInGamer>();
                gamers = ChooseGamers();
            }            

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Guide.IsVisible)
            {
                Vector2 position = new Vector2(100, 150);

                // Make the lobby slide into place during transitions.
                float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                spriteBatch.Begin();

                background.Draw(spriteBatch);

                spriteBatch.DrawString(scoreFont, "Single Player", new Vector2(600, 100), Color.White);
                spriteBatch.DrawString(scoreFont, "Press X to sign in", new Vector2(600, 600), Color.White);

                foreach (SignedInGamer gamer in gamers)
                {                    
                    DrawGamer(gamer, position);
                    position.Y += 100;
                }

                spriteBatch.End();
            }
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        public List<SignedInGamer> ChooseGamers()
        {

            // Check whether any other profiles should also be included.
            foreach (SignedInGamer gamer in Gamer.SignedInGamers)
            {
                // Never include more profiles than the MaxLocalGamers constant.
                if (gamers.Count >= MaxGamers)
                    break;

                gamers.Add(gamer);

            }

            return gamers;
        }

        void DrawGamer(SignedInGamer gamer, Vector2 position)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            Vector2 iconWidth = new Vector2(34, 0);
            Vector2 iconOffset = new Vector2(0, 12);

            Vector2 iconPosition = position + iconOffset;

            // Draw the "is ready" icon.
            if (ready[(int)gamer.PlayerIndex])
                spriteBatch.Draw(isReadyTexture, iconPosition,
                                 FadeAlphaDuringTransition(Color.Lime));

            iconPosition += iconWidth;

            string text = gamer.Gamertag;

            Color color = Color.White;

            Vector2 drawLength = new Vector2(text.Length, 0);

            string name;
            if (text.Length > 8)
                name = text.Substring(0, 8);
            else
                name = text;

            //name
            spriteBatch.DrawString(font, name, position + iconWidth * 2,
                                   FadeAlphaDuringTransition(color));

            spriteBatch.DrawString(scoreFont, "Press A to select character", new Vector2(600, 550), Color.White);

            // icon
           // spriteBatch.Draw(icons[iconsStates[(int)gamer.PlayerIndex]], position + iconWidth * 8, Color.White);

        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(@"Background\HomeScreen"),
                                                           new MainMenuScreen());
        }

        /// <summary>
        /// Helper modifies a color to fade its alpha value during screen transitions.
        /// </summary>
        Color FadeAlphaDuringTransition(Color color)
        {
            return new Color(color.R, color.G, color.B, TransitionAlpha);
        }

        void SignIn()
        {

            if (Gamer.SignedInGamers.Count == 0)
            {
                if (!Guide.IsVisible)
                    Guide.ShowSignIn(4, false);
                SignedIn = false;
            }

            else if (signMoreIn)
            {
                if (!Guide.IsVisible)
                    Guide.ShowSignIn(4, false);
                SignedIn = false;
                signMoreIn = false;
            }

            int count = 1;
            foreach (SignedInGamer gamer in Gamer.SignedInGamers)
            {
                if (gamer != null)
                    count--;
            }

            if (count > 0)
            {
                SignedIn = false;
                if (!Guide.IsVisible)
                    Guide.ShowSignIn(1, false);
            }
            else
                SignedIn = true;
        }
    }
}