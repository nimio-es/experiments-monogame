
#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

using MonoTouch.StoreKit;
using MonoTouch.Foundation;
#endregion

namespace DemoPlataforma
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class DemoPlataformaGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;
		Sprite player;
		Level level;
		EnemiesManager enemies;
		HotPointsManager hotpoints;
		AnimationsManager animations;
		BolsaItemsManager bolsa;
		CameraManager cameras;

		public DemoPlataformaGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
			graphics.SupportedOrientations = DisplayOrientation.LandscapeRight | DisplayOrientation.LandscapeLeft;
			graphics.ApplyChanges ();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
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
			this.Services.AddService ( typeof ( SpriteBatch ), spriteBatch );

			SoundManager.Initialize ( Content );
			TextureManager.Initialize ( Content );
			
			font = Content.Load <SpriteFont> ( "Fonts/font.xnb" );
			
			level = new Level ( this );
			Components.Add ( level );
			player = new Sprite ( this, level );
			Components.Add ( player );
			cameras = new CameraManager ( this, level, player );			
			Components.Add ( cameras );
			this.Services.AddService ( typeof ( CameraManager ), cameras );
			enemies = new EnemiesManager ( this, level );
			Components.Add ( enemies );
			hotpoints = new HotPointsManager ( this, level );
			Components.Add ( hotpoints );
			animations = new AnimationsManager ( this, level );
			Components.Add ( animations );
			bolsa = new BolsaItemsManager ( this );
			Components.Add ( bolsa );
			// Creamos los botones relacionados con el movimiento
			Button.BuildButtons ( this, player );
			
			// Añadimos el contador de frames
			Components.Add ( new FPSCounterComponent ( this, spriteBatch, font ) );
		
			
			// Añadimos una animacion de presentacion
			animations.AddAnimacion ( new AnimacionInicio ( font ) );
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent ()
		{
			base.UnloadContent ();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			if ( ! SoundManager.IsPlayingMusic ) SoundManager.PlayMusica ();
			
			// comprobamos si el jugador ha "tropezado" con algun enemigo
			if ( ! player.IsDeading )
				if ( enemies.CheckColision ( player ) )
					player.YouAreDead ();
			
			// comprobamos si se ha alcanzado algun punto caliente
			hotpoints.CheckHotPoints ( player );
			
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.Black);
			
			spriteBatch.Begin ( SpriteSortMode.BackToFront, SpriteBlendMode.AlphaBlend );
			float y_title = 240;
			spriteBatch.Draw ( TextureManager.Title,
			                  new Vector2 ( 0, y_title ),
			                  new Rectangle ( 0, 0, TextureManager.Title.Width, TextureManager.Title.Height ),
			                  Color.White,
			                  0.0f,
			                  Vector2.Zero,
			                  Vector2.One,
			                  SpriteEffects.None, 0.2f );
			base.Draw (gameTime);
			spriteBatch.End ();
		}
		
		public AnimationsManager Animations { get { return animations; } }
		public BolsaItemsManager BolsaItems { get { return bolsa; } }
		public CameraManager Cameras { get { return cameras; } }
		public SpriteFont GeneralFont { get { return font; } }
	}
}

