using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class Item
	{
		
		public const int ITEM_OJO = 0;
		
		private static Rectangle FrameItemOjoOff = new Rectangle ( 0, 187, 40, 40 );
		private static Rectangle [] FramesItemOjoOn = new Rectangle [] 
			{
				new Rectangle ( 41, 187, 40, 40 ),
				new Rectangle ( 82, 187, 40, 40 ),
				new Rectangle ( 123, 187, 40, 40 ),
				new Rectangle ( 164, 187, 40, 40 )
			};
		private static int [] IndiceAnimacionItemOjo = new int [] { 0, 0, 0, 1, 2, 3, 2, 1 };
				
		private int _tipo;
		private bool _active = false;
		private Rectangle _frame_off;
		private Rectangle [] _frames_on;
		private int [] _frame_index;
		private int _current_frame, _total_frames;
		private TimeSpan _wait_frame = TimeSpan.FromMilliseconds ( 200 );
		private TimeSpan _last_update = TimeSpan.Zero;
		
		public Item ( int tipoItem ) 
		{
			_tipo = tipoItem;
			switch ( tipoItem )
			{
				case ITEM_OJO:
					_frame_off = FrameItemOjoOff;
					_frames_on = FramesItemOjoOn;
					_frame_index = IndiceAnimacionItemOjo;
					break;
				default:
					break;
			}
			
			_total_frames = _frame_index.Length;
			_current_frame = 0;
		}
		
		public int ItemType { get { return _tipo; } }
		public bool IsActive { get { return _active; } set { _active = value; } }
		
		public void Update ( GameTime gameTime )
		{
			if ( _active )
				if ( gameTime.TotalRealTime - _last_update > _wait_frame )
				{
					_current_frame = ( _current_frame + 1 ) % _total_frames;
					_last_update = gameTime.TotalRealTime;
				}
		}
		
		public void Draw ( SpriteBatch sb, Vector2 screenCords )
		{
			this.Draw ( sb, screenCords, 0.0f, 1.0f, 0.15f, 255 );
		}
		
		public void Draw ( SpriteBatch sb, Vector2 screenCords, float rotation, float scale, float depth, float alpha )
		{
			Rectangle? frame;
			
			if ( _active )
				frame = _frames_on [ _frame_index [ _current_frame ] ];
			else
				frame = _frame_off;
			
			sb.Draw ( 
						TextureManager.Items,
						screenCords,
						frame,
						new Color ( 1.0f, 1.0f, 1.0f, alpha ),
						rotation,
						Vector2.Zero,
						scale,
						SpriteEffects.None,
						depth );
						
		}
		
		public Item Clone ()
		{
			Item copia = new Item ( _tipo );
			copia._active = true;
			return copia;
		}
				
	}
}

