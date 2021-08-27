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
    class Fox : Player
    {
        bool multiplayer = false;

        public Fox(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            walkingSpeed = 2;
            runningSpeed = 3;
            position = pos;
            STARTING_POSITION = pos;
            weight = .75f;
        }

        public Fox(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls, Color c)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            multiplayer = true;
            walkingSpeed = 2;
            runningSpeed = 3;
            position = pos;
            STARTING_POSITION = pos;
            weight = .75f;
            color = c;
        }
        public override void LoadContent(ContentManager content)
        {
            string mp = "";
            if (multiplayer)
                mp = "MP";

            String textureNameStanding = @"Fox\RedFox" + mp;
            String[] textureNamesWalking = { @"Fox\RedFox" + mp, @"Fox\FoxWalk1" + mp, @"Fox\FoxWalk2" + mp };
            String[] textureNamesRunning = { @"Fox\FoxRun1" + mp, @"Fox\FoxRun2" + mp, @"Fox\FoxRun3" + mp, @"Fox\FoxRun4", @"Fox\FoxRun5" + mp };
            String[] textureNamesJumping = { @"Fox\RedFox" + mp, @"Fox\FoxRun2" + mp, @"Fox\FoxRun3" + mp };
            String[] textureNamesFalling = { @"Fox\FoxRun3" + mp };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();

            jumpSound = content.Load<SoundEffect>(@"Sounds\jump_f").CreateInstance();
        }
    }
}
