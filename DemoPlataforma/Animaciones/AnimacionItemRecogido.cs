using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class AnimacionItemRecogido : AnimacionBase
	{
		public delegate void FinAnimacion ();
		
		private const int INICIANDO = 0;
		private const int ACERCANDO = 1;
		private const int PRESENTACION = 2;
		private const int ALEJANDO = 3;
		private const int FINALIZADO = 4;
		
		private float DELTA_ROTACION = MathHelper.ToRadians ( 360 / 12 );
		private float DELTA_ESCALADO = 1.25f;
		private int NUM_PASOS = 12;
		
		protected Item _item;
		protected int _fase = INICIANDO;
		protected float _rotacion = 0.0f;
		protected float _escala = 0.5f;
		protected int _paso;
		protected TimeSpan _wait_frame = new TimeSpan ( TimeSpan.TicksPerSecond / 50 );
		protected TimeSpan _elapsed = TimeSpan.Zero;
		protected int _screenWidth, _screenHeight;
		protected Vector2 screenPosition = new Vector2 ( 480 / 2, 320 / 2 );
		protected FinAnimacion _metodo_fin_animacion;
		
		public AnimacionItemRecogido ( Item item, FinAnimacion metodo ) 
		{
			_item = item;
			_metodo_fin_animacion = metodo;
		}
		
		public override void Update ( GameTime gameTime )
		{
			switch ( _fase )
			{
				case INICIANDO:
					_screenWidth = 480;
					_screenHeight = 320;
					_fase = ACERCANDO;
					break;
				case ACERCANDO:
					updateAcercando ( gameTime );
					break;
				case PRESENTACION:
					_elapsed += gameTime.ElapsedGameTime;
					_item.Update ( gameTime );
					if ( _elapsed > TimeSpan.FromSeconds ( 1 ) )
					{
						_fase = ALEJANDO;
						_rotacion = MathHelper.ToRadians ( 360.0f );
						_elapsed = TimeSpan.Zero;
						_paso = 0;
					}
					break;
				case ALEJANDO:
					updateAlejando ( gameTime );
					break;
				case FINALIZADO:
#if DEBUG
					Console.WriteLine ( "Se ha finalizado la animacion" );
#endif
					_ended = true;
					_metodo_fin_animacion ();
					break;
				default:
					break;
			}
		}
		
		public override void Draw ( SpriteBatch sb )
		{
			if ( ( _fase != INICIANDO ) && ( _fase != FINALIZADO ) )
			{
				int finalSize = (int) ( 40 * _escala );
				int w = ( _screenWidth - finalSize ) / 2;
				int h = ( _screenHeight - finalSize - 20 ) / 2;
				screenPosition.X = w;
				screenPosition.Y = h;

				_item.Draw ( sb, screenPosition, _rotacion, _escala, 0.01f, 0.95f );
			}
		}
		
		private void updateAcercando ( GameTime gameTime )
		{
			_elapsed += gameTime.ElapsedGameTime;
			if ( _elapsed > _wait_frame )
			{
				_elapsed = TimeSpan.Zero;
				
				_paso++;
				if ( _paso >= NUM_PASOS )
				{
					_fase = PRESENTACION;
					_rotacion = 0.0f;
				}
				else
				{
					_rotacion += DELTA_ROTACION;
					_escala *= DELTA_ESCALADO;
				}
			}
			
			_item.Update ( gameTime );
		}
		
		private void updateAlejando ( GameTime gameTime )
		{
			_elapsed += gameTime.ElapsedGameTime;
			if ( _elapsed > _wait_frame )
			{
				_elapsed = TimeSpan.Zero;
				
				_paso++;
				if ( _paso >= NUM_PASOS )
				{
					_fase = FINALIZADO;
				}
				else
				{
					_rotacion -= DELTA_ROTACION;
					_escala /= DELTA_ESCALADO;
				}
			}
			
			_item.Update ( gameTime );			
		}
	}
}

