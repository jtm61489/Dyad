#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


#endregion

namespace Dyad
{
    public class Moveable
    {
        #region Feilds

        protected const float JUMP_FORCE = -3.5f;        

        protected Vector2 center;
        protected Vector2 position;
        protected Texture2D texture;
        protected Color[] textureData;        
        protected Matrix transform;
        protected Matrix previousTransform;
        protected Matrix previousOnlyRotation;
        protected Rectangle collRect;
        protected Animation animation;        
        protected List<Moveable> objectsToLaunch;
        protected List<Moveable> objectsOnTop;
        protected List<Moveable> objectsOnBottom;
        protected float initialVelocity;
        protected float timer = 0;        
        protected float rotation;        
        protected float SCALE;
        protected float weight;
        protected float force;
        protected float startHeight;
        protected float walkingSpeed;
        protected float runningSpeed;
        protected float rotatingSpeed;
        protected bool isJumping = false;
        protected bool isFalling = false;
        protected bool isWalking = false;
        protected bool isRunning = false;        
        protected bool direction = true;      
        protected bool fixedObject;        
        protected bool forcedJumping = false;
        protected Vector2 bottomLeft;
        protected Vector2 bottomRight;
        protected float yInter;
        protected Vector2 STARTING_POSITION;
        protected float previousRotation;
        protected bool collideable;
        protected bool alive = true;
        protected int playerNumber;
        protected int currentScore;
        protected bool restart = false;
        protected int health = 5;
        protected Color color = Color.White;

        Rectangle collRectTemp;

        protected SoundEffectInstance jumpSound;
        protected bool playJumpSound = false;
        private bool beingPushed = false;

        #endregion

        #region Initilization

        public Moveable(float scale, bool fixedObj, bool collide)            
        {
            SCALE = scale;
            objectsOnTop = new List<Moveable>();
            objectsOnBottom = new List<Moveable>();
            objectsToLaunch = new List<Moveable>();
            fixedObject = fixedObj;
            bottomLeft = new Vector2();
            bottomRight = new Vector2();
            collideable = collide;            
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        #endregion

        #region Methods

        public Vector2 CalcDiff(float x)
        {
            Vector2 unitVec = new Vector2(x, (float)(x * Math.Sin(rotation)));

            return unitVec;
        }

        public Vector2 CalcMovement(float x)
        {
            Vector2 unitVec = new Vector2(x, (float)(x * Math.Sin(rotation)));

            if (Vector2.Zero != unitVec)
                unitVec.Normalize();            

            if (x > 0)
            {
                direction = true;
            }
            else if (x < 0)
            {
                direction = false;
            }

            float speed;
            if (isWalking)
                speed = walkingSpeed;
            else
                speed = runningSpeed;

            return unitVec * speed;
        }

        public Vector2 CalcMovement(float x, float spe)
        {
            Vector2 unitVec = new Vector2(x, (float)(x * Math.Sin(rotation)));

            if (Vector2.Zero != unitVec)
                unitVec.Normalize();

            return unitVec * spe;
        }

        /// <summary>
        /// moves X coord, changes y coord based on rotation, no rotating y stays the same
        /// </summary>
        /// <param name="movement"></param>
        /// <param name="vec"></param>
        public void Move(Vector2 movement, Vector2 vec)
        {
            position += movement;
            animation.Animate();
            
            if(vec.X > .8f || vec.X < -.8f)
            {
                isRunning = true;
                isWalking = false;
            }
            else if (vec.X <= .8f && vec.X > -.8f && vec.X != 0)
            {
                isRunning = false;
                isWalking = true;
            }
            else
            {
                isRunning = false;
                isWalking = false;
            }
        }

        public void StopMove(float x)
        {            
            position.X = x;
            isRunning = false;
            isWalking = false;
            animation.Reset();            
        }

        public bool IsBeingPushed()
        {
            return beingPushed;
        }

        public void SetBeingPushed(bool pushed)
        {
            beingPushed = pushed;
        }

        public void MoveWithoutAnimate(Vector2 vec)
        {
            position += vec;
        }

        public void Jump(float y)
        {
            position.Y += y;
            animation.Animate();

            if (!isJumping)
            {
                timer = 0;
            }

            isJumping = true;
        }

        public void ForceJump(float y)
        {
            position.Y += y;            
        }

        public void ForceMove(Vector2 movement, float rot)
        {
            position += movement;            
            //rotation = rot;
        }

        /// <summary>
        /// used by gravity
        /// </summary>
        /// <param name="y"></param>
        public void Fall(float y)
        {         
            if (!isFalling)
            {
                timer = 0;
                animation.Reset();
                startHeight = position.Y;
            }

            position.Y += y;
            isFalling = true;            
        }

        /// <summary>
        /// stop falling at certain position
        /// </summary>
        /// <param name="y"></param> 
        public void StopFall(float y)
        {
            position.Y = y; //- collRect.Height / 2; // + Math.Abs((float)(Math.Tan(rotation) * collRect.Width / 2)) - 1;
            timer = 0;
            isFalling = false;
            animation.Reset();
            initialVelocity = 0;            
        }

        /// <summary>
        /// stop jumping
        /// </summary>
        public void Stop()
        {            
            timer = 0;
            isJumping = false;            
            animation.Reset();
            initialVelocity = 0;
            forcedJumping = false;
        }

        /// <summary>
        /// stop jumping at certain position
        /// </summary>
        /// <param name="y"></param>
        public void StopJump(float y)
        {
            position.Y = y;// +(collRect.Height / 2) - Math.Abs((float)(Math.Tan(rotation) * collRect.Width / 2)) + 1;
            animation.Reset();
            timer = 0;
            isJumping = false;
            forcedJumping = false;
            initialVelocity = 0;
        }

        /// <summary>
        /// stops everything to deal with jumping
        /// </summary>
        public void StopTimer()
        {            
            timer = 0;
            isJumping = false;
            isFalling = false;
            forcedJumping = false;
        }

        public void StartJump()
        {
            isJumping = true;
            animation.Reset();
            timer = 0;
            foreach (Moveable obj in objectsOnBottom)
            {
                obj.RemoveObjectOnTop(this);
            }
            objectsOnBottom.Clear();

        }

        public void StartForceJump()
        {
            isJumping = true;
            animation.Reset();
            timer = 0;
            forcedJumping = true;
        }

        public void Kill(int score, int gameType)
        {
            if (alive && gameType == 2)
            {
                currentScore = score;
                position = new Vector2(-10000, 10000);
                fixedObject = true;
            }

            alive = false;
            
        }    

        public bool IsAlive()
        {
            return alive;
        }

        public bool IsForcedJumping()
        {
            return forcedJumping;
        }

        public bool IsJumping()
        {
            return isJumping;
        }

        public bool IsFalling()
        {
            return isFalling;
        }
        
        #endregion

        public bool IsCollideable()
        {
            return collideable;
        }               

        public void AddObjectOnTop(Moveable obj)
        {
            if(!objectsOnTop.Contains(obj) && !objectsOnBottom.Contains(obj))
            {
                objectsOnTop.Add(obj);
            }
        }

        public void RemoveObjectOnTop(Moveable obj)
        {
            if (objectsOnTop.Contains(obj))
            {
                objectsOnTop.Remove(obj);
            }
        }

        public void AddObjectOnBottom(Moveable obj)
        {
            if (!objectsOnTop.Contains(obj) && !objectsOnBottom.Contains(obj))
            {
                objectsOnBottom.Add(obj);
            }
        }

        public void RemoveObjectOnBottom(Moveable obj)
        {
            if (objectsOnTop.Contains(obj))
            {                
                objectsOnBottom.Remove(obj);
            }
        }        

        public bool IsObjectOnTop(Moveable obj)
        {
            return objectsOnTop.Contains(obj);
        }

        public void AddForce(float v)
        {
            force = .5f * v * v * weight;            
        }        

        public void ResetForce()
        {
            force = 0;
        }

        public bool IsFixedObject()
        {
            return fixedObject;
        }

        public void SetHealth(int h)
        {
            health = h;
        }

        #region Set Methods
        public void SetInitialVelocity(float v)
        {
            initialVelocity = v;
        }

        public void SetStanding()
        {
            isRunning = false;
            isWalking = false;
        }

        public void SetCurrentScore(int score)
        {
            currentScore = score;
        }

        public void SetRotation(float rot)
        {
            //rotation = rot;

            /*//Update player matrix: account for change in center, rotation, and new position 
            transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                                    Matrix.CreateScale(SCALE) *
                                    Matrix.CreateRotationZ(rotation) *
                                    Matrix.CreateTranslation(new Vector3(position, 0));

            collRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, texture.Width, texture.Height), transform);*/
        }
#endregion

        #region Get Methods

        public Rectangle GetTrueRect()
        {
            return collRectTemp;
        }

        //public Matrix GetTrueTransform()
        //{
        //    return transformTemp;
        //}

        public Color[] GetTrueData()
        {
            return animation.GetTrueTextureData();
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }

        public Matrix GetTransform()
        {
            return transform;
        }

        public Rectangle GetRect()
        {
            return collRect;
        }

        public Color[] GetData()
        {
            return textureData;
        }

        public float GetTime()
        {
            return timer;
        }

        public float GetJumpForce()
        {
            return JUMP_FORCE;
        }

        public float GetRotation()
        {
            return rotation;
        }

        public virtual Texture2D GetTexture()
        {
            return animation.GetCollTexture();
        }

        public virtual Texture2D GetTrueTexture()
        {
            return animation.GetTexture();
        }

        public float GetInitialVelocity()
        {
            return initialVelocity;
        }

        public float GetScale()
        {
            return SCALE;
        }

        public Color GetPixelColor(int i, int k)
        {
            return textureData[(i * texture.Width) + k];
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetCenter()
        {
            return center;
        }

        public float GetWeight()
        {
            float total = 0;
            foreach (Moveable obj in objectsOnTop)
            {
                total += obj.GetWeight();
            }
            return weight; //+ total;
        }

        public List<Moveable> GetObjectsOnTop()
        {
            return objectsOnTop;
        }

        public List<Moveable> GetObjectsOnBottom()
        {
            return objectsOnBottom;
        }

        public float GetStartHeight()
        {
            return startHeight;
        }

        public float GetForce()
        {
            return force;
        }

        public int GetHealth()
        {
            return health;
        }        

        public float GetBottomPoint(float x)
        {
            return (((bottomLeft.Y - bottomRight.Y) / (bottomLeft.X - bottomRight.X)) * x + yInter);
        }
        #endregion

        #region Virtual Methods
        
        public virtual void HitTerrain()
        {
        }

        public virtual float GetSpeed()
        {
            float speed;
            if (isWalking)
                speed = walkingSpeed;
            else
                speed = runningSpeed;
            return speed;
        }

        public virtual string GetName()
        {
            return "Default";
        }

        public virtual int GetPlayerNumber()
        {
            return 0;
        }

        public virtual List<int> GetBestTimes()
        {
            return new List<int>();
        }

        public virtual void SetBestTimes(List<int> times, int numlvls)
        {
        }

        public virtual int GetBestTime(int lvl)
        {
            return 0;
        }

        public virtual void SetBestTime(int time, int lvl)
        {
        }

        public virtual int GetBestMPScore(int lvl)
        {
            return 0;
        }

        public virtual List<int> GetBestMPScores()
        {
            return new List<int>();
        }

        public virtual void SetBestMPScore(int score, int lvl)
        {
        }

        public virtual void SetBestMPScores(List<int> scores, int numlvls)
        {
        }

        public virtual void UpdateCamera(Vector2 camera)
        {
            position -= camera;
        }

        public virtual void Restart()
        {            
        }

        public virtual void JumpSound()
        {
            if (SoundOptionsMenuScreen.IsEffectsOn())
            {
                playJumpSound = true;
                if (jumpSound.State == SoundState.Playing)
                {
                    jumpSound.Stop();                      
                }               
            }
        }

        #endregion

        

        #region Update and Draw

        public virtual void Update(GameTime time)            
        {           

            if (isJumping || isFalling)
                timer += .05f;            

            Texture2D previousTexture = texture;

            bool falling = false;
            if ((!isJumping && !isFalling && objectsOnBottom.Count == 0) || isFalling)
                falling = true;

            animation.Update(isJumping, falling, isRunning, isWalking, time);
            texture = animation.GetTexture();

            if (playJumpSound)
            {
                jumpSound.Play();
                playJumpSound = false;

            }

            /*if (previousTexture.Bounds.Width > texture.Bounds.Width)
            {
                float diffX = ((previousTexture.Bounds.Width - texture.Bounds.Width) * SCALE) / 2;

                if (direction)
                    MoveWithoutAnimate(CalcDiff(diffX));
                else
                    MoveWithoutAnimate(CalcDiff(-diffX));
            }
            else if (previousTexture.Bounds.Width < texture.Bounds.Width)
            {
                float diffX = ((texture.Bounds.Width - previousTexture.Bounds.Width) * SCALE) / 2;

                if (direction)
                    MoveWithoutAnimate(CalcDiff(-diffX));
                else
                    MoveWithoutAnimate(CalcDiff(diffX));
            }*/

            center = animation.GetCenter();
            textureData = animation.GetTextureData();

            previousTransform = transform;

            Texture2D colltexture = animation.GetCollTexture();

            // for plank, dont use them anymore
            /*previousOnlyRotation = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                                    Matrix.CreateScale(SCALE) *
                                    Matrix.CreateRotationZ(previousRotation) *
                                    Matrix.CreateTranslation(new Vector3(position, 0));*/

            //Update player matrix: account for change in center, rotation, and new position 
            transform = Matrix.CreateTranslation(new Vector3(-center, 0)) *
                                    Matrix.CreateScale(SCALE) *
                                    Matrix.CreateRotationZ(rotation) *
                                    Matrix.CreateTranslation(new Vector3(position, 0));

            //transformTemp = Matrix.CreateTranslation(new Vector3(-animation.GetCenter(), 0)) *
            //                        Matrix.CreateScale(SCALE) *
            //                        Matrix.CreateRotationZ(rotation) *
            //                        Matrix.CreateTranslation(new Vector3(position, 0));
            
            collRectTemp = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, animation.GetTexture().Width, animation.GetTexture().Height), transform);
            

            collRect = Collision.CalculateBoundingRectangle(
                                    new Rectangle(0, 0, colltexture.Width, colltexture.Height), transform);            

            bottomLeft = Vector2.Transform(new Vector2(colltexture.Bounds.Left, colltexture.Bounds.Bottom), transform);
            bottomRight = Vector2.Transform(new Vector2(colltexture.Bounds.Right, colltexture.Bounds.Bottom), transform);
            yInter = (-((bottomLeft.Y - bottomRight.Y) / (bottomLeft.X - bottomRight.X)) * bottomLeft.X) + bottomLeft.Y;
 
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            if (direction)
            {
                spriteBatch.Draw(texture, position, null, color,
                    rotation, center, SCALE, SpriteEffects.None, 0.0f);
                
            }
            else
            {
                spriteBatch.Draw(texture, position, null, color,
                    rotation, center, SCALE, SpriteEffects.FlipHorizontally, 0.0f);              
                
            }

        #endregion

        }
    }
}
