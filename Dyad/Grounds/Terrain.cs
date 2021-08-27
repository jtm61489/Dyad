using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dyad
{
    class Terrain : Moveable
    {   
        protected Texture2D text;

        public Terrain(Vector2 pos, Texture2D txt, float rot, float scal, bool rotater, bool collideable)
            : base(scal, rotater, collideable)
        {
            position = pos;
            STARTING_POSITION = pos;
            text = txt;
            rotation = rot;
        }

        public override void LoadContent(ContentManager content)
        {   
            animation = new Animation(text);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();
        }        
        
        public override void Update(GameTime gameTime)
        {           

            //Update player matrix: account for change in center, rotation, and new position 
            transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                                    Matrix.CreateScale(SCALE) *
                                    Matrix.CreateRotationZ(rotation) *
                                    Matrix.CreateTranslation(new Vector3(position, 0));

            collRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, texture.Width, texture.Height), transform); 

        }        
    }
}
