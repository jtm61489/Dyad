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
    public class Goal
    {
        string fileName;
        Vector2 position;
        Texture2D texture;
        int whichCharacter;
        Rectangle collRect;
        Matrix transform;
        Vector2 center;
        float SCALE;

        public Goal(Vector2 pos, string textureName, int whichChar, float scal)
        {
            position = pos;
            fileName = textureName;
            whichCharacter = whichChar;
            SCALE = scal;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(fileName);
            center = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White,
                    0, center, SCALE, SpriteEffects.None, 0.0f);
        }

        public int GetWhichCharacter()
        {
            return whichCharacter;
        }

        public Rectangle GetRect()
        {
            return collRect;
        }

        public virtual void UpdateCamera(Vector2 camera)
        {
            position -= camera;
            collRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //Update player matrix: account for change in center, rotation, and new position 
            transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                                    Matrix.CreateScale(SCALE) *
                                    //Matrix.CreateRotationZ(rotation) *
                                    Matrix.CreateTranslation(new Vector3(position, 0));

            collRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, texture.Width, texture.Height), transform);  
        }
    }
}
