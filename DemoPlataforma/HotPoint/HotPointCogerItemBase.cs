using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public abstract class HotPointCogerItemBase : HotPointBase
	{
		
		protected Item _item;
		protected bool _pendiente = true;
		
		public HotPointCogerItemBase ( Game game, Level universe ) : base ( game, universe )
		{
		}
		
		public override void Update (GameTime gameTime)
		{
			if ( _item != null ) _item.Update ( gameTime );
		}
		
		public override void Draw (SpriteBatch sb)
		{
			Vector2 screenPos = _universe.GetScreenCoords ( _position );
			_item.Draw ( sb, screenPos );
		}

		public override void Trigger (Sprite player)
		{
			if ( this._pendiente ) 
			{
				this._pendiente = false;
				this._item.IsActive = false;

				_game.Animations.AddAnimacion (
					new AnimacionItemRecogido ( _item.Clone (), FinAnimacion ) );
				
				SoundManager.PlayTesoro ();
			}
		}
		
		private void FinAnimacion ()
		{
			_game.BolsaItems.AddItem ( _item.Clone () );
#if DEBUG
			Console.WriteLine ( "Se ha añadido un nuevo item a la bolsa" );
#endif
		}

	}
}

