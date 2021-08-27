using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Dyad
{
    public class Background
    {

        private Texture2D background;
        private Vector2 position = new Vector2(0, 0);
        private Vector2 startingPosition = new Vector2(0, 0);
        private bool moving = false;

        public Background()
        {
        }

        public Background(Vector2 pos)
        {
            position = pos;
            startingPosition = pos;
            moving = true;
        }

        public void LoadContent(ContentManager content, String fileName)
        {
            background = content.Load<Texture2D>(fileName);
        }

        public void Update(Vector2 camera)
        {
            if (moving)
            {
                position = startingPosition - camera;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, position, Color.White);
        }
    }
}
