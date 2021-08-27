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
    class Level6 : GameplayScreen
    {

        public Level6()
            : base(5, 1)
        {         
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D Floor = content.Load<Texture2D>(@"Terrain\Floor");
            Texture2D Pill = content.Load<Texture2D>(@"Terrain\Pillar");

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(400, 550), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(4200, 600), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(400, 250), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(4200, 260), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            // top
            allMoveableObjs.Add(new Terrain(new Vector2(0, 360), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 360), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2000, 360), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(3000, 360), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(4000, 360), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(5000, 360), Floor, 0, 1f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(800, 269), Pill, 0, .2f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(900, 247), Pill, 0, .3f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 224), Pill, 0, .4f, true, true));

            allMoveableObjs.Add(new Saw(new Vector2(1350, 260), 1350, 1750, false, 3, .2f, false));

            allMoveableObjs.Add(new Saw(new Vector2(2500, 100), 2000, 2500, true, 3, .2f, false));
            allMoveableObjs.Add(new Saw(new Vector2(2000, 260), 2000, 2500, false, 3, .2f, false));

            allMoveableObjs.Add(new Terrain(new Vector2(2800, 269), Pill, 0, .2f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2900, 247), Pill, 0, .3f, true, true));
            allMoveableObjs.Add(new Saw(new Vector2(2650, 100), 2650, 3050, false, 5, .25f, false));

            allMoveableObjs.Add(new Saw(new Vector2(3200, 400), 0, 400, false, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(3400, 0), 0, 300, true, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(3600, 400), 0, 400, false, 3, .2f, true));

            // bottom
            allMoveableObjs.Add(new Terrain(new Vector2(0, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(3000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(4000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(5000, 700), Floor, 0, 1f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(800, 609), Pill, 0, .2f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(900, 587), Pill, 0, .3f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 564), Pill, 0, .4f, true, true));

            allMoveableObjs.Add(new Saw(new Vector2(1350, 600), 1350, 1750, false, 3, .2f, false));

            allMoveableObjs.Add(new Saw(new Vector2(2500, 600), 2000, 2500, true, 3, .2f, false));
            allMoveableObjs.Add(new Saw(new Vector2(2000, 460), 2000, 2500, false, 3, .2f, false));

            allMoveableObjs.Add(new Terrain(new Vector2(2700, 609), Pill, 0, .2f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2800, 587), Pill, 0, .3f, true, true));
            allMoveableObjs.Add(new Saw(new Vector2(2550, 460), 2550, 2950, false, 5, .25f, false));

            allMoveableObjs.Add(new Saw(new Vector2(3200, 600), 400, 700, true, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(3400, 500), 400, 700, false, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(3600, 600), 400, 700, true, 3, .2f, true));


            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            camera = new Camera(new Vector2(0, 0), 0, 720, 3500, 0, 1.5f, false);
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

