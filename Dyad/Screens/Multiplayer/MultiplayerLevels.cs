using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    public class MultiplayerLevels : GameplayScreen
    {

        //protected List<int> iconStates = new List<int>();
       // protected List<Texture2D> icons = new List<Texture2D>();        
       // protected List<SignedInGamer> gamers = new List<SignedInGamer>();
       // protected List<Color> colors = new List<Color>();
        

        public MultiplayerLevels(int lvl)
            : base(lvl, 2)
        {
        }

        public void InitPlayers(List<int> iconStates, List<Texture2D> icons, List<SignedInGamer> gmrs, List<Color> colors)
        {
            this.icons = icons;
            this.iconStates = iconStates;
            this.colors = colors;
            gamers = gmrs;
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);
        }
        
        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            int x = 100;


            foreach (Moveable player in allMoveableObjs)
            {
                if (player is Player && player.IsAlive())
                {

                    string name;
                    if (player.GetName().Length > 8)
                        name = player.GetName().Substring(0, 8);
                    else
                        name = player.GetName();

                    spriteBatch.DrawString(gameFont,  name + " " + player.GetHealth(), new Vector2(x, 50), Color.ForestGreen,
                        0f, Vector2.Zero, .7f, SpriteEffects.None, 0);
                    x += 200;
                }
                else if (player is Player)
                {
                    x += 200;
                }
            }


            spriteBatch.End();


        }
    }
}

