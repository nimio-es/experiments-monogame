using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class EnemyEmpalamiento : EnemyBase
	{
		
		private static Rectangle [] FramesPinchos = new Rectangle [] 
			{
				new Rectangle ( 164, 243, 80, 80 ),
				new Rectangle ( 245, 243, 80, 80 )
			};
		
		private TimeSpan _last_update = TimeSpan.Zero;
		private TimeSpan _espera_entre_frames = new TimeSpan ( TimeSpan.TicksPerSecond / 10 * 7 );
		private Rectangle _rectangulo_influencia;
		private int _current_frame = 0, _max_frames = 2;
		private Rectangle _collision_rectangle = Rectangle.Empty;
		
		public EnemyEmpalamiento ( Game game, Level universe, float x, float y ) : base ( game, universe )
		{
			_position = new Vector2 ( x, y );
			_rectangulo_influencia = new Rectangle ( (int) x, (int) y, 80, 80 );
			_collision_rectangle = new Rectangle ( (int) x + 5, (int) y + 25, 70, 50 );
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
			if ( gameTime.TotalGameTime - _last_update > _espera_entre_frames )
			{
				_current_frame = ( _current_frame + 1 ) % _max_frames;
				_last_update = gameTime.TotalGameTime;
			}
		}
		
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch sb = (SpriteBatch) _game.Services.GetService ( typeof ( SpriteBatch ) );
			Vector2 screenPos = _universe.GetScreenCoords ( _position );
			
			sb.Draw (
						TextureManager.Decorados,
						screenPos,
						FramesPinchos [ _current_frame ],
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
				0.00f );
#endif

		}
	}
}

