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
    class Multiplayer_Level1 : MultiplayerLevels
    {

        int numberOfArrows;
        List<Arrow> arrows = new List<Arrow>();
        int arrowsInPlay = 0;
        

        public Multiplayer_Level1()
            :base(0)
        {            
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            base.LoadContent(gamers, e);

            numberOfArrows = MultiplayerOptionsMenuScreen.GetNumberOfArrows();
            arrowsInPlay = 0;

            // load player
            allMoveableObjs = new List<Moveable>();
            int i = 0;

            Texture2D P1 = content.Load<Texture2D>(@"Terrain\Platform1");

            foreach (SignedInGamer gamer in gamers)
            {
                

                if (iconStates[(int)gamer.PlayerIndex] == 0)
                {
                    Fox fox = new Fox(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 100), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    fox.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    fox.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    fox.SetHealth(5);
                    allMoveableObjs.Add(fox);                    
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 1)
                {
                    Cat cat = new Cat(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 100), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    cat.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    cat.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    cat.SetHealth(5);
                    allMoveableObjs.Add(cat);                    
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 2)
                {
                    Squirrel squirrel = new Squirrel(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 100), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
                    squirrel.SetBestMPScores(FileHandler.GetBestScores(gamer.Gamertag), ScreenManager.GetMultiLevels().Count);
                    squirrel.SetBestTimes(FileHandler.GetBestTimes(gamer.Gamertag), ScreenManager.GetNormalLevels().Count);
                    squirrel.SetHealth(3);
                    allMoveableObjs.Add(squirrel);                    
                }
                else if (iconStates[(int)gamer.PlayerIndex] == 3)
                {
                    Reindeer reindeer = new Reindeer(gamer.Gamertag, .5f, new Vector2(400 + (i * 100), 100), false, i + 1, ScreenManager.GetNormalLevels().Count, colors[(int)gamer.PlayerIndex]);
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

            allMoveableObjs.Add(new Terrain(new Vector2(10500, 600), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(10350, 500), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(10600, 400), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(10775, 300), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(10950, 400), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(11200, 500), P1, 0, 1, true, true));
            allMoveableObjs.Add(new Terrain(new Vector2(11050, 600), P1, 0, 1, true, true));

            allMoveableObjs.Add(new Terrain(new Vector2(10050, 600), P1, 0, 1, true, true));
            
            

            allMoveableObjs.Add(new Saw(new Vector2(10300, 100), 10400, 11400, false, 4, .25f, false));

            
            foreach (Moveable obj in allMoveableObjs)
            {
                obj.LoadContent(content);
                obj.Update(new GameTime());
            }

            Texture2D MW = content.Load<Texture2D>(@"objects\MovingWall");
            Texture2D butt = content.Load<Texture2D>(@"Objects\BlueButton");
            Texture2D Floor = content.Load<Texture2D>(@"Terrain\Floor");
            Texture2D Pill = content.Load<Texture2D>(@"Terrain\Pillar");
            Texture2D RMLP = content.Load<Texture2D>(@"Terrain\RustyMetalLargePlatform");
            Texture2D MPC = content.Load<Texture2D>(@"Terrain\MetalPlatformChain");
            Texture2D RMSP = content.Load<Texture2D>(@"Terrain\RustyMetalSmallPlatform");
            Texture2D SP1 = content.Load<Texture2D>(@"Terrain\sandplank1");

            Texture2D MP1 = content.Load<Texture2D>(@"Terrain\MetalPlat1");
            Texture2D RMMP = content.Load<Texture2D>(@"Terrain\RustyMetalMediumPlatform");

            Terrain wall = new Terrain(Vector2.Zero, MW, 0, 1f, true, true);
            wall.LoadContent(content);
            wall.Update(new GameTime());

            Terrain button = new Terrain(Vector2.Zero, butt, 0, 1f, true, true);
            button.LoadContent(content);
            button.Update(new GameTime());

            Texture2D floor = Floor;

            Terrain fl = new Terrain(Vector2.Zero, floor, 0, 1f, true, true);
            fl.LoadContent(content);
            fl.Update(new GameTime());

            Texture2D pill = Pill;

            Terrain pillar = new Terrain(Vector2.Zero, pill, 0, 1f, true, true);
            pillar.LoadContent(content);
            pillar.Update(new GameTime());



            Texture2D rustyLG = RMLP;
            Texture2D chain = MPC;            
            Texture2D metal1 = MP1;            
            Texture2D plat1 = P1;
            Texture2D rustyMD = RMMP;
            Texture2D rustySM = RMSP;
            Texture2D sand1 = SP1;

            List<Terrain> terrain = new List<Terrain>();
            terrain.Add(new Terrain(Vector2.Zero,
                        rustyLG, 0, 1f, true, true));
            terrain.Add(new Terrain(Vector2.Zero,
                        chain, 0, 1f, true, true));            
            terrain.Add(new Terrain(Vector2.Zero,
                        metal1, 0, 1f, true, true));            
            terrain.Add(new Terrain(Vector2.Zero,
                        plat1, 0, 1f, true, true));
            terrain.Add(new Terrain(Vector2.Zero,
                        rustyMD, 0, 1f, true, true));
            terrain.Add(new Terrain(Vector2.Zero,
                        rustySM, 0, 1f, true, true));
            terrain.Add(new Terrain(Vector2.Zero,
                        sand1, 0, 1f, true, true));

            foreach (Terrain ter in terrain)
            {
                ter.LoadContent(content);
                ter.Update(new GameTime());
            }


            Random rand = new Random();
            
            i = 0;
            float height = 500;
            float height2 = 300;

            while (i < 10000)
            {

                int which = rand.Next(0, terrain.Count);

                i += terrain[which].GetRect().Width / 2;

                int x = i;
                int y = (int)MathHelper.Clamp((rand.Next(-100, 100) + height), 350, 525);

                i += terrain[which].GetRect().Width / 2;

                Terrain temp = new Terrain(new Vector2(x, y), terrain[which].GetTexture(),
                    terrain[which].GetRotation(), terrain[which].GetScale(), true, true);
                temp.LoadContent(content);
                temp.Update(new GameTime());

                height = temp.GetPosition().Y;

                bool use = true;
                foreach (Moveable ter in allMoveableObjs)
                {
                    if (Collision.RectangleCollision(temp, ter))
                    {
                        use = false;
                    }
                }

                if (use)
                    allMoveableObjs.Add(temp);

                i += rand.Next(50, 200);


                int which2 = rand.Next(0, terrain.Count);

                int y2 = (int)MathHelper.Clamp((rand.Next(-100, 100) + height2), 100, 275);

                Terrain temp2 = new Terrain(new Vector2(x, y2), terrain[which2].GetTexture(),
                    terrain[which2].GetRotation(), terrain[which2].GetScale() - .1f, true, true);
                temp2.LoadContent(content);
                temp2.Update(new GameTime());

                height2 = temp2.GetPosition().Y;

                use = true;
                foreach (Moveable ter in allMoveableObjs)
                {
                    if (Collision.RectangleCollision(temp2, ter))
                    {
                        use = false;
                    }
                }

                if (use)
                    allMoveableObjs.Add(temp2);
            }

            int xgap = 0;
            int ygap = 0;

            for (int k = 0; k < 10000; k += fl.GetRect().Width + xgap)
            {
                xgap = 0;
                ygap = 0;

                if (rand.Next(0, 3) == 0)
                {
                    xgap = 200;
                }

                if (rand.Next(0, 3) == 0)
                {
                    ygap = -25;
                }

                if (rand.Next(0, 3) == 0)
                {
                    ygap = 50;
                }                

                Terrain temp = new Terrain(new Vector2(k + xgap, 650 + ygap), fl.GetTexture(),
                    fl.GetRotation(), fl.GetScale(), true, true);
                temp.LoadContent(content);
                temp.Update(new GameTime());

                allMoveableObjs.Add(temp);
                
            }
            

            // load background
            Background background = new Background();
            background.LoadContent(content, @"Background\GameBG");
            backgrounds.Add(background);

            //forground = new Forground();
            //forground.LoadContent(content, "GameFG");

            camera = new Camera(new Vector2(0, 0), 0, 720, 10100, 0, 2, false);            
            camera.UpdateX(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs, Vector2.Zero, Vector2.Zero);
            camera.UpdateY(0, 0, Vector2.Zero, Vector2.Zero, goal1, goal2, allMoveableObjs);

        }
        

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            int quicker = 1;

            if (camera.GetPosition().X >= 10100)
            {
                numberOfArrows = 50;
                quicker = 5;
            }

            if (numberOfArrows > arrowsInPlay && timer >= (10 / quicker) * arrowsInPlay)
            {
                allMoveableObjs.Add(arrows[arrowsInPlay]);
                arrowsInPlay++;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }        
    }
}

