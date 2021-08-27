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
    public class Animation
    {        
        private bool jumping = false;
        private bool falling = false;
        private bool walking = false;
        private bool running = false;
        
        private int whichTexture = 0;

        List<TextureInfo> walkingTextures;
        List<TextureInfo> runningTextures;
        List<TextureInfo> jumpingTextures;
        List<TextureInfo> fallingTextures;
        TextureInfo standingTexture;

        double previousTime;
        double currentTime;

        // milliseconds
        const int TIME_DIFFERENCE = 150;
        const int TIME_DIFFERENCE_FAST = 100;

        bool fastRun = false;

        struct TextureInfo
        {
            public Texture2D texture;
            public Color[] textureData;
            public Vector2 center;
        }

        public Animation(Texture2D text)
        {
            TextureInfo temp = new TextureInfo();
            temp.texture = text;
            temp.textureData = new Color[temp.texture.Width * temp.texture.Height];
            temp.texture.GetData(temp.textureData);
            temp.center = new Vector2(temp.texture.Width / 2, temp.texture.Height / 2);

            walkingTextures = new List<TextureInfo>();
            runningTextures = new List<TextureInfo>();
            jumpingTextures = new List<TextureInfo>();
            fallingTextures = new List<TextureInfo>();
            standingTexture = new TextureInfo();

            standingTexture = temp;
            walkingTextures.Add(temp);
            runningTextures.Add(temp);
            jumpingTextures.Add(temp);
            fallingTextures.Add(temp);

        }

        public Animation(ContentManager content, String fileNameStanding, String[] fileNamesRunning, String[] fileNamesWalking, String[] fileNamesJumping, String[] fileNamesFalling)
        {
            walkingTextures = new List<TextureInfo>();
            runningTextures = new List<TextureInfo>();
            jumpingTextures = new List<TextureInfo>();
            fallingTextures = new List<TextureInfo>();

            standingTexture = new TextureInfo();
            standingTexture.texture = content.Load<Texture2D>(fileNameStanding);
            standingTexture.textureData = new Color[standingTexture.texture.Width * standingTexture.texture.Height];
            standingTexture.texture.GetData(standingTexture.textureData);
            standingTexture.center = new Vector2(standingTexture.texture.Width / 2, standingTexture.texture.Height / 2);

            foreach (String fileName in fileNamesRunning)
            {
                TextureInfo temp = new TextureInfo();
                temp.texture = content.Load<Texture2D>(fileName);
                temp.textureData = new Color[temp.texture.Width * temp.texture.Height];
                temp.texture.GetData(temp.textureData);
                temp.center = new Vector2(temp.texture.Width / 2, temp.texture.Height / 2);
                runningTextures.Add(temp);
            }

            foreach (String fileName in fileNamesWalking)
            {
                TextureInfo temp = new TextureInfo();
                temp.texture = content.Load<Texture2D>(fileName);                 
                temp.textureData = new Color[temp.texture.Width * temp.texture.Height];
                temp.texture.GetData(temp.textureData);                
                temp.center = new Vector2(temp.texture.Width / 2, temp.texture.Height / 2);
                walkingTextures.Add(temp);
            }

            foreach (String fileName in fileNamesJumping)
            {
                TextureInfo temp = new TextureInfo();
                temp.texture = content.Load<Texture2D>(fileName);
                temp.textureData = new Color[temp.texture.Width * temp.texture.Height];
                temp.texture.GetData(temp.textureData);
                temp.center = new Vector2(temp.texture.Width / 2, temp.texture.Height / 2);
                jumpingTextures.Add(temp);
            }

            foreach (String fileName in fileNamesFalling)
            {
                TextureInfo temp = new TextureInfo();
                temp.texture = content.Load<Texture2D>(fileName);
                temp.textureData = new Color[temp.texture.Width * temp.texture.Height];
                temp.texture.GetData(temp.textureData);
                temp.center = new Vector2(temp.texture.Width / 2, temp.texture.Height / 2);
                fallingTextures.Add(temp);
            }
        }

        public void Update(bool jump, bool fall, bool run, bool walk, GameTime time)
        {
            jumping = jump;
            falling = fall;
            running = run;
            walking = walk;
            currentTime = time.TotalGameTime.TotalMilliseconds;

            if (jumping)
            {
                if (whichTexture != jumpingTextures.Count)
                {
                    whichTexture = whichTexture % jumpingTextures.Count;
                }
                else
                {
                    whichTexture = jumpingTextures.Count - 1;
                }
            }

            else if (falling)
            {
                whichTexture = whichTexture % fallingTextures.Count;
            }
            else if (walking)
            {
                if (whichTexture == walkingTextures.Count)
                {
                    whichTexture = whichTexture % (walkingTextures.Count);
                    whichTexture++;
                }
                else
                {
                    whichTexture = whichTexture % walkingTextures.Count;
                }
            }

            else if (running)
            {
                whichTexture = whichTexture % runningTextures.Count;
            }

            else
            {
                whichTexture = 0;
            }
        }

        public void Animate()
        {

            if (running && fastRun && currentTime - previousTime > TIME_DIFFERENCE_FAST)
            {
                previousTime = currentTime;
                whichTexture++;
            }   
            else if (currentTime - previousTime > TIME_DIFFERENCE)
            {
                previousTime = currentTime;
                whichTexture++;
            }
                     
        }

        public void Reset()
        {
            whichTexture = 0;
        }

        public void SetFastRun()
        {
            fastRun = true;
        }

        public Texture2D GetTexture()
        {
            Texture2D texture;
            
            if (jumping)
            {
                texture = jumpingTextures[whichTexture].texture;
            }

            else if (falling)
            {
                texture = fallingTextures[whichTexture].texture;
            }

            else if (walking)
            {
                texture = walkingTextures[whichTexture].texture;
            }

            else if (running)
            {
                texture = runningTextures[whichTexture].texture;
            }

            else
            {
                texture = standingTexture.texture;
            }

            return texture;
        }

        public Vector2 GetCenter()
        {

            Vector2 vec;
            /*
            if (jumping)
            {
                vec = jumpingTextures[whichTexture].center;
            }

            else if (falling)
            {
                vec = fallingTextures[whichTexture].center;
            }

            else if (walking)
            {
                vec = walkingTextures[whichTexture].center;
            }

            else if (running)
            {
                vec = runningTextures[whichTexture].center;
            }

            else
            {*/
                vec = standingTexture.center;
            //}

            return vec;
        }

        public Color[] GetTextureData()
        {
            Color[] data;
            /*
            if (jumping)
            {
                data = jumpingTextures[whichTexture].textureData;
            }

            else if (falling)
            {
                data = fallingTextures[whichTexture].textureData;
            }

            else if (walking)
            {
                data = walkingTextures[whichTexture].textureData;
            }

            else if (running)
            {
                data = runningTextures[whichTexture].textureData;
            }

            else
            {*/
                data = standingTexture.textureData;
            //}

            return data;
        }

        public Texture2D GetCollTexture()
        {
            return standingTexture.texture;
        }        

        public Vector2 GetTrueCenter()
        {

            Vector2 vec;
            
            if (jumping)
            {
                vec = jumpingTextures[whichTexture].center;
            }

            else if (falling)
            {
                vec = fallingTextures[whichTexture].center;
            }

            else if (walking)
            {
                vec = walkingTextures[whichTexture].center;
            }

            else if (running)
            {
                vec = runningTextures[whichTexture].center;
            }

            else
            {
            vec = standingTexture.center;
            }

            return vec;
        }

        public Color[] GetTrueTextureData()
        {
            Color[] data;
            
            if (jumping)
            {
                data = jumpingTextures[whichTexture].textureData;
            }

            else if (falling)
            {
                data = fallingTextures[whichTexture].textureData;
            }

            else if (walking)
            {
                data = walkingTextures[whichTexture].textureData;
            }

            else if (running)
            {
                data = runningTextures[whichTexture].textureData;
            }

            else
            {
            data = standingTexture.textureData;
            }

            return data;
        }
    }
}
