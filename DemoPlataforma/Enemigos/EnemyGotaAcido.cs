using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class EnemyGotaAcido : EnemyBase
	{
		
		public const int GOTA_AZUL = 0;
		public const int GOTA_AMARILLA = 1;
		
		// Fases de la animacion de la gota
		public const int FASE_OCULTO = 0;
		public const int FASE_BROTANDO = 1;
		public const int FASE_CAYENDO = 2;
		public const int FASE_ROMPIENDO = 3;
		
		protected static Rectangle [] GlobalFramesGotaAzul = new Rectangle [] { 
			new Rectangle ( 0, 164, 40, 40 ),      // frame 1 brotando
			new Rectangle ( 41, 164, 40, 40 ),     // frame 2 brotando
			new Rectangle ( 82, 164, 40, 40 ),     // frame cayendo
			new Rectangle ( 123, 164, 40, 40 ),    // frame 1 rompiendo
			new Rectangle ( 164, 164, 40, 40 ),    // frame 2 rompiendo
			new Rectangle ( 205, 164, 40, 40 ),    // frame 3 rompiendo
			new Rectangle ( 246, 164, 40, 40 ) };  // frame 4 rompiendos

	protected static Rectangle [] GlobalFramesGotaAmarilla = new Rectangle [] { 
			new Rectangle ( 0, 82, 40, 40 ),      // frame 1 brotando
			new Rectangle ( 41, 82, 40, 40 ),     // frame 2 brotando
			new Rectangle ( 82, 82, 40, 40 ),     // frame cayendo
			new Rectangle ( 123, 82, 40, 40 ),    // frame 1 rompiendo
			new Rectangle ( 164, 82, 40, 40 ),    // frame 2 rompiendo
			new Rectangle ( 205, 82, 40, 40 ),    // frame 3 rompiendo
			new Rectangle ( 246, 82, 40, 40 ) };  // frame 4 rompiendos

		
		// son los mismos para todas las gotas indistintamente el color que tengan
		protected static Rectangle [] RectangleColisionPerFrame = new Rectangle []
			{
				new Rectangle ( 8, 0, 17, 6 ),
				new Rectangle ( 12, 0, 11, 14 ),
				new Rectangle ( 13, 14, 11, 20 ),
				new Rectangle ( 7, 27, 24, 7 ),
				new Rectangle ( 2, 16, 35, 20 ),
				new Rectangle ( 2, 20, 35, 20 ),
				new Rectangle ( 9, 31, 28, 9 ) 
			};
		// los frames que corresponde a cada estado son siempre los mismos
		protected static int [][] frames = new int [][]
			{
				new int [] { 0, -1},
				new int [] { 2, 0, 1 },
				new int [] { 1, 2 },
				new int [] { 4, 3, 4, 5, 6 }
			};

		
		protected Vector2 _posicionArranque;
		protected float _alturaFinal;
		protected Rectangle _actionRectangle;
		protected int _fase = FASE_OCULTO;
		protected Rectangle [] _frames_gota;
		protected int _current_frame;
		protected Rectangle _current_collision_rectangle;
		protected TimeSpan 	_last_update = TimeSpan.Zero,
							_esperaVolverEmpezar,
							_velocidadGotea,
							_velocidadCae,
							_velocidadRompe,
							_nextWait;
		
		
		public EnemyGotaAcido ( Game game, 
			Level universe,
			int tipoGota, 
			Vector2 posicionArranque, 
			float altura,
			TimeSpan esperaVolverEmpezar,
			TimeSpan velocidadGotea,
			TimeSpan velocidadCae,
			TimeSpan velocidadRompe ) : base ( game, universe )
		{
			switch ( tipoGota )
			{
				case GOTA_AZUL: 
					_frames_gota = GlobalFramesGotaAzul;
					break;
				case GOTA_AMARILLA:
					_frames_gota = GlobalFramesGotaAmarilla;
					break;
				default:
					_frames_gota = GlobalFramesGotaAzul;
					break;
			}
				
			_posicionArranque = posicionArranque;
			_alturaFinal = altura;
			_actionRectangle = new Rectangle ( (int) _posicionArranque.X, (int) _posicionArranque.Y, 40, (int) ( _alturaFinal - _posicionArranque.Y ) );
			_esperaVolverEmpezar = esperaVolverEmpezar;
			_velocidadGotea = velocidadGotea;
			_velocidadCae = velocidadCae;
			_velocidadRompe = velocidadRompe;
		}
		
		
		public override void Update (GameTime gameTime)
		{
			
			if ( gameTime.TotalGameTime - _last_update > _nextWait )
			{
				switch ( _fase )
				{
					case FASE_OCULTO:
						updateFaseOculto ();
						break;
					case FASE_BROTANDO:
						updateFaseBrotando ();
						break;
					case FASE_CAYENDO:
						updateFaseCae ();
						break;
					case FASE_ROMPIENDO:
						updateFaseRompe ();
						break;
					default:
						break;
				}
				_last_update = gameTime.TotalGameTime;
			}
		}
		
		public override void Draw (GameTime gameTime)
		{
			if ( _fase == FASE_OCULTO ) return;
			
			SpriteBatch sb = (SpriteBatch) _game.Services.GetService ( typeof ( SpriteBatch ) );
			Vector2 screenPos = _universe.GetScreenCoords ( _position );
			
			sb.Draw ( 
						TextureManager.Sprites, 
						screenPos, 
						_frames_gota [ frames [_fase ][ _current_frame + 1 ] ], 
						Color.White, 
						0.0f, 
						Vector2.Zero,
						1.0f,
						SpriteEffects.None, 
						0.3f );
			
#if SHOW_ENEMIES
			Vector2 posRecuadro = _universe.GetScreenCoords ( 
						new Vector2 ( _current_collision_rectangle.X, _current_collision_rectangle.Y ) );
			Rectangle recRecuadro = new Rectangle ( (int) posRecuadro.X, (int) posRecuadro.Y,
					_current_collision_rectangle.Width, _current_collision_rectangle.Height );
			
			sb.Draw (
				TextureManager.Decorados,
				recRecuadro,
				_frame_remarcado,
				Color.White,
				0.0f,
				Vector2.Zero,
				SpriteEffects.None,
				0.01f );
#endif

		}
		
		public override Rectangle ActionRectangle {
			get {
				return _actionRectangle;
			}
		}
		
		public override Rectangle CollisionRectangle {
			get {
				return _current_collision_rectangle;
			}
		}
		
		protected void updateFaseOculto ()
		{
			_nextWait = _velocidadGotea;
			_current_frame = 0;
			_position = _posicionArranque;
			_fase = FASE_BROTANDO;
			_current_collision_rectangle = RectangleColisionPerFrame [ frames [ _fase ][ _current_frame + 1 ] ];
			_current_collision_rectangle.X += (int) _position.X;
			_current_collision_rectangle.Y += (int) _position.Y;
		}
		
		protected void updateFaseBrotando ()
		{
			_current_frame++;
			if ( _current_frame == frames [ FASE_BROTANDO ][ 0 ] )
			{
				_current_frame = 0;
				_nextWait = _velocidadCae;
				_fase = FASE_CAYENDO;
				_current_collision_rectangle = RectangleColisionPerFrame [ frames [ _fase ][ _current_frame + 1 ] ];
				_current_collision_rectangle.X += (int) _position.X;
				_current_collision_rectangle.Y += (int) _position.Y;
			}
			else
			{
				_current_collision_rectangle = RectangleColisionPerFrame [ frames [ _fase ][ _current_frame + 1 ] ];
				_current_collision_rectangle.X += (int) _position.X;
				_current_collision_rectangle.Y += (int) _position.Y;
			}
		}
		
		protected void updateFaseCae ()
		{
			_position.Y += 4;
			if ( _position.Y > _alturaFinal )
			{
				_current_frame = 0;
				_position.Y = _alturaFinal;
				_nextWait = _velocidadRompe;
				_fase = FASE_ROMPIENDO;
				_current_collision_rectangle = RectangleColisionPerFrame [ frames [ _fase ][ _current_frame + 1 ] ];
				_current_collision_rectangle.X += (int) _position.X;
				_current_collision_rectangle.Y += (int) _position.Y;
				SoundManager.PlayGota ();
			}
			else
			{
				_current_collision_rectangle.Y += 4;
			}
		}
		
		protected void updateFaseRompe ()
		{
			_current_frame++;
			if ( _current_frame == frames [ FASE_ROMPIENDO ][ 0 ] )
			{
				_current_frame = 0;
				_nextWait = _esperaVolverEmpezar;
				_fase = FASE_OCULTO;
				_position = Vector2.Zero;
				_current_collision_rectangle = Rectangle.Empty;
			}
			else
			{
				_current_collision_rectangle = RectangleColisionPerFrame [ frames [ _fase ][ _current_frame + 1 ] ];
				_current_collision_rectangle.X += (int) _position.X;
				_current_collision_rectangle.Y += (int) _position.Y;
			}
		}
		
	}
}

