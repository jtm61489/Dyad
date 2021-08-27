#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

#endregion

namespace Dyad
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class Dyad : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;        


        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        static readonly string[] preloadAssets =
        {
            @"Misc\gradient",
        };

        
        #endregion

        #region Initialization


        /// <summary>
        /// The main game constructor.
        /// </summary>
        public Dyad()
        {        

            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(new GamerServicesComponent(this));
            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(@"Background\HomeScreen"), null, null, PlayerIndex.One);
            screenManager.AddScreen(new MainMenuScreen(), null, null, PlayerIndex.One);            

            //Guide.SimulateTrialMode = true;
            
        }


        /// <summary>
        /// Loads graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }


        #endregion
    }


    #region Entry Point

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            using (Dyad game = new Dyad())
            {                
                game.Run();
            }
        }
    }

    #endregion
}
