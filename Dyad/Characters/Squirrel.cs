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
    class Squirrel : Player
    {
        public Squirrel(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls)
            : base(name, scale*2, rot, playerNumber, numLvls)
        {
            walkingSpeed = 2f;
            runningSpeed = 3f;
            position = pos;
            STARTING_POSITION = pos;
            weight = .5f;
            color = Color.White;
        }

        public Squirrel(String name, float scale, Vector2 pos, bool rot, int playerNumber, int numLvls, Color c)
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
            String textureNameStanding = @"Squirrel\BlueSquirrel" ;
            String[] textureNamesWalking = { @"Squirrel\BlueSquirrel", @"Squirrel\BlueSquirrelWalkRun1", @"Squirrel\BlueSquirrelWalkRun2" };
            String[] textureNamesRunning = { @"Squirrel\BlueSquirrelWalkRun1", @"Squirrel\BlueSquirrelWalkRun2" };
            String[] textureNamesJumping = { @"Squirrel\BlueSquirrelJump1", @"Squirrel\BlueSquirrelJump2", @"Squirrel\BlueSquirrelJump3" };
            String[] textureNamesFalling = { @"Squirrel\BlueSquirrelJump3" };
            animation = new Animation(content, textureNameStanding, textureNamesRunning, textureNamesWalking, textureNamesJumping, textureNamesFalling);
            texture = animation.GetTexture();
            center = animation.GetCenter();
            textureData = animation.GetTextureData();
            animation.SetFastRun();


            jumpSound = content.Load<SoundEffect>(@"Sounds\jump_s").CreateInstance();

        }
    }
}
