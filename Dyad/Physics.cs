using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dyad
{
    class Physics
    {
        private const float GRAVITY = 2;

        public static float Velocity(float initial, float time, float accel)
        {
            return 2 * (initial + accel * time);
        }

        public static void Jump(Moveable obj, List<Moveable> allMoveableObjs)
        {           

            float v = 0;
            if (obj.GetInitialVelocity() != 0)
            {
                v = Velocity(-obj.GetInitialVelocity(), obj.GetTime(), GRAVITY);
                float objComing = Collision.ObjectComingUp(obj, allMoveableObjs, (int)v);

                if (objComing != 0)
                {
                    obj.StopJump(objComing);
                }
                else if (v >= 0)
                {
                    obj.Stop();
                }
                else
                {
                    bool stop = false;
                    foreach (Moveable obj2 in obj.GetObjectsOnTop())
                    {
                        if (!obj2.IsFixedObject())
                        {
                            obj2.ForceJump(v);
                        }
                        else
                        {
                            obj.Stop();
                            stop = true;
                            break;
                        }
                    }
                    if (!stop)
                    {
                        obj.Jump(v);
                    }
                }
            }
            else
            {
                v = Velocity(obj.GetJumpForce(), obj.GetTime(), GRAVITY);

                float objComing = Collision.ObjectComingUp(obj, allMoveableObjs, (int)v);

                if (objComing != 0)
                {
                    obj.StopJump(objComing);
                }
                else if (v >= 0)
                {
                    obj.Stop();
                }
                else
                {
                    obj.Jump(v);
                }
            }
        }

        public static void Gravity(List<Moveable> objs)
        {
            foreach (Moveable obj in objs)
            {
                if (!(obj.IsFixedObject()))
                {
                    if (obj.IsJumping())
                    {
                        if (obj.GetRotation() != 0)
                            obj.SetRotation(0);

                        Jump(obj, objs);
                    }
                    else
                    {
                        Fall(obj, objs);
                    }
                }
            }
        }

        public static void Fall(Moveable fallingObj, List<Moveable> allMoveableObjs)
        {
            
            bool onObject = Collision.OnObject(fallingObj, allMoveableObjs);
            bool isFalling = fallingObj.IsFalling();
 
            if(!onObject)
            {
                // set back to normal roation when not on another object
                fallingObj.SetRotation(0);
            }

            if (isFalling || !(onObject))
            {
                float velocity = Physics.Velocity(fallingObj.GetInitialVelocity(), fallingObj.GetTime(), GRAVITY);
                float collision = 0;

                collision = Collision.ObjectComingDown(fallingObj, allMoveableObjs, (int)velocity);

                // sees obj to stop on
                if (collision != 0)
                {
                    float newVelocity = Velocity(fallingObj.GetInitialVelocity(), fallingObj.GetTime() - .05f, GRAVITY);

                    fallingObj.AddForce(newVelocity);

                    // put on top of obj
                    fallingObj.StopFall(collision);

                    return;
                }
                else
                {
                    // no collision keep falling
                    fallingObj.Fall(velocity);
                }
            }

            // stop timer if on object or land
            else if ((onObject))
            {
                fallingObj.StopTimer();
            }
        }

        public static Vector2 HorizontalMovement(Moveable player, Vector2 thumbstick, List<Moveable> allMoveableObjs)
        {
            // return this
            Vector2 amountMoved = Vector2.Zero;

            Vector2 movement = Vector2.Zero;
            movement = player.CalcMovement(thumbstick.X);

            // no movement just exit
            if (movement.X == 0)
            {
                if (!player.IsBeingPushed())
                {
                    player.SetStanding();
                }
                else
                {
                    player.SetBeingPushed(false);
                }
                return amountMoved;
            }

            bool boxCantmove = false;
            bool runningIntoObject = false;
            bool obstructed = false;

            foreach (Moveable obj in allMoveableObjs)
            {

                if (player.Equals(obj))
                    continue;

                if (obj is Box || obj is Player)
                {

                    float objectComingRight = Collision.SingleObjectComingRight(player, obj, (int)movement.X);                    
                    float objectComingLeft = Collision.SingleObjectComingLeft(player, obj, Math.Abs((int)movement.X));

                    if ((objectComingRight != 0 && movement.X > 0) ||
                        (objectComingLeft != 0 && movement.X < 0))
                    {

                        float objectComingRight1 = Collision.ObjectComingRight(obj, allMoveableObjs, (int)movement.X);
                        float objectComingLeft1 = Collision.ObjectComingLeft(obj, allMoveableObjs, Math.Abs((int)movement.X));

                        if (((objectComingRight1 != 0) && movement.X > 0) ||
                        ((objectComingLeft1 != 0) && movement.X < 0))
                        {
                            obstructed = true;
                        }

                        if (!obstructed)
                        {
                            obj.Move(movement, thumbstick);
                            obj.SetBeingPushed(true);
                            player.Move(movement, thumbstick);
                            return movement;
                        }
                        else
                            boxCantmove = true;
                    }
                }                
            }

            if (!boxCantmove)
            {
                if (thumbstick.X < 0 )
                {
                    float collision = Collision.ObjectComingLeft(player, allMoveableObjs, (int)movement.X);

                    // no collision keep moving
                    if (collision == 0 && !runningIntoObject)
                    {
                        player.Move(movement, thumbstick);
                        amountMoved = movement;
                    }

                    // will collide or is colliding
                    // places one pixel right of land
                    else if (!runningIntoObject)
                    {
                        amountMoved.X = collision - player.GetPosition().X;
                        player.StopMove(collision);
                    }

                    else
                    {
                        player.SetStanding();
                    }


                }
                else if (thumbstick.X > 0 && !Collision.OnObjectRight(player, allMoveableObjs))
                {
                    float collision = Collision.ObjectComingRight(player, allMoveableObjs, (int)movement.X);

                    // no collision keep moving
                    if (collision == 0 && !runningIntoObject)
                    {
                        player.Move(movement, thumbstick);
                        amountMoved = movement;
                    }

                    // will collide or is colliding
                    // places one pixel right of land
                    else if (!runningIntoObject)
                    {
                        amountMoved.X = collision - player.GetPosition().X;
                        player.StopMove(collision);
                    }

                    else
                    {
                        player.SetStanding();
                    }
                }
            }
            return amountMoved;
        }
    }    
}
