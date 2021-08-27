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
    class Level2: GameplayScreen
    {
        public Level2()
            :base(1, 1)
        {            
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)            
        {
            base.LoadContent(gamers, e);

            Texture2D RMLP = content.Load<Texture2D>(@"Terrain\RustyMetalLargePlatform");
            Texture2D MPC = content.Load<Texture2D>(@"Terrain\MetalPlatformChain");            
            Texture2D MW = content.Load<Texture2D>(@"Objects\MovingWall");
            Texture2D P1 = content.Load<Texture2D>(@"Terrain\Platform1");

            
            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(100, 500), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(3100, 600), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(200, 500), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(2800, 600), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            allMoveableObjs.Add(new Terrain(new Vector2(-100, 680), RMLP, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(495, 680), RMLP, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1090, 680), RMLP, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1685, 680), RMLP, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2280, 680), RMLP, 0, 1f, true, true));                       
            

            Wall wall1 = new Wall(new Vector2(650, 552), @"Objects\MovingWall", .5f, true);

            allMoveableObjs.Add(new Terrain(new Vector2(0, 475), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(100, 550), P1, 0, 1, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(680, 411), MPC, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(295, 411), MPC, 0, 1f, true, true));


            allMoveableObjs.Add(wall1);
            allMoveableObjs.Add(new Button(new Vector2(570, 340), @"Objects\BlueButton", @"Objects\RedButton", .5f, true, wall1, 552, 675, false, 3));

            //forground = new Forground();
            //forground.LoadContent(content, "GameFG");          

            // part 2            

            allMoveableObjs.Add(new Terrain(new Vector2(2875, 680), RMLP, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(3470, 680), RMLP, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(4065, 680), RMLP, 0, 1f, true, true));
            
            Wall wall2 = new Wall(new Vector2(2600, 552), @"Objects\MovingWall", .5f, true);

            allMoveableObjs.Add(new Terrain(new Vector2(1900, 475), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2000, 550), P1, 0, 1, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(2575, 411), MPC, 0, 1f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2195, 411), MPC, 0, 1f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(2600, 280), MW, 0, .5f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(2600, 130), MW, 0, .5f, true, true));          


            allMoveableObjs.Add(wall2);
            allMoveableObjs.Add(new Button(new Vector2(2500, 340), @"Objects\BlueButton", @"Objects\RedButton", .5f, true, wall2, 552, 740, false, 3));

            allMoveableObjs.Add(new Box(new Vector2(3300, 100), .5f, false));


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
    }
}
