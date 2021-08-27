using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Dyad
{
    public class Camera
    {

        private int type;
        Vector2 camera;
        int top;
        float bottom;
        int right;
        int left;
        float speed;
        bool vertical;

        public Camera(Vector2 startingPos, int tp, int btm, int rt, int lt)
        {
            type = 1;
            camera = startingPos;
            top = tp;
            bottom = btm;
            right = rt;
            left = lt;
        }

        public Camera(Vector2 startingPos, int tp, int btm, int rt, int lt, float spd, bool vert)
        {
            type = 2;
            camera = startingPos;
            top = tp;
            bottom = btm;
            right = rt;
            left = lt;
            speed = spd;
            vertical = vert;
        }

        public int GetCameraType()
        {
            return type;
        }

        public bool GetCameraDirection()
        {
            return vertical;
        }

        public float GetBottom()
        {
            return bottom;
        }

        public Vector2 GetPosition()
        {
            return camera;
        }

        public bool IsStopped()
        {
            if (camera.X >= right)
            {
                return true;
            }

            return false;
        }

        public void ChangePosition(Vector2 vec, Goal goal1, Goal goal2, List<Moveable> allMoveableObjs)
        {

            Vector2 previousCam = camera;

            camera += vec;

            foreach (Moveable obj in allMoveableObjs)
            {
                obj.UpdateCamera(camera - previousCam);
            }

            if (goal1 != null)
                goal1.UpdateCamera(camera - previousCam);
            if (goal2 != null)
                goal2.UpdateCamera(camera - previousCam);
        }

        public void UpdateY(float p1, float p2, Vector2 p1movement, Vector2 p2movement,
            Goal goal1, Goal goal2, List<Moveable> allMoveableObjs)
        {

            Vector2 previousCam = camera;

            if (!vertical)
            {

                if (p1 > 360 && p2 > 360)
                {
                    if (p1 >= p2 && p2movement.Y > 0)
                    {
                        if (camera.Y + 720 < bottom)
                            camera.Y += p2movement.Y;
                    }
                    else if (p1movement.Y > 0)
                    {
                        if (camera.Y + 720 < bottom)
                            camera.Y += p1movement.Y;
                    }
                }
                else if (p1 <= 360 && p2 <= 360)
                {
                    if (p1 <= p2 && p2movement.Y < 0)
                    {
                        if (camera.Y > top)
                            camera.Y += p2movement.Y;
                    }
                    else if (p1movement.Y < 0)
                    {
                        if (camera.Y > top)
                            camera.Y += p1movement.Y;
                    }
                }
                else if (p1 >= 360 && p1 <= 700 && p2 <= 90 && p2movement.Y < 0)
                {
                    if (camera.Y > top)
                        camera.Y += p2movement.Y;
                }
                else if (p2 >= 360 && p2 <= 700 && p1 <= 90 && p1movement.Y < 0)
                {
                    if (camera.Y > top)
                        camera.Y += p1movement.Y;
                }
                else if (p1 <= 360 && p1 >= 20 && p2 <= 700 && p2movement.Y > 0)
                {
                    if (camera.Y + 720 < bottom)
                        camera.Y += p2movement.Y;
                }
                else if (p2 <= 360 && p2 >= 20 && p1 <= 700 && p1movement.Y > 0)
                {
                    if (camera.Y + 720 < bottom)
                        camera.Y += p1movement.Y;
                }

                foreach (Moveable obj in allMoveableObjs)
                {
                    obj.UpdateCamera(camera - previousCam);
                }

                if (goal1 != null)
                    goal1.UpdateCamera(camera - previousCam);
                if (goal2 != null)
                    goal2.UpdateCamera(camera - previousCam);

                bottom -= (camera.Y - previousCam.Y);
            }
        }

        public float UpdateX(float p1, float p2, Vector2 p1movement, Vector2 p2movement,
            Goal goal1, Goal goal2, List<Moveable> allMoveableObjs, Vector2 thumbstickLeft, Vector2 thumbstickRight)
        {
            Vector2 previousCam = camera;
            bool undo = false;
            float undoValue = 0;

            if (type == 1)
            {

                if (p1 >= 640 && p2 >= 640 && camera.X + 1280 < right)
                {
                    if (p2 >= p1 && p2movement.X > 0)
                    {
                        camera.X += p2movement.X;
                    }
                    else if (p1movement.X > 0)
                    {
                        camera.X += p1movement.X;
                    }
                }
                else if (p1 <= 640 && p2 <= 640 && camera.X > left)
                {
                    if (p2 <= p1 && p2movement.X < 0)
                    {
                        camera.X += p2movement.X;
                    }
                    else if (p1movement.X < 0)
                    {
                        camera.X += p1movement.X;
                    }
                }
                else if (p1 >= 640 && p1 <= 1205 && p2 <= 90 && p2movement.X < 0 && camera.X > left)
                {
                    camera.X += p2movement.X;
                }
                else if (p2 >= 640 && p2 <= 1205 && p1 <= 90 && p1movement.X < 0 && camera.X > left)
                {
                    camera.X += p1movement.X;
                }
                else if (p1 <= 640 && p1 >= 90 && p2 <= 1195 && p2movement.X > 0 && camera.X + 1280 < right)
                {
                    camera.X += p2movement.X;
                }
                else if (p2 <= 640 && p2 >= 90 && p1 <= 1195 && p1movement.X > 0 && camera.X + 1280 < right)
                {
                    camera.X += p1movement.X;
                }

                foreach (Moveable obj in allMoveableObjs)
                {
                    obj.UpdateCamera(camera - previousCam);
                }

                if (goal1 != null)
                    goal1.UpdateCamera(camera - previousCam);
                if (goal2 != null)
                    goal2.UpdateCamera(camera - previousCam);

                foreach (Moveable obj in allMoveableObjs)
                {
                    if (obj is Player)
                    {
                        float pos = obj.GetPosition().X;
                        int playerNum = obj.GetPlayerNumber();

                        if (pos >= 1205 && playerNum == 1 && thumbstickLeft.X > 0)
                        {
                            obj.Move(-p1movement, thumbstickLeft);
                            undoValue = previousCam.X - camera.X;
                            undo = true;
                            camera.X = previousCam.X;
                        }
                        else if (pos <= 75 && playerNum == 1 && thumbstickLeft.X < 0)
                        {
                            obj.Move(-p1movement, thumbstickLeft);
                            undoValue = previousCam.X - camera.X;
                            undo = true;
                            camera.X = previousCam.X;
                        }
                        else if (pos >= 1205 && playerNum == 2 && thumbstickRight.X > 0)
                        {
                            obj.Move(-p2movement, thumbstickRight);
                            undoValue = previousCam.X - camera.X;
                            undo = true;
                            camera.X = previousCam.X;
                        }
                        else if (pos <= 75 && playerNum == 2 && thumbstickRight.X < 0)
                        {
                            obj.Move(-p2movement, thumbstickRight);
                            undoValue = previousCam.X - camera.X;
                            undo = true;
                            camera.X = previousCam.X;
                        }
                    }
                }

                if (undo)
                {
                    foreach (Moveable obj in allMoveableObjs)
                    {
                        obj.UpdateCamera(new Vector2(undoValue, 0));
                    }
                    if (goal1 != null)
                        goal1.UpdateCamera(new Vector2(undoValue, 0));
                    if (goal2 != null)
                        goal2.UpdateCamera(new Vector2(undoValue, 0));
                }        
            }

            else if(!vertical)
            {
                if (camera.X < right)
                {
                    camera.X += speed;
                }

                foreach (Moveable obj in allMoveableObjs)
                {
                    obj.UpdateCamera(camera - previousCam);
                }

                if (goal1 != null)
                    goal1.UpdateCamera(camera - previousCam);
                if (goal2 != null)
                    goal2.UpdateCamera(camera - previousCam);
            }

            if (!undo)
                return (camera.X - previousCam.X);
            else
                return 0;
        }
    }
}
