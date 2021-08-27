using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Dyad
{
    class Button : Moveable
    {
        protected Wall wall;
        protected float top;
        protected float bottom;
        protected float speed;
        protected bool moving;
        protected string fileName;
        protected bool whichWay;
        protected Texture2D onTexture;
        protected string onTextureName;
        protected SoundEffect sound;
        protected bool firstTouch = true;

        public Button(Vector2 pos, string name, string name2, float scale, bool rot, Wall wal, int tp, int bott, bool which, float spd)
            : base(scale, rot, true)
        {
            position = pos;
            wall = wal;
            top = tp;
            bottom = bott;
            speed = spd;
            fileName = name;
            whichWay = which;
            onTextureName = name2;
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = fileName;
            String[] textureNamesWalking = { fileName };
            String[] textureNamesRunning = { fileName };
            String[] textureNamesJumping = { fileName };
            String[] textureNamesFalling = { fileName };
            onTexture = content.Load<Texture2D>(onTextureName);
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();

            sound = content.Load<SoundEffect>(@"Sounds\button_press");
        }

        public float GetWallY()
        {
            return wall.GetPosition().Y;
        }

        public void MoveWallUp(bool playSound)
        {
            moving = true;
            wall.moving = true;
            wall.MoveWall(-speed, playSound);

            MoveObjs(wall);
        }

        private void MoveObjs(Moveable obj)
        {
            foreach (Moveable obj2 in obj.GetObjectsOnTop())
            {
                if (!(obj2 is Weapon))
                {
                    float pos = obj.GetRect().Top - obj2.GetRect().Height / 2;
                    float objPos = obj2.GetPosition().Y;

                    obj2.ForceJump(-(objPos - pos));
                     
                    MoveObjs(obj2);
                }
            }            
        }

        public void MoveWallDown(List<Moveable> objs, int gameType, int score, bool playSound)
        {
            moving = true;
            wall.moving = true;
            wall.MoveWall(speed, playSound);

            foreach (Moveable obj in objs)
            {
                if (this.Equals(obj) || obj.IsFixedObject())
                    continue;

                float coll = Collision.CollDown(wall, obj, (int)speed);

                if (coll != 0)
                {

                    bool onGround = false;
                    foreach (Moveable obj2 in obj.GetObjectsOnBottom())
                    {
                        if (obj2 is Terrain)
                        {
                            onGround = true;
                        }
                    }

                    if (obj is Player && onGround)
                    {
                        obj.Kill(score, gameType);
                    }                   

                    if (obj.IsJumping())
                    {
                        obj.Stop();
                    }

                    float movement = speed;

                    obj.ForceJump(movement);
                }
            }
        }

        public void NoMovment()
        {
            moving = false;
            wall.moving = false;
            wall.StopSound();
        }

        public bool WallIsMoving()
        {
            return moving;
        }

        public float GetTop()
        {
            return top;
        }

        public float GetBottom()
        {
            return bottom;
        }

        public Wall GetWall()
        {
            return wall;
        }

        public bool GetWhichWay()
        {
            return whichWay;
        }

        public override float GetSpeed()
        {
            return speed;
        }

        public override void UpdateCamera(Vector2 camera)
        {
            position -= camera;
            bottom -= camera.Y;
            top -= camera.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (objectsOnTop.Count == 0)
            {
                spriteBatch.Draw(texture, position, null, Color.White,
                        rotation, center, SCALE, SpriteEffects.None, 0.0f);
                firstTouch = true;
            }
            else
            {
                spriteBatch.Draw(onTexture, position, null, Color.White,
                        rotation, center, SCALE, SpriteEffects.None, 0.0f);

                if (firstTouch && SoundOptionsMenuScreen.IsEffectsOn())
                {
                    sound.Play();
                }

                firstTouch = false;
            }
        }
    }

}

