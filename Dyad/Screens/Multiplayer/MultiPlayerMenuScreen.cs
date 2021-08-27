#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace Dyad
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MultiPlayerMenuScreen : MenuScreen
    {
        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MultiPlayerMenuScreen()
            : base("MultiPlayer")
        {

            offset = 50;

            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry highScoreMenuEntry = new MenuEntry("High Scores");
            MenuEntry exitMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            highScoreMenuEntry.Selected += HighScoreMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            //MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(highScoreMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {   
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new MultiplayerSignInScreen(e.PlayerIndex, 1));
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new MultiplayerOptionsMenuScreen(), e.PlayerIndex);
        }


        void HighScoreMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new BackgroundScreenHighScore(), new HighScoreScreenMultiplayer());

        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {           
            LoadingScreen.Load(ScreenManager, true, null,
                   new BackgroundScreen(@"Background\HomeScreen"), new SubMenuScreen());
        }  


        #endregion
    }
}
