using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Dyad
{
    class MultiplayerLevelScreen: MenuScreen
    {

        List<MultiplayerLevels> Levels = new List<MultiplayerLevels>();
        List<SignedInGamer> gamers = new List<SignedInGamer>();

        List<int> iconStates = new List<int>();
        List<Texture2D> icons = new List<Texture2D>();
        PlayerIndex index;
        List<Color> colors = new List<Color>();

        public MultiplayerLevelScreen(List<MultiplayerLevels> allLevels, List<Texture2D> icons, List<int> iconStates, List<Color> colors)
            : base("Levels")
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.icons = icons;
            this.iconStates = iconStates;
            this.colors = colors;

            Levels = allLevels;            
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            index = e;

            base.LoadContent(gamers, e);

            this.gamers = gamers;

            bool trial = Guide.IsTrialMode;

            // Create our menu entries.
            MenuEntry level1MenuEntry = new MenuEntry("Level 1");
            MenuEntry level2MenuEntry = new MenuEntry("Level 2");
            MenuEntry level3MenuEntry = new MenuEntry("Level 3");
            MenuEntry level4MenuEntry = new MenuEntry("Level 4");            
            MenuEntry exitMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            level2MenuEntry.Selected += Level2MenuEntrySelected;
            level3MenuEntry.Selected += Level3MenuEntrySelected;
            level4MenuEntry.Selected += Level4MenuEntrySelected;            
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(level1MenuEntry);
            if(!trial)
                MenuEntries.Add(level2MenuEntry);
            MenuEntries.Add(level3MenuEntry);
            if(!trial)
                MenuEntries.Add(level4MenuEntry);            
            MenuEntries.Add(exitMenuEntry);


        }

        void Level1MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Levels[0].InitPlayers(iconStates, icons, gamers, colors);
            LoadingScreen.Load(ScreenManager, true, null, gamers, index,
                               Levels[0]);
        }

        void Level2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Levels[1].InitPlayers(iconStates, icons, gamers, colors);
            LoadingScreen.Load(ScreenManager, true, null, gamers, index,
                               Levels[1]);
        }
        
        void Level3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Levels[2].InitPlayers(iconStates, icons, gamers, colors);
            LoadingScreen.Load(ScreenManager, true, null, gamers, index,
                               Levels[2]);
        }
        
        void Level4MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Levels[3].InitPlayers(iconStates, icons, gamers, colors);
            LoadingScreen.Load(ScreenManager, true, null, gamers, index,
                               Levels[3]);
        }
        
        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Go back to Multiplayer Menu?";

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
            LoadingScreen.Load(ScreenManager, true, null,
                               new BackgroundScreen(@"Background\HomeScreen"), new MultiPlayerMenuScreen());
        }
    }
}


