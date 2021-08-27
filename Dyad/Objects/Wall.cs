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
    class Wall : Moveable
    {
        string fileName;
        public bool moving = false;
        protected SoundEffect sound;
        protected SoundEffectInstance instance;

        public Wall(Vector2 pos, string name,float scale, bool rot)
            : base(scale, rot, true)
        {
            fileName = name;
            position = pos;
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = fileName;
            String[] textureNamesWalking = { fileName };
            String[] textureNamesRunning = { fileName };
            String[] textureNamesJumping = { fileName };
            String[] textureNamesFalling = { fileName };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();

            sound = content.Load<SoundEffect>(@"Sounds\wall_moving");
            instance = sound.CreateInstance();
        }

        public void MoveWall(float movement, bool playSound)
        {
            position.Y += movement;
            if (instance.State != SoundState.Playing && SoundOptionsMenuScreen.IsEffectsOn() && playSound)
                Music.PlaySoundEffect(instance);
        }

        public void MoveUpToObj(float y)
        {
            position.Y = y + collRect.Height / 2;
        }

        public bool IsMoving()
        {
            return moving;
        }

        public void StopSound()
        {
            instance.Pause();
        }
    } 
}

