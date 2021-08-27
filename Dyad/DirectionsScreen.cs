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
    class DirectionsScreen : MenuScreen
    {
        ContentManager content;
        Background background1;
        Background background2;
        int counter = 0;

        public DirectionsScreen()
            : base("")
        {
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            background1 = new Background();
            background1.LoadContent(content, @"Background\SinglePlayerDirections");

            background2 = new Background();
            background2.LoadContent(content, @"Background\Directions2");

            counter = 0;

        }

        public override void HandleInput(InputState input)
        {


            int index1 = (int)PlayerIndex.One;
            int index2 = (int)PlayerIndex.Two;
            int index3 = (int)PlayerIndex.Three;
            int index4 = (int)PlayerIndex.Four;

            // exit with B
            if (
                (input.LastGamePadStates[index1].Buttons.B == ButtonState.Released
                && input.CurrentGamePadStates[index1].Buttons.B == ButtonState.Pressed) || (
                input.LastGamePadStates[index1].Buttons.A == ButtonState.Released
                && input.CurrentGamePadStates[index1].Buttons.A == ButtonState.Pressed) || (
                input.LastGamePadStates[index1].Buttons.X == ButtonState.Released
                && input.CurrentGamePadStates[index1].Buttons.X == ButtonState.Pressed) || (
                input.LastGamePadStates[index1].Buttons.Y == ButtonState.Released
                && input.CurrentGamePadStates[index1].Buttons.Y == ButtonState.Pressed) || (
                input.LastGamePadStates[index1].Buttons.Start == ButtonState.Released
                && input.CurrentGamePadStates[index1].Buttons.Start == ButtonState.Pressed) || (
                input.LastGamePadStates[index1].Buttons.Back == ButtonState.Released
                && input.CurrentGamePadStates[index1].Buttons.Back == ButtonState.Pressed)

                ||

                (input.LastGamePadStates[index2].Buttons.B == ButtonState.Released
                && input.CurrentGamePadStates[index2].Buttons.B == ButtonState.Pressed) || (
                input.LastGamePadStates[index2].Buttons.A == ButtonState.Released
                && input.CurrentGamePadStates[index2].Buttons.A == ButtonState.Pressed) || (
                input.LastGamePadStates[index2].Buttons.X == ButtonState.Released
                && input.CurrentGamePadStates[index2].Buttons.X == ButtonState.Pressed) || (
                input.LastGamePadStates[index2].Buttons.Y == ButtonState.Released
                && input.CurrentGamePadStates[index2].Buttons.Y == ButtonState.Pressed) || (
                input.LastGamePadStates[index2].Buttons.Start == ButtonState.Released
                && input.CurrentGamePadStates[index2].Buttons.Start == ButtonState.Pressed) || (
                input.LastGamePadStates[index2].Buttons.Back == ButtonState.Released
                && input.CurrentGamePadStates[index2].Buttons.Back == ButtonState.Pressed)

                ||

                (input.LastGamePadStates[index3].Buttons.B == ButtonState.Released
                && input.CurrentGamePadStates[index3].Buttons.B == ButtonState.Pressed) || (
                input.LastGamePadStates[index3].Buttons.A == ButtonState.Released
                && input.CurrentGamePadStates[index3].Buttons.A == ButtonState.Pressed) || (
                input.LastGamePadStates[index3].Buttons.X == ButtonState.Released
                && input.CurrentGamePadStates[index3].Buttons.X == ButtonState.Pressed) || (
                input.LastGamePadStates[index3].Buttons.Y == ButtonState.Released
                && input.CurrentGamePadStates[index3].Buttons.Y == ButtonState.Pressed) || (
                input.LastGamePadStates[index3].Buttons.Start == ButtonState.Released
                && input.CurrentGamePadStates[index3].Buttons.Start == ButtonState.Pressed) || (
                input.LastGamePadStates[index3].Buttons.Back == ButtonState.Released
                && input.CurrentGamePadStates[index3].Buttons.Back == ButtonState.Pressed)

                ||

                (input.LastGamePadStates[index4].Buttons.B == ButtonState.Released
                && input.CurrentGamePadStates[index4].Buttons.B == ButtonState.Pressed) || (
                input.LastGamePadStates[index4].Buttons.A == ButtonState.Released
                && input.CurrentGamePadStates[index4].Buttons.A == ButtonState.Pressed) || (
                input.LastGamePadStates[index4].Buttons.X == ButtonState.Released
                && input.CurrentGamePadStates[index4].Buttons.X == ButtonState.Pressed) || (
                input.LastGamePadStates[index4].Buttons.Y == ButtonState.Released
                && input.CurrentGamePadStates[index4].Buttons.Y == ButtonState.Pressed) || (
                input.LastGamePadStates[index4].Buttons.Start == ButtonState.Released
                && input.CurrentGamePadStates[index4].Buttons.Start == ButtonState.Pressed) || (
                input.LastGamePadStates[index4].Buttons.Back == ButtonState.Released
                && input.CurrentGamePadStates[index4].Buttons.Back == ButtonState.Pressed)

                )
            {
                counter++;
                if (counter == 2)
                {
                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(@"Background\HomeScreen"),
                                            new MainMenuScreen());
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;            

            spriteBatch.Begin();            

            if (counter == 0)
            {
                background1.Draw(spriteBatch);
            }
            else
            {
                background2.Draw(spriteBatch);
            }

            spriteBatch.End();
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