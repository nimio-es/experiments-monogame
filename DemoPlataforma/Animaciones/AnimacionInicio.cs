using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class AnimacionInicio : AnimacionBase
	{
		
		internal enum Estado { 
								Iniciando, 
								FadeInTituloGraficos, 
								FadeInTextoGraficos,
								EsperaTextoInGraficos,
								FadeOutTextoGraficos,
								EsperaEntreCreditos,
								FadeInTituloMusica,
								FadeInTextoMusica,
								EsperaTextoInMusica,
								FadeOutTextoMusica,
								Final
			};
		
		private Color _color_titulo = new Color ( 0, 165, 248, 0 );
		private Color _color_texto = new Color ( 255, 255, 255, 0 );
		private TimeSpan _elapse_last_update = TimeSpan.Zero;
		private TimeSpan _wait_next_update = TimeSpan.FromSeconds ( 5 );
		private Estado _estado = Estado.Iniciando;
		private int _etapa_degradado = 0;
		private SpriteFont _font;
		private const int NumEtapasFade = 20;
		
		public AnimacionInicio ( SpriteFont font )
		{
			_ended = false;
			_font = font;
		}
		
		public override void Update (GameTime gameTime)
		{
			switch ( _estado )
			{
				case Estado.Iniciando:
					updateIniciando ( gameTime );
					break;
				case Estado.FadeInTituloGraficos:
					updateFadeInTituloGraficos ( gameTime );
					break;
				case Estado.FadeInTextoGraficos:
					updateFadeInTextoGraficos ( gameTime );
					break;
				case Estado.EsperaTextoInGraficos:
					updateEsperaTextoInGraficos ( gameTime );
					break;
				case Estado.FadeOutTextoGraficos:
					updateFadeOutTextoGraficos ( gameTime );
					break;
				case Estado.EsperaEntreCreditos:
					updateEsperaEntreCreditos ( gameTime );
					break;
				case Estado.FadeInTituloMusica:
					updateFadeInTituloMusica ( gameTime );
					break;
				case Estado.FadeInTextoMusica:
					updateFadeInTextoMusica ( gameTime );
					break;
				case Estado.EsperaTextoInMusica:
					updateEsperaTextoInMusica ( gameTime );
					break;
				case Estado.FadeOutTextoMusica:
					updateFadeOutTextoMusica ( gameTime);
					break;
				case Estado.Final:
					_ended = true;
					break;
				default:
					break;
			}
		}
		
		public override void Draw (SpriteBatch sb)
		{
			switch ( _estado )
			{
				case Estado.FadeInTituloGraficos:
					drawTituloGraficos ( sb );
					break;
				case Estado.FadeInTextoGraficos:
					drawTextoGraficos ( sb );
					break;
				case Estado.EsperaTextoInGraficos:
					drawTextoGraficos ( sb );
					break;
				case Estado.FadeOutTextoGraficos:
					drawTextoGraficos ( sb );
					break;
				case Estado.FadeInTituloMusica:
					drawTituloMusica ( sb );
					break;
				case Estado.FadeInTextoMusica:
					drawTextoMusica ( sb );
					break;
				case Estado.EsperaTextoInMusica:
					drawTextoMusica ( sb );
					break;
				case Estado.FadeOutTextoMusica:
					drawTextoMusica ( sb );
					break;
				default:
					break;
			}
		}
		
		private void updateIniciando ( GameTime gameTime )	
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_estado = Estado.FadeInTituloGraficos;
				_wait_next_update = new TimeSpan ( (long) ( TimeSpan.TicksPerSecond * 2.0f / NumEtapasFade ) );
				_elapse_last_update = TimeSpan.Zero;
			}
		}
		
		private void updateFadeInTituloGraficos ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_etapa_degradado++;
				if ( _etapa_degradado == NumEtapasFade )
				{
					_etapa_degradado = 0;
					_estado = Estado.FadeInTextoGraficos;
				}
				else
				{
					_color_titulo.A = (byte) ( 255 / NumEtapasFade * _etapa_degradado );
					_elapse_last_update = TimeSpan.Zero;
				}
			}
		}
		
		private void updateFadeInTextoGraficos ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_etapa_degradado++;
				if ( _etapa_degradado == NumEtapasFade )
				{
					_etapa_degradado = 0;
					_estado = Estado.EsperaTextoInGraficos;
					_wait_next_update = TimeSpan.FromSeconds ( 3 );
				}
				else
				{
					_color_texto.A = (byte) ( 255 / NumEtapasFade * _etapa_degradado );
					_elapse_last_update = TimeSpan.Zero;
				}
			}
		}
		
		private void updateEsperaTextoInGraficos ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_estado = Estado.FadeOutTextoGraficos;
				_wait_next_update = new TimeSpan ( (long) ( TimeSpan.TicksPerSecond * 2.0f / NumEtapasFade ) );
			}			
		}
		
		private void updateFadeOutTextoGraficos ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_etapa_degradado++;
				if ( _etapa_degradado == NumEtapasFade )
				{
					_etapa_degradado = 0;
					_estado = Estado.EsperaEntreCreditos;
					_wait_next_update = TimeSpan.FromSeconds ( 3 );
				}
				else
				{
					_color_texto.A = (byte) ( 255 / NumEtapasFade * (NumEtapasFade - _etapa_degradado) );
					_color_titulo.A = (byte) ( 255 / NumEtapasFade * (NumEtapasFade - _etapa_degradado) );
					_elapse_last_update = TimeSpan.Zero;
				}
			}
		}
		
		private void updateEsperaEntreCreditos ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_estado = Estado.FadeInTituloMusica;
				_wait_next_update = new TimeSpan ( (long) ( TimeSpan.TicksPerSecond * 2.0f / NumEtapasFade ) );
			}						
		}
		
		private void updateFadeInTituloMusica ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_etapa_degradado++;
				if ( _etapa_degradado == NumEtapasFade )
				{
					_etapa_degradado = 0;
					_estado = Estado.FadeInTextoMusica;
				}
				else
				{
					_color_titulo.A = (byte) ( 255 / NumEtapasFade * _etapa_degradado );
					_elapse_last_update = TimeSpan.Zero;
				}
			}
		}
		
		private void updateFadeInTextoMusica ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_etapa_degradado++;
				if ( _etapa_degradado == NumEtapasFade )
				{
					_etapa_degradado = 0;
					_estado = Estado.EsperaTextoInMusica;
					_wait_next_update = TimeSpan.FromSeconds ( 3 );
				}
				else
				{
					_color_texto.A = (byte) ( 255 / NumEtapasFade * _etapa_degradado );
					_elapse_last_update = TimeSpan.Zero;
				}
			}
		}
		
		private void updateEsperaTextoInMusica ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_estado = Estado.FadeOutTextoMusica;
				_wait_next_update = new TimeSpan ( (long) ( TimeSpan.TicksPerSecond * 2.0f / NumEtapasFade ) );
			}			
		}
		
		private void updateFadeOutTextoMusica ( GameTime gameTime )
		{
			_elapse_last_update += gameTime.ElapsedGameTime;
			if ( _elapse_last_update > _wait_next_update )
			{
				_etapa_degradado++;
				if ( _etapa_degradado == NumEtapasFade )
				{
					_etapa_degradado = 0;
					_estado = Estado.Final;
					_wait_next_update = TimeSpan.FromSeconds ( 3 );
				}
				else
				{
					_color_texto.A = (byte) ( 255 / NumEtapasFade * (NumEtapasFade - _etapa_degradado) );
					_color_titulo.A = (byte) ( 255 / NumEtapasFade * (NumEtapasFade - _etapa_degradado) );
					_elapse_last_update = TimeSpan.Zero;
				}
			}
		}		
	
		private void drawTituloGraficos (SpriteBatch sb)
		{
			string texto = "Graphics & Sounds:";
			Vector2 dimTexto = _font.MeasureString ( texto );
			sb.DrawString ( 
				_font, 
				texto, 
				new Vector2 ( 465 - ( dimTexto.X * 1.15f ), 70 ), 
				_color_titulo,
				0.0f,
				Vector2.Zero,
				1.15f,
				SpriteEffects.None,
				0.0f );
		}
		
		private void drawTextoGraficos ( SpriteBatch sb )
		{
			string texto1 = "taken from";
			Vector2 dimTexto1 = _font.MeasureString ( texto1 );
			string texto2 = "\"Abu Simbel Profanation Deluxe\"";
			Vector2 dimTexto2 = _font.MeasureString ( texto2 );
			string texto3 = "http://www.masoftware.es";
			Vector2 dimTexto3 = _font.MeasureString ( texto3 );

			drawTituloGraficos ( sb );
			sb.DrawString ( 
				_font, 
				texto1, 
				new Vector2 ( 465 - dimTexto1.X, 100 ), 
				_color_texto, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.0f );
			sb.DrawString ( 
				_font, 
				texto2, 
				new Vector2 ( 465 - dimTexto2.X, 120 ), 
				_color_texto, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.0f );
			sb.DrawString ( 
				_font, 
				texto3, 
				new Vector2 ( 465 - dimTexto3.X, 140 ), 
				_color_texto, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.0f );
		}
		
		private void drawTituloMusica (SpriteBatch sb)
		{
			string texto = "Game Music:";
			Vector2 dimTexto = _font.MeasureString ( texto );
			sb.DrawString ( 
				_font, 
				texto, 
				new Vector2 ( 465 - ( dimTexto.X * 1.15f ), 70 ), 
				_color_titulo,
				0.0f,
				Vector2.Zero,
				1.15f,
				SpriteEffects.None,
				0.0f );
		}		

		private void drawTextoMusica ( SpriteBatch sb )
		{
			string texto1 = "\"Cauldron II (The Witch Who Stepped in Dub)\"";
			Vector2 dimTexto1 = _font.MeasureString ( texto1 );
			string texto2 = "Sound of Hazel";
			Vector2 dimTexto2 = _font.MeasureString ( texto2 );
			string texto3 = "http://www.soundofhazel.com";
			Vector2 dimTexto3 = _font.MeasureString ( texto3 ); 

			drawTituloMusica ( sb );			
			sb.DrawString ( 
				_font, 
				texto1, 
				new Vector2 ( 465 - dimTexto1.X, 100 ), 
				_color_texto, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.0f );
			sb.DrawString ( 
				_font, 
				texto2, 
				new Vector2 ( 465 - dimTexto2.X, 120 ), 
				_color_texto, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.0f );
			sb.DrawString ( 
				_font, 
				texto3, 
				new Vector2 ( 465 - dimTexto3.X, 140 ), 
				_color_texto, 
				0.0f, 
				Vector2.Zero, 
				1.0f, 
				SpriteEffects.None, 
				0.0f );
		}
		
	}
}

