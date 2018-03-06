
#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using MonoTouch.StoreKit;
using MonoTouch.Foundation;

using System.Collections.Generic;
#endregion

namespace MonoGameTouchScreenTest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont exampleFont;
		List<String> verboseTouches = new List<String> ();

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			base.Initialize ();
			
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			
			exampleFont = Content.Load<SpriteFont>("SpriteFont1");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent ()
		{
			// TODO: Unload any non ContentManager content here
			base.UnloadContent ();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			verboseTouches.Clear();
			TouchCollection touchs = TouchPanel.GetState();
			if ( touchs.Count > 0 )
			{
				for(int i = 0; i < touchs.Count; i++ )
				{
					TouchLocation tl = touchs[i];
					verboseTouches.Add ( "Id: " + tl.Id );
					TouchLocation ptl;
					if ( tl.TryGetPreviousLocation ( out ptl ) )
						verboseTouches.Add ( String.Format ( "Prev: ({0}, {1})", ptl.Position.X, ptl.Position.Y ) );
					verboseTouches.Add ( String.Format ( "Position: ({0}, {1})", tl.Position.X, tl.Position.Y ) );
					verboseTouches.Add ( "State: " + tl.State.ToString() );
					verboseTouches.Add ( "--------------" );
				}
			}
			else
			{
				verboseTouches.Add ( "Push the screen" );
				verboseTouches.Add ( "to view the position" );
			}
			
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
			
			spriteBatch.Begin();
			
			float x = 15, y = 15;

			foreach ( String s in verboseTouches )
			{
				spriteBatch.DrawString ( exampleFont, s, new Vector2(x, y), Color.White, 0.0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0.0f );
				y+= 20;
			}
			
			spriteBatch.End();
			
			base.Draw (gameTime);
		}
	}
}

