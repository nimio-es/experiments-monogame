using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	
	public abstract class EnemyBase
	{
	
		protected Game _game;
		protected Vector2 _position;
		protected Level _universe;
		
#if SHOW_ENEMIES
		protected Rectangle _frame_remarcado = new Rectangle ( 225, 61, 20, 20 );
#endif
		
		public abstract Rectangle ActionRectangle { get; } 
		public abstract Rectangle CollisionRectangle { get; }
	
		public EnemyBase ( Game game, Level universe )
		{
			_game = game;
			_universe = universe;
		}
		
		public abstract void Update ( GameTime gameTime );
		public abstract void Draw ( GameTime gameTime );
	}
}

