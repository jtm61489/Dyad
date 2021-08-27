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
    public class Forground
    {

        private Texture2D forground;
        private Color[] textureData;

        public Forground()
        {
        }

        public void LoadContent(ContentManager content, String fileName)
        {
            forground = content.Load<Texture2D>(fileName);
            textureData = new Color[forground.Width * forground.Height];
            forground.GetData(textureData);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(forground, new Vector2(0, 0), Color.White);
        }

        public Color GetPixelColor(int i, int k)
        {
            return textureData[(i * forground.Width) + k];
        }

        public int GetHeight()
        {
            return forground.Height;
        }
    }
}
