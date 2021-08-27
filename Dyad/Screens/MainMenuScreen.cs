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
    class MainMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("")
        {

            offset = 50;

            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry directionsMenuEntry = new MenuEntry("Directions");
            MenuEntry soundMenuEntry = new MenuEntry("Sound");
            MenuEntry creditsMenuEntry = new MenuEntry("Credits");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            directionsMenuEntry.Selected += DirectionsMenuEntrySelected;
            soundMenuEntry.Selected += SoundMenuEntrySelected;
            creditsMenuEntry.Selected += CreditsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(directionsMenuEntry);
            MenuEntries.Add(soundMenuEntry);
            MenuEntries.Add(creditsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            FileHandler.Load();
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\HomeScreen"), new SubMenuScreen());
            
        }

        /// <summary>
        /// Event handler for when the directions menu entry is selected.
        /// </summary>
        void DirectionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new DirectionsScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the sound menu entry is selected.
        /// </summary>
        void SoundMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new SoundOptionsMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\Credits"), new CreditsScreen());           
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to quit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
