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
    class Collision
    {

        public static bool RectangleCollision(Moveable obj1, Moveable obj2)
        {        
            return obj1.GetRect().Intersects(obj2.GetRect());
        }

        public static bool RectangleCollision(Rectangle rect, Moveable obj)
        {
            return rect.Intersects(obj.GetRect());
        }

        public static bool RectangleCollision(Rectangle rec1, Rectangle rec2)
        {
            return rec1.Intersects(rec2);
        } 

        public static bool OnObject(Moveable obj1, List<Moveable> allMoveableObjs)
        {
            bool returnMe = false;

            if (!obj1.IsCollideable() || obj1 is Weapon)
                return false;

            foreach (Moveable obj in obj1.GetObjectsOnBottom())
            {
                obj.RemoveObjectOnTop(obj1);
            }

            obj1.GetObjectsOnBottom().Clear();

            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon)
                    continue;

                if (Collision.CollDown(obj1, obj2, 4) != 0)
                {
                    // change to objects rotation
                    obj1.SetRotation(obj2.GetRotation());

                    obj2.AddObjectOnTop(obj1);
                    obj1.AddObjectOnBottom(obj2);

                    returnMe = true;
                }
                else
                {
                    obj2.RemoveObjectOnTop(obj1);
                    obj1.RemoveObjectOnBottom(obj2);
                }
            }            

            return returnMe;
        }

        public static bool OnObject(Moveable obj1, Moveable obj2, int speed)
        {
            if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon || obj1 is Weapon)
            {
                return false;
            }

            if (Collision.CollDown(obj1, obj2, (int)(speed/obj1.GetScale())) != 0)
            {
                // change to objects rotation
                obj1.SetRotation(obj2.GetRotation());

                obj2.AddObjectOnTop(obj1);
                obj1.AddObjectOnBottom(obj2);

                return true;
            }

            obj2.RemoveObjectOnTop(obj1);
            obj1.RemoveObjectOnBottom(obj2);

            return false;
        }

        public static float OnObject(Moveable obj1, Wall obj2, int speed)
        {
            if (!obj2.IsCollideable())
            {
                return 0;
            }

            if (Collision.CollDown(obj1, obj2, (int)(speed / obj1.GetScale())) != 0)
            {
                // change to objects rotation
                obj1.SetRotation(obj2.GetRotation());

                obj2.AddObjectOnTop(obj1);
                obj1.AddObjectOnBottom(obj2);

                return obj1.GetRect().Bottom;
            }

            obj2.RemoveObjectOnTop(obj1);
            obj1.RemoveObjectOnBottom(obj2);

            return 0;
        }

        public static bool OnObjectLeft(Moveable obj1, List<Moveable> allMoveableObjs)
        {
            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon)
                    continue;

                if (Collision.CollLeft(obj1, obj2, 2) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool OnObjectRight(Moveable obj1, List<Moveable> allMoveableObjs)
        {
            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon)
                    continue;

                if (Collision.CollRight(obj1, obj2, 2) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool BelowObject(Moveable obj1, List<Moveable> allMoveableObjs)
        {
            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon)
                    continue;

                if (CollUp(obj1, obj2, 4) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static float ObjectComingDown(Moveable obj1, List<Moveable> allMoveableObjs, int speed)
        {
            float returnMe = 0;

            if (obj1 is Weapon)
                return 0;

            foreach (Moveable obj2 in allMoveableObjs)
            {

                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon)
                    continue;

                float coll = CollDown(obj1, obj2, speed);

                if (coll != 0)
                {
                    //obj2.AddObjectOnTop(obj1);
                    //obj1.AddObjectOnBottom(obj2);

                    obj1.SetRotation(obj2.GetRotation());

                    returnMe = coll;
                }
                else
                {
                    //obj2.RemoveObjectOnTop(obj1);
                    //obj1.RemoveObjectOnBottom(obj2);
                }
            }

            return returnMe;
        }

        public static float ObjectComingLeft(Moveable obj1, List<Moveable> allMoveableObjs, int speed)
        {
            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon || obj1 is Weapon)
                    continue;

                float coll = CollLeft(obj1, obj2, speed);

                if (coll != 0)
                    return coll;
            }

            return 0;
        }

        public static float ObjectComingRight(Moveable obj1, List<Moveable> allMoveableObjs, int speed)
        {
            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon || obj1 is Weapon)
                    continue;

                float coll = CollRight(obj1, obj2, speed);

                if (coll != 0)
                    return coll;
            }

            return 0;
        }

        public static float ObjectComingUp(Moveable obj1, List<Moveable> allMoveableObjs, int speed)
        {
            float returnMe = 0;

            speed = Math.Abs(speed);

            foreach (Moveable obj2 in allMoveableObjs)
            {
                if (obj1.Equals(obj2) || !obj2.IsCollideable() || obj2 is Weapon || obj1 is Weapon)
                    continue;

                float coll = CollUp(obj1, obj2, speed);

                if (coll != 0)
                {
                    returnMe = coll;
                }

                else
                {
                    //obj1.RemoveObjectOnBottom(obj2);
                    //obj2.RemoveObjectOnTop(obj1);
                }

            }

            return returnMe;
        }

        public static float SingleObjectComingLeft(Moveable obj1, Moveable obj2, int speed)
        {
            if (!obj2.IsCollideable() || obj2 is Weapon)
            {
                return 0;
            }

            float coll = CollLeft(obj1, obj2, speed);

            if (coll != 0)
                return coll;

            return 0;
        }

        public static float ArrowHelper(Moveable obj1, Moveable obj2, int speed)
        {
            if (!obj1.GetRect().Intersects(obj2.GetRect()))
            {
                return 0;
            }

            float coll = CollLeft(obj1, obj2, speed);

            if (coll == 0)
            {
                coll = CollUp(obj1, obj2, speed);
            }

            if (coll == 0)
            {
                coll = CollDown(obj1, obj2, speed);
            }

            if (coll == 0)
            {
                coll = CollRight(obj1, obj2, speed);
            }

            if (coll != 0)
                return coll;

            return 0;
        }

        public static float SingleObjectComingRight(Moveable obj1, Moveable obj2, int speed)
        {
            if (!obj2.IsCollideable() || obj2 is Weapon)
            {
                return 0;
            }

            float coll = CollRight(obj1, obj2, speed);

            if (coll != 0)
                return coll;

            return 0;
        }        

        public static float CollDown(Moveable obj1, Moveable obj2, int speed)
        {
            Vector2 objPos = obj1.GetPosition();            

            Vector3 pos = new Vector3(obj1.GetPosition(), 0);

            return FutureCollisionDown(obj1, obj2, (int)(speed/obj1.GetScale()), pos);
        }

        public static float CollLeft(Moveable obj1, Moveable obj2, int speed)
        {           

            Vector2 objPos = obj1.GetPosition();

            speed = Math.Abs(speed);

            Vector3 pos = new Vector3(obj1.GetPosition(), 0);

            return FutureCollisionLeft(obj1, obj2, (int)(speed / obj1.GetScale()), pos);
        }

        public static float CollRight(Moveable obj1, Moveable obj2, int speed)
        {
            Vector2 objPos = obj1.GetPosition();

            Vector3 pos = new Vector3(obj1.GetPosition(), 0);

            return FutureCollisionRight(obj1, obj2, (int)(speed / obj1.GetScale()), pos);
        }

        public static float CollUp(Moveable obj1, Moveable obj2, int speed)
        {

            Vector2 objPos = obj1.GetPosition();

            Vector3 pos = new Vector3(obj1.GetPosition(), 0);

            return FutureCollisionUp(obj1, obj2, (int)(speed / obj1.GetScale()), pos);

        }

        private static float FutureCollisionDown(Moveable obj1, Moveable obj2, int speed, Vector3 position)
        {
            Vector2 center = new Vector2(obj1.GetTexture().Width / 2, -obj1.GetTexture().Height / 2 - speed/2 - 1);

            Matrix transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                Matrix.CreateScale(obj1.GetScale()) *
                Matrix.CreateRotationZ(obj1.GetRotation()) *
                                    Matrix.CreateTranslation(position);

            Rectangle futureRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, obj1.GetTexture().Width, speed), transform);

            Rectangle obj2Rec = obj2.GetRect();

            if (RectangleCollision(futureRect, obj2Rec))
            {
                // returns loacal point in plank texture that is colliding
                Vector2[] coll = IntersectMatrixFuture(transform, obj1.GetTexture().Width, speed,
                     obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData());

                // no future collision
                if (coll[0].X == -1 && coll[0].Y == -1)
                    return 0;

                float y = 0;

                if (obj1.GetRotation() > 0)
                {
                    int getY = (int)obj1.GetBottomPoint(coll[1].X + futureRect.Left);                    

                    float diff = obj1.GetRect().Bottom - getY;

                    y = Vector2.Transform(coll[0], obj2.GetTransform()).Y - diff;
                }
                else
                {
                    y = Vector2.Transform(coll[0], obj2.GetTransform()).Y - obj1.GetRect().Height/2 - 2;
                }

                return y;

            }

            return 0;
        }

        private static float FutureCollisionLeft(Moveable obj1, Moveable obj2, int speed, Vector3 position)
        {
            Vector2 center = new Vector2(obj1.GetTexture().Width/2 + speed/2 + 1, obj1.GetTexture().Height / 2);

            Matrix transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                Matrix.CreateScale(obj1.GetScale()) *
                Matrix.CreateRotationZ(obj1.GetRotation()) *
                                    Matrix.CreateTranslation(position);

            Rectangle futureRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, speed, obj1.GetTexture().Height - 10), transform);

            Rectangle obj2Rec = obj2.GetRect();

            if (RectangleCollision(futureRect, obj2Rec))
            {
                // returns loacal point in plank texture that is colliding
                Vector2[] coll = IntersectMatrixFutureLeft(transform, speed, obj1.GetTexture().Height,
                     obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData());

                // no future collision
                if (coll[0].X == -1 && coll[0].Y == -1)
                    return 0;

                float x = Vector2.Transform(coll[0], obj2.GetTransform()).X + obj1.GetRect().Width/2 + 1;

                return x;

            }

            return 0;
        }

        private static float FutureCollisionRight(Moveable obj1, Moveable obj2, int speed, Vector3 position)
        {
            Vector2 center = new Vector2(-obj1.GetTexture().Width / 2 - speed/2 - 1, obj1.GetTexture().Height / 2);

            Matrix transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                Matrix.CreateScale(obj1.GetScale()) *
                Matrix.CreateRotationZ(obj1.GetRotation()) *
                                    Matrix.CreateTranslation(position);

            Rectangle futureRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, speed, obj1.GetTexture().Height - 10), transform);

            Rectangle obj2Rec = obj2.GetRect();

            if (RectangleCollision(futureRect, obj2Rec))
            {
                // returns loacal point in plank texture that is colliding
                Vector2 coll = IntersectMatrixFuture(transform, speed, obj1.GetTexture().Height,
                     obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData())[0];

                // no future collision
                if (coll.X == -1 && coll.Y == -1)
                    return 0;

                float x = Vector2.Transform(coll, obj2.GetTransform()).X - obj1.GetRect().Width/2 - 1;

                return x;

            }

            return 0;
        }

        private static float FutureCollisionUp(Moveable obj1, Moveable obj2, int speed, Vector3 position)
        {
            Vector2 center = new Vector2(obj1.GetTexture().Width / 2, obj1.GetTexture().Height/2 + speed/2 + 1);

            Matrix transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                Matrix.CreateScale(obj1.GetScale()) *
                Matrix.CreateRotationZ(obj1.GetRotation()) *
                                    Matrix.CreateTranslation(position);

            Rectangle futureRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, obj1.GetTexture().Width, speed), transform);

            Rectangle obj2Rec = obj2.GetRect();

            if (RectangleCollision(futureRect, obj2Rec))
            {
                // returns loacal point in plank texture that is colliding
                Vector2 coll = IntersectMatrixFutureUp(transform, obj1.GetTexture().Width, speed,
                     obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData())[0];

                // no future collision
                if (coll.X == -1 && coll.Y == -1)
                    return 0;

                float y = Vector2.Transform(coll, obj2.GetTransform()).Y + obj1.GetRect().Height / 2;

                if (y > obj1.GetPosition().Y)
                {
                    return obj1.GetPosition().Y + 2;
                }

                return y;

            }

            return 0;
        }       

        #region Misc

        public static Vector2 VectorCollidingWith(Moveable obj1, Moveable obj2)
        {
            Vector2 objPos = obj1.GetPosition();

            float angle = obj1.GetRotation();
            float tanTa = Math.Abs((float)Math.Tan(angle) * obj1.GetRect().Width / 2);

            Vector3 pos = new Vector3(objPos.X,
                             objPos.Y + (obj1.GetRect().Height / 2) - tanTa,
                             0);

            Vector2 center = new Vector2(1, 3);

            Matrix transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                //Matrix.CreateScale(obj.GetScale()) *
                //Matrix.CreateRotationZ(obj1.GetRotation()) *
                                    Matrix.CreateTranslation(pos);

            Rectangle futureRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, 2, 6), transform);

            Rectangle obj2Rec = obj2.GetRect();

            if (RectangleCollision(futureRect, obj2Rec))
            {
                return IntersectMatrixFuture(transform, futureRect.Width, futureRect.Height,
                         obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData())[0];
            }

            // should never happen, this should be error
            return new Vector2(-1, -1);

        }


        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        // returns true if colliding
        private static Vector2[] IntersectMatrixFuture(Matrix transformA, int widthA, int heightA,
                                    Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            //Calculate a matrix which transforms from A's local space into world space
            // and then into B's local space
            Matrix transformAtoB = transformA * Matrix.Invert(transformB);

            //for each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                //for each pixel in the row
                for (int xA = 0; xA < widthA; xA++)
                {
                    //calculate this pixel's location in B
                    Vector2 positionInB = Vector2.Transform(new Vector2(xA, yA), transformAtoB);

                    //round the pixels
                    int xB = (int)Math.Round(positionInB.X);
                    int yB = (int)Math.Round(positionInB.Y);

                    //Check if the pixel is within B
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        Color colorB = dataB[xB + yB * widthB];
                        if (colorB.A >= 250)
                        {
                            Vector2[] vec = new Vector2[2];
                            vec[0] = new Vector2(xB, yB);
                            vec[1] = new Vector2(xA, yA);
                            return vec;
                        }
                    }
                }
            }
            Vector2[] vec2 = new Vector2[2];
            vec2[0] = new Vector2(-1,-1);
            vec2[1] = new Vector2(-1, -1);
            return vec2;
        }

        // returns true if colliding
        private static Vector2[] IntersectMatrixFutureUp(Matrix transformA, int widthA, int heightA,
                                    Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            //Calculate a matrix which transforms from A's local space into world space
            // and then into B's local space
            Matrix transformAtoB = transformA * Matrix.Invert(transformB);

            //for each row of pixels in A
            for (int yA = heightA; yA > 0; yA--)
            {
                //for each pixel in the row
                for (int xA = 0; xA < widthA; xA++)
                {
                    //calculate this pixel's location in B
                    Vector2 positionInB = Vector2.Transform(new Vector2(xA, yA), transformAtoB);

                    //round the pixels
                    int xB = (int)Math.Round(positionInB.X);
                    int yB = (int)Math.Round(positionInB.Y);

                    //Check if the pixel is within B
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        Color colorB = dataB[xB + yB * widthB];
                        if (colorB.A >= 250)
                        {
                            Vector2[] vec = new Vector2[2];
                            vec[0] = new Vector2(xB, yB);
                            vec[1] = new Vector2(xA, yA);
                            return vec;
                        }
                    }
                }
            }
            Vector2[] vec2 = new Vector2[2];
            vec2[0] = new Vector2(-1, -1);
            vec2[1] = new Vector2(-1, -1);
            return vec2;
        }

        // returns true if colliding
        private static Vector2[] IntersectMatrixFutureLeft(Matrix transformA, int widthA, int heightA,
                                    Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            //Calculate a matrix which transforms from A's local space into world space
            // and then into B's local space
            Matrix transformAtoB = transformA * Matrix.Invert(transformB);

            //for each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                //for each pixel in the row
                for (int xA = widthA; 0 < xA; xA--)
                {
                    //calculate this pixel's location in B
                    Vector2 positionInB = Vector2.Transform(new Vector2(xA, yA), transformAtoB);

                    //round the pixels
                    int xB = (int)Math.Round(positionInB.X);
                    int yB = (int)Math.Round(positionInB.Y);

                    //Check if the pixel is within B
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        Color colorB = dataB[xB + yB * widthB];
                        if (colorB.A >= 250)
                        {
                            Vector2[] vec = new Vector2[2];
                            vec[0] = new Vector2(xB, yB);
                            vec[1] = new Vector2(xA, yA);
                            return vec;
                        }
                    }
                }
            }
            Vector2[] vec2 = new Vector2[2];
            vec2[0] = new Vector2(-1, -1);
            vec2[1] = new Vector2(-1, -1);
            return vec2;
        }
        // returns true if colliding
        private static int FindTranformedBottom(Matrix transformA, Matrix transformB, int widthA, int heightA, int heightRect, int x, Color[] dataA)                                    
        {

            //Calculate a matrix which transforms from A's local space into world space
            // and then into B's local space
            Matrix transformAtoB = transformB * Matrix.Invert(transformA);

            //for each row of pixels in A
            for (int yA = heightRect; yA >= 0; yA--)
            {

                //calculate this pixel's location in B
                Vector2 position = Vector2.Transform(new Vector2(x, yA), transformAtoB);               


                //round the pixels
                int xB = (int)Math.Round(position.X);
                int yB = (int)Math.Round(position.Y);

                //Check if the pixel is within B
                if (0 <= xB && xB <= widthA && 0 <= yB && yB <= heightA)
                {

                    //Get the colors of the pixels
                    //Color colorA = dataA[x + yB * widthA];

                    //Check whether both pixels are transparent
                    //if (colorA.A != 0)
                        return heightRect - yA;
                }
            }

            return -1;
        }

        public static bool ObjectsIntersect(Moveable obj1, Moveable obj2)
        {

            if (obj1.GetRect().Intersects(obj2.GetRect()))
            {
                return IntersectPixels(obj1.GetTransform(), obj1.GetTexture().Width, obj1.GetTexture().Height, obj1.GetData(),
                    obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData());
            }
            else
                return false;
        }


        public static bool ObjectsTrueIntersect(Moveable obj1, Moveable obj2)
        {  

            if (obj1.GetTrueRect().Intersects(obj2.GetTrueRect()))
            {
                return IntersectPixels(obj1.GetTransform(), obj1.GetTrueTexture().Width, obj1.GetTrueTexture().Height, obj1.GetTrueData(),
                    obj2.GetTransform(), obj2.GetTexture().Width, obj2.GetTexture().Height, obj2.GetData());
            }
            else
                return false;
        }

        // returns true if colliding
        private static bool IntersectPixels(Matrix transformA, int widthA, int heightA, Color[] dataA,
                                    Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            //Calculate a matrix which transforms from A's local space into world space
            // and then into B's local space
            Matrix transformAtoB = transformA * Matrix.Invert(transformB);

            //for each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                //for each pixel in the row
                for (int xA = 0; xA < widthA; xA++)
                {
                    //calculate this pixel's location in B
                    Vector2 positionInB = Vector2.Transform(new Vector2(xA, yA), transformAtoB);

                    //round the pixels
                    int xB = (int)Math.Round(positionInB.X);
                    int yB = (int)Math.Round(positionInB.Y);

                    //Check if the pixel is within B
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        //Get the colors of the pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        //Check whether both pixels are transparent
                        if (colorB.A >= 250 && colorA.A >= 250)
                            return true;
                    }
                }
            }
            return false;
        }

        #endregion

    }
}
