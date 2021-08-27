using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dyad
{
    class Plank : Moveable
    {         
        private int bottom;
        private float weightDistrabutionLeft;
        private float weightDistrabutionRight;
        private bool rotatingLeft = false;
        private bool rotatingRight = false;
        private bool rotating = false;        

        public Plank(Vector2 pos, int baseBottom, float scale, bool rot)
            : base (scale, rot, true)
        {
            position = pos;
            STARTING_POSITION = pos;
            bottom = baseBottom;
            rotation = 0;            
            rotatingSpeed = .01f;            
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = @"Objects\See-SawPlank";
            String[] textureNamesWalking = { @"Objects\See-SawPlank" };
            String[] textureNamesRunning = { @"Objects\See-SawPlank" };
            String[] textureNamesJumping = { @"Objects\See-SawPlank" };
            String[] textureNamesFalling = { @"Objects\See-SawPlank" };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();
        }

        public override void Update(GameTime time)
        {            

            weightDistrabutionLeft = 0;
            weightDistrabutionRight = 0;
            force = 0;

            foreach (Moveable obj in objectsOnTop)
            {
                if (collRect.Center.X > obj.GetPosition().X && !objectsToLaunch.Contains(obj))
                {
                    if (obj.GetForce() == 0)
                    {
                        weightDistrabutionLeft -= obj.GetWeight();
                    }
                    else
                    {
                        force -= obj.GetForce();
                    }
                }
                else if (collRect.Center.X < obj.GetPosition().X && !objectsToLaunch.Contains(obj))
                {
                    if (obj.GetForce() == 0)
                    {
                        weightDistrabutionRight += obj.GetWeight();
                    }
                    else
                    {
                        force += obj.GetForce();
                    }
                }

                obj.ResetForce();
            }

            float totalForce = weightDistrabutionLeft + weightDistrabutionRight + force;

            if (collRect.Bottom >= bottom)
            {
                if (rotation <= 0)
                {
                    rotatingLeft = false;
                }
                else if (rotation >= 0)
                {
                    rotatingRight = false;
                }

                foreach (Moveable obj in objectsToLaunch)
                {
                    obj.StartForceJump();
                    JumpAllOnTop(obj, obj.GetInitialVelocity());
                    objectsOnTop.Remove(obj);
                }
                objectsToLaunch.Clear();
            }

            if (totalForce > 0 && rotation <= 0)
            {
                rotatingLeft = false;
                rotatingRight = true;

                foreach (Moveable obj in objectsOnTop)
                {
                    if ((!obj.Equals(this)) && collRect.Center.X > obj.GetPosition().X && !objectsToLaunch.Contains(obj))
                    {
                        float f = (float)Math.Sqrt( (Math.Abs(force - weightDistrabutionLeft) * 2) / obj.GetWeight());
                            
                        obj.SetInitialVelocity(f);
                        objectsToLaunch.Add(obj);
                    }
                }
            }

            else if (totalForce < 0 && rotation >= 0)
            {
                rotatingLeft = true;
                rotatingRight = false;

                foreach (Moveable obj in objectsOnTop)
                {
                    if ((!obj.Equals(this)) && collRect.Center.X <= obj.GetPosition().X && !objectsToLaunch.Contains(obj))
                    {
                        float f = (float)Math.Sqrt( (Math.Abs(force - weightDistrabutionRight) * 2) / obj.GetWeight());

                        obj.SetInitialVelocity(f);
                        objectsToLaunch.Add(obj);                    
                    }
                }            
            }

            if (rotatingLeft)
            {
                previousRotation = rotation;
                rotation -= rotatingSpeed;
                rotating = true;

                foreach (Moveable obj in objectsOnTop)
                {
                    if ((!obj.Equals(this)) && collRect.Center.X <= obj.GetPosition().X && !objectsToLaunch.Contains(obj))
                    {
                        float f = (float)Math.Sqrt((Math.Abs(force - weightDistrabutionRight) * 2) / obj.GetWeight());

                        obj.SetInitialVelocity(f);
                        objectsToLaunch.Add(obj);
                    }
                }        
            }
            else if (rotatingRight)
            {
                previousRotation = rotation;
                rotation += rotatingSpeed;
                rotating = true;

                foreach (Moveable obj in objectsOnTop)
                {
                    if ((!obj.Equals(this)) && collRect.Center.X > obj.GetPosition().X && !objectsToLaunch.Contains(obj))
                    {
                        float f = (float)Math.Sqrt((Math.Abs(force - weightDistrabutionLeft) * 2) / obj.GetWeight());

                        obj.SetInitialVelocity(f);
                        objectsToLaunch.Add(obj);
                    }
                }
            }
            else
            {
                rotating = false;
            }

            List<Moveable> removeThese = new List<Moveable>();

            foreach (Moveable obj in objectsToLaunch)
            {
                Vector2 pixel = Collision.VectorCollidingWith(obj, this);

                if (pixel.X == -1 && pixel.Y == -1)
                {
                    removeThese.Add(obj);                    
                }
                else
                {
                    Vector2 vec1 = Vector2.Transform(pixel, transform);
                    Vector2 vec2 = Vector2.Transform(pixel, previousOnlyRotation);
                    Vector2 temp;

                    temp = vec1 - vec2;

                    obj.ForceMove(temp, rotation);
                }
            }

            foreach (Moveable obj in removeThese)
            {
                objectsToLaunch.Remove(obj);
            }
            base.Update(time);

        }

        public bool IsRotating()
        {
            return rotating;
        }

        private static void JumpAllOnTop(Moveable obj, float force/*, float totalWeight*/)
        {

            foreach (Moveable obj2 in obj.GetObjectsOnTop())
            {
                if (!obj.Equals(obj2))
                {
                    obj2.SetInitialVelocity(Math.Abs(force) /*/ totalWeight*/);
                    obj2.StartForceJump();

                    JumpAllOnTop(obj2, force/*, totalWeight*/);
                }
            }
        }
    }
}
