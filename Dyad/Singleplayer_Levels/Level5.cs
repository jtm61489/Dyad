using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace Dyad
{
    class Level5 : GameplayScreen
    {        

        public Level5()
            : base(4, 1)
        {        
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");
            Texture2D P1 = content.Load<Texture2D>(@"Terrain\Platform1");
            Texture2D Floor = content.Load<Texture2D>(@"Terrain\Floor");
            Texture2D ST = content.Load<Texture2D>(@"Misc\SawTrack");
            Texture2D Pill = content.Load<Texture2D>(@"Terrain\Pillar");

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            // kinda backround elements: sawtracks need to be here to get lowest priority
            allMoveableObjs.Add(new Terrain(new Vector2(350, 220), ST, MathHelper.ToRadians(90), 1, true, false));
            allMoveableObjs.Add(new Terrain(new Vector2(350, 640), ST, MathHelper.ToRadians(90), 1, true, false));

            Cat cat = new Cat(name, .5f, new Vector2(475, 550), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(-25, 240), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(950, 550), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(850, 440), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            // for cat goal
            allMoveableObjs.Add(new Terrain(new Vector2(-25, 300), P1, 0, 1, true, true));

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            allMoveableObjs.Add(new Terrain(new Vector2(100, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(200, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(300, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(400, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(700, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(800, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(900, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 650), Floor, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1100, 650), Floor, 0, 1, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(640, 375), Pill, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(640, -75), Pill, 0, 1, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(100, 375), Pill, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(100, -125), Pill, 0, 1, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(1180, 375), Pill, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1180, -75), Pill, 0, 1, true, true));


            allMoveableObjs.Add(new Terrain(new Vector2(220, 500), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 400), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(220, 325), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 200), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(220, 80), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 0), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(220, -100), P1, 0, 1, true, true));


            allMoveableObjs.Add(new Terrain(new Vector2(850, 500), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 400), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(850, 300), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 200), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(850, 100), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 0), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(850, -100), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, -200), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(850, -300), P1, 0, 1, true, true));

            
            allMoveableObjs.Add(new Saw(new Vector2(350, 0), 0, 720, false, 5, .25f, true));

            allMoveableObjs.Add(new Box(new Vector2(540, -500), .4f, false));           

            camera = new Camera(new Vector2(0, 0), -500, 720, 3000, -300);
            camera.UpdateX(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs, Vector2.Zero, Vector2.Zero);
            camera.UpdateY(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs);


            foreach (Moveable obj in allMoveableObjs)
            {
                obj.LoadContent(content);                
            }

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }
    }
}
