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
    class SubMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public SubMenuScreen()
            : base("")
        {
            offset = 50;

            // Create our menu entries.
            MenuEntry singlePlayerGameMenuEntry = new MenuEntry("Single Player");
            MenuEntry multiPlayerMenuEntry = new MenuEntry("MultiPlayer");
            MenuEntry exitMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            singlePlayerGameMenuEntry.Selected += SinglePlayerGameMenuEntrySelected;
            multiPlayerMenuEntry.Selected += MultiPlayerMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(singlePlayerGameMenuEntry);
            MenuEntries.Add(multiPlayerMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void SinglePlayerGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\HomeScreen"), new SinglePlayerMenuScreen());

        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void MultiPlayerMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\HomeScreen"), new MultiPlayerMenuScreen());
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            //const string message = "Go back to main menu?";

            //MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            //confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            //ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
            LoadingScreen.Load(ScreenManager, true, null,
                   new BackgroundScreen(@"Background\HomeScreen"), new MainMenuScreen());
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\HomeScreen"), new MainMenuScreen());
        }


        #endregion
    }
}

