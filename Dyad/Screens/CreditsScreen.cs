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
    class CreditsScreen : MenuScreen
    {        
        ContentManager content;        

        public CreditsScreen()
            :base("")
        {
        }        

        public override void  LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            //base.LoadContent(gamers, e);

            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            //background = new Background();
            //background.LoadContent(content, @"Background\Credits");                      

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
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(@"Background\HomeScreen"),
                                                           new MainMenuScreen());
            }

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