using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Dyad
{
    class SingleplayerLevelScreen: MenuScreen
    {

        List<GameScreen> Levels = new List<GameScreen>();        
        List<SignedInGamer> gamers = new List<SignedInGamer>();
        PlayerIndex index;

        public SingleplayerLevelScreen(List<GameScreen> allLevels)
            : base("Levels")
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            Levels = allLevels;
            
        }

        public override void LoadContent(List<SignedInGamer> gamers, PlayerIndex e)
        {
            index = e;
            ControllingPlayer = e;

            this.gamers = gamers;

            List<int> times = new List<int>();
            foreach (SignedInGamer gamer in gamers)
            {
                if (gamer.PlayerIndex == e)
                {
                    times = FileHandler.GetBestTimes(gamer.Gamertag);
                }
            }
            
            int lvl = 0;

            for (int i = 0; i < times.Count; i++)
            {
                if (times[i] == 9999)
                {
                    lvl = i;
                    break;
                }
                lvl = ScreenManager.GetNormalLevels().Count;
            }

            //   DELETE THIS SO YOU NEED TO BEAT LVLS TO UNLOCK NEXT LVL
            //lvl = 100;
            ///////////////////////////////////////////////////////////

            // Create our menu entries.
            MenuEntry level1MenuEntry = new MenuEntry("Level 1");
            MenuEntry level2MenuEntry = new MenuEntry("Level 2");
            MenuEntry level3MenuEntry = new MenuEntry("Level 3");
            MenuEntry level4MenuEntry = new MenuEntry("Level 4");
            MenuEntry level5MenuEntry = new MenuEntry("Level 5");
            MenuEntry level6MenuEntry = new MenuEntry("Level 6");
            MenuEntry level7MenuEntry = new MenuEntry("Level 7");
            MenuEntry level8MenuEntry = new MenuEntry("Level 8");
            MenuEntry level9MenuEntry = new MenuEntry("Level 9");
            MenuEntry level10MenuEntry = new MenuEntry("Level 10");
            MenuEntry exitMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            level2MenuEntry.Selected += Level2MenuEntrySelected;
            level3MenuEntry.Selected += Level3MenuEntrySelected;
            level4MenuEntry.Selected += Level4MenuEntrySelected;
            level5MenuEntry.Selected += Level5MenuEntrySelected;
            level6MenuEntry.Selected += Level6MenuEntrySelected;
            level7MenuEntry.Selected += Level7MenuEntrySelected;
            level8MenuEntry.Selected += Level8MenuEntrySelected;
            level9MenuEntry.Selected += Level9MenuEntrySelected;
            level10MenuEntry.Selected += Level10MenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            bool trial = Guide.IsTrialMode;


            // Add entries to the menu.
            if (lvl >= 0)
                MenuEntries.Add(level1MenuEntry);
            if ((lvl >= 1 && !trial) || (trial))
                MenuEntries.Add(level2MenuEntry);
            if (lvl >= 2 && !trial)
                MenuEntries.Add(level3MenuEntry);
            if (lvl >= 3 && !trial)
                MenuEntries.Add(level4MenuEntry);
            if ((lvl >= 4 && !trial) || (trial))
                MenuEntries.Add(level5MenuEntry);
            if (lvl >= 5 && !trial)
                MenuEntries.Add(level6MenuEntry);
            if (lvl >= 6 && !trial)
                MenuEntries.Add(level7MenuEntry);
            if (lvl >= 7 && !trial)
                MenuEntries.Add(level8MenuEntry);
            if (lvl >= 8 && !trial)
                MenuEntries.Add(level9MenuEntry);
            if (lvl >= 9 && !trial)
                MenuEntries.Add(level10MenuEntry);
            MenuEntries.Add(exitMenuEntry);


        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        void Level1MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[0]);
        }

        void Level2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[1]);
        }

        void Level3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[2]);
        }

        void Level4MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[3]);
        }

        void Level5MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[4]);
        }

        void Level6MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[5]);
        }

        void Level7MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[6]);
        }

        void Level8MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[7]);
        }

        void Level9MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[8]);
        }

        void Level10MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, index, gamers, index,
                               Levels[9]);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Go back to Single Player Menu?";

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
                               new BackgroundScreen(@"Background\HomeScreen"), new SinglePlayerMenuScreen());
        }
    }
}
