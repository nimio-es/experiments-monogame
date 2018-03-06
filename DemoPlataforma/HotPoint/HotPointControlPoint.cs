using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class HotPointControlPoint : HotPointBase
	{
		
		protected int _width = 40;
		protected int _height = 40;
		protected Vector2 _posCamara;
		protected Vector2 _posPlayer;
		
		public HotPointControlPoint ( Game game, Level universe ) : base ( game, universe )
		{
		}
		
		public override Vector2 Position {
			get {
				return base.Position;
			}
			set {
				_position = value;
				_sensible_area = new Rectangle ( (int) _position.X, (int) _position.Y, _width, _height );
			}
		}
		
		public int Width 
		{ 
			get { return _width; } 
			set 
			{ 
				_width = value;
				_sensible_area.Width = value;
			}
		}
		
		public int Height
		{
			get { return _height; }
			set
			{
				_height = value;
				_sensible_area.Height = value;
			}
		}
		
		public Vector2 PosicionCamara 
		{
			get { return _posCamara; }
			set { _posCamara = value; }
		}
		
		public Vector2 PosicionPlayer
		{
			get { return _posPlayer; }
			set { _posPlayer = value; }
		}
		
		public override void Update ( GameTime gameTime )
		{
			// NADA QUE ACTUALIZAR EN UN PUNTO DE CONTROL
		}
		
		public override void Draw (SpriteBatch sb)
		{
			// NADA QUE DIBUJAR EN UN PUNTO DE CONTROL
		}
		
		public override void Trigger (Sprite player)
		{
			player.SetNewControlPoint ( _posPlayer, _posCamara );
			this._enabled = false;
		}
	}
}

