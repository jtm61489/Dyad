#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
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
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class SoundOptionsMenuScreen : MenuScreen
    {
        #region Fields
        
        MenuEntry musicMenuEntry;
        MenuEntry effectsMenuEntry;
        MenuEntry musicVolumeMenuEntry;
        MenuEntry effectsVolumeMenuEntry;

        static bool music = true;
        static bool effects = true;

        static int musicLevel = 5;
        static int effectsLevel = 5;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public SoundOptionsMenuScreen()
            : base("")
        {

            offset = 50;

            // Create our menu entries.
            if (music)
            {
                musicMenuEntry = new MenuEntry("Music:   ON");
            }
            else
            {
                musicMenuEntry = new MenuEntry("Music:   OFF");
            }
            if (effects)
            {
                effectsMenuEntry = new MenuEntry("Sound Effects:   ON");
            }
            else
            {
                effectsMenuEntry = new MenuEntry("Sound Effects:   OFF");
            }

            musicVolumeMenuEntry = new MenuEntry("Music Volume:   " + musicLevel);
            effectsVolumeMenuEntry = new MenuEntry("Sound Effects Volume:   " + effectsLevel);
            
            MenuEntry changeMenuEntry = new MenuEntry("Change Song");
            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            effectsMenuEntry.Selected += EffectsMenuEntrySelected;
            musicVolumeMenuEntry.Selected += MusicVolumeMenuEntrySelected;
            effectsVolumeMenuEntry.Selected += EffectsVolumeMenuEntrySelected;
            changeMenuEntry.Selected += ChangeMenuEntrySelected;
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(effectsMenuEntry);
            MenuEntries.Add(musicVolumeMenuEntry);
            MenuEntries.Add(effectsVolumeMenuEntry);
            MenuEntries.Add(changeMenuEntry);
            MenuEntries.Add(back);
        }

        void MusicVolumeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (musicLevel == 10)
            {
                musicLevel = 1;
            }

            else
            {
                musicLevel++;
            }

            Music.ChangeVolumeMusic(musicLevel);

            MenuEntries.RemoveAt(2);
            musicVolumeMenuEntry.Text = "Music Volume:   " + musicLevel;
            MenuEntries.Insert(2, musicVolumeMenuEntry);
        }

        void EffectsVolumeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (effectsLevel == 10)
            {
                effectsLevel = 1;
            }

            else
            {
                effectsLevel++;
            }

            Music.ChangeVolumeEffects(effectsLevel);

            MenuEntries.RemoveAt(3);
            effectsVolumeMenuEntry.Text = "Effects Volume:   " + effectsLevel;
            MenuEntries.Insert(3, effectsVolumeMenuEntry);
        }

        void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (music)
            {
                music = false;
                musicMenuEntry.Text = "Music:   OFF";
            }
            else
            {
                music = true;
                musicMenuEntry.Text = "Music:   ON";
            }
        }

        void EffectsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (effects)
            {
                effects = false;
                effectsMenuEntry.Text = "Sound Effects:   OFF";
            }
            else
            {
                effects = true;
                effectsMenuEntry.Text = "Sound Effects:   ON";
            }
        }

        void ChangeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Music.ChangeSong();
        }

        static public bool IsMusicOn()
        {
            return music;
        }

        static public bool IsEffectsOn()
        {
            return effects;
        }

        #endregion
        
    }
}
