using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	
	/// <summary>
	/// Gestiona todas las animaciones pendientes
	/// </summary>
	public class AnimationsManager : DrawableGameComponent
	{
		
		protected List<AnimacionBase> _animaciones = new List<AnimacionBase> ();
		
		protected Level _universe;
		
		public AnimationsManager ( Game game, Level universe ) : base ( game )
		{
			_universe = universe;
		}
		
		public override void Update (GameTime gameTime)
		{
			int total_anim = _animaciones.Count;
			for ( int i = 0; i < total_anim; i++ )
			{
				AnimacionBase ab = _animaciones [ i ];
				if ( ab.IsEnded )
				{
					_animaciones.RemoveAt ( i );
					total_anim--;
					i--;
					continue;
				}
				
				ab.Update ( gameTime );
			}
			
			base.Update (gameTime);
		}
		
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch sb = (SpriteBatch) Game.Services.GetService ( typeof ( SpriteBatch ) );
			
			int total_anim = _animaciones.Count; 
			for ( int i = 0; i < total_anim; i++ )
				if ( ! _animaciones [ i ].IsEnded )
					_animaciones [ i ].Draw ( sb );
				
			base.Draw (gameTime);
		}
		
		public void AddAnimacion ( AnimacionBase animacion )
		{
			animacion.Game = (DemoPlataformaGame) Game;
			animacion.Universe = _universe;
			_animaciones.Add ( animacion );
		}
		
	}
}

