using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Level7 : GameplayScreen
    {
        List<List<Arrow>> ArrowGroups = new List<List<Arrow>>();       
        
        public Level7()
            : base(6, 1)
        {
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D RMLP = content.Load<Texture2D>(@"Terrain\RustyMetalLargePlatform");
            Texture2D MPC = content.Load<Texture2D>(@"Terrain\MetalPlatformChain");
            Texture2D MW = content.Load<Texture2D>(@"Objects\MovingWall");
            Texture2D P1 = content.Load<Texture2D>(@"Terrain\Platform1");
            Texture2D Pill = content.Load<Texture2D>(@"Terrain\Pillar");
                        
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(100, 500), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(225, 270), @"Cat\Cat_Goal", 1, .4f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(100, 100), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(100, 150), @"Fox\Fox_Goal", 2, .4f);
            goal2.LoadContent(content);

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            allMoveableObjs.Add(new Terrain(new Vector2(-100, 475), Pill, 0, 1f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(-100, 700), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(50, 700), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(200, 700), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(350, 700), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 700), RMLP, 0, .25f, true, true));
            
            allMoveableObjs.Add(new Terrain(new Vector2(1100, 700), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1250, 700), RMLP, 0, .25f, true, true));            


            allMoveableObjs.Add(new Terrain(new Vector2(200, 650), MW, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(414, 650), MW, 0, .25f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(600, 600), P1, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(700, 500), P1, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(800, 400), P1, 0, .5f, true, true));



            Wall wall1 = new Wall(new Vector2(307, 593), @"Terrain\RustyMetalSmallPlatform", .5f, true);
            allMoveableObjs.Add(wall1);
            allMoveableObjs.Add(new Button(new Vector2(307, 178), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall1, 350, 593, true, 3));



            // top
            allMoveableObjs.Add(new Terrain(new Vector2(-100, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(50, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(200, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(350, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(650, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(800, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1100, 200), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1250, 200), RMLP, 0, .25f, true, true));

            // cat goal lane            
            allMoveableObjs.Add(new Terrain(new Vector2(200, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(350, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(500, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(650, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(800, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(950, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1100, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1250, 320), RMLP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(120, 260), MW, 0, .25f, true, true));          

            

            Wall wall2 = new Wall(new Vector2(1200, 350), @"Objects\MovingWall", .2f, true);
            allMoveableObjs.Add(wall2);
            allMoveableObjs.Add(new Button(new Vector2(1150, 178), @"Objects\BlueButton", @"Objects\RedButton", .25f, true,
                wall2, 350, 650, false, 3));

            Wall wall3 = new Wall(new Vector2(1450, 700), @"Terrain\RustyMetalSmallPlatform", .5f, true);
            allMoveableObjs.Add(wall3);
            allMoveableObjs.Add(new Button(new Vector2(1250, 178), @"Objects\BlueButton", @"Objects\RedButton",
                .25f, true, wall3, 300, 700, true, 3));

            //forground = new Forground();
            //forground.LoadContent(content, "GameFG");          

            
            //allMoveableObjs.Add(new Box(new Vector2(1400, 100), .5f, false));

            
            List<Arrow> group1 = new List<Arrow>();

            endOfArrows = 1500;

            Arrow arrow1 = new Arrow(new Vector2(0, 600), 3.5f, endOfArrows);
            Arrow arrow2 = new Arrow(new Vector2(0, 620), 3.5f, endOfArrows);
            Arrow arrow3 = new Arrow(new Vector2(0, 640), 3.5f, endOfArrows);
            Arrow arrow4 = new Arrow(new Vector2(0, 660), 3.5f, endOfArrows);
            Arrow arrow5 = new Arrow(new Vector2(0, 580), 3.5f, endOfArrows);
            Arrow arrow6 = new Arrow(new Vector2(0, 560), 3.5f, endOfArrows);
            Arrow arrow7 = new Arrow(new Vector2(0, 540), 3.5f, endOfArrows);
            Arrow arrow8 = new Arrow(new Vector2(0, 520), 3.5f, endOfArrows);
            Arrow arrow9 = new Arrow(new Vector2(0, 500), 3.5f, endOfArrows);
            Arrow arrow10 = new Arrow(new Vector2(0, 480), 3.5f, endOfArrows);
            Arrow arrow11 = new Arrow(new Vector2(0, 460), 3.5f, endOfArrows);
            Arrow arrow12 = new Arrow(new Vector2(0, 440), 3.5f, endOfArrows);
            Arrow arrow13 = new Arrow(new Vector2(0, 420), 3.5f, endOfArrows);
            Arrow arrow14 = new Arrow(new Vector2(0, 400), 3.5f, endOfArrows);
            Arrow arrow15 = new Arrow(new Vector2(0, 380), 3.5f, endOfArrows);

            Arrow arrow16 = new Arrow(new Vector2(0, 250), 3.5f, endOfArrows);
            Arrow arrow17 = new Arrow(new Vector2(0, 280), 3.5f, endOfArrows);

            endOfArrows += 50;

            /// all arrows
            allMoveableObjs.Add(arrow1);
            group1.Add(arrow1);
            allMoveableObjs.Add(arrow2);
            group1.Add(arrow2);
            allMoveableObjs.Add(arrow3);
            group1.Add(arrow3);
            allMoveableObjs.Add(arrow4);
            group1.Add(arrow4);
            allMoveableObjs.Add(arrow5);
            group1.Add(arrow5);
            allMoveableObjs.Add(arrow6);
            group1.Add(arrow6);
            allMoveableObjs.Add(arrow7);
            group1.Add(arrow7);
            allMoveableObjs.Add(arrow8);
            group1.Add(arrow8);
            allMoveableObjs.Add(arrow9);
            group1.Add(arrow9);
            allMoveableObjs.Add(arrow10);
            group1.Add(arrow10);
            allMoveableObjs.Add(arrow11);
            group1.Add(arrow11);
            allMoveableObjs.Add(arrow12);
            group1.Add(arrow12);
            allMoveableObjs.Add(arrow13);
            group1.Add(arrow13);
            allMoveableObjs.Add(arrow14);
            group1.Add(arrow14);
            allMoveableObjs.Add(arrow15);
            group1.Add(arrow15);
            allMoveableObjs.Add(arrow16);
            group1.Add(arrow16);
            allMoveableObjs.Add(arrow17);
            group1.Add(arrow17);

            ArrowGroups.Add(group1);

            List<Arrow> group2 = new List<Arrow>();

            Arrow arrow18 = new Arrow(new Vector2(0, 660), 3.5f, endOfArrows);
            Arrow arrow19 = new Arrow(new Vector2(100, 640), 3.5f, endOfArrows);
            Arrow arrow20 = new Arrow(new Vector2(200, 620), 3.5f, endOfArrows);
            Arrow arrow21 = new Arrow(new Vector2(300, 600), 3.5f, endOfArrows);
            Arrow arrow22 = new Arrow(new Vector2(400, 580), 3.5f, endOfArrows);
            Arrow arrow23 = new Arrow(new Vector2(500, 560), 3.5f, endOfArrows);
            Arrow arrow24 = new Arrow(new Vector2(600, 540), 3.5f, endOfArrows);
            Arrow arrow25 = new Arrow(new Vector2(700, 520), 3.5f, endOfArrows);
            Arrow arrow26 = new Arrow(new Vector2(800, 500), 3.5f, endOfArrows);
            Arrow arrow27 = new Arrow(new Vector2(900, 480), 3.5f, endOfArrows);
            Arrow arrow28 = new Arrow(new Vector2(1000, 460), 3.5f, endOfArrows);
            Arrow arrow29 = new Arrow(new Vector2(1100, 440), 3.5f, endOfArrows);
            Arrow arrow30 = new Arrow(new Vector2(1200, 420), 3.5f, endOfArrows);
            Arrow arrow31 = new Arrow(new Vector2(1300, 400), 3.5f, endOfArrows);
            Arrow arrow32 = new Arrow(new Vector2(1400, 380), 3.5f, endOfArrows);

            Arrow arrow33 = new Arrow(new Vector2(1400, 250), 3.5f, endOfArrows);
            Arrow arrow34 = new Arrow(new Vector2(1400, 280), 3.5f, endOfArrows);

            Arrow arrow71 = new Arrow(new Vector2(700, 250), 3.5f, endOfArrows);
            Arrow arrow72 = new Arrow(new Vector2(700, 280), 3.5f, endOfArrows);

            
            // all arrows
            allMoveableObjs.Add(arrow18);
            group2.Add(arrow18);
            allMoveableObjs.Add(arrow19);
            group2.Add(arrow19);
            allMoveableObjs.Add(arrow20);
            group2.Add(arrow20);
            allMoveableObjs.Add(arrow21);
            group2.Add(arrow21);
            allMoveableObjs.Add(arrow22);
            group2.Add(arrow22);
            allMoveableObjs.Add(arrow23);
            group2.Add(arrow23);
            allMoveableObjs.Add(arrow24);
            group2.Add(arrow24);
            allMoveableObjs.Add(arrow25);
            group2.Add(arrow25);
            allMoveableObjs.Add(arrow26);
            group2.Add(arrow26);
            allMoveableObjs.Add(arrow27);
            group2.Add(arrow27);
            allMoveableObjs.Add(arrow28);
            group2.Add(arrow28);
            allMoveableObjs.Add(arrow29);
            group2.Add(arrow29);
            allMoveableObjs.Add(arrow30);
            group2.Add(arrow30);
            allMoveableObjs.Add(arrow31);
            group2.Add(arrow31);
            allMoveableObjs.Add(arrow32);
            group2.Add(arrow32);
            
            
            allMoveableObjs.Add(arrow71);
            group2.Add(arrow71);
            allMoveableObjs.Add(arrow72);
            group2.Add(arrow72);
            allMoveableObjs.Add(arrow33);
            group2.Add(arrow33);
            allMoveableObjs.Add(arrow34);
            group2.Add(arrow34);

            ArrowGroups.Add(group2);

            endOfArrows += 1400 + 50;

            List<Arrow> group3 = new List<Arrow>();

            Arrow arrow35 = new Arrow(new Vector2(1400, 660), 3.5f, endOfArrows);
            Arrow arrow36 = new Arrow(new Vector2(1300, 640), 3.5f, endOfArrows);
            Arrow arrow37 = new Arrow(new Vector2(1200, 620), 3.5f, endOfArrows);
            Arrow arrow38 = new Arrow(new Vector2(1100, 600), 3.5f, endOfArrows);
            Arrow arrow39 = new Arrow(new Vector2(1000, 580), 3.5f, endOfArrows);
            Arrow arrow40 = new Arrow(new Vector2(900, 560), 3.5f, endOfArrows);
            Arrow arrow41 = new Arrow(new Vector2(800, 540), 3.5f, endOfArrows);
            Arrow arrow42 = new Arrow(new Vector2(700, 520), 3.5f, endOfArrows);
            Arrow arrow43 = new Arrow(new Vector2(600, 500), 3.5f, endOfArrows);
            Arrow arrow44 = new Arrow(new Vector2(500, 480), 3.5f, endOfArrows);
            Arrow arrow45 = new Arrow(new Vector2(400, 460), 3.5f, endOfArrows);
            Arrow arrow46 = new Arrow(new Vector2(300, 440), 3.5f, endOfArrows);
            Arrow arrow47 = new Arrow(new Vector2(200, 420), 3.5f, endOfArrows);
            Arrow arrow48 = new Arrow(new Vector2(100, 400), 3.5f, endOfArrows);
            Arrow arrow49 = new Arrow(new Vector2(0, 380), 3.5f, endOfArrows);

            Arrow arrow50 = new Arrow(new Vector2(1400, 250), 3.5f, endOfArrows);
            Arrow arrow51 = new Arrow(new Vector2(1400, 280), 3.5f, endOfArrows);
            Arrow arrow73 = new Arrow(new Vector2(700, 250), 3.5f, endOfArrows);
            Arrow arrow74 = new Arrow(new Vector2(700, 280), 3.5f, endOfArrows);

            // all arrows
            allMoveableObjs.Add(arrow35);
            group3.Add(arrow35);
            allMoveableObjs.Add(arrow36);
            group3.Add(arrow36);
            allMoveableObjs.Add(arrow37);
            group3.Add(arrow37);
            allMoveableObjs.Add(arrow38);
            group3.Add(arrow38);
            allMoveableObjs.Add(arrow39);
            group3.Add(arrow39);
            allMoveableObjs.Add(arrow40);
            group3.Add(arrow40);
            allMoveableObjs.Add(arrow41);
            group3.Add(arrow41);
            allMoveableObjs.Add(arrow42);
            group3.Add(arrow42);
            allMoveableObjs.Add(arrow43);
            group3.Add(arrow43);
            allMoveableObjs.Add(arrow44);
            group3.Add(arrow44);
            allMoveableObjs.Add(arrow45);
            group3.Add(arrow45);
            allMoveableObjs.Add(arrow46);
            group3.Add(arrow46);
            allMoveableObjs.Add(arrow47);
            group3.Add(arrow47);
            allMoveableObjs.Add(arrow48);
            group3.Add(arrow48);
            allMoveableObjs.Add(arrow49);
            group3.Add(arrow49);

            allMoveableObjs.Add(arrow73);
            group3.Add(arrow73);
            allMoveableObjs.Add(arrow74);
            group3.Add(arrow74);
            allMoveableObjs.Add(arrow50);
            group3.Add(arrow50);
            allMoveableObjs.Add(arrow51);
            group3.Add(arrow51);
            

            ArrowGroups.Add(group3);

            endOfArrows += 1400 + 50;


            List<Arrow> group4 = new List<Arrow>();

            Arrow arrow52 = new Arrow(new Vector2(0, 660), 3.5f, endOfArrows);
            Arrow arrow53 = new Arrow(new Vector2(0, 640), 3.5f, endOfArrows);
            Arrow arrow54 = new Arrow(new Vector2(200, 620), 3.5f, endOfArrows);
            Arrow arrow55 = new Arrow(new Vector2(200, 600), 3.5f, endOfArrows);
            Arrow arrow56 = new Arrow(new Vector2(400, 580), 3.5f, endOfArrows);
            Arrow arrow57 = new Arrow(new Vector2(400, 560), 3.5f, endOfArrows);
            Arrow arrow58 = new Arrow(new Vector2(600, 540), 3.5f, endOfArrows);
            Arrow arrow59 = new Arrow(new Vector2(600, 520), 3.5f, endOfArrows);
            Arrow arrow60 = new Arrow(new Vector2(800, 500), 3.5f, endOfArrows);
            Arrow arrow61 = new Arrow(new Vector2(800, 480), 3.5f, endOfArrows);
            Arrow arrow64 = new Arrow(new Vector2(1000, 460), 3.5f, endOfArrows);
            Arrow arrow65 = new Arrow(new Vector2(1000, 440), 3.5f, endOfArrows);
            Arrow arrow66 = new Arrow(new Vector2(1200, 420), 3.5f, endOfArrows);
            Arrow arrow67 = new Arrow(new Vector2(1200, 400), 3.5f, endOfArrows);
            Arrow arrow68 = new Arrow(new Vector2(1400, 380), 3.5f, endOfArrows);

            Arrow arrow69 = new Arrow(new Vector2(700, 250), 3.5f, endOfArrows);
            Arrow arrow70 = new Arrow(new Vector2(700, 280), 3.5f, endOfArrows);
            

            // all arrows
            allMoveableObjs.Add(arrow52);
            group4.Add(arrow52);
            allMoveableObjs.Add(arrow53);
            group4.Add(arrow53);
            allMoveableObjs.Add(arrow54);
            group4.Add(arrow54);
            allMoveableObjs.Add(arrow55);
            group4.Add(arrow55);
            allMoveableObjs.Add(arrow56);
            group4.Add(arrow56);
            allMoveableObjs.Add(arrow57);
            group4.Add(arrow57);
            allMoveableObjs.Add(arrow58);
            group4.Add(arrow58);
            allMoveableObjs.Add(arrow59);
            group4.Add(arrow59);
            allMoveableObjs.Add(arrow60);
            group4.Add(arrow60);
            allMoveableObjs.Add(arrow61);
            group4.Add(arrow61);
            allMoveableObjs.Add(arrow64);
            group4.Add(arrow64);
            allMoveableObjs.Add(arrow65);
            group4.Add(arrow65);
            allMoveableObjs.Add(arrow66);
            group4.Add(arrow66);
            allMoveableObjs.Add(arrow67);
            group4.Add(arrow67);
            
            allMoveableObjs.Add(arrow69);
            group4.Add(arrow69);
            allMoveableObjs.Add(arrow70);
            group4.Add(arrow70);

            allMoveableObjs.Add(arrow68);
            group4.Add(arrow68);

            ArrowGroups.Add(group4);

            endOfArrows += 1400 + 50;


            camera = new Camera(new Vector2(0, 0), 0, 720, 4000, -200);
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

            endOfArrows -= 3;
            foreach (List<Arrow> group in ArrowGroups)
            {
                bool ready = true;
                foreach (Arrow arrow in group)
                {
                    if (!arrow.IsReady())
                    {
                        ready = false;
                        break;
                    }
                }

                if (ready)
                {
                    foreach (Arrow arrow in group)
                    {
                        arrow.AllRestart(endOfArrows);
                    }
                    endOfArrows = (int)(group[group.Count - 1].GetPosition().X + 50);
                }

            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}
