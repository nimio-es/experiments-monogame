using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class HotPointsManager : DrawableGameComponent
	{
		
		private HotPointBase [] _hotpoints;
		private Level _universe;
		private Rectangle _last_viewport = Rectangle.Empty;
		private List<HotPointBase> _sensible_hotpoints = new List<HotPointBase> ();
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DemoPlataforma.HotPointsManager"/> class.
		/// </summary>
		/// <param name='game'>
		/// Game.
		/// </param>
		public HotPointsManager ( Game game, Level universe ) : base ( game )
		{
			_universe = universe;
			initializeHotPoints ();
		}
		
		public void initializeHotPoints ()
		{
			_hotpoints = new HotPointBase []
			{
				new HotPointItemCogerItemOjo ( Game, _universe ) {
						IsEnable = true,
						IsVisible = true, 
						Position = new Vector2 ( 440, 260 )
					},

				new HotPointControlPoint ( Game, _universe ) {
						IsEnable = true,
						Position = new Vector2 ( 660, 140 ),
						Width = 20,
						Height = 80,
						PosicionPlayer = new Vector2 ( 680, 180 ),
						PosicionCamara = new Vector2 ( 640, 180 )						
					},

				new HotPointControlPoint ( Game, _universe ) {
						IsEnable = true,
						Position = new Vector2 ( 1120, 340 ),
						Width = 80,
						Height = 20,
						PosicionPlayer = new Vector2 ( 1140, 340 ),
						PosicionCamara = new Vector2 ( 800, 320 )						
					},

				new HotPointItemDejarItemOjo ( Game, _universe ) {
						IsEnable = true,
						IsVisible = true,
						Position = new Vector2 ( 680, 680 )
					}
			};
		}
		
		public override void Update (GameTime gameTime)
		{
			Rectangle current_viewport = _universe.CurrentCamera.CurrentViewPort;
			if ( ( current_viewport.X != _last_viewport.X ) || ( current_viewport.Y != _last_viewport.Y ) )
			{
				_sensible_hotpoints.Clear ();
				int tot_hp = _hotpoints.Length;
				for ( int i = 0; i < tot_hp; i++ )
					if ( _hotpoints [ i ].SensibleArea.Value.Intersects ( current_viewport ) )
						_sensible_hotpoints.Add ( _hotpoints [ i ] );
				_last_viewport = current_viewport;
			}

			foreach ( HotPointBase hpb in _sensible_hotpoints )
				hpb.Update ( gameTime );
			
			base.Update (gameTime);
		}
		
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch sb = (SpriteBatch) Game.Services.GetService ( typeof ( SpriteBatch ) );
			
			foreach ( HotPointBase hpb in _sensible_hotpoints )
				hpb.Draw ( sb );
			
			base.Draw (gameTime);
		}
		
		/// <summary>
		/// Comprueba si el jugador ha alcanzado alguno de los puntos calientes
		/// </summary>
		/// <param name='player'>
		/// Player.
		/// </param>
		public void CheckHotPoints ( Sprite player )
		{
			int total_hp = _sensible_hotpoints.Count;
			for ( int i = 0; i < total_hp; i++ )
			{
				HotPointBase hpb = _sensible_hotpoints [ i ];
				if ( hpb.IsEnable )
					if ( hpb.Check ( player ) )
					{
						hpb.Trigger ( player );
						break;
					}
			}
		}
		
	}
}

