using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public abstract class HotPointBase
	{
		
		protected DemoPlataformaGame _game;
		protected Level _universe;
		protected bool _enabled;
		protected bool _visible;
		protected string _name;
		protected Vector2 _position;
		protected Rectangle _sensible_area = Rectangle.Empty;
		
		public HotPointBase ( Game game, Level universe )
		{
			_game = (DemoPlataformaGame) game;
			_universe = universe;
		}
		
		public bool IsEnable {
			get { return _enabled; }
			set { _enabled = value; }
		}
		
		public bool IsVisible {
			get { return _visible; }
			set { _visible = value; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public virtual Vector2 Position 
		{
			get { return _position; }
			set 
			{ 
				_position = value; 
				if ( Rectangle.Empty == _sensible_area )
				{
					_sensible_area = new Rectangle ( (int) _position.X - 6, (int) _position.Y - 6, 52, 52 );
				}
				else
				{
					_sensible_area.X = (int) _position.X - 6;
					_sensible_area.Y = (int) _position.Y - 6;
				}
			}
		}
		
		public Rectangle? SensibleArea 
		{
			get { return _sensible_area; }
		}
		
		public abstract void Update ( GameTime gameTime );
		public abstract void Draw ( SpriteBatch sb );
		
		public virtual bool Check ( Sprite player )
		{
			return player.SpaceRegion.Intersects ( _sensible_area );
		}
		
		public abstract void Trigger ( Sprite player );
		
	}
}

