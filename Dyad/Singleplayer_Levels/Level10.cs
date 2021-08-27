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
    class Level10 : GameplayScreen
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

        public Level10()
            : base(9, 1)
        {
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);


            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");
            Texture2D MW = content.Load<Texture2D>(@"Objects\MovingWall");

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(400, 550), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(400, -5250), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(800, 250), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(800, -5250), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            Wall wall1 = new Wall(new Vector2(400, 700), @"Terrain\Floor", .4f, true);
            allMoveableObjs.Add(wall1);
            allMoveableObjs.Add(new Button(new Vector2(100, 600), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall1, -5170, 700, true, 1));
            allMoveableObjs.Add(new Box(new Vector2(100, 50), .4f, false));

            Wall wall2 = new Wall(new Vector2(800, 700), @"Terrain\Floor", .4f, true);
            allMoveableObjs.Add(wall2);
            allMoveableObjs.Add(new Button(new Vector2(1100, 600), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall2, -5170, 700, true, 1));
            allMoveableObjs.Add(new Box(new Vector2(1100, 50), .4f, false));


            allMoveableObjs.Add(new Terrain(new Vector2(600, 700), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, 520), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, 340), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, 160), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -20), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -200), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -380), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -560), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -740), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -920), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -1100), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -1280), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -1460), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -1640), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -1820), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -2000), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -2180), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -2360), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -2540), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -2720), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -2900), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -3080), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -3260), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -3440), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -3620), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -3800), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -3980), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -4160), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -4340), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -4520), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -4700), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -4880), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -5060), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(600, -5240), MW, 0, .5f, true, true));


            // left /////////////////////  

                //middle
            allMoveableObjs.Add(new Terrain(new Vector2(400, 300), RMSP, 0, .25f, true, true));
            
                //middle gap
            allMoveableObjs.Add(new Terrain(new Vector2(550, 0), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(250, 0), RMSP, 0, .25f, true, true));
                
                //left
            allMoveableObjs.Add(new Terrain(new Vector2(250, -300), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(357, -300), RMSP, 0, .25f, true, true));
                
                //right
            allMoveableObjs.Add(new Terrain(new Vector2(443, -600), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(550, -600), RMSP, 0, .25f, true, true));

                //middle
            allMoveableObjs.Add(new Terrain(new Vector2(400, -900), RMSP, 0, .25f, true, true));

                //middle gap
            allMoveableObjs.Add(new Terrain(new Vector2(550, -1200), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(250, -1200), RMSP, 0, .25f, true, true));

                //left
            allMoveableObjs.Add(new Terrain(new Vector2(250, -1500), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(357, -1500), RMSP, 0, .25f, true, true));

                //right
            allMoveableObjs.Add(new Terrain(new Vector2(443, -1800), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(550, -1800), RMSP, 0, .25f, true, true));

                //middle
            allMoveableObjs.Add(new Terrain(new Vector2(400, -2100), RMSP, 0, .25f, true, true));

                //middle gap
            allMoveableObjs.Add(new Terrain(new Vector2(550, -2400), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(250, -2400), RMSP, 0, .25f, true, true));

                //left
            allMoveableObjs.Add(new Terrain(new Vector2(250, -2700), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(357, -2700), RMSP, 0, .25f, true, true));

                //right
            allMoveableObjs.Add(new Terrain(new Vector2(443, -3000), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(550, -3000), RMSP, 0, .25f, true, true));


            //saw section

            allMoveableObjs.Add(new Saw(new Vector2(250, -3300), 250, 550, false, 5, .2f, false));

            allMoveableObjs.Add(new Saw(new Vector2(550, -3500), 250, 550, false, 5, .2f, false));

            allMoveableObjs.Add(new Saw(new Vector2(250, -3800), 250, 350, false, 3, .15f, false));
            allMoveableObjs.Add(new Saw(new Vector2(550, -3800), 450, 550, false, 3, .15f, false));

            allMoveableObjs.Add(new Saw(new Vector2(550, -4100), 350, 450, false, 2, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(550, -4200), -4300, -4100, false, 2, .1f, true));
            allMoveableObjs.Add(new Saw(new Vector2(250, -4200), -4300, -4100, false, 2, .1f, true));

            
            allMoveableObjs.Add(new Saw(new Vector2(350, -4400), -400, -4300, false, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(450, -4450), -400, -4350, false, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(550, -4500), -400, -4400, false, 3, .2f, true));

            allMoveableObjs.Add(new Saw(new Vector2(250, -4600), -400, -4500, false, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(350, -4650), -400, -4550, false, 3, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(450, -4700), -400, -4600, false, 3, .2f, true));
            




            //right ////////////////////////
                
                //middle gap
            allMoveableObjs.Add(new Terrain(new Vector2(650, 300), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, 300), RMSP, 0, .25f, true, true));
                
                // middle
            allMoveableObjs.Add(new Terrain(new Vector2(800, 0), RMSP, 0, .25f, true, true));
            
                //right
            allMoveableObjs.Add(new Terrain(new Vector2(843, -300), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, -300), RMSP, 0, .25f, true, true));

                //left
            allMoveableObjs.Add(new Terrain(new Vector2(650, -600), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(757, -600), RMSP, 0, .25f, true, true));


                // just arrows

            // arrows hitting walls
            allMoveableObjs.Add(new Terrain(new Vector2(700, -2100), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(750, -2400), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(800, -2600), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, -2700), MW, 0, .5f, true, true));
            
            // all arrows

            // open section

            arrow1 = new Arrow(new Vector2(1250, 300), 3);
            arrow1.LoadContent(content);
            arrow2 = new Arrow(new Vector2(1400, 300), 3);
            arrow2.LoadContent(content);

            arrow3 = new Arrow(new Vector2(2000, 100), 3);
            arrow3.LoadContent(content);
            arrow4 = new Arrow(new Vector2(2150, 50), 3);
            arrow4.LoadContent(content);

            arrow5 = new Arrow(new Vector2(2850, -100), 3);
            arrow5.LoadContent(content);
            arrow6 = new Arrow(new Vector2(2900, -200), 3);
            arrow6.LoadContent(content);
            arrow7 = new Arrow(new Vector2(2950, -150), 3);
            arrow7.LoadContent(content);
            arrow8 = new Arrow(new Vector2(3050, -250), 3);
            arrow8.LoadContent(content);
            arrow9 = new Arrow(new Vector2(3050, -325), 3);
            arrow9.LoadContent(content);
            arrow10 = new Arrow(new Vector2(2850, -50), 3);
            arrow10.LoadContent(content);
            
            // wall section

            // 2100, 2400, 2600, 2700
            arrow11 = new Arrow(new Vector2(1300, 300), 3);
            arrow11.LoadContent(content);
            arrow12 = new Arrow(new Vector2(1300, 0), 3);
            arrow12.LoadContent(content);
            arrow13 = new Arrow(new Vector2(1300, -200), 3);
            arrow13.LoadContent(content);
            arrow14 = new Arrow(new Vector2(1300, -300), 3);
            arrow14.LoadContent(content);

            arrow15 = new Arrow(new Vector2(1500, 300), 3);
            arrow15.LoadContent(content);
            arrow16 = new Arrow(new Vector2(1500, 0), 3);
            arrow16.LoadContent(content);
            arrow17 = new Arrow(new Vector2(1500, -200), 3);
            arrow17.LoadContent(content);
            arrow18 = new Arrow(new Vector2(1500, -300), 3);
            arrow18.LoadContent(content);
                        
            arrow19 = new Arrow(new Vector2(1800, 0), 3);
            arrow19.LoadContent(content);
            arrow20 = new Arrow(new Vector2(1800, -200), 3);
            arrow20.LoadContent(content);
            arrow21 = new Arrow(new Vector2(1800, -300), 3);
            arrow21.LoadContent(content);
            

            //middle gap
            allMoveableObjs.Add(new Terrain(new Vector2(650, -2900), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, -2900), RMSP, 0, .25f, true, true));

            // middle
            allMoveableObjs.Add(new Terrain(new Vector2(800, -3200), RMSP, 0, .25f, true, true));

            //right
            allMoveableObjs.Add(new Terrain(new Vector2(843, -3500), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, -3500), RMSP, 0, .25f, true, true));

            //left
            allMoveableObjs.Add(new Terrain(new Vector2(650, -3800), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(757, -3800), RMSP, 0, .25f, true, true));

            //middle gap
            allMoveableObjs.Add(new Terrain(new Vector2(650, -4100), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, -4100), RMSP, 0, .25f, true, true));

            // middle
            allMoveableObjs.Add(new Terrain(new Vector2(800, -4400), RMSP, 0, .25f, true, true));

            //right
            allMoveableObjs.Add(new Terrain(new Vector2(843, -4700), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, -4700), RMSP, 0, .25f, true, true));

            //left
            allMoveableObjs.Add(new Terrain(new Vector2(650, -5000), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(757, -5000), RMSP, 0, .25f, true, true));


            // load background
            Background background1 = new Background(new Vector2(0, 500));
            background1.LoadContent(content, @"Background\BuildingBot");

            Background background2 = new Background(new Vector2(0, -220));
            background2.LoadContent(content, @"Background\BuildingMid");

            Background background3 = new Background(new Vector2(0, -940));
            background3.LoadContent(content, @"Background\BuildingMid");

            Background background4 = new Background(new Vector2(0, -1660));
            background4.LoadContent(content, @"Background\BuildingMid");

            Background background5 = new Background(new Vector2(0, -2380));
            background5.LoadContent(content, @"Background\BuildingMid");

            Background background6 = new Background(new Vector2(0, -3100));
            background6.LoadContent(content, @"Background\BuildingMid");

            Background background7 = new Background(new Vector2(0, -3820));
            background7.LoadContent(content, @"Background\BuildingMid");

            Background background8 = new Background(new Vector2(0, -4540));
            background8.LoadContent(content, @"Background\BuildingMid");

            Background background9 = new Background(new Vector2(0, -5260));
            background9.LoadContent(content, @"Background\BuildingTop");

            //Background background10 = new Background(new Vector2(0, -5980));
            //background10.LoadContent(content, @"Background\BuildingTop");

            backgrounds.Add(background1);
            backgrounds.Add(background2);
            backgrounds.Add(background3);
            backgrounds.Add(background4);
            backgrounds.Add(background5);
            backgrounds.Add(background6);
            backgrounds.Add(background7);
            backgrounds.Add(background8);
            backgrounds.Add(background9);
            //backgrounds.Add(background10);

            camera = new Camera(new Vector2(0, 500), -6000, 720, 1280, 0, 0, true);
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

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            
            foreach (Moveable obj in allMoveableObjs)
            {
                if (obj is Button)
                {
                    Button butt = (Button)obj;
                    if (butt.GetWall().GetPosition().Y <= 600)
                    {
                        camera.ChangePosition(new Vector2(0, -butt.GetSpeed()), goal1, goal2, allMoveableObjs);
                        break;
                    }
                }                
            }            

            if (camera.GetPosition().Y <= -800 && camera.GetPosition().Y >= -810 
                && !allMoveableObjs.Contains(arrow1))
            {             
                allMoveableObjs.Add(arrow1);
                allMoveableObjs.Add(arrow2);
                allMoveableObjs.Add(arrow3);
                allMoveableObjs.Add(arrow4);
                allMoveableObjs.Add(arrow5);
                allMoveableObjs.Add(arrow6);
                allMoveableObjs.Add(arrow7);
                allMoveableObjs.Add(arrow8);
                allMoveableObjs.Add(arrow9);
                allMoveableObjs.Add(arrow10);
                 
            }

            if (camera.GetPosition().Y <= -1900 && camera.GetPosition().Y  >= -1910 
                && !allMoveableObjs.Contains(arrow11))
            {
                allMoveableObjs.Add(arrow11);
                allMoveableObjs.Add(arrow12);
                allMoveableObjs.Add(arrow13);
                allMoveableObjs.Add(arrow14);

                allMoveableObjs.Add(arrow15);
                allMoveableObjs.Add(arrow16);
                allMoveableObjs.Add(arrow17);
                allMoveableObjs.Add(arrow18);

                allMoveableObjs.Add(arrow19);
                allMoveableObjs.Add(arrow20);
                allMoveableObjs.Add(arrow21);
                
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}


