using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class CameraManager : DrawableGameComponent
	{
		struct CameraBlock 
		{
			public readonly Rectangle Region;
			public readonly Camera NewCamera;
			
			public CameraBlock ( Rectangle region, Camera camera )
			{
				Region = region;
				NewCamera = camera;
			}
		}
		
		private List<CameraBlock> camerasInTheWorld = new List<CameraBlock> ();
		private Level _universe;
		private Sprite _player;
		private TimeSpan _velocidadMovimiento = new TimeSpan ( TimeSpan.TicksPerSecond / 300 );
		private TimeSpan _velocidadChecking = new TimeSpan ( TimeSpan.TicksPerSecond / 5 );
		
		private Camera _currentCamera;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DemoPlataforma.CameraManager"/> class.
		/// </summary>
		/// <param name='game'>
		/// Game.
		/// </param>
		/// <param name='universe'>
		/// Universe.
		/// </param>
		/// <param name='player'>
		/// Player.
		/// </param>
		public CameraManager ( Game game, Level universe, Sprite player ) : base ( game )
		{
			_universe = universe;
			_player = player;
			
			// por defecto la camara inicial comienza en la posicion cero y se desplaza hasta Y = 90
			_currentCamera = new Camera ( 0 );
			_currentCamera.StartPoint = new Vector2 ( 0, 0 );
			_currentCamera.EndPoint = new Vector2 ( 0, 90 );
			_universe.CurrentCamera = _currentCamera;
			
			initializeCamerasInWorld ();
		}
		
		/// <summary>
		/// Establece puntos donde se cambia la camara que sigue al personaje
		/// </summary>
		private void initializeCamerasInWorld ()
		{
			#region Pantalla 1
			// ************************** PANTALLA 1 **********************************
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 200, 260, 80, 40 ),
				new Camera ( 1 ) {
						EndPoint = new Vector2 ( 0, 90 ),
						NumSteps = 5
					} ) );
			camerasInTheWorld.Add ( new CameraBlock ( 
				new Rectangle ( 160, 180, 60, 40 ),  
				new Camera ( 2 ) { 
						StartPoint = new Vector2 ( 0, 90 ), 
						EndPoint = new Vector2 ( 0, 0 ),
						NumSteps = 15
					} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 320, 140, 60, 40 ),
				new Camera ( 3 ) {
						EndPoint = new Vector2 ( 160, 0 )
					} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 360, 220, 40, 40 ),
				new Camera ( 4 ) {
						EndPoint = new Vector2 ( 160, 90 ),
						NumSteps = 15
					} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 400, 240, 40, 60 ),
				new Camera ( 5 ) {
						EndPoint = new Vector2 ( 160, 90 ),
						NumSteps = 5
					} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 420, 180, 40, 40 ),
				new Camera ( 6 ) {
						EndPoint = new Vector2 ( 160, 0 ),
						NumSteps = 15
					} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 520, 180, 40, 80 ),
				new Camera ( 7 ) {
						EndPoint = new Vector2 ( 160, 90 ),
						NumSteps = 10
					} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 600, 140, 40, 80 ),
				new Camera ( 8 ) {
						EndPoint = new Vector2 ( 560, 90 ),
						NumSteps = 30
				} ) );
			#endregion
			
			#region Pantalla 2
			// ************************** PANTALLA 2 **********************************
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 660, 180, 20, 40 ),
				new Camera ( 9 ) {
						EndPoint = new Vector2 ( 640, 90 ),
						NumSteps = 30
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 740, 160, 60, 60 ),
				new Camera ( 10 ) {
						EndPoint = new Vector2 ( 640, 0 ),
						NumSteps = 30
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 880, 100, 40, 40 ),
				new Camera ( 11 ) {
						EndPoint = new Vector2 ( 800, 0 ),
						NumSteps = 40
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 1160, 100, 40, 40 ),
				new Camera ( 12 ) {
						EndPoint = new Vector2 ( 800, 0 ),
						NumSteps = 6
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 1160, 180, 80, 20 ),
				new Camera ( 13 ) {
						EndPoint = new Vector2 ( 800, 90 ),
						NumSteps = 20
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 760, 240, 40, 60 ),
				new Camera ( 14 ) {
						EndPoint = new Vector2 ( 640, 90 ),
						NumSteps = 10
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 760, 240, 40, 60 ),
				new Camera ( 15 ) {
						EndPoint = new Vector2 ( 640, 90 ),
						NumSteps = 10
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 880, 240, 20, 60 ),
				new Camera ( 16 ) {
						EndPoint = new Vector2 ( 640, 90 ),
						NumSteps = 15
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 920, 240, 20, 60 ),
				new Camera ( 17 ) {
						EndPoint = new Vector2 ( 800, 90 ),
						NumSteps = 10
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 980, 240, 80, 60 ),
				new Camera ( 18 ) {
						EndPoint = new Vector2 ( 800, 90 ),
						NumSteps = 15
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 1100, 240, 20, 60 ),
				new Camera ( 19 ) {
						EndPoint = new Vector2 ( 800, 180 ),
						NumSteps = 30
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 800, 305, 80, 20 ),
				new Camera ( 20 ) {
						EndPoint = new Vector2 ( 640, 400 ),
						NumSteps = 20
				} ) );			
			#endregion
			
			#region Pantalla 3
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 1120, 340, 80, 20 ),
				new Camera ( 21 ) {
						EndPoint = new Vector2 ( 800, 320 ),
						NumSteps = 20
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 980, 420, 120, 40 ),
				new Camera ( 22 ) {
						EndPoint = new Vector2 ( 800, 400 ),
						NumSteps = 20
				} ) );
			#endregion
			
			#region Pantalla 4
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 1000, 624, 80, 10 ),
				new Camera ( 23 ) {
						EndPoint = new Vector2 ( 800, 640 ),
						NumSteps = 15
				} ) );
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 1020, 700, 40, 20 ),
				new Camera ( 24 ) {
						EndPoint = new Vector2 ( 800, 640 ),
						NumSteps = 15
				} ) );			
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 840, 680, 20, 120 ),
				new Camera ( 25 ) {
						EndPoint = new Vector2 ( 640, 640 ),
						NumSteps = 20
				} ) );			
			camerasInTheWorld.Add ( new CameraBlock (
				new Rectangle ( 640, 900, 640, 10 ),
				new Camera ( 26 ) {
						EndPoint = new Vector2 ( 800, 720 ),
						NumSteps = 30
				} ) );

			
			#endregion
		}
		
		private TimeSpan _lastUpdate = TimeSpan.Zero;
		private TimeSpan _lastCheck = TimeSpan.Zero;
		
		public override void Update (GameTime gameTime)
		{
			
			if ( gameTime.TotalGameTime - _lastUpdate > _velocidadMovimiento )
			{
				_currentCamera.MoveCamera ( _universe, _player );
				_lastUpdate = gameTime.TotalGameTime;
			}
			
			if ( gameTime.TotalGameTime - _lastCheck > _velocidadChecking )
			{
				foreach ( CameraBlock cb in camerasInTheWorld )
				{
					Rectangle rp = _player.SpaceRegion;
					if ( rp.Intersects ( cb.Region ) )
					{
						if ( cb.NewCamera.Id != _currentCamera.Id )
						{
							ChangeCamera ( cb.NewCamera );
						}
					}
				}
				
				_lastCheck = gameTime.TotalGameTime;
			}
			
			base.Update (gameTime);
		}
		
		/// <summary>
		/// Realiza el cambio de camara
		/// </summary>
		/// <param name='newCamera'>
		/// New camera.
		/// </param>
		public void ChangeCamera ( Camera newCamera )
		{
			
			Camera oldCamera = _currentCamera;
			newCamera.TransitionFromCamera ( oldCamera );
			_currentCamera = newCamera;
			_universe.CurrentCamera = newCamera;
		}
		
		public override void Draw (GameTime gameTime)
		{
			
#if SHOW_CAMERAS
			
			SpriteBatch sb = (SpriteBatch) Game.Services.GetService ( typeof ( SpriteBatch ) );
			FigureData fd = TextureManager.Figures [ FigureEnum.MarcaCamera ];
			
			foreach ( CameraBlock cb in camerasInTheWorld )
			{
				Rectangle vp = _currentCamera.CurrentViewPort;
				if ( vp.Intersects ( cb.Region ) )
				{
					Rectangle inScreen = new Rectangle ( cb.Region.X - vp.X, cb.Region.Y - vp.Y, cb.Region.Width, cb.Region.Height );
					sb.Draw ( fd.Container, inScreen, fd.Rect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, fd.Depth );
				}
			}
#endif
			
			base.Draw (gameTime);
		}
		
	}
}

