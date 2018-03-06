using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public abstract class AnimacionBase
	{
		public delegate void FinAnimacion ();
		
		protected DemoPlataformaGame _game;
		protected Level _universe;
		protected bool _ended = false;
		
		public AnimacionBase ()
		{
		}
		
		public bool IsEnded { get { return _ended; } }
		
		public DemoPlataformaGame Game 
		{
			get { return _game; }
			set { _game = value; }
		}
		
		public Level Universe 
		{
			get { return _universe; }
			set { _universe = value; }
		}
		
		public abstract void Update ( GameTime gameTime );
		public abstract void Draw ( SpriteBatch sb );
	}
}

