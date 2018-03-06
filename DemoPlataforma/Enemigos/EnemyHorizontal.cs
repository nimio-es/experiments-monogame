using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class EnemyHorizontal : EnemyBase
	{
		
		/**** Enemigos que se mueven horizontalmente ****/
		public const int TIPO_ALFOMBRA_AMARILLA = 0;
		public const int TIPO_ESTRELLA_AZUL = 3;
		public const int TIPO_SERPIENTE_VERDE = 6;
		public const int TIPO_TORNILLO_AZUL = 10;
		public const int TIPO_ARANA_PATAS_VERDES = 13;
		public const int TIPO_MOMIA_GRIS = 16;
		
		
		private static Rectangle [] FramesAlfombraAmarilla = new Rectangle [] {
				new Rectangle ( 246, 205, 40, 40 ),
				new Rectangle ( 287, 205, 40, 40 ),
				new Rectangle ( 328, 205, 40, 40 ),
				new Rectangle ( 369, 205, 40, 40 ),
				new Rectangle ( 410, 205, 40, 40 )
			};
		private static int [] IndiceAnimacionAlfombraAmarilla = new int [] { 0, 1, 2, 3, 4, 3, 2, 1 };

		private static Rectangle [] FramesEstrellaAzul = new Rectangle [] {
				new Rectangle ( 328, 451, 40, 40 ),
				new Rectangle ( 369, 451, 40, 40 ),
				new Rectangle ( 410, 451, 40, 40 ),
				new Rectangle ( 451, 451, 40, 40 ),
				new Rectangle ( 492, 451, 40, 40 )
			};
		
		private static int [] IndiceAnimacionEstrellaAzul = new int [] { 0, 1, 2, 3, 4, 4, 4, 3, 2, 1, 0 };
		
		private static Rectangle [] FramesSerpienteVerde = new Rectangle [] {
				new Rectangle ( 533, 123, 40, 40 ),
				new Rectangle ( 574, 123, 40, 40 ),
				new Rectangle ( 615, 123, 40, 40 ),
				new Rectangle ( 656, 123, 40, 40 )			
			};
		
		private static int [] IndiceAnimacionSerpienteVerde = new int [] { 0, 0, 1, 1, 2, 2, 3, 3, 2, 1 };
		
		private static Rectangle [] FramesTornilloAzul = new Rectangle [] {
				new Rectangle ( 533, 328, 40, 40),
				new Rectangle ( 574, 328, 40, 40),
				new Rectangle ( 615, 328, 40, 40),
				new Rectangle ( 656, 328, 40, 40),
				new Rectangle ( 533, 369, 40, 40),
				new Rectangle ( 574, 369, 40, 40),
				new Rectangle ( 615, 369, 40, 40),
				new Rectangle ( 656, 369, 40, 40)
			};
		
		private static int [] IndiceAnimacionTornilloAzul = new int [] { 0, 1, 2, 3, 4, 5, 6, 7 }; 
		
		private static Rectangle [] FramesAranaPatasVerdes = new Rectangle [] {
				new Rectangle ( 0, 492, 40, 40 ),
				new Rectangle ( 41, 492, 40, 40 ),
				new Rectangle ( 82, 492, 40, 40 ),
				new Rectangle ( 123, 492, 40, 40 ),
				new Rectangle ( 164, 492, 40, 40 )
			};
		
		private static int [] IndiceAnimacionAranasPatasVerdes = new int [] { 0, 1, 1, 2, 2, 3, 3, 4, 4, 4, 3, 3, 2, 2, 1, 1, 0 };
		
		private static Rectangle [] FramesMomiaGris = new Rectangle [] {
				new Rectangle ( 451, 246, 40, 40 ),
				new Rectangle ( 492, 246, 40, 40 ),
				new Rectangle ( 533, 246, 40, 40 ),
				new Rectangle ( 574, 246, 40, 40 ),
				new Rectangle ( 615, 246, 40, 40 ),
				new Rectangle ( 656, 246, 40, 40 )
			};
		
		private static int [] IndiceAnimacionMomiaGris = new int [] { 0, 1, 2, 3, 4, 5 };
		
		private static Rectangle [] ColisionAlformbaAmarilla = new Rectangle [] {
				new Rectangle ( 2, 2, 36, 33 ),
				new Rectangle ( 2, 4, 36, 28 ),
				new Rectangle ( 2, 3, 34, 27 ),
				new Rectangle ( 2, 11, 35, 18 ),
				new Rectangle ( 3, 9, 27, 29 )
			};

		private static Rectangle [] ColisionEstrellaAzul = new Rectangle [] {
				new Rectangle ( 4, 4, 32, 32 ),
				new Rectangle ( 4, 4, 32, 32 ),
				new Rectangle ( 4, 4, 32, 32 ),
				new Rectangle ( 4, 4, 32, 32 ),
				new Rectangle ( 5, 4, 30, 32 )
			};
		
		private static Rectangle [] ColisionSerpienteVerde = new Rectangle [] {
				new Rectangle ( 3, 8, 32, 30 ),
				new Rectangle ( 1, 8, 34, 30 ),
				new Rectangle ( 5, 8, 31, 30 ),
				new Rectangle ( 14, 10, 24, 28 )
			};
		
		private static Rectangle [] ColisionTornilloAzul = new Rectangle [] {
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 ),
				new Rectangle ( 5, 5, 30, 30 )
			};
		
		private static Rectangle [] ColisionAranaPatasVerdes = new Rectangle [] {
				new Rectangle ( 4, 5, 30, 32 ),
				new Rectangle ( 5, 9, 31, 28 ),
				new Rectangle ( 3, 13, 32, 24 ),
				new Rectangle ( 6, 10, 28, 22 ),
				new Rectangle ( 5, 4, 30, 21 ) 
			};
		
		public static Rectangle [] ColisionMomiaGris = new Rectangle [] {
				new Rectangle ( 3, 2, 34, 36 ),
				new Rectangle ( 3, 2, 34, 36 ),
				new Rectangle ( 3, 2, 34, 36 ),
				new Rectangle ( 3, 2, 34, 36 ),
				new Rectangle ( 3, 2, 34, 36 ),
				new Rectangle ( 3, 2, 34, 36 )
			};

		
		protected Rectangle _rectangulo_influencia;
		protected Rectangle [] _frames_animacion;
		protected Rectangle [] _rectangulos_colision;
		protected int [] _indice_animacion;
		protected float _direccion;
		protected TimeSpan _last_update = TimeSpan.Zero;
		protected TimeSpan _velocidad_animacion;
		protected int _total_frames, _current_frame = 0;
		protected SpriteEffects _effect_derecha = SpriteEffects.None;
		protected SpriteEffects _effect_izquierda = SpriteEffects.FlipHorizontally;
		protected SpriteEffects _effect = SpriteEffects.None;
		protected Rectangle _collision_rectangle = Rectangle.Empty;
		
		public EnemyHorizontal ( Game game, 
			Level universe,
			int tipoEnemigo, 
			float x_inicial,
			float y_inicial,
			float x_final,
			TimeSpan esperaEntreFrames ) : base ( game, universe )
		{
		
			switch ( tipoEnemigo )
			{
				case TIPO_ALFOMBRA_AMARILLA:
					_frames_animacion = FramesAlfombraAmarilla;
					_indice_animacion = IndiceAnimacionAlfombraAmarilla;
					_rectangulos_colision = ColisionAlformbaAmarilla;
					_effect_derecha = SpriteEffects.FlipHorizontally;
					_effect_izquierda = SpriteEffects.None;
					break;
				case TIPO_ESTRELLA_AZUL:
					_frames_animacion = FramesEstrellaAzul;
					_indice_animacion = IndiceAnimacionEstrellaAzul;
					_rectangulos_colision = ColisionEstrellaAzul;
					_effect_derecha = SpriteEffects.None;
					_effect_izquierda = SpriteEffects.None;
					break;
				case TIPO_SERPIENTE_VERDE:
					_frames_animacion = FramesSerpienteVerde;
					_indice_animacion = IndiceAnimacionSerpienteVerde;
					_rectangulos_colision = ColisionSerpienteVerde;
					_effect_derecha = SpriteEffects.None;
					_effect_izquierda = SpriteEffects.FlipHorizontally;
					break;
				case TIPO_TORNILLO_AZUL:
					_frames_animacion = FramesTornilloAzul;
					_indice_animacion = IndiceAnimacionTornilloAzul;
					_rectangulos_colision = ColisionTornilloAzul;
					_effect_derecha = SpriteEffects.None;
					_effect_izquierda = SpriteEffects.None;
					break;
				case TIPO_ARANA_PATAS_VERDES:
					_frames_animacion = FramesAranaPatasVerdes;
					_indice_animacion = IndiceAnimacionAranasPatasVerdes;
					_rectangulos_colision = ColisionAranaPatasVerdes;
					_effect_derecha = SpriteEffects.None;
					_effect_izquierda = SpriteEffects.None;
					break;
				case TIPO_MOMIA_GRIS:
					_frames_animacion = FramesMomiaGris;
					_indice_animacion = IndiceAnimacionMomiaGris;
					_rectangulos_colision = ColisionMomiaGris;
					_effect_derecha = SpriteEffects.None;
					_effect_izquierda = SpriteEffects.FlipHorizontally;
					break;
				default:
					throw new NotSupportedException ( "No existe el tipo de enemigo por defecto" );
			}
			
			_position = new Vector2 ( x_inicial, y_inicial );
			if ( x_final > x_inicial ) // empieza moviendose hacia la derecha
			{
				_rectangulo_influencia = new Rectangle ( (int) x_inicial, (int) y_inicial, (int) ( x_final - x_inicial ), 40 );
				_direccion = 4;
				_effect = _effect_derecha;
			} 
			else  // empieza moviendoze hacia la izquierda
			{
				_rectangulo_influencia = new Rectangle ( (int) x_final, (int) y_inicial, (int) ( x_inicial - x_final ), 40 );
				_direccion = -4;
				_effect = _effect_izquierda;
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
				_position.X += _direccion;
				if ( _direccion > 0 ) // vamos hacia la derecha
				{
					if ( _position.X >= _rectangulo_influencia.Right )
					{
						_position.X = _rectangulo_influencia.Right;
						_effect = _effect_izquierda;
						_direccion = -_direccion;
					}
				}
				else // vamos hacia la izquierda
				{
					if ( _position.X <= _rectangulo_influencia.X )
					{
						_position.X = _rectangulo_influencia.X;
						_effect = _effect_derecha;
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
				_effect, 
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

