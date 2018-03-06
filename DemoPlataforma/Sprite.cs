using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace DemoPlataforma
{
	
	public class Sprite : DrawableGameComponent
	{
		
		/// <summary>
		/// Permite establecer un punto de inicio cuando matan al "bicho"
		/// </summary>
		public struct LivePoint {
				public Vector2 Position;
				public Camera CameraToUse;
			
				public LivePoint ( Vector2 position, Camera camera )
				{
					Position = position;
					CameraToUse = camera;
				}
			};
		
		enum MovingState { Stop = 0, Walking = 1, Jumping = 2, Falling = 3 }
		enum HeadDirection { ToFront = 0, ToBack = 1, ToMe = 2, ToUp = 3 }
		
		private List<Nullable<Rectangle>> [,] _frames;
		private int [,] _current_frame = new int [4,2] { { 0, 1 }, { 0, 4 }, { 0, 1 }, { 0, 4} }; 
		
		private Random _rmove = new Random ();
		private Vector2 _universePosition = new Vector2 ( 100, 260 );
		private Rectangle _sensibleArea = new Rectangle ( 109, 263, 26, 35 );
		private SpriteEffects _effect = SpriteEffects.None;
		private MovingState _moving = MovingState.Stop;
		private HeadDirection _head = HeadDirection.ToFront;
		private Vector2 _direction = Vector2.Zero;
		private Level _level;
		private int _jumpingStep = 0;
		private int _jumpingSteps = 18;
		private int _jumpingFase = 0; // 0 --> para arriba, 1 --> para abajo
		private const int longJumpSteps = 14;
		private bool _muriendo = false;

		private static TimeSpan elapse_change_head = new TimeSpan ( TimeSpan.TicksPerSecond );
		private static TimeSpan elapse_step_moving = new TimeSpan ( TimeSpan.TicksPerSecond / 40 );
		private TimeSpan _last_change_head = TimeSpan.Zero;
		private TimeSpan _last_step_moving = TimeSpan.Zero;
		
		// primer punto de arranque cuando palma
		private LivePoint _puntoReinicio = new LivePoint ( new Vector2 ( 160, 180 ), new Camera ( 10000 ) { EndPoint = new Vector2 ( 0, 0 ), NumSteps = 8 } );
			
		public bool IsFalling { get { return _moving == Sprite.MovingState.Falling; } }
		public bool IsJumping { get { return _moving == Sprite.MovingState.Jumping ; } }
		public bool IsDeading { get { return _muriendo; } }
		
		// Constructor 
		public Sprite ( Game game, Level level ) : base ( game )
		{
			_level = level;
			initializeFrames ();
		}
		
		/// <summary>
		/// Inicia el proceso de muerte del machango
		/// </summary>
		public void YouAreDead ()
		{
			_muriendo = true;
			Stop ();
			SoundManager.PlayGrito ();
			( (DemoPlataformaGame) Game ).Animations.AddAnimacion ( 
				new AnimacionMuerte ( finMuerte ) {
					Posicion = _universePosition
				} );
		}
		
		private void finMuerte ()
		{
			_universePosition = _puntoReinicio.Position;
			_sensibleArea.X = (int) _universePosition.X;
			_sensibleArea.Y = (int) _universePosition.Y;
			( (DemoPlataformaGame ) Game ).Cameras.ChangeCamera ( _puntoReinicio.CameraToUse );
			_muriendo = false;
		}
		
		public void SetNewControlPoint ( Vector2 playerPosition, Vector2 cameraPosition )
		{
			_puntoReinicio.Position = playerPosition;
			_puntoReinicio.CameraToUse = new Camera ( 10000 ) { EndPoint = cameraPosition, NumSteps = 15 };
		}
		
		#region Iniciando los frames
		/// <summary>
		/// Initializes the frames.
		/// </summary>
		private void initializeFrames ()
		{
			// matriz principal
			_frames = new List<Nullable<Rectangle>> [4,4];
			
			// -- Stopped
			_frames [ (int) MovingState.Stop, (int) HeadDirection.ToFront ] = 
					new List<Nullable<Rectangle>> ( new Nullable<Rectangle> [] { new Rectangle ( 492, 0, 40, 40 ) } );
			_frames [ (int) MovingState.Stop, (int) HeadDirection.ToBack ] =
					new List<Nullable<Rectangle>> ( new Nullable<Rectangle> [] { new Rectangle ( 533, 0, 40, 40 ) } );
			_frames [ (int) MovingState.Stop, (int) HeadDirection.ToMe ] =
					new List<Nullable<Rectangle>> ( new Nullable<Rectangle> [] { new Rectangle ( 574, 0, 40, 40 ) } );
			_frames [ (int) MovingState.Stop, (int) HeadDirection.ToUp ] =
					new List<Nullable<Rectangle>> ( new Nullable<Rectangle> [] { new Rectangle ( 615, 0, 40, 40 ) } );
						
			// -- Walking
			_frames [ (int) MovingState.Walking, (int) HeadDirection.ToFront ] =
					new List<Nullable<Rectangle>> ( new Nullable<Rectangle> [] 
				{
					new Rectangle ( 0, 0, 40, 40 ),
					new Rectangle ( 41, 0, 40, 40 ),
					new Rectangle ( 82, 0, 40, 40 ),
					new Rectangle ( 123, 0, 40, 40 )
				} );
			_frames [ (int) MovingState.Walking, (int) HeadDirection.ToBack ] =
				new List<Nullable<Rectangle>> ( new Nullable<Rectangle> []
				{
					new Rectangle ( 164, 0, 40, 40 ),
					new Rectangle ( 205, 0, 40, 40 ),
					new Rectangle ( 246, 0, 40, 40 ),
					new Rectangle ( 287, 0, 40, 40 )
				} );
			_frames [ (int) MovingState.Walking, (int) HeadDirection.ToMe ] =
				new List<Nullable<Rectangle>> ( new Nullable<Rectangle> []
				{
					new Rectangle ( 328, 0, 40, 40 ),
					new Rectangle ( 369, 0, 40, 40 ),
					new Rectangle ( 410, 0, 40, 40 ),
					new Rectangle ( 451, 0, 40, 40 )
				} );
			_frames [ (int) MovingState.Walking, (int) HeadDirection.ToUp ] =
				_frames [ (int) MovingState.Walking, (int) HeadDirection.ToFront ]; // en este caso es lo mismo que mirar para delante
			
			
			// -- Falling (todos los frames son iguales a cuando camina hacia delante)
			_frames [ (int) MovingState.Falling, (int) HeadDirection.ToFront ] =
				_frames [ (int) MovingState.Walking, (int) HeadDirection.ToFront ];
			_frames [ (int) MovingState.Falling, (int) HeadDirection.ToBack ] =
				_frames [ (int) MovingState.Walking, (int) HeadDirection.ToFront ];
			_frames [ (int) MovingState.Falling, (int) HeadDirection.ToMe ] =
				_frames [ (int) MovingState.Walking, (int) HeadDirection.ToFront ];
			_frames [ (int) MovingState.Falling, (int) HeadDirection.ToUp ] =
				_frames [ (int) MovingState.Walking, (int) HeadDirection.ToFront ];
			
			// -- Jumping (todos los frames son el mismo)
			_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToFront ] =
				new List<Nullable<Rectangle>> ( new Nullable<Rectangle> []
				{
					new Rectangle ( 656, 0, 40, 40 )
				} );
			_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToBack ] = 
				_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToFront ];
			_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToMe ] = 
				_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToFront ];
			_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToUp ] = 
				_frames [ (int) MovingState.Jumping, (int) HeadDirection.ToFront ];
			
		}
		#endregion
				
		public override void Update (GameTime gameTime)
		{
			if ( _muriendo ) return;
			
			TimeSpan _realTime = gameTime.TotalRealTime;
			
			// -- la direccion de la cabeza
			if ( _realTime - _last_change_head >= elapse_change_head )
			{
				_last_change_head = _realTime;
				_head = (HeadDirection) _rmove.Next ( 4 );
			}
			
			// -- el movimiento
			if ( _realTime - _last_step_moving >= elapse_step_moving )
			{
				_last_step_moving = _realTime;
				_current_frame [ (int) _moving, 0 ] = ( _current_frame [ (int) _moving, 0 ] + 1 ) % _current_frame [ (int) _moving, 1 ];
				
				if ( _moving != Sprite.MovingState.Stop )
				{
					
					Vector2 posicionPrevia = _universePosition;
					
					// tenemos que movernos en relacion al "universo"
					_universePosition = _level.WantToMoveTo ( new Rectangle ( (int) _universePosition.X, (int) _universePosition.Y, 40, 40 ), _direction );
					_sensibleArea.X = ( (int) _universePosition.X ) + 7;
					_sensibleArea.Y = (int) _universePosition.Y + 3;
				
					if ( _moving == MovingState.Walking )
					{
						// a cada paso hay que comprobar si tenemos que caer
						if ( _level.HaveToFall ( new Rectangle ( (int) _universePosition.X, (int) _universePosition.Y, 40, 40 ) ) )
						{
							Stop ();
							Fall ();
						}
					}
					else
					{
						// cuando se est cayendo
						if ( ( _moving == MovingState.Falling ) && ( _universePosition.Y == posicionPrevia.Y ) ) Stop ();
					
						// cuando se esta saltando
						if ( _moving == MovingState.Jumping )
						{
							if ( _jumpingFase == 0 )
							{
								if ( ++_jumpingStep > _jumpingSteps )
								{
									_jumpingFase = 1;
									_direction.Y = 6;
								}
							}
							else 
							{
								// si no se ha avanzado verticalmente, entonces podemos dar por finalizada la fase de caida del salto
								if ( _universePosition.Y == posicionPrevia.Y ) Stop ();
							}
						}
					}
				}
			}
			
			
			base.Update (gameTime);
		}
		
		public Rectangle SensibleArea { get { return _sensibleArea; } }
		
		public override void Draw (GameTime gameTime)
		{
			
			if ( _muriendo ) return;			
			
			SpriteBatch sb = (SpriteBatch) Game.Services.GetService ( typeof ( SpriteBatch ) );
			
			sb.Draw ( TextureManager.Sprites, 
			         _level.GetScreenCoords ( _universePosition ), 
			         _frames [ (int) _moving, (int) _head ] [ _current_frame [ (int) _moving, 0 ] ], 
			         Color.White, 
			         0.0f,
			         Vector2.Zero, 
			         Vector2.One,
			         _effect,
			         0.3f);
			
#if SHOW_ENEMIES
			Vector2 posRecuadro = _level.GetScreenCoords ( new Vector2 ( _sensibleArea.X, _sensibleArea.Y ) );
			Rectangle recRecuadro = new Rectangle ( 
						(int) posRecuadro.X, 
						(int) posRecuadro.Y, 
						_sensibleArea.Width,
						_sensibleArea.Height );
			
			sb.Draw ( TextureManager.Decorados,
						recRecuadro,
						new Rectangle ( 225, 61, 20, 20 ),
						Color.White );
#endif
			
			base.Draw (gameTime);
		}
		
		
		#region para el movimiento
		
		public void MoveToRight ()
		{
			_direction = new Vector2 ( 4, 0 );
			_effect = SpriteEffects.None;
			_moving = MovingState.Walking;
			SoundManager.PlayWalking ();
		}
		
		public void MoveToLeft ()
		{
			_direction = new Vector2 ( -4, 0 );
			_effect = SpriteEffects.FlipHorizontally;
			_moving = MovingState.Walking;
			SoundManager.PlayWalking ();
		}
		
		public void Stop ()
		{
			_direction = Vector2.Zero;
			_moving = MovingState.Stop;
			SoundManager.StopWalking ();
			SoundManager.StopFalling ();
			SoundManager.StopJumping ();
		}
		
		public void Fall ()
		{
			_direction = new Vector2 ( 0, 6 );
			_moving = Sprite.MovingState.Falling;
			SoundManager.PlayFalling ();
		}
		
#if SIMULATOR
		public void StartJumpingLeft ()
		{
			_direction = new Vector2 ( -4, -6 );
			_moving = Sprite.MovingState.Jumping;
			_effect = SpriteEffects.FlipHorizontally;
			_jumpingStep = 0;
			_jumpingSteps = longJumpSteps;  // por defecto empezamos con un salto largo
			_jumpingFase = 0;
			SoundManager.PlayJumping ();
		}
		
		public void StartJumpintRight ()
		{
			_direction = new Vector2 ( 4, -6 );
			_moving = Sprite.MovingState.Jumping;
			_effect = SpriteEffects.None;
			_jumpingStep = 0;
			_jumpingSteps = longJumpSteps;
			_jumpingFase = 0;
			SoundManager.PlayJumping ();
		}
#endif
		
		/// <summary>
		/// Rectangulo que representa la region del espacio en que el bicho interactua con los objetos
		/// </summary>
		/// <value>
		/// The space region.
		/// </value>
		public Rectangle SpaceRegion 
		{
			get { return new Rectangle ( (int) _universePosition.X, (int) _universePosition.Y, 40, 40 ); }
		}

		#endregion
	}
}

