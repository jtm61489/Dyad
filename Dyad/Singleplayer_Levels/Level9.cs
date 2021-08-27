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
    class Level9: GameplayScreen
    {
        
        public Level9()
            : base(8, 1)
        {            
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");
            Texture2D Floor = content.Load<Texture2D>(@"Terrain\Floor");
            Texture2D SP1 = content.Load<Texture2D>(@"Terrain\sandplank1");
            

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(250, 550), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(1125, 203), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(350, 550), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(375, -147), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            allMoveableObjs.Add(new Terrain(new Vector2(1000, 700), Floor, 0, 2f, true, true));                        

            allMoveableObjs.Add(new Terrain(new Vector2(300, 525), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(150, 450), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(0, 375), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(150, 300), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(0, 225), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(150, 150), RMSP, 0, .25f, true, true));            
                                    
            allMoveableObjs.Add(new Terrain(new Vector2(1300, 465), SP1, 0, .75f, true, true));            

            Wall wall1 = new Wall(new Vector2(230, 0), @"Objects\MovingWall", .5f, true);
            allMoveableObjs.Add(wall1);
            allMoveableObjs.Add(new Button(new Vector2(1120, 437), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall1, -600, 0, true, 3));

            allMoveableObjs.Add(new Wall(new Vector2(230, -90), @"Objects\MovingWall", .5f, true));

            Wall wall2 = new Wall(new Vector2(1000, 0), @"Objects\MovingWall", .5f, true);
            allMoveableObjs.Add(wall2);
            allMoveableObjs.Add(new Button(new Vector2(1200, 593), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall2, -600, 0, true, 3));
            
            Wall wall3 = new Wall(new Vector2(900, 375), @"Objects\MovingWall", .5f, true);
            allMoveableObjs.Add(wall3);
            allMoveableObjs.Add(new Button(new Vector2(975, 260), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall3, 375, 650, false, 3));

            Wall wall4 = new Wall(new Vector2(1300, 362), @"Objects\MovingWall", .5f, true);
            allMoveableObjs.Add(wall4);
            allMoveableObjs.Add(new Button(new Vector2(450, 593), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall4, 362, 700, false, 3f));

            Wall wall5 = new Wall(new Vector2(1200, 165), @"Objects\MovingWall", .5f, true);
            allMoveableObjs.Add(wall5);
            allMoveableObjs.Add(new Button(new Vector2(1400, 437), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall5, -100, 175, true, 3));

            Wall wall6 = new Wall(new Vector2(550, 650), @"Objects\MovingWall", .5f, true);
            allMoveableObjs.Add(wall6);
            allMoveableObjs.Add(new Button(new Vector2(400, 230), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall6, 175, 650, true, 3));

            // button 6 sits on this
            allMoveableObjs.Add(new Terrain(new Vector2(400, 250), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Wall(new Vector2(340, 160), @"Objects\MovingWall", .5f, true));

            // right of wall 5
            allMoveableObjs.Add(new Terrain(new Vector2(1300, 250), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1500, 150), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1700, 50), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1500, -50), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Wall(new Vector2(1200, -15), @"Objects\MovingWall", .5f, true));

            allMoveableObjs.Add(new Box(new Vector2(800, 240), .5f, false));
            allMoveableObjs.Add(new Box(new Vector2(800, -310), .2f, false));

            // top
            allMoveableObjs.Add(new Terrain(new Vector2(320, -25), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(530, -25), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(675, -25), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(825, -25), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(975, -25), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1125, -25), SP1, 0, .25f, true, true));

            // middle
            allMoveableObjs.Add(new Terrain(new Vector2(320, 125), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(530, 125), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(675, 125), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(825, 125), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(975, 125), SP1, 0, .25f, true, true));

            // bottom
            allMoveableObjs.Add(new Terrain(new Vector2(675, 275), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(825, 275), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(975, 275), SP1, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1125, 275), SP1, 0, .25f, true, true));
            
            
            
            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            camera = new Camera(new Vector2(0, 0), -600, 720, 3000, -500);
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
