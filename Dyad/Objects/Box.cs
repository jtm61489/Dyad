using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dyad
{
    class Box : Moveable
    {
        public Box(Vector2 pos, float scale, bool rot)
            : base(scale, rot, true)
        {
            position = pos;            
            weight = 1;
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = @"Objects\Box";
            String[] textureNamesWalking = { @"Objects\Box", @"Objects\Box", @"Objects\Box" };
            String[] textureNamesRunning = { @"Objects\Box", @"Objects\Box", @"Objects\Box" };
            String[] textureNamesJumping = { @"Objects\Box", @"Objects\Box", @"Objects\Box" };
            String[] textureNamesFalling = { @"Objects\Box" };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();            
        }        
    }
}
