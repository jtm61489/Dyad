#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
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
#endregion

namespace Dyad
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class BeatLevelMenuScreen : MenuScreen
    {
        #region Initialization

        List<GameScreen> screens = new List<GameScreen>();
        List<SignedInGamer> gamers = new List<SignedInGamer>();

        PlayerIndex index;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeatLevelMenuScreen(List<SignedInGamer> gmrs, PlayerIndex e, bool lastLvl)
            : base("Level Completed")
        {
            index = e;

            gamers = gmrs;
            bool trial = Guide.IsTrialMode;

            if (lastLvl)
            {
                MenuEntry creditsMenuEntry = new MenuEntry("Credits");
                creditsMenuEntry.Selected += CreditsMenuEntrySelected;
                MenuEntries.Add(creditsMenuEntry);
            }
            else
            {
                // Create our menu entries.
                if (!trial)
                {
                    MenuEntry nextLevelMenuEntry = new MenuEntry("Next Level");
                    nextLevelMenuEntry.Selected += NextLevelMenuEntrySelected;
                    MenuEntries.Add(nextLevelMenuEntry);
                }
                MenuEntry resetLevelMenuEntry = new MenuEntry("Reset Level");
                MenuEntry changeLevelMenuEntry = new MenuEntry("Change Level");
                MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

                // Hook up menu event handlers.

                resetLevelMenuEntry.Selected += ResetGameMenuEntrySelected;
                changeLevelMenuEntry.Selected += ChangeLevelGameMenuEntrySelected;
                quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

                // Add entries to the menu.

                MenuEntries.Add(resetLevelMenuEntry);
                MenuEntries.Add(changeLevelMenuEntry);
                MenuEntries.Add(quitGameMenuEntry);
            }
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\Credits"), new CreditsScreen());
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(@"Background\HomeScreen"),
                                                           new MainMenuScreen());
        }

        void NextLevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            List<GameScreen> lvlScreens = ScreenManager.GetNormalLevels();

            List<GameScreen> screens = ScreenManager.GetScreens();
            int nextLevel = 0;

            for (int i = 0; i < lvlScreens.Count; i++)
            {
                if(screens[0].GetType() == lvlScreens[i].GetType())
                {
                    nextLevel = i;
                }
            }
            
            nextLevel++;

            if (nextLevel == lvlScreens.Count)
            {
                nextLevel--;
            }

            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, gamers, index,
                               lvlScreens[nextLevel]);   
        }

        void ResetGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            screens = ScreenManager.GetScreens();
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, gamers, index,
                               screens[0]);
        }

        void ChangeLevelGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, gamers, index,
                               new BackgroundScreen(@"Background\UrbanBackground"), new SingleplayerLevelScreen(ScreenManager.GetNormalLevels()));
        }

        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {            
        }

        #endregion
    }
}
