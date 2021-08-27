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
    public class MultiplayerSignInScreen : GameScreen
    {
        PlayerIndex index;
        SpriteFont scoreFont;
        ContentManager content;        
        static int MaxGamers = 4;
        List<SignedInGamer> gamers = new List<SignedInGamer>();
        Texture2D isReadyTexture;
        bool[] ready = new bool[4];
        bool allReady;
        bool SignedIn = false;
        List<Texture2D> icons = new List<Texture2D>();
        List<int> iconsStates = new List<int>();
        bool signMoreIn = false;
        Background background;

        List<Color> colors = new List<Color>();
        List<int> whichColor = new List<int>();

        public struct RoundStats
        {
            public string name;
            public int score;
        }
        
        int gameType;

        // first round
        public MultiplayerSignInScreen(PlayerIndex e, int type)
        {
            index = e;            
            gameType = type;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            scoreFont = content.Load<SpriteFont>(@"Misc\scorefont");
            isReadyTexture = content.Load<Texture2D>(@"Misc\chat_ready");


            // load all textures            
            icons.Add(content.Load<Texture2D>(@"Fox\RedFoxMP"));
            icons.Add(content.Load<Texture2D>(@"Cat\GreenCatMP"));
            icons.Add(content.Load<Texture2D>(@"Squirrel\BlueSquirrel"));            
            icons.Add(content.Load<Texture2D>(@"Reindeer\Reindeer"));

            background = new Background();
            background.LoadContent(content, @"Background\UrbanBackground");

            //default icon states for each player
            
            for (int i = 0; i < 4; i++)
            {
                iconsStates.Add(i);
                whichColor.Add(i);
            }
            
            colors.Add(Color.Red);
            colors.Add(Color.Blue);
            colors.Add(Color.Green); 
            colors.Add(Color.Gold);
            colors.Add(Color.Brown);            
            colors.Add(Color.Orange);
            colors.Add(Color.Purple);
            colors.Add(Color.Teal);
            colors.Add(Color.Coral);


        }

        public override void HandleInput(InputState input)
        {

            foreach (SignedInGamer gamer in gamers)
            {
                int index2 = (int)gamer.PlayerIndex;

                if ((input.LastGamePadStates[0].Buttons.X == ButtonState.Released
                    && input.CurrentGamePadStates[0].Buttons.X == ButtonState.Pressed) || (
                    input.LastGamePadStates[1].Buttons.X == ButtonState.Released
                    && input.CurrentGamePadStates[1].Buttons.X == ButtonState.Pressed) || (
                    input.LastGamePadStates[2].Buttons.X == ButtonState.Released
                    && input.CurrentGamePadStates[2].Buttons.X == ButtonState.Pressed) || (
                    input.LastGamePadStates[3].Buttons.X == ButtonState.Released
                    && input.CurrentGamePadStates[3].Buttons.X == ButtonState.Pressed))
                {
                    signMoreIn = true;
                }

                if (input.LastGamePadStates[index2].Buttons.A == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.A == ButtonState.Pressed)
                {
                    ready[index2] = true;
                }

                if (input.LastGamePadStates[index2].Buttons.Y == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.Y == ButtonState.Pressed
                    && !ready[index2])
                {
                    List<int> indexes = new List<int>();
                    foreach (SignedInGamer gamer2 in gamers)
                    {
                        indexes.Add((int)gamer2.PlayerIndex);
                    }

                    bool done = false;
                    while (!done)
                    {
                        whichColor[index2]++;
                        if (whichColor[index2] == colors.Count)
                        {
                            whichColor[index2] = 0;
                        }
                        done = true;
                        for (int i = 0; i < whichColor.Count; i++)
                        {
                            if (i != index2 && indexes.Contains(i) && whichColor[index2] == whichColor[i])
                            {
                                done = false;
                                break;
                            }
                        }                        
                    }
                }

                if (input.LastGamePadStates[index2].Buttons.B == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.B == ButtonState.Pressed
                    && ready[index2])
                {
                    ready[index2] = false;
                }

                // exit with B
                else if (input.LastGamePadStates[index2].Buttons.B == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.B == ButtonState.Pressed
                    && !ready[index2])
                {
                    MessageBoxScreen confirmQuitMessageBox =
                        new MessageBoxScreen("Go back to Main Menu?");

                    confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

                    ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer, null, index);
                }

                if (input.LastGamePadStates[index2].Buttons.A == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.A == ButtonState.Pressed
                    && allReady)
                {
                    List<Color> c = new List<Color>();
                    for (int i = 0; i < whichColor.Count; i++)
                    {
                        c.Add(colors[whichColor[i]]);
                    }
                    allReady = false;

                    if (gamers.Count < 2)
                    {
                        MessageBoxScreen confirmQuitMessageBox =
                       new MessageBoxScreen("Need at least 2 players\nPress X to sign in another player", false);

                        ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer, null, index);
                    }

                    else
                    {
                        LoadingScreen.Load(ScreenManager, true, null, gamers, index,
                                 new BackgroundScreen(@"Background\UrbanBackground"), new MultiplayerLevelScreen(ScreenManager.GetMultiLevels(), icons, iconsStates, c));
                    }
                }                

                if ((input.CurrentGamePadStates[index2].DPad.Right == ButtonState.Released
                    && input.LastGamePadStates[index2].DPad.Right == ButtonState.Pressed
                    || (input.CurrentGamePadStates[index2].ThumbSticks.Left.X > 0
                    && input.LastGamePadStates[index2].ThumbSticks.Left.X == 0))
                    && iconsStates[index2] < icons.Count - 1
                    && !ready[index2])
                {
                    iconsStates[index2]++;
                }

                if ((input.CurrentGamePadStates[index2].DPad.Left == ButtonState.Released
                    && input.LastGamePadStates[index2].DPad.Left == ButtonState.Pressed
                    || (input.CurrentGamePadStates[index2].ThumbSticks.Left.X < 0
                    && input.LastGamePadStates[index2].ThumbSticks.Left.X == 0))
                    && iconsStates[index2] > 0
                    && !ready[index2])
                {
                    iconsStates[index2]--;
                }
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            SignIn();

            if (SignedIn)
            {
                gamers = new List<SignedInGamer>();
                gamers = ChooseGamers(index);
                
                SignedIn = false;
            }

            foreach (SignedInGamer gamer in Gamer.SignedInGamers)
            {
                if (!ready[(int)gamer.PlayerIndex])
                {
                    allReady = false;
                    break;
                }
                allReady = true;
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

                spriteBatch.DrawString(scoreFont, "Multiplayer", new Vector2(600, 100), Color.White);
                spriteBatch.DrawString(scoreFont, "Move left and right to change character \nPress A to select character \nPress X to sign in \nPress Y to change color", new Vector2(600, 550), Color.White);
                foreach (SignedInGamer gamer in gamers)
                {
                    if (gameType == 0 && index == gamer.PlayerIndex)
                    {
                        DrawGamer(gamer, position);
                    }
                    else if (gameType != 0)
                    {
                        DrawGamer(gamer, position);
                        position.Y += 100;
                    }
                }

                spriteBatch.End();
            }
        }

        public List<SignedInGamer> ChooseGamers(PlayerIndex playerIndex)
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

            // icon
            spriteBatch.Draw(icons[iconsStates[(int)gamer.PlayerIndex]], position + iconWidth * 8, colors[whichColor[(int)gamer.PlayerIndex]]);

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

            if (Gamer.SignedInGamers[index] == null)
            {
                if (!Guide.IsVisible)
                    Guide.ShowSignIn(4, false);
                SignedIn = false;
                iconsStates.Clear();
                whichColor.Clear();
                for (int i = 0; i < 4; i++)
                {
                    iconsStates.Add(i);
                    whichColor.Add(i);
                }
            }

            else if (signMoreIn)
            {
                if (!Guide.IsVisible)
                    Guide.ShowSignIn(4, false);
                SignedIn = false;
                signMoreIn = false;
                iconsStates.Clear();
                whichColor.Clear();
                for (int i = 0; i < 4; i++)
                {
                    iconsStates.Add(i);
                    whichColor.Add(i);
                }
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
                    Guide.ShowSignIn(4, false);
                iconsStates.Clear();
                whichColor.Clear();
                for (int i = 0; i < 4; i++)
                {
                    iconsStates.Add(i);
                    whichColor.Add(i);
                }
            }
            else
                SignedIn = true;
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }
    }
}