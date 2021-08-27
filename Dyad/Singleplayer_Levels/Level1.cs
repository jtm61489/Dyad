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
    class Level1 : GameplayScreen
    {
        public Level1()
            : base(0, 1)
        {            
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");

            // load player
            allMoveableObjs = new List<Moveable>();

            string name = FindName(e);

            Cat cat = new Cat(name, .5f, new Vector2(318, 150), false, 1, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(cat);
            cat.SetBestTimes(FileHandler.GetBestTimes(name), ScreenManager.GetNormalLevels().Count);
            cat.SetBestMPScores(FileHandler.GetBestScores(name), ScreenManager.GetMultiLevels().Count);
            goal1 = new Goal(new Vector2(-20, 597), @"Cat\Cat_Goal", 1, .5f);
            goal1.LoadContent(content);

            Fox fox = new Fox("Test2", .5f, new Vector2(323, 593), false, 2, ScreenManager.GetNormalLevels().Count);
            allMoveableObjs.Add(fox);
            goal2 = new Goal(new Vector2(1100, 154), @"Fox\Fox_Goal", 2, .5f);
            goal2.LoadContent(content);

            Texture2D L3 = content.Load<Texture2D>("Level3");

            allMoveableObjs.Add(new Terrain(new Vector2(640, 300), L3, 0, 1f, true, true));            

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\Level1Background");
            backgrounds.Add(background);

            camera = new Camera(new Vector2(0, 0), -300, 720, 2000, -400);
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