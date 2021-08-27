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
    class Multiplayer_Level2 : MultiplayerLevels
    {
        
        List<Arrow> arrows = new List<Arrow>();
        
        int numberOfArrows;
        int arrowsInPlay;

        public Multiplayer_Level2()
            : base(1)
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
                    Fox fox = new Fox(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 500), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    fox.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    fox.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    fox.SetHealth(5);
                    allMoveableObjs.Add(fox);
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 1)
                {
                    Cat cat = new Cat(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 500), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    cat.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    cat.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    cat.SetHealth(5);
                    allMoveableObjs.Add(cat);
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 2)
                {
                    Squirrel squirrel = new Squirrel(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 500), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    squirrel.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    squirrel.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    squirrel.SetHealth(3);
                    allMoveableObjs.Add(squirrel);
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 3)
                {
                    Reindeer reindeer = new Reindeer(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 500), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    reindeer.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    reindeer.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    reindeer.SetHealth(7);
                    allMoveableObjs.Add(reindeer);
                }

                i++;
            }

            for (int k = 0; k < numberOfArrows; k++)
            {
                Arrow arrow = new Arrow();
                arrow.LoadContent(content);
                arrows.Add(arrow);
            }            

            allMoveableObjs.Add(new Terrain(new Vector2(1000, 700), Floor, 0, 2f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(200, 500), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(1000, 500), RMSP, 0, .25f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(400, 400), RMSP, 0, .25f, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(800, 400), RMSP, 0, .25f, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(600, 300), RMSP, 0, .5f, true, true));

            allMoveableObjs.Add(new Saw(new Vector2(600, 250), 400, 800, false, 1.5f, .2f, false));

            

            foreach (Moveable obj in allMoveableObjs)
            {
                obj.LoadContent(content);
                obj.Update(new GameTime());
            }

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);
            //forground = new Forground();
            //forground.LoadContent(content, "GameFG");

            camera = new Camera(new Vector2(0, 0), 0, 720, 1280, 0);
            camera.UpdateX(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs, Vector2.Zero, Vector2.Zero);
            camera.UpdateY(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs);

        }       

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {            

            if (numberOfArrows > arrowsInPlay && timer >= 10 * arrowsInPlay)
            {
                allMoveableObjs.Add(arrows[arrowsInPlay]);
                arrowsInPlay++;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        
    }
}

