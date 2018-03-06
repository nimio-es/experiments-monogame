using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class BolsaItemsManager: DrawableGameComponent
	{
		
		private Rectangle _frame = new Rectangle ( 251, 672, 90, 36 );
		private Vector2 _screen_pos = new Vector2 ( 195, 248 );
		private List<Item> _bolsa = new List<Item> ();
		
		public BolsaItemsManager ( Game game ) : base ( game )
		{
		}
		
		public override void Update (GameTime gameTime)
		{
			foreach ( Item item in _bolsa )
				item.Update ( gameTime );
			
			base.Update (gameTime);
		}
		
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch sb = (SpriteBatch) Game.Services.GetService ( typeof ( SpriteBatch ) );
			
			sb.Draw ( 
						TextureManager.Decorados,
						_screen_pos,
						_frame,
						Color.White,
						0.0f,
						Vector2.Zero,
						1.0f,
						SpriteEffects.None,
						0.03f );
			
			int x = (int) _screen_pos.X + 14;
			int y = (int) _screen_pos.Y + 10;
			Vector2 spi = new Vector2 ( x, y );
			foreach ( Item item in _bolsa )
			{
				item.Draw ( sb, spi, 0.0f, 0.45f, 0.02f, 255 );
				spi.X += 20;
			}
			
			base.Draw (gameTime);
		}
		
		public void AddItem ( Item item )
		{
			_bolsa.Add ( item );
		}
		
		public void DeleteItem ( int tipo )
		{
			for ( int i = 0; i < _bolsa.Count; i++ )
				if ( _bolsa [ i ].ItemType == tipo )
				{
					_bolsa.RemoveAt ( i );
					break;
				}
		}
	}
}

