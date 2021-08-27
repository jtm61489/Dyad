#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace Dyad
{
    public class GameplayScreen : GameScreen
    {
        #region Fields

        //protected SoundEffectInstance music;
        protected bool musicOn;
        protected bool effectsOn;
        protected ContentManager content;
        protected SpriteFont gameFont;        
        protected List<Background> backgrounds;        
        protected Forground forground;        
        protected List<Moveable> allMoveableObjs;        
        protected Goal goal1;
        protected Goal goal2;
        protected Camera camera;
        protected int gameType;       
        protected double startTime = -1;
        protected double timer = -1;
        protected double pausedTime = 0;        

        protected Rectangle screenRect = new Rectangle(0, 0, 1280, 720);
        protected Texture2D timerBox;
        protected Texture2D spikes;

        protected int level;       

        protected int arrowScore = 0;

        protected float endOfArrows = 0;

        protected float pauseAlpha;

        bool paused = false;        

        PlayerIndex pIndex;

        protected List<Texture2D> icons = new List<Texture2D>();
        protected List<int> iconStates = new List<int>();
        protected List<Color> colors = new List<Color>();
        protected List<SignedInGamer> gamers = new List<SignedInGamer>();

        #endregion
        
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen(int lvl, int gameType)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            this.gameType = gameType;
            level = lvl;
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent(List<SignedInGamer> gmrs, PlayerIndex e)
        {
            pIndex = e;
            gamers = gmrs;
            backgrounds = new List<Background>();

            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>(@"Misc\gamefont");
            timerBox = content.Load<Texture2D>(@"Misc\Timerbox");
            spikes = content.Load<Texture2D>(@"Objects\SpikeWall");

            Music.ChangeSong();

            paused = false;

            arrowScore = 0;

            startTime = -1;
            timer = -1;
            pausedTime = 0;
            
        }

        public String FindName(PlayerIndex e)
        {
            int index = 0;
            for (int i = 0; i < gamers.Count; i++)
            {
                if (gamers[i].PlayerIndex == e)
                {
                    index = i;
                }
            }
            return gamers[index].Gamertag;
            
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            allMoveableObjs.Clear();
            backgrounds.Clear();
            //content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            bool trial = Guide.IsTrialMode;            

            foreach (Background bg in backgrounds)
            {
                bg.Update(camera.GetPosition());
            }

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            bool playerOneInGoal = false;
            bool playerTwoInGoal = false;

            if (!IsActive && pausedTime == 0)
            {
                pausedTime = gameTime.TotalGameTime.TotalSeconds;
            }

            // update logic for game
            if (IsActive && !otherScreenHasFocus)
            {

                if (musicOn && !(Music.SongState() == SoundState.Playing))
                {
                    Music.Play();
                }

                if (pausedTime != 0 && startTime != -1)
                {
                    startTime += gameTime.TotalGameTime.TotalSeconds - pausedTime;
                    pausedTime = 0;
                    paused = false;
                }

                if (startTime == -1)
                    startTime = gameTime.TotalGameTime.TotalSeconds;
                
                timer = (gameTime.TotalGameTime.TotalSeconds - startTime);

                float p1 = 0;
                float p2 = 0;
                Vector2 p1movement = new Vector2();
                Vector2 p2movement = new Vector2();
                List<Moveable> delete = new List<Moveable>();

                foreach (Moveable player in allMoveableObjs)
                {
                    if (player is Player)
                    {
                        if (player.GetPlayerNumber() == 1)
                        {
                            p1movement.Y = player.GetPosition().Y;
                        }
                        else
                        {
                            p2movement.Y = player.GetPosition().Y;
                        }
                    }
                }

                Physics.Gravity(allMoveableObjs);

                foreach (Moveable player in allMoveableObjs)
                {
                    if (player is Player)
                    {
                        if (player.GetPlayerNumber() == 1)
                        {
                            p1movement.Y = player.GetPosition().Y - p1movement.Y;
                            p1 = player.GetPosition().Y;
                        }
                        else
                        {
                            p2movement.Y = player.GetPosition().Y - p2movement.Y;
                            p2 = player.GetPosition().Y;
                        }
                    }
                }

                camera.UpdateY(p1, p2, p1movement, p2movement, goal1, goal2, allMoveableObjs);
                
                if (camera.GetCameraType() == 2)
                {
                    camera.UpdateX(p1, p2, p1movement, p2movement, goal1, goal2,
                            allMoveableObjs, Vector2.Zero, Vector2.Zero);
                }

                foreach (Moveable obj in allMoveableObjs)
                {
                    obj.Update(gameTime);
                }

                foreach (Moveable obj in allMoveableObjs)
                {
                    if (obj is Player)
                    {   

                        if (obj.GetPosition().Y > camera.GetBottom())
                        {
                            obj.Kill(arrowScore, gameType);
                        }

                        if (camera.GetCameraType() == 2 && !camera.GetCameraDirection())
                        {
                            if (obj.GetPosition().X <= 125)
                            {
                                obj.Kill(arrowScore, gameType);
                            }
                        }

                        foreach (Moveable weapon in allMoveableObjs)
                        {
                            if (weapon is Weapon)
                            {
                                if (Collision.ObjectsTrueIntersect(obj, weapon))
                                {
                                    if (gameType == 2 & obj.IsAlive())
                                    {
                                        int hp = obj.GetHealth();
                                        hp--;
                                        obj.SetHealth(hp);
                                        if (hp == 0)
                                        {
                                            obj.Kill(arrowScore, gameType);
                                        }
                                    }

                                        // single player no hp
                                    else
                                    {
                                        // uncomment to allow death in single player
                                        obj.Kill(arrowScore, gameType);
                                    }


                                    weapon.Restart();

                                    if (weapon is Arrow)
                                    {
                                        arrowScore++;
                                    }
                                }
                            }

                            if (weapon is Wall)
                            {
                                Collision.OnObject(obj, weapon, 1);
                            }

                        }

                        // kills player on wall/terrain squish
                        foreach (Moveable wl in obj.GetObjectsOnBottom())
                        {
                            if (wl is Wall)
                            {
                                Wall wall = (Wall)wl;
                                foreach (Moveable terrain in allMoveableObjs)
                                {
                                    if (terrain is Terrain)
                                    {
                                        if (wall.IsMoving() && Collision.ObjectsIntersect(obj, terrain) && !obj.GetObjectsOnBottom().Contains(terrain))
                                        {
                                            obj.Kill(arrowScore, gameType);       
                                        }
                                    }
                                }
                            }
                        }

                        if (goal1 != null && obj.GetPlayerNumber() == 1 && obj.GetRect().Intersects(goal1.GetRect()))
                        {
                            playerOneInGoal = true;
                        }

                        if (goal2 != null && obj.GetPlayerNumber() == 2 && obj.GetRect().Intersects(goal2.GetRect()))
                        {
                            playerTwoInGoal = true;
                        }
                    }

                    if (obj is Button)
                    {
                        List<Moveable> objs = obj.GetObjectsOnTop();
                        Button butt = (Button)obj;

                        List<Moveable> remove = new List<Moveable>();
                        foreach (Moveable arrow in objs)
                        {
                            if(arrow is Arrow)
                            {
                                remove.Add(arrow);
                            }
                        }

                        foreach (Moveable arrow in remove)
                        {
                            objs.Remove(arrow);
                        }

                        float wallY = butt.GetWallY();
                        bool direction = butt.GetWhichWay();

                        bool playSound = true;
                        if (this is Level10 || this is Multiplayer_Level4)
                            playSound = false;

                        if (direction)
                        {
                            if (objs.Count > 0 && wallY >= butt.GetTop())
                            {
                                bool move = true;
                                foreach (Moveable obj2 in allMoveableObjs)
                                {
                                    if (obj2 is Box)
                                    {
                                        Wall wall = butt.GetWall();
                                        float y = Collision.OnObject(obj2, wall, (int)butt.GetSpeed());
                                        if (y != 0)
                                        {
                                            move = false;
                                            wall.MoveUpToObj(y);
                                        }
                                    }
                                }
                                if (move)
                                {
                                    butt.MoveWallUp(playSound);
                                }
                            }
                            else if (objs.Count == 0 && wallY <= butt.GetBottom())
                            {
                                butt.MoveWallDown(allMoveableObjs, gameType, arrowScore, playSound);
                            }
                            else
                            {
                                butt.NoMovment();
                            }
                        }
                        else
                        {
                            if (objs.Count == 0 && wallY >= butt.GetTop())
                            {
                                bool move = true;
                                foreach (Moveable obj2 in allMoveableObjs)
                                {
                                    if (obj2 is Box)
                                    {
                                        Wall wall = butt.GetWall();
                                        float y = Collision.OnObject(obj2, wall, (int)butt.GetSpeed());
                                        if (y != 0)
                                        {
                                            move = false;
                                            wall.MoveUpToObj(y);
                                        }
                                    }
                                }
                                if (move)
                                {
                                    butt.MoveWallUp(playSound);
                                }
                            }
                            else if (objs.Count > 0 && wallY <= butt.GetBottom())
                            {
                                butt.MoveWallDown(allMoveableObjs, gameType, arrowScore, playSound);
                            }
                            else
                            {
                                butt.NoMovment();
                            }
                        }
                    }

                    if (obj is Arrow)
                    {
                        foreach (Moveable terrain in allMoveableObjs)
                        {
                            if (!(terrain is Player) && !(terrain is Arrow))
                            {
                                if (obj.IsFixedObject() && Collision.ArrowHelper(obj, terrain, (int)obj.GetSpeed()) != 0) //Collision.ObjectsIntersect(obj, terrain))
                                {
                                    obj.HitTerrain();
                                    arrowScore++;
                                }
                            }

                            Arrow arrow = (Arrow)obj;
                            if (arrow.IsReadyToDelete())
                            {
                                delete.Add(obj);
                            }
                        }
                    }
                }                
                

                // delete arrows for performance
                foreach (Moveable obj in delete)
                {
                    allMoveableObjs.Remove(obj);
                }

                delete.Clear();

                // end game logic

                if (gameType == 1)
                {
                    // one player dies
                    foreach (Moveable player in allMoveableObjs)
                    {
                        if (player is Player && !player.IsAlive())
                        {
                            ScreenManager.AddScreen(new DeathMenuScreen(gamers, pIndex), ControllingPlayer);
                        }
                    }
                }
                    // multiplayer
                else if (gameType == 2)
                {

                    bool over = true;
                    bool oneAlive = false;
                    foreach (Moveable player in allMoveableObjs)
                    {
                        // at least 2 are alive to keep playing 
                        if (player is Player && player.IsAlive())
                        {
                            if (oneAlive)
                            {
                                over = false;
                            }
                            oneAlive = true;
                        }                   
                      
                    }                    

                    if (over)
                    {
                        List<Moveable> players = new List<Moveable>();
                        foreach (Moveable player in allMoveableObjs)
                        {
                            if (player is Player)
                            {
                                // set winners score, add 10 to make sure score is winner
                                if (player.IsAlive())
                                {
                                    player.SetCurrentScore(arrowScore + 10);
                                }
                                if (player.GetCurrentScore() > player.GetBestMPScore(level))
                                {
                                    player.SetBestMPScore(player.GetCurrentScore(), level);
                                }
                                players.Add(player);
                            }
                        }
                        if (!trial)
                        {
                            FileHandler.Save(players.ToArray());
                        }
                        LoadingScreen.Load(ScreenManager, true, null, gamers, pIndex,
                               new MultiplayerScoreScreen(PlayerIndex.One, players, icons, iconStates, colors));
                    }
                }

                if (playerOneInGoal && playerTwoInGoal && gameType == 1)
                {
                    List<Moveable> players = new List<Moveable>();
                    foreach (Moveable player in allMoveableObjs)
                    {
                        if (player is Player)
                        {
                            player.SetCurrentScore((int)timer);
                            if (player.GetCurrentScore() < player.GetBestTime(level))
                            {
                                player.SetBestTime(player.GetCurrentScore(), level);
                            }
                            players.Add(player);
                            break;
                        }
                    }

                    if (!trial)
                    {                        
                        FileHandler.Save(players.ToArray());
                    }
                    bool lastLvl = false;
                    if (level == ScreenManager.GetNormalLevels().Count - 1)
                    {
                        lastLvl = true;
                        Music.Stop();
                    }
                    ScreenManager.AddScreen(new BeatLevelMenuScreen(gamers, pIndex, lastLvl), ControllingPlayer, null, pIndex);
                }
            }            
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (gameType == 1)
            {
                HandlePlayerInput(input, pIndex);
            }
            else
            {

                foreach (SignedInGamer gamer in gamers)
                {
                    HandlePlayerInput(input, gamer.PlayerIndex);
                }
            }
        }

        

        public void HandlePlayerInput(InputState input, PlayerIndex index)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)index;

            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];
            GamePadState previousGamePadState = input.LastGamePadStates[playerIndex];
            
            Vector2 thumbstickLeft = gamePadState.ThumbSticks.Left;
            Vector2 thumbstickRight = gamePadState.ThumbSticks.Right;
            float triggerRight = gamePadState.Triggers.Right;
            float triggerLeft = gamePadState.Triggers.Left;            
            Vector2 previous = Vector2.Zero;

            // for camera.X
            Vector2 p1movement = Vector2.Zero;
            Vector2 p2movement = Vector2.Zero;
            float p1 = 0;
            float p2 = 0;            

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];
            

            if ((input.IsPauseGame(null) || gamePadDisconnected) && !paused)
            {
                if (gameType == 1 && input.IsPauseGame(pIndex))
                {
                    Music.Pause();
                    ScreenManager.AddScreen(new PauseMenuScreen(gamers, pIndex), ControllingPlayer, null, pIndex);
                    paused = true;
                }
                else if(gameType != 1)
                {
                    Music.Pause();
                    ScreenManager.AddScreen(new MultiplayerPauseMenuScreen(), ControllingPlayer, null, pIndex);
                    paused = true;
                }                
            }
            
            
            // single player
            else if (gameType == 1)
            {

                foreach (Moveable player in allMoveableObjs)
                {

                    // take care of all of the players input
                    if (player is Player)
                    {

                        int playerNumber = player.GetPlayerNumber();

                        if (!player.IsJumping() && player.GetObjectsOnTop().Count == 0 &&
                            (player.GetObjectsOnBottom().Count > 0) &&
                            ((playerNumber == 1 && triggerLeft > .5f)
                            || playerNumber == 2 && triggerRight > .5f))
                        {
                            player.StartJump();
                            player.JumpSound();
                        }

                        if (player.IsJumping() && !player.IsForcedJumping() &&
                            ((playerNumber == 1 && triggerLeft < .2f)
                            || playerNumber == 2 && triggerRight < .2f))
                        {
                            player.Stop();
                        }

                        if (playerNumber == 1)
                        {
                            p1movement = Physics.HorizontalMovement(player, thumbstickLeft, allMoveableObjs);
                            p1 = player.GetPosition().X;
                        }
                        else if (playerNumber == 2)
                        {
                            p2movement = Physics.HorizontalMovement(player, thumbstickRight, allMoveableObjs);
                            p2 = player.GetPosition().X;
                        }

                    }
                }

                if ((thumbstickLeft.X != 0 || thumbstickRight.X != 0) && camera.GetCameraType() == 1)
                {
                    endOfArrows -= camera.UpdateX(p1, p2, p1movement, p2movement, goal1, goal2,
                        allMoveableObjs, thumbstickLeft, thumbstickRight);
                }
            }

                // multiplayer
            else
            {
                Moveable player = new Moveable(0, true, true);
                String name = "";
                foreach(SignedInGamer gamer in gamers)
                {
                    if(gamer.PlayerIndex == index)
                    {
                        name = gamer.Gamertag;
                    }
                }
                foreach (Moveable p in allMoveableObjs)
                {
                    if (p is Player && p.GetName() == name)
                    {
                        player = p;
                    }
                }
                 

                // take care of all of the players input
                if (player is Player)
                {

                    if (!player.IsJumping() && player.GetObjectsOnTop().Count == 0 &&
                        (player.GetObjectsOnBottom().Count > 0) &&
                        ((triggerLeft > .5f)
                        || triggerRight > .5f))
                    {
                        player.StartJump();
                        player.JumpSound();
                    }

                    if (player.IsJumping() && !player.IsForcedJumping() &&
                        ((triggerLeft < .2f)
                        && triggerRight < .2f))
                    {
                        player.Stop();
                    }

                    p1movement = Physics.HorizontalMovement(player, thumbstickLeft, allMoveableObjs);

                }
            }
        }
        

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;            

            spriteBatch.Begin();

            foreach (Background bg in backgrounds)
            {
                bg.Draw(spriteBatch);
            }

            if (goal1 != null)
                goal1.Draw(spriteBatch);
            if (goal2 != null)
                goal2.Draw(spriteBatch);

            foreach (Moveable obj in allMoveableObjs)
            {
                bool skip = false;
                if (obj.GetRect().Intersects(screenRect))
                {
                    if (gameType == 2 && obj is Player && !obj.IsAlive())
                    {
                        skip = true;
                    }                   

                    if(!skip)
                        obj.Draw(spriteBatch);
                }
            }            

            if (forground != null)
                forground.Draw(spriteBatch);

            if (camera.GetCameraType() == 2 && !camera.GetCameraDirection())
            {
                spriteBatch.Draw(spikes, new Vector2(0, 0), Color.White);
            }

            if (gameType == 1 && camera.GetCameraType() == 1)
            {                
                spriteBatch.Draw(timerBox, new Vector2(190, 71), null, Color.White,
                    0, new Vector2(timerBox.Bounds.Center.X, timerBox.Bounds.Center.Y), 
                    .75f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(gameFont, "" + (int)timer, new Vector2(190, 71), Color.DarkRed
                    , 0f, new Vector2(gameFont.MeasureString("" + (int)timer).X/2
                      ,gameFont.MeasureString("" + (int)timer).Y/2 )
                    , .75f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        #endregion

    }
}
