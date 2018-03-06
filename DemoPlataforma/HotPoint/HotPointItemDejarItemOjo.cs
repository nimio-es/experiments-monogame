using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class HotPointItemDejarItemOjo : HotPointDejarItemBase
	{
		public HotPointItemDejarItemOjo ( Game game, Level universe ) : base ( game, universe )
		{
			_name = "DEJAR-OJO";
			_item = new Item ( Item.ITEM_OJO ) {
					IsActive = false
				};
			_universe.Status [ _name ] = true;
		}
		
		protected override void doMoore ()
		{
			_game.Animations.AddAnimacion ( new AnimacionMensajeFinal () );
		}
	}
}

