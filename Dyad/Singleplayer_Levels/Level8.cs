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
    class Level8 : GameplayScreen
    {

        Arrow arrow1;
        Arrow arrow2;
        Arrow arrow3;
        Arrow arrow4;
        Arrow arrow5;
        Arrow arrow6;
        Arrow arrow7;
        Arrow arrow8;
        Arrow arrow9;
        Arrow arrow10;
        Arrow arrow11;
        Arrow arrow12;
        Arrow arrow13;
        Arrow arrow14;
        Arrow arrow15;
        Arrow arrow16;
        Arrow arrow17;
        Arrow arrow18;
        Arrow arrow19;
        Arrow arrow20;
        Arrow arrow21;
        Arrow arrow22;
        Arrow arrow23;
        Arrow arrow24;
        Arrow arrow25;
        Arrow arrow26;
        Arrow arrow27;
        Arrow arrow28;
        Arrow arrow29;
        Arrow arrow30;
        Arrow arrow31;
        Arrow arrow32;
        Arrow arrow33;
        Arrow arrow34;
        Arrow arrow35;
        Arrow arrow36;
        Arrow arrow37;
        Arrow arrow38;
        Arrow arrow39;
        Arrow arrow40;
        Arrow arrow41;
        Arrow arrow42;
        Arrow arrow43;
        Arrow arrow44;

        public Level8()
            : base(7, 1)
        {         
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D Floor = content.Load<Texture2D>(@"Terrain\Floor");

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
            
            // bottom
            allMoveableObjs.Add(new Terrain(new Vector2(0, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(3000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(4000, 700), Floor, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(5000, 700), Floor, 0, 1f, true, true));

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            camera = new Camera(new Vector2(0, 0), 0, 720, 3500, 0, 2, false);
            camera.UpdateX(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs, Vector2.Zero, Vector2.Zero);
            camera.UpdateY(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs);

            
            
            /// all arrows
            arrow1 = new Arrow(new Vector2(1300, 280), 3);
            arrow1.LoadContent(content);
            arrow2 = new Arrow(new Vector2(1300, 620), 3);
            arrow2.LoadContent(content);            

            arrow3 = new Arrow(new Vector2(1300, 280), 3);
            arrow3.LoadContent(content);
            arrow4 = new Arrow(new Vector2(1300, 620), 3);
            arrow4.LoadContent(content);
            arrow5 = new Arrow(new Vector2(1350, 255), 3);
            arrow5.LoadContent(content);
            arrow6 = new Arrow(new Vector2(1350, 595), 3);
            arrow6.LoadContent(content);

            arrow7 = new Arrow(new Vector2(1300, 280), 3);
            arrow7.LoadContent(content);
            arrow8 = new Arrow(new Vector2(1300, 620), 3);
            arrow8.LoadContent(content);
            arrow9 = new Arrow(new Vector2(1475, 230), 3);
            arrow9.LoadContent(content);
            arrow10 = new Arrow(new Vector2(1475, 570), 3);
            arrow10.LoadContent(content);

            arrow11 = new Arrow(new Vector2(1300, 280), 3);
            arrow11.LoadContent(content);
            arrow12 = new Arrow(new Vector2(1350, 620), 3);
            arrow12.LoadContent(content);
            arrow13 = new Arrow(new Vector2(1475, 230), 3);
            arrow13.LoadContent(content);
            arrow14 = new Arrow(new Vector2(1525, 570), 3);
            arrow14.LoadContent(content);
            arrow15 = new Arrow(new Vector2(1650, 280), 3);
            arrow15.LoadContent(content);
            arrow16 = new Arrow(new Vector2(1700, 620), 3);
            arrow16.LoadContent(content);

            arrow17 = new Arrow(new Vector2(1300, 280), 3);
            arrow17.LoadContent(content);
            arrow18 = new Arrow(new Vector2(1300, 620), 3);
            arrow18.LoadContent(content);
            arrow19 = new Arrow(new Vector2(1600, 230), 4);
            arrow19.LoadContent(content);
            arrow20 = new Arrow(new Vector2(1600, 570), 4);
            arrow20.LoadContent(content);

            arrow21 = new Arrow(new Vector2(1300, 280), 4);
            arrow21.LoadContent(content);
            arrow22 = new Arrow(new Vector2(1380, 620), 4);
            arrow22.LoadContent(content);
            arrow23 = new Arrow(new Vector2(1350, 250), 4);
            arrow23.LoadContent(content);
            arrow24 = new Arrow(new Vector2(1430, 590), 4);
            arrow24.LoadContent(content);
            arrow25 = new Arrow(new Vector2(1400, 220), 4);
            arrow25.LoadContent(content);
            arrow26 = new Arrow(new Vector2(1480, 560), 4);
            arrow26.LoadContent(content);
            arrow27 = new Arrow(new Vector2(1550, 140), 4);
            arrow27.LoadContent(content);
            arrow28 = new Arrow(new Vector2(1530, 480), 4);
            arrow28.LoadContent(content);

            arrow29 = new Arrow(new Vector2(1300, 280), 4);
            arrow29.LoadContent(content);
            arrow30 = new Arrow(new Vector2(1600, 620), 4);
            arrow30.LoadContent(content);
            arrow31 = new Arrow(new Vector2(1400, 250), 6);
            arrow31.LoadContent(content);
            arrow32 = new Arrow(new Vector2(1530, 590), 6);
            arrow32.LoadContent(content);


            arrow33 = new Arrow(new Vector2(1300, 280), 6);
            arrow33.LoadContent(content);
            arrow34 = new Arrow(new Vector2(1400, 620), 6);
            arrow34.LoadContent(content);
            arrow35 = new Arrow(new Vector2(1700, 280), 6);
            arrow35.LoadContent(content);
            arrow36 = new Arrow(new Vector2(1800, 620), 6);
            arrow36.LoadContent(content);
            arrow37 = new Arrow(new Vector2(2100, 280), 6);
            arrow37.LoadContent(content);
            arrow38 = new Arrow(new Vector2(2200, 620), 6);
            arrow38.LoadContent(content);
            arrow39 = new Arrow(new Vector2(2500, 280), 6);
            arrow39.LoadContent(content);
            arrow40 = new Arrow(new Vector2(2600, 620), 6);
            arrow40.LoadContent(content);
            arrow41 = new Arrow(new Vector2(2900, 280), 6);
            arrow41.LoadContent(content);
            arrow42 = new Arrow(new Vector2(3000, 620), 6);
            arrow42.LoadContent(content);
            arrow43 = new Arrow(new Vector2(3300, 280), 6);
            arrow43.LoadContent(content);
            arrow44 = new Arrow(new Vector2(3400, 620), 6);
            arrow44.LoadContent(content);



            foreach (Moveable obj in allMoveableObjs)
            {
                obj.LoadContent(content);
            }

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }       

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (camera.GetPosition().X / 300 >= 1 && !allMoveableObjs.Contains(arrow1))
            {
                allMoveableObjs.Add(arrow1);
                allMoveableObjs.Add(arrow2);                
            }

            if (camera.GetPosition().X / 500 >= 1 && !allMoveableObjs.Contains(arrow3))
            {                
                allMoveableObjs.Add(arrow3);
                allMoveableObjs.Add(arrow4);
                allMoveableObjs.Add(arrow5);
                allMoveableObjs.Add(arrow6);
            }

            if (camera.GetPosition().X / 700 >= 1 && !allMoveableObjs.Contains(arrow7))
            {
                allMoveableObjs.Add(arrow7);
                allMoveableObjs.Add(arrow8);
                allMoveableObjs.Add(arrow9);
                allMoveableObjs.Add(arrow10);
            }

            if (camera.GetPosition().X / 900 >= 1 && !allMoveableObjs.Contains(arrow11))
            {
                allMoveableObjs.Add(arrow11);
                allMoveableObjs.Add(arrow12);
                allMoveableObjs.Add(arrow13);
                allMoveableObjs.Add(arrow14);
                allMoveableObjs.Add(arrow15);
                allMoveableObjs.Add(arrow16);
            }

            if (camera.GetPosition().X / 1100 >= 1 && !allMoveableObjs.Contains(arrow17))
            {
                allMoveableObjs.Add(arrow17);
                allMoveableObjs.Add(arrow18);
                allMoveableObjs.Add(arrow19);
                allMoveableObjs.Add(arrow20);                
            }

            if (camera.GetPosition().X / 1700 >= 1 && !allMoveableObjs.Contains(arrow21))
            {
                allMoveableObjs.Add(arrow21);
                allMoveableObjs.Add(arrow22);
                allMoveableObjs.Add(arrow23);
                allMoveableObjs.Add(arrow24);
                allMoveableObjs.Add(arrow25);
                allMoveableObjs.Add(arrow26);
                allMoveableObjs.Add(arrow27);
                allMoveableObjs.Add(arrow28);
            }

            if (camera.GetPosition().X / 1950 >= 1 && !allMoveableObjs.Contains(arrow29))
            {
                allMoveableObjs.Add(arrow29);
                allMoveableObjs.Add(arrow30);
                allMoveableObjs.Add(arrow31);
                allMoveableObjs.Add(arrow32);                
            }

            if (camera.GetPosition().X / 2200 >= 1 && !allMoveableObjs.Contains(arrow33))
            {
                allMoveableObjs.Add(arrow33);
                allMoveableObjs.Add(arrow34);
                allMoveableObjs.Add(arrow35);
                allMoveableObjs.Add(arrow36);
                allMoveableObjs.Add(arrow37);
                allMoveableObjs.Add(arrow38);
                allMoveableObjs.Add(arrow39);
                allMoveableObjs.Add(arrow40);
                allMoveableObjs.Add(arrow41);
                allMoveableObjs.Add(arrow42);
                allMoveableObjs.Add(arrow43);
                allMoveableObjs.Add(arrow44);                
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}


