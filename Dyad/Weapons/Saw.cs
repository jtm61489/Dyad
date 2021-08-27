using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;

namespace Dyad
{
    class Saw : Weapon
    {

        float top;
        float bottom;
        bool startingDirection = false;        
        bool movementDirection;
        string fileName;

        public Saw(Vector2 pos, int t, int bot, bool dir, float sp, float scal, bool movement)            
        {
            position = pos;
            top = t;
            bottom = bot;
            startingDirection = dir;
            speed = sp;            
            movementDirection = movement;

            if (scal < .2)
            {
                SCALE = scal * 8;
                fileName = @"Objects\SawbladeSmall2";
            }
            else if (scal < .5)
            {
                SCALE = scal * 4;
                fileName = @"Objects\SawbladeSmall";
            }
            else
            {
                SCALE = scal;
                fileName = @"Objects\Sawblade";
            }
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = fileName;
            String[] textureNamesWalking = { fileName, fileName, fileName };
            String[] textureNamesRunning = { fileName, fileName, fileName };
            String[] textureNamesJumping = { fileName, fileName, fileName };
            String[] textureNamesFalling = { fileName };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();

        }

        public override void UpdateCamera(Vector2 camera)
        {
            base.UpdateCamera(camera);

            if (movementDirection)
            {
                top -= camera.Y;
                bottom -= camera.Y;
            }
            else
            {
                top -= camera.X;
                bottom -= camera.X;
            }

        }

        public override void Update(GameTime time)
        {
            if (position.Y >= bottom && movementDirection)
            {
                startingDirection = true;
            }
            else if (position.Y <= top && movementDirection)
            {
                startingDirection = false;
            }
            else if (position.X >= bottom && !movementDirection)
            {
                startingDirection = true;
            }
            else if (position.X <= top && !movementDirection)
            {
                startingDirection = false;
            }

            if (startingDirection && movementDirection)
            {
                position.Y -= speed;
            }
            else if (movementDirection)
            {
                position.Y += speed;
            }
            else if (startingDirection && !movementDirection)
            {
                position.X -= speed;
            }
            else if (!movementDirection)
            {
                position.X += speed;
            }

            rotation += .2f;

            base.Update(time);
        }
    }
}
