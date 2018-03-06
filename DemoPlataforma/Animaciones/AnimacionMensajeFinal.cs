using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class AnimacionMensajeFinal : AnimacionBase
	{
		
		const string TextoFinal1 = "Thank You!";
		const string TextoFinal2 = "For playing at this proof of concept";
		Vector2 posTexto1 = new Vector2 ( 100, 80 );
		Vector2 posTexto2 = new Vector2 ( 10, 150 );
		Color colorTexto;
		
		public AnimacionMensajeFinal ()
		{
			colorTexto = new Color (  ( (float) Color.Yellow.R ) / 255.0f,  ( (float) Color.Yellow.G ) / 255.0f, ( (float) Color.Yellow.B ) / 255.0f, 0.9f );
		}
		
		public override void Draw (SpriteBatch sb)
		{
			
			sb.DrawString ( _game.GeneralFont, TextoFinal1, posTexto1, colorTexto, 0.0f, Vector2.Zero, 2.8f, SpriteEffects.None, 0.0f );
			sb.DrawString ( _game.GeneralFont, TextoFinal2, posTexto2, colorTexto, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.0f );
		}
		
		public override void Update (GameTime gameTime)
		{
		}
	}
}

