using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class EnemyVertical : EnemyBase
	{
		
		// -- los tipos de enemigos verticales que hay
		public const int TIPO_CUADRADO_ROJO = 0;
		public const int TIPO_BARRAS_AMARILLAS = 3;
		
		
		// --- los frames de cada enemigo
		private static Rectangle [] FramesCuadradoRojo = new Rectangle [] {
			new Rectangle ( 0, 369, 40, 40 ),
			new Rectangle ( 41, 369, 40, 40 ),
			new Rectangle ( 82, 369, 40, 40 ),
			new Rectangle ( 123, 369, 40, 40 ),
			new Rectangle ( 164, 369, 40, 40 ),
			new Rectangle ( 205, 369, 40, 40 ),
			new Rectangle ( 246, 369, 40, 40 ),
			new Rectangle ( 287, 369, 40, 40 )
		};
		
		private static int [] IndiceAnimacionCuadradoRojo = new int [] { 0, 1, 2, 3, 4, 5, 6, 7 };
		
		private static Rectangle [] FramesBarrasAmarillas = new Rectangle [] {
			new Rectangle ( 328, 369, 40, 40 ),
			new Rectangle ( 369, 369, 40, 40 ),
			new Rectangle ( 410, 369, 40, 40 ),
			new Rectangle ( 451, 369, 40, 40 ),
			new Rectangle ( 492, 369, 40, 40 )
		};
		
		private static int [] IndiceAnimacionBarrasAmarillas = new int [] { 0, 1, 2, 3, 4, 3, 2, 1 };
		
		// --- los rectangulos de colision de cada enemigo
		private static Rectangle [] ColisionCuadradoRojo = new Rectangle [] {
			new Rectangle ( 4, 4, 32, 32 ),
			new Rectangle ( 4, 5, 32, 30 ),
			new Rectangle ( 4, 11, 32, 18 ),
			new Rectangle ( 4, 12, 32, 16 ),
			new Rectangle ( 4, 17, 32, 6 ),
			new Rectangle ( 4, 12, 32, 16 ),
			new Rectangle ( 4, 10, 32, 23 ),
			new Rectangle ( 4, 5, 32, 30 )
		};
		
		private static Rectangle [] ColisionBarrasAmarillas = new Rectangle [] {
			new Rectangle ( 4, 2, 32, 38 ),
			new Rectangle ( 4, 5, 32, 31 ),
			new Rectangle ( 4, 8, 32, 25 ),
			new Rectangle ( 4, 10, 32, 21 ),
			new Rectangle ( 4, 13, 32, 16 )
		};
		
		
		protected Rectangle _rectangulo_influencia;
		protected Rectangle [] _frames_animacion;
		protected Rectangle [] _rectangulos_colision;
		protected int [] _indice_animacion;
		protected float _direccion;
		protected TimeSpan _last_update = TimeSpan.Zero;
		protected TimeSpan _velocidad_animacion;
		int _total_frames, _current_frame = 0;
		protected Rectangle _collision_rectangle = Rectangle.Empty;

		public EnemyVertical (Game game, 
			Level universe,
			int tipoEnemigo, 
			float x_inicial,
			float y_inicial,
			float y_final,
			TimeSpan esperaEntreFrames ) : base ( game, universe )
		{
			// indicamos cuales son los frames que corresponden
			switch ( tipoEnemigo )
			{
				case TIPO_CUADRADO_ROJO:
					_frames_animacion = FramesCuadradoRojo;
					_indice_animacion = IndiceAnimacionCuadradoRojo;
					_rectangulos_colision = ColisionCuadradoRojo;
					break;
				case TIPO_BARRAS_AMARILLAS:
					_frames_animacion = FramesBarrasAmarillas;
					_indice_animacion = IndiceAnimacionBarrasAmarillas;
					_rectangulos_colision = ColisionBarrasAmarillas;
					break;
				default:
					throw new NotSupportedException ( "No existe el tipo de enemigo por defecto" );
			}
			
			_position = new Vector2 ( x_inicial, y_inicial );
			if ( y_final > y_inicial ) // empieza moviendose abajo
			{
				_rectangulo_influencia = new Rectangle ( (int) x_inicial, (int) y_inicial, 40, (int) ( y_final - y_inicial ) );
				_direccion = 4;
			} 
			else  // empieza moviendoze hacia arriba
			{
				_rectangulo_influencia = new Rectangle ( (int) x_inicial, (int) y_final, 40, (int) ( y_inicial - y_final ) );
				_direccion = -4;
			}
			
			_velocidad_animacion = esperaEntreFrames;
			_total_frames = _indice_animacion.Length;
		}
		
		public override Rectangle ActionRectangle {
			get {
				 return _rectangulo_influencia;
			}
		}
		
		public override Rectangle CollisionRectangle {
			get {
				return _collision_rectangle;
			}
		}
		
		public override void Update (GameTime gameTime)
		{
			if ( gameTime.TotalGameTime - _last_update > _velocidad_animacion )
			{
				_current_frame++;
				if ( _current_frame == _total_frames ) _current_frame = 0;
				
				_position.Y += _direccion;
				if ( _direccion > 0 ) // vamos hacia abajo
				{
					if ( _position.Y >= _rectangulo_influencia.Bottom )
					{
						_position.Y = _rectangulo_influencia.Bottom;
						_direccion = -_direccion;
					}
				}
				else // vamos hacia arriba
				{
					if ( _position.Y <= _rectangulo_influencia.Y )
					{
						_position.Y = _rectangulo_influencia.Y;
						_direccion = -_direccion;
					}
				}
				
				_collision_rectangle = _rectangulos_colision [ _indice_animacion [ _current_frame ] ];
				_collision_rectangle.X += (int) _position.X;
				_collision_rectangle.Y += (int) _position.Y;
				
				_last_update = gameTime.TotalGameTime;
			}
		}
		
		public override void Draw ( GameTime gameTime )
		{
			SpriteBatch sb = (SpriteBatch) _game.Services.GetService ( typeof ( SpriteBatch ) );
			Vector2 screenPos = _universe.GetScreenCoords ( _position );
			
			sb.Draw ( 
				TextureManager.Sprites, 
				screenPos, 
				_frames_animacion [ _indice_animacion [ _current_frame ] ], 
				Color.White, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.3f );
			
#if SHOW_ENEMIES
			Vector2 posRecuadro = _universe.GetScreenCoords ( new Vector2 ( _collision_rectangle.X, _collision_rectangle.Y ) );
			Rectangle recRecuadro = new Rectangle ( 
						(int) posRecuadro.X, 
						(int) posRecuadro.Y,
						_collision_rectangle.Width,
						_collision_rectangle.Height );			

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

	}
}

