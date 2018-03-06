using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class HotPointItemCogerItemOjo : HotPointCogerItemBase
	{
		
		public HotPointItemCogerItemOjo ( Game game, Level universe ) : base ( game, universe )
		{
			_name = "COGER-OJO";
			_item = new Item ( Item.ITEM_OJO ) {
					IsActive = true
				};
			_universe.Status [ _name ] = true;
		}
		
	}
}

