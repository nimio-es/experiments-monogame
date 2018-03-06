using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class AnimacionMuerte: AnimacionBase
	{
		
		// --- los frames de cada enemigo
		private static Rectangle [] Frames = new Rectangle [] {
			new Rectangle ( 0, 615, 40, 40 ),
			new Rectangle ( 41, 615, 40, 40 ),
			new Rectangle ( 82, 615, 40, 40 ),
			new Rectangle ( 123, 615, 40, 40 ),
			new Rectangle ( 164, 615, 40, 40 )
		};

	
		private Vector2 _posicion;
		private AnimacionBase.FinAnimacion _fin_animacion;
		private TimeSpan _elapse_last_update = TimeSpan.Zero;
		private TimeSpan _wait_frame = new TimeSpan ( TimeSpan.TicksPerSecond / 6 );
		private int _current_frame = 0;
		
		public AnimacionMuerte ( AnimacionBase.FinAnimacion finAnimacion ) : base ()
		{
			_fin_animacion = finAnimacion;
			_ended = false;
		}
		
		public Vector2 Posicion { get { return _posicion; } set { _posicion = value; } }
		
		public override void Update (GameTime gameTime)
		{
			if ( _ended ) return;
			
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update >= _wait_frame )
			{
				_current_frame++;
				if ( _current_frame == Frames.Length )
				{
					_ended = true;
					_fin_animacion ();
				}
				_elapse_last_update = TimeSpan.Zero;
			}
		}
		
		public override void Draw (SpriteBatch sb)
		{
			Vector2 universePos = _universe.GetScreenCoords ( _posicion );
			
			sb.Draw ( TextureManager.Sprites, universePos, Frames [ _current_frame ], Color.White );
		}
	}
}

