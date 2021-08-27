#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Dyad
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class MultiplayerPauseMenuScreen : MenuScreen
    {
        #region Initialization

        List<GameScreen> screens = new List<GameScreen>();        

        /// <summary>
        /// Constructor.
        /// </summary>
        public MultiplayerPauseMenuScreen()
            : base("Paused")
        {            

            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry soundMenuEntry = new MenuEntry("Sound");        
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            soundMenuEntry.Selected += SoundMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(soundMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
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

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(@"Background\HomeScreen"),
                                                           new MultiPlayerMenuScreen());
        }

        void ResetGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to Reset this level?";

            MessageBoxScreen confirmResetMessageBox = new MessageBoxScreen(message);

            confirmResetMessageBox.Accepted += ConfirmResetMessageBoxAccepted;

            ScreenManager.AddScreen(confirmResetMessageBox, ControllingPlayer);
        }

        void ConfirmResetMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            screens = ScreenManager.GetScreens();
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               screens[0]);
        }

        void SoundMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new SoundOptionsMenuScreen(), null);
        }

        #endregion
    }
}
