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
    class Level4 : GameplayScreen
    {
        public Level4()
            : base(3, 1)
        {
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(300, 280), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(200, 520), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(300, 480), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(200, 280), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

           // Texture2D L6 = content.Load<Texture2D>(@"Background\DoorLevelV2");
            
            Texture2D L1 = content.Load<Texture2D>(@"Background\DoorLevelBot");
            Texture2D L2 = content.Load<Texture2D>(@"Background\DoorLevelMid");
            Texture2D L3 = content.Load<Texture2D>(@"Background\DoorLevelTop");
            Texture2D L4 = content.Load<Texture2D>(@"Background\DoorLevelRight");

            //allMoveableObjs.Add(new Terrain(new Vector2(640, 360), L6, 0, 1f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(526, 614), L1, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(526, 364), L2, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(640, 115), L3, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1166, 385), L4, 0, 1f, true, true));

            Wall wall1 = new Wall(new Vector2(400, 250), @"Objects\MovingWallWood", 1f, true);
            allMoveableObjs.Add(wall1);
            allMoveableObjs.Add(new Button(new Vector2(200, 340), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall1, 0, 250, true, .25f));

            Wall wall2 = new Wall(new Vector2(600, 250), @"Objects\MovingWallWood", 1f, true);
            allMoveableObjs.Add(wall2);
            allMoveableObjs.Add(new Button(new Vector2(200, 580), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall2, 0, 250, true, .25f));

            Wall wall3 = new Wall(new Vector2(800, 250), @"Objects\MovingWallWood", 1f, true);
            allMoveableObjs.Add(wall3);
            allMoveableObjs.Add(new Button(new Vector2(200, 580), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall3, 0, 250, true, .25f));

            Wall wall4 = new Wall(new Vector2(400, 480), @"Objects\MovingWallWood", 1f, true);
            allMoveableObjs.Add(wall4);
            allMoveableObjs.Add(new Button(new Vector2(200, 580), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall4, 480, 730, false, .25f));

            Wall wall5 = new Wall(new Vector2(600, 480), @"Objects\MovingWallWood", 1f, true);
            allMoveableObjs.Add(wall5);
            allMoveableObjs.Add(new Button(new Vector2(200, 340), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall5, 480, 730, false, .25f));

            Wall wall6 = new Wall(new Vector2(800, 480), @"Objects\MovingWallWood", 1f, true);
            allMoveableObjs.Add(wall6);
            allMoveableObjs.Add(new Button(new Vector2(200, 340), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall6, 480, 730, false, .25f));

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            camera = new Camera(new Vector2(0, 0), -300, 720, 1500, 0);
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