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
    class MultiplayerScoreScreen : GameScreen
    {
        PlayerIndex index;
        SpriteFont scoreFont;
        ContentManager content;
        
        List<SignedInGamer> gamers = new List<SignedInGamer>();
        Texture2D isReadyTexture;
        bool[] ready = new bool[4];
        
        List<Texture2D> icons = new List<Texture2D>();
        List<int> iconsStates = new List<int>();
        List<Color> colors = new List<Color>();

        List<Moveable> players = new List<Moveable>();

        Background background;

        public MultiplayerScoreScreen()
        {
        }

        // first round
        public MultiplayerScoreScreen(PlayerIndex e, List<Moveable> pls, List<Texture2D> icons, List<int> iconsStates, List<Color> colors)
        {
            players = pls;
            index = e;
            this.icons = icons;
            this.iconsStates = iconsStates;
            this.colors = colors;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            this.gamers = gamers;

            background = new Background();
            background.LoadContent(content, @"Background\GameOver");

            scoreFont = content.Load<SpriteFont>(@"Misc\scorefont");
            isReadyTexture = content.Load<Texture2D>(@"Misc\chat_ready");


            // load all textures            
            icons.Add(content.Load<Texture2D>(@"Fox\RedFox"));
            icons.Add(content.Load<Texture2D>(@"Cat\GreenCat"));
            icons.Add(content.Load<Texture2D>(@"Squirrel\BlueSquirrel"));
            icons.Add(content.Load<Texture2D>(@"Reindeer\Reindeer"));

            //default icon states for each player
            for (int i = 0; i < 4; i++)
                iconsStates.Add(i);

        }

        public override void HandleInput(InputState input)
        {

            foreach (SignedInGamer gamer in Gamer.SignedInGamers)
            {
                int index2 = (int)gamer.PlayerIndex;                

                // exit with B
                if ((input.LastGamePadStates[index2].Buttons.B == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.B == ButtonState.Pressed ) || (
                    input.LastGamePadStates[index2].Buttons.A == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.A == ButtonState.Pressed ) || ( 
                    input.LastGamePadStates[index2].Buttons.X == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.X == ButtonState.Pressed ) || (
                    input.LastGamePadStates[index2].Buttons.Y == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.Y == ButtonState.Pressed ) || (
                    input.LastGamePadStates[index2].Buttons.Start == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.Start == ButtonState.Pressed  ) || (
                    input.LastGamePadStates[index2].Buttons.Back == ButtonState.Released
                    && input.CurrentGamePadStates[index2].Buttons.Back == ButtonState.Pressed  )
                    )
                {
                    MessageBoxScreen confirmQuitMessageBox =
                        new MessageBoxScreen("Exit Score Screen?");

                    confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

                    ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
                }              
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {           

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Guide.IsVisible)
            {

                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                spriteBatch.Begin();

                String tie;

                int winner = FindWinner();

                //tie
                if (winner == 0)
                    tie = "Tied!";

                else
                    tie = "Winner: " + players[winner - 1].GetName();

                background.Draw(spriteBatch);

                if (players.Count == 1)
                {
                    spriteBatch.DrawString(scoreFont, players[0].GetName() + "'s score: " + players[0].GetCurrentScore(),
                        new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 200, 300),
                        Color.DarkBlue);
                }
                else if (players.Count == 2)
                {
                    spriteBatch.DrawString(scoreFont, tie + "\n" + players[0].GetName() + "'s score: " + players[0].GetCurrentScore() +
                        "\n" + players[1].GetName() + "'s score: " + players[1].GetCurrentScore(),
                        new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 200, 300),
                        Color.DarkBlue);
                }
                else if (players.Count == 3)
                {
                    spriteBatch.DrawString(scoreFont, tie + "\n" + players[0].GetName() + "'s score: " + players[0].GetCurrentScore() +
                        "\n" + players[1].GetName() + "'s score: " + players[1].GetCurrentScore() + "\n" + players[2].GetName() + "'s score: " + players[2].GetCurrentScore(),
                        new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 200, 300),
                        Color.DarkBlue);
                }
                else
                {
                    spriteBatch.DrawString(scoreFont, tie + "\n" + players[0].GetName() + "'s score: " + players[0].GetCurrentScore() +
                        "\n" + players[1].GetName() + "'s score: " + players[1].GetCurrentScore() + "\n" + players[2].GetName() + "'s score: " + players[2].GetCurrentScore() + "\n" + players[3].GetName() + "'s score: " + players[3].GetCurrentScore(),
                        new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 200, 300),
                        Color.DarkBlue);
                }
                

                spriteBatch.End();
            }
        }


        public int FindWinner()
        {
            int highest = 0;
            Boolean tie = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (highest != i && players[highest].GetCurrentScore() < players[i].GetCurrentScore())
                {
                    highest = i;
                    tie = false;
                }
                else if (highest != i && players[highest].GetCurrentScore() == players[i].GetCurrentScore())
                    tie = true;
            }

            if (tie)
                // Tie
                return 0;

            else
                return highest + 1;
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null, gamers, index,
                     new BackgroundScreen(@"Background\UrbanBackground"), new MultiplayerLevelScreen(ScreenManager.GetMultiLevels(), icons, iconsStates, colors));
        }

        /// <summary>
        /// Helper modifies a color to fade its alpha value during screen transitions.
        /// </summary>
        Color FadeAlphaDuringTransition(Color color)
        {
            return new Color(color.R, color.G, color.B, TransitionAlpha);
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