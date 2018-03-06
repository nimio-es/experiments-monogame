using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public abstract class HotPointDejarItemBase : HotPointBase
	{
		
		protected Item _item;
		protected bool _pendiente = true;

		public HotPointDejarItemBase ( Game game, Level universe ) : base ( game, universe )
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

				_game.Animations.AddAnimacion (
					new AnimacionItemRecogido ( _item.Clone (), FinAnimacion ) );
				
				SoundManager.PlayTesoro ();
			}
		}
		
		protected abstract void doMoore ();
		
		private void FinAnimacion ()
		{
			_game.BolsaItems.DeleteItem ( Item.ITEM_OJO );
			this._item.IsActive = true;
			doMoore ();
		}
	}
}

