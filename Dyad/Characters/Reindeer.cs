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
    class Reindeer : Player
    {
        public Reindeer(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            walkingSpeed = 2f;
            runningSpeed = 3f;
            position = pos;
            STARTING_POSITION = pos;
            weight = .5f;
        }

        public Reindeer(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls, Color c)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            walkingSpeed = 2f;
            runningSpeed = 3f;
            position = pos;
            STARTING_POSITION = pos;
            weight = .5f;
            color = c;
        }

        public override void LoadContent(ContentManager content)
        {
            String textureNameStanding = @"Reindeer\Reindeer";
            String[] textureNamesWalking = { @"Reindeer\Reindeer", @"Reindeer\ReindeerWalk1", @"Reindeer\ReindeerWalk2", @"Reindeer\ReindeerWalk3" };
            String[] textureNamesRunning = { @"Reindeer\ReindeerRun1", @"Reindeer\ReindeerRun2", @"Reindeer\ReindeerRun3", @"Reindeer\ReindeerRun4" };
            String[] textureNamesJumping = { @"Reindeer\Reindeer", @"Reindeer\ReindeerJump1", @"Reindeer\ReindeerJump2" };
            String[] textureNamesFalling = { @"Reindeer\ReindeerJump2" };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();

            jumpSound = content.Load<SoundEffect>(@"Sounds\jump_d").CreateInstance();
        }
    }
}
