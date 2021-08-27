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
    class MultiplayerOptionsMenuScreen : MenuScreen
    {
        #region Fields

        const int MAX_ARROWS = 15;
        MenuEntry numArrowsMenuEntry;
        static int number = 10;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public MultiplayerOptionsMenuScreen()
            : base("Options")
        {

            offset = 50;

            // Create our menu entries.
            numArrowsMenuEntry = new MenuEntry("Number Of Arrows:  " + number);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            numArrowsMenuEntry.Selected += NumArrowsMenuEntrySelected;            
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(numArrowsMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            numArrowsMenuEntry.Text = "Number Of Arrows:  " + number;            
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void NumArrowsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            number++;

            if (number > MAX_ARROWS)
                number = 1;

            SetMenuEntryText();
        }

        public static int GetNumberOfArrows()
        {
            return number;
        }

        #endregion
    }
}
