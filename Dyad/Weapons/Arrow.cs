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
    class Arrow : Weapon
    {

        Random rand = new Random();
        int arrowType;
        bool delete = false;
        float rotationSpeed = .05f;
        Vector2 restartPos;
        float restartSpd;        
        float endOfArrows;

        // random
        public Arrow()
        {
            position = new Vector2(1300, rand.Next(50, 650));
            speed = rand.Next(2, 7);
            arrowType = 1;
        }

        // one time 
        public Arrow(Vector2 startPos, float spd)
        {
            position = startPos;
            speed = spd;            
            arrowType = 2;
        }

        //repeat same pos
        public Arrow(Vector2 startPos, float spd, float end)
        {
            position = new Vector2(startPos.X + end, startPos.Y);
            restartSpd = spd;
            speed = spd;
            arrowType = 3;
            restartPos = startPos;
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = @"Objects\Arrow";
            String[] textureNamesWalking = { @"Objects\Arrow", @"Objects\Arrow", @"Objects\Arrow" };
            String[] textureNamesRunning = { @"Objects\Arrow", @"Objects\Arrow", @"Objects\Arrow" };
            String[] textureNamesJumping = { @"Objects\Arrow", @"Objects\Arrow", @"Objects\Arrow" };
            String[] textureNamesFalling = { @"Objects\Arrow" };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();
        }

        public override void Update(GameTime time)
        {
            if ((position.X <= 0 && arrowType == 1) || 
                (!fixedObject && (position.Y >= 740 /*|| !isFalling*/)))
            {
                restart = true;
            }
            if (restart && arrowType == 2)
            {
                delete = true;                
            }
            else if(restart && arrowType != 3)
            {
                position = new Vector2(1300, rand.Next(50, 650));
                speed = rand.Next(2, 7);
                restart = false;
                rotation = 0f;
                fixedObject = true;                
                isFalling = false;
                timer = 0;
                foreach (Moveable obj in objectsOnBottom)
                {
                    obj.RemoveObjectOnTop(this);
                }
                objectsOnBottom.Clear();
            }            
            else if (isFalling)
            {
                rotation += rotationSpeed;
            }
            else
            {
                position.X -= speed;
            }

            base.Update(time);
        }

        public bool IsReadyToDelete()
        {
            return delete;
        }

        public override void HitTerrain()
        {
            fixedObject = false;            
            position.X += speed;
            isFalling = true;
            speed = 0;
        }

        public override void Restart()
        {
            restart = true;
        }        

        public bool IsReady()
        {
            return restart;
        }

        public override float GetSpeed()
        {
            return speed;
        }
        public void AllRestart(float end)
        {
            endOfArrows = end;            
            speed = restartSpd;
            position = new Vector2(restartPos.X + endOfArrows, restartPos.Y);
            restart = false;
            rotation = 0f;
            fixedObject = true;
            isFalling = false;
            timer = 0;           

            foreach (Moveable obj in objectsOnBottom)
            {
                obj.RemoveObjectOnTop(this);
            }
            objectsOnBottom.Clear();
        }
    }
}
