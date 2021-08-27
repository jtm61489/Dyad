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
    class Multiplayer_Level4 : MultiplayerLevels
    {

        List<Arrow> arrows = new List<Arrow>();

        int numberOfArrows;
        int arrowsInPlay;

        public Multiplayer_Level4()
            : base(3)
        {
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");
            Texture2D Floor = content.Load<Texture2D>(@"Terrain\Floor");

            numberOfArrows = MultiplayerOptionsMenuScreen.GetNumberOfArrows();
            arrowsInPlay = 0;
            // load player
            allMoveableObjs = new List<Moveable>();
            int i = 0;

            foreach (SignedInGamer gamer in gamers)
            {                

                if (iconStates[(int)gamer.PlayerIndex] == 0)
                {
                    Fox fox = new Fox(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 550), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    fox.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    fox.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    fox.SetHealth(5);
                    allMoveableObjs.Add(fox);
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 1)
                {
                    Cat cat = new Cat(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 550), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    cat.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    cat.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    cat.SetHealth(5);
                    allMoveableObjs.Add(cat);
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 2)
                {
                    Squirrel squirrel = new Squirrel(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 550), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    squirrel.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    squirrel.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    squirrel.SetHealth(3);
                    allMoveableObjs.Add(squirrel);
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 3)
                {
                    Reindeer reindeer = new Reindeer(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 550), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    reindeer.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    reindeer.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    reindeer.SetHealth(7);
                    allMoveableObjs.Add(reindeer);
                }

                i++;
            }

            for (int k = 0; k < 50; k++)
            {
                Arrow arrow = new Arrow();
                arrow.LoadContent(content);
                arrows.Add(arrow);
            }

            Texture2D P1 = content.Load<Texture2D>(@"Terrain\Platform1");

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

            // saws 5700
            allMoveableObjs.Add(new Saw(new Vector2(250, -1000), 250, 550, false, 4, .2f, false));
            allMoveableObjs.Add(new Saw(new Vector2(900, -1000), 600, 900, false, 4, .2f, false));

            allMoveableObjs.Add(new Saw(new Vector2(550, -1200), 250, 550, false, 4, .2f, false));
            allMoveableObjs.Add(new Saw(new Vector2(600, -1200), 600, 900, false, 4, .2f, false));

            allMoveableObjs.Add(new Saw(new Vector2(250, -1500), -2000, -1500, false, 4, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(470, -1600), -2000, -1500, false, 4, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(690, -1700), -2000, -1500, false, 4, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(860, -1800), -2000, -1500, false, 4, .2f, true));

            allMoveableObjs.Add(new Saw(new Vector2(350, -2300), -2300, -2300, false, 4, 1f, true));

            allMoveableObjs.Add(new Saw(new Vector2(700, -2800), -2800, -2800, false, 4, 1f, true));

            allMoveableObjs.Add(new Saw(new Vector2(200, -3400), 200, 500, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(250, -3500), 250, 550, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(300, -3600), 300, 600, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(350, -3700), 350, 650, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(400, -3800), 400, 700, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(450, -3900), 450, 750, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(500, -4000), 500, 800, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(550, -4100), 550, 850, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(600, -4200), 600, 900, false, 4, .1f, false));

            allMoveableObjs.Add(new Saw(new Vector2(200, -5100), 200, 500, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(550, -5000), 250, 550, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(300, -4900), 300, 600, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(650, -4800), 350, 650, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(400, -4700), 400, 700, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(750, -4600), 450, 750, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(500, -4500), 500, 800, false, 4, .1f, false));
            allMoveableObjs.Add(new Saw(new Vector2(850, -4400), 550, 850, false, 4, .1f, false));

            allMoveableObjs.Add(new Saw(new Vector2(200, -4400), -4900, -4400, false, 4, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(400, -4900), -4900, -4400, false, 4, .2f, true));

            allMoveableObjs.Add(new Saw(new Vector2(700, -4600), -5100, -4400, false, 4, .2f, true));
            allMoveableObjs.Add(new Saw(new Vector2(900, -5100), -5100, -4400, false, 4, .2f, true));

            // top when it stops
            allMoveableObjs.Add(new Terrain(new Vector2(300, -5200), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(150, -5300), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(400, -5400), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(575, -5500), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(750, -5400), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, -5300), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(850, -5200), P1, 0, 1, true, true));

            allMoveableObjs.Add(new Saw(new Vector2(100, -5700), 100, 1180, false, 4, .25f, false));


            foreach (Moveable obj in allMoveableObjs)
            {
                obj.LoadContent(content);
                obj.Update(new GameTime());
            }

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

            backgrounds.Add(background1);
            backgrounds.Add(background2);
            backgrounds.Add(background3);
            backgrounds.Add(background4);
            backgrounds.Add(background5);
            backgrounds.Add(background6);
            backgrounds.Add(background7);
            backgrounds.Add(background8);
            backgrounds.Add(background9);            

            camera = new Camera(new Vector2(0, 500), -6000, 720, 1280, 0, 0, true);
            camera.UpdateX(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs, Vector2.Zero, Vector2.Zero);
            camera.UpdateY(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs);

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            int quicker = 1;

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

            if (camera.GetPosition().Y <= -5100)
            {
                numberOfArrows = 50;
                quicker = 5;
            }

            if (numberOfArrows > arrowsInPlay && timer >= (10/quicker) * arrowsInPlay)
            {
                allMoveableObjs.Add(arrows[arrowsInPlay]);
                arrowsInPlay++;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


    }
}


