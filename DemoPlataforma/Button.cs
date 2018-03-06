using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

namespace DemoPlataforma
{
	
	
	public enum ButtonState { Released = 0, Pushed = 1 }
	
	public class Button : DrawableGameComponent
	{
		
		public const int ToLeftButton = 0;
		public const int ToRightButton = 1;
#if SIMULATOR
		public const int LeftJump = 2;
		public const int RightJump = 3;
#else
		public const int Jump = 2;
#endif
		private const int ReleasedState = 0;
		private const int PushedStated = 1;

		private static int [,] _num_frames = 
#if SIMULATOR
				new int [4,2] { { 0, 4 }, { 0, 4 }, { 0, 1}, { 0, 1} };
#else
				new int [3,2] { { 0, 4 }, { 0, 4 }, { 0, 1} };
#endif
		
		private static TimeSpan _elapseAnimation = new TimeSpan ( TimeSpan.TicksPerSecond / 10 );
		
		private static Texture2D _botones;

		private static Vector2[] _buttonsCoords = { 
			new Vector2(14, 246), 
			new Vector2(90, 246), 
#if SIMULATOR
			new Vector2(300, 246), 
#endif
			new Vector2(380, 246) 		
		};
		
		private static Nullable<Rectangle>[] _frames_buttons_frame = 
		{
			new Rectangle ( 3, 2, 70, 70 ),
			new Rectangle ( 74, 2, 70, 70 )
		};
		
		private static Nullable<Rectangle>[] _frames_movement_buttons = {
			new Rectangle ( 3, 76, 41, 41 ),
			new Rectangle ( 45, 76, 41, 41 ),
			new Rectangle ( 87, 76, 41, 41 ),
			new Rectangle ( 131, 76, 41, 41 )
		};
		
		private static Nullable<Rectangle> _frame_jump = new Rectangle ( 181, 76, 40, 40 );
		
		
		private int _what;
		private Vector2 _position, _pos_johnny;
		Nullable<Rectangle> _sensible_rect;
		private SpriteEffects _effect = SpriteEffects.None; 
		private ButtonState _pushed_state = ButtonState.Released;
		private TimeSpan _last_frame_time = TimeSpan.Zero;
		private Sprite _player;
		
		public Button ( Game game, int whatButton, Sprite player ) : base (game)
		{
			_what = whatButton;
			_position = _buttonsCoords [ whatButton ];
			_pos_johnny = new Vector2 ( _position.X + 15, _position.Y + 10 );
			_sensible_rect = new Rectangle ( (int) _position.X + 6, (int) _position.Y + 6, 58, 58 );
			if ( whatButton == ToLeftButton ) _effect = SpriteEffects.FlipHorizontally;
#if SIMULATOR
			if ( whatButton == LeftJump ) _effect = SpriteEffects.FlipHorizontally;
#endif
			_player = player;
		}
		
		public override void Update (GameTime gameTime )
		{
			
			if ( ! _player.IsFalling )
			{
				// se comprueba si es necesario avanzar un frame en la animacion interna
				if ( gameTime.TotalGameTime - _last_frame_time >= _elapseAnimation )
				{ 
					_num_frames [ _what, 0 ] = ( _num_frames [ _what, 0 ] + 1 ) % _num_frames [ _what, 1 ];
					_last_frame_time = gameTime.TotalGameTime;
				}
			
				// se comprueba si se ha realizado alguna pulsacion sobre el boton
				TouchCollection touches = TouchPanel.GetState ();
				bool old_touched = _pushed_state == ButtonState.Pushed;
				bool touched = false;
				foreach ( TouchLocation tl in touches )
					if ( tl.State != TouchLocationState.Released )
						touched |= ( ( _sensible_rect.Value.Left <= tl.Position.X ) && ( _sensible_rect.Value.Right >= tl.Position.X )
							&& ( _sensible_rect.Value.Top <= tl.Position.Y ) && ( _sensible_rect.Value.Bottom >= tl.Position.Y ) );
				_pushed_state = touched ? ButtonState.Pushed : ButtonState.Released;
			
				if ( touched && ! old_touched )
				{
					switch ( _what )
					{
						case ToLeftButton:
							_player.MoveToLeft ();
							break;
						case ToRightButton:
							_player.MoveToRight ();
							break;
#if SIMULATOR
						case LeftJump:
							_player.StartJumpingLeft ();
							break;
						case RightJump:
							_player.StartJumpintRight ();
							break;
#endif					
						default:
							_player.Stop ();
							break;
					}
				}
				else 
				{
					if ( ! touched && old_touched )
					{
						switch ( _what )
						{
							case ToLeftButton:
							case ToRightButton:
								_player.Stop ();
								break;
#if SIMULATOR
							case LeftJump:
							case RightJump:
							default:
								break;
#endif							
					
						}
					}
				}
			}
			else // -- en caso de que estuvera cayendo, hay que borrar el estado de "tocado" para que siga funcionando tan pronto se reactive
				_pushed_state = ButtonState.Released;
			
			base.Update ( gameTime );
		}
		
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch sb = (SpriteBatch) this.Game.Services.GetService( typeof(SpriteBatch) );
			
			if ( ! _player.IsFalling )
			{
				// first draw the circle of the button
				sb.Draw ( 	_botones,
			    	     	_position,
			        	 	_frames_buttons_frame [ (int) _pushed_state ],
			         		Color.White,
			         		0.0f,
			         		Vector2.Zero,
			         		Vector2.One,
			         		SpriteEffects.None,
			         		0.05f );
			
				Nullable<Rectangle> frame = ( _what == ToLeftButton || _what == ToRightButton ) ?  _frames_movement_buttons [ _num_frames [ _what, 0 ] ] : _frame_jump;
				sb.Draw ( 	_botones, 
			    	     	_pos_johnny,
			        	 	frame,
				         	Color.White,
				         	0.0f,
				         	Vector2.Zero,
				         	Vector2.One,
			    	     	_effect,
			        	  	0.00f ); 
			}
			
			base.Draw (gameTime);
		}
	
		
		/// <summary>
		/// Construye los cuatro botones necesarios para el control
		/// </summary>
		public static void BuildButtons ( Game game, Sprite player )
		{
			_botones = game.Content.Load<Texture2D>("buttons");
			game.Components.Add ( new Button ( game, Button.ToLeftButton, player ) );
			game.Components.Add ( new Button ( game, Button.ToRightButton, player ) );
#if SIMULATOR
			game.Components.Add ( new Button ( game, Button.LeftJump, player ) );
			game.Components.Add ( new Button ( game, Button.RightJump, player ) );
#else
			game.Components.Add ( new Button ( game, Button.Jump, player ) );
#endif
		}
	}
}

