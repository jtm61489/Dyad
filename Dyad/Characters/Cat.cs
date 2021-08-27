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
    class Cat : Player
    {
        bool multiplayer = false;

        public Cat(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            walkingSpeed = 2;
            runningSpeed = 3;
            position = pos;
            STARTING_POSITION = pos;
            weight = .5f;
        }

        public Cat(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls, Color c)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            multiplayer = true;
            walkingSpeed = 2;
            runningSpeed = 3;
            position = pos;
            STARTING_POSITION = pos;
            weight = .5f;
            color = c;
        }

        public override void LoadContent(ContentManager content)
        {
            string mp = "";
            if (multiplayer)
                mp = "MP";

            String textureNameStanding = @"Cat\GreenCat" + mp;
            String[] textureNamesRunning = { @"Cat\CatRun1" + mp, @"Cat\CatRun2" + mp, @"Cat\CatRun3" + mp, @"Cat\CatRun4" + mp };
            String[] textureNamesWalking = { @"Cat\CatWalk1" + mp, @"Cat\CatWalk2" + mp, @"Cat\CatWalk3" + mp };
            String[] textureNamesJumping = { @"Cat\CatJump1" + mp, @"Cat\CatJump2" + mp, @"Cat\CatJump3" + mp };
            String[] textureNamesFalling = { @"Cat\CatJump3" + mp };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();

            jumpSound = content.Load<SoundEffect>(@"Sounds\jump_c").CreateInstance();
        }
    }
}
