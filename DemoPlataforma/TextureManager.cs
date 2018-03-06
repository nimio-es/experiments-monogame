using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DemoPlataforma
{
	
	public enum FigureEnum : uint {
		
#if SHOW_WALLS
		MarcaBloquePared,
#endif
#if SHOW_CAMERAS
		MarcaCamera,
#endif
		
		FiguraEntrada,
		Columna1,
		SpiderWeb,
		MonteCalaveras,
		PiedraJeroglifico1,
		
		LadrilloSueloConHierbaVerde,
		LadrilloSueloConHierbaAzul,
		BriznaHierbaVerdeEsquinaIzquierda,
		BriznaHierbaVerdeEsquinaDerecha,
		BriznaHierbaRojaEsquinaIzquierda,
		BriznaHierbaRojaEsquinaDerecha,
		BriznaHierbaRoja1,
		BriznaHierbaRoja2,
		BriznaHierbaRoja3,
		BriznaHierbaAzulEsquinaIzquierda,
		BriznaHierbaAzulEsquinaDerecha,
		BriznaHierbaAzul1,
		BriznaHierbaAzul2,
		BriznaHierbaAzul3,

		PiedraCuadradaOcre1,
		PiedraCuadradaOcre2,
		PiedraCuadradaOcre3,
		PiedraCuadradaOcre4,
		PiedraCuadradaOcre5,
		PiedraCuadradaOcre6,
		PiedraCuadradaOcre7,
		PiedraCuadradaOcre8,
		PiedraCuadradaOcre9,
		PiedraCuadradaOcre10,
		PiedraCuadradaOcre11,
		PiedrillaOcre1,
		PiedrillaOcre2,
		PiedrillaOcre3,
		PiedrillaOcre4,
		PiedraRectangularHorizontalOcre5,
		
		PiedraCuadradaVerde1,
		PiedraCuadradaVerde2,
		PiedraCuadradaVerde3,
		PiedraCuadradaVerde4,
		PiedraCuadradaVerde5,
		PiedraCuadradaVerde6,
		PiedraCuadradaVerde7,
		PiedraCuadradaVerde8,
		PiedraCuadradaVerde9,
		PiedraCuadradaVerde10,
		PiedraCuadradaVerde11,
		PiedrillaVerde1,
		PiedrillaVerde2,
		PiedrillaVerde3,
		PiedrillaVerde4,
		
		PiedraCuadradaClara1,
		PiedraCuadradaClara2,
		PiedraCuadradaClara3,
		PiedraCuadradaClara4,
		PiedraCuadradaClara5,
		PiedraCuadradaClara6,
		PiedraCuadradaClara7,
		PiedraCuadradaClara8,
		PiedraCuadradaClara9,
		PiedraCuadradaClara10,
		PiedraCuadradaClara11,
		PiedrillaClara1,
		PiedrillaClara2,
		PiedrillaClara3,
		PiedrillaClara4,
		PiedraRectangularHorizontalClara1,
		PiedraRectangularHorizontalClara2,
		PiedraRectangularHorizontalClara3,
		PiedraRectangularHorizontalClara4,
		PiedraRectangularHorizontalClara5,
		PiedraRectangularHorizontalClara6,
		
		PiedrillasVerticalRoja1,
		PiedrillasVerticalRoja2,

		PiedraCuadradaAzul1,
		PiedraCuadradaAzul2,
		PiedraCuadradaAzul3,
		PiedraCuadradaAzul4,
		PiedraCuadradaAzul5,
		PiedraCuadradaAzul6,
		PiedraCuadradaAzul7,
		PiedraCuadradaAzul8,
		PiedraCuadradaAzul9,
		PiedraCuadradaAzul10,
		PiedraCuadradaAzul11,
		
		
		DobleLadrilloOcre,
		DobleLadrilloVerde,
		DobleLadrilloVioleta,
		LadrilloSimpleVerde,
		LadrilloSimpleVerdePartido,
		LadrilloSimpleVioleta,
		LadrilloSimpleVioletaPartido
	}
	
	public class FigureData {
		
		public readonly FigureEnum Tipo;
		public readonly Texture2D Container;
		public readonly Rectangle Rect;
		public readonly float Depth;
		
		public FigureData ( FigureEnum tipo, Texture2D container, Rectangle rectangle, float depth )
		{
			Tipo = tipo;
			Container = container;
			Rect = rectangle;
			Depth = depth;
		}
	}
	
	public static class TextureManager
	{
		
		
		
		public static Texture2D Sprites, Piedras, Decorados, Title, Items;
		public static Dictionary<FigureEnum, FigureData> Figures; 
		
		public static void Initialize ( ContentManager content )
		{
			Sprites = content.Load<Texture2D> ( "sprites" );
			Piedras = content.Load<Texture2D> ( "piedras" );
			Decorados = content.Load<Texture2D> ( "decorados" );
			Items = content.Load<Texture2D> ( "items.png" );
			Title = content.Load<Texture2D> ( "title" );
			createDictionary ();
		}
		
		private static void createDictionary ()
		{
			Figures = new Dictionary<FigureEnum, FigureData> ();
			
#if SHOW_WALLS
			Figures.Add ( FigureEnum.MarcaBloquePared, new FigureData ( FigureEnum.MarcaBloquePared, Decorados, new Rectangle ( 431, 285, 20, 20 ), 0.01f ) );
#endif
#if SHOW_CAMERAS
			Figures.Add ( FigureEnum.MarcaCamera, new FigureData ( FigureEnum.MarcaCamera, Decorados, new Rectangle ( 389, 285, 20, 20 ), 0.01f ) );
#endif
			Figures.Add ( FigureEnum.FiguraEntrada, new FigureData ( FigureEnum.FiguraEntrada, Decorados, new Rectangle ( 246, 346, 100, 300 ), 0.5f ) );
			Figures.Add ( FigureEnum.Columna1, new FigureData ( FigureEnum.Columna1, Decorados, new Rectangle ( 305, 0, 80, 100 ), 0.5f ) );
			Figures.Add ( FigureEnum.SpiderWeb, new FigureData ( FigureEnum.SpiderWeb, Decorados, new Rectangle ( 0, 306, 80, 60 ), 0.1f ) );
			Figures.Add ( FigureEnum.MonteCalaveras, new FigureData ( FigureEnum.MonteCalaveras, Decorados, new Rectangle ( 82, 306, 80, 40 ), 0.1f ) );
			Figures.Add ( FigureEnum.PiedraJeroglifico1, new FigureData ( FigureEnum.PiedraJeroglifico1, Decorados, new Rectangle ( 410, 0, 80, 80 ), 0.1f ) );
			
			Figures.Add ( FigureEnum.BriznaHierbaVerdeEsquinaIzquierda, new FigureData ( FigureEnum.BriznaHierbaVerdeEsquinaIzquierda, Decorados, new Rectangle ( 347, 306, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaVerdeEsquinaDerecha, new FigureData ( FigureEnum.BriznaHierbaVerdeEsquinaDerecha, Decorados, new Rectangle ( 326, 306, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaRojaEsquinaIzquierda, new FigureData ( FigureEnum.BriznaHierbaRojaEsquinaIzquierda, Decorados, new Rectangle ( 389, 306, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaRojaEsquinaDerecha, new FigureData ( FigureEnum.BriznaHierbaRojaEsquinaDerecha, Decorados, new Rectangle ( 368, 306, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaRoja1, new FigureData ( FigureEnum.BriznaHierbaRoja1, Decorados, new Rectangle ( 368, 264, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaRoja2, new FigureData ( FigureEnum.BriznaHierbaRoja2, Decorados, new Rectangle ( 389, 264, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaRoja3, new FigureData ( FigureEnum.BriznaHierbaRoja3, Decorados, new Rectangle ( 368, 285, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaAzulEsquinaIzquierda, new FigureData ( FigureEnum.BriznaHierbaAzulEsquinaIzquierda, Decorados, new Rectangle ( 389, 222, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaAzulEsquinaDerecha, new FigureData ( FigureEnum.BriznaHierbaAzulEsquinaDerecha, Decorados, new Rectangle ( 368, 222, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaAzul1, new FigureData ( FigureEnum.BriznaHierbaAzul1, Decorados, new Rectangle ( 368, 180, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaAzul2, new FigureData ( FigureEnum.BriznaHierbaAzul2, Decorados, new Rectangle ( 389, 180, 20, 20 ), 0.1f ) );
			Figures.Add ( FigureEnum.BriznaHierbaAzul3, new FigureData ( FigureEnum.BriznaHierbaAzul3, Decorados, new Rectangle ( 368, 201, 20, 20 ), 0.1f ) );
			
			Figures.Add ( FigureEnum.LadrilloSueloConHierbaVerde, new FigureData ( FigureEnum.LadrilloSueloConHierbaVerde, Decorados, new Rectangle ( 164, 347, 80, 19 ), 0.5f ) );
			Figures.Add ( FigureEnum.LadrilloSueloConHierbaAzul, new FigureData ( FigureEnum.LadrilloSueloConHierbaAzul, Decorados, new Rectangle ( 246, 648, 80, 20 ), 0.5f ) );

			Figures.Add ( FigureEnum.PiedraCuadradaOcre1, new FigureData ( FigureEnum.PiedraCuadradaOcre1, Piedras, new Rectangle ( 0, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre2, new FigureData ( FigureEnum.PiedraCuadradaOcre2, Piedras, new Rectangle ( 41, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre3, new FigureData ( FigureEnum.PiedraCuadradaOcre3, Piedras, new Rectangle ( 82, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre4, new FigureData ( FigureEnum.PiedraCuadradaOcre4, Piedras, new Rectangle ( 123, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre5, new FigureData ( FigureEnum.PiedraCuadradaOcre5, Piedras, new Rectangle ( 164, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre6, new FigureData ( FigureEnum.PiedraCuadradaOcre6, Piedras, new Rectangle ( 205, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre7, new FigureData ( FigureEnum.PiedraCuadradaOcre7, Piedras, new Rectangle ( 246, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre8, new FigureData ( FigureEnum.PiedraCuadradaOcre8, Piedras, new Rectangle ( 287, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre9, new FigureData ( FigureEnum.PiedraCuadradaOcre9, Piedras, new Rectangle ( 328, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre10, new FigureData ( FigureEnum.PiedraCuadradaOcre10, Piedras, new Rectangle ( 369, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaOcre11, new FigureData ( FigureEnum.PiedraCuadradaOcre11, Piedras, new Rectangle ( 410, 0, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaOcre1, new FigureData ( FigureEnum.PiedrillaOcre1, Piedras, new Rectangle ( 451, 0, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaOcre2, new FigureData ( FigureEnum.PiedrillaOcre2, Piedras, new Rectangle ( 451, 20, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaOcre3, new FigureData ( FigureEnum.PiedrillaOcre3, Piedras, new Rectangle ( 472, 0, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaOcre4, new FigureData ( FigureEnum.PiedrillaOcre4, Piedras, new Rectangle ( 472, 20, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalOcre5, new FigureData ( FigureEnum.PiedraRectangularHorizontalOcre5, Piedras, new Rectangle ( 410, 41, 80, 40 ), 0.5f ) );

			Figures.Add ( FigureEnum.PiedraCuadradaVerde1, new FigureData ( FigureEnum.PiedraCuadradaVerde1, Piedras, new Rectangle ( 0, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde2, new FigureData ( FigureEnum.PiedraCuadradaVerde2, Piedras, new Rectangle ( 41, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde3, new FigureData ( FigureEnum.PiedraCuadradaVerde3, Piedras, new Rectangle ( 82, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde4, new FigureData ( FigureEnum.PiedraCuadradaVerde4, Piedras, new Rectangle ( 123, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde5, new FigureData ( FigureEnum.PiedraCuadradaVerde5, Piedras, new Rectangle ( 164, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde6, new FigureData ( FigureEnum.PiedraCuadradaVerde6, Piedras, new Rectangle ( 205, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde7, new FigureData ( FigureEnum.PiedraCuadradaVerde7, Piedras, new Rectangle ( 246, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde8, new FigureData ( FigureEnum.PiedraCuadradaVerde8, Piedras, new Rectangle ( 287, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde9, new FigureData ( FigureEnum.PiedraCuadradaVerde9, Piedras, new Rectangle ( 328, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde10, new FigureData ( FigureEnum.PiedraCuadradaVerde10, Piedras, new Rectangle ( 369, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaVerde11, new FigureData ( FigureEnum.PiedraCuadradaVerde11, Piedras, new Rectangle ( 410, 246, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaVerde1, new FigureData ( FigureEnum.PiedrillaVerde1, Piedras, new Rectangle ( 451, 246, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaVerde2, new FigureData ( FigureEnum.PiedrillaVerde2, Piedras, new Rectangle ( 451, 266, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaVerde3, new FigureData ( FigureEnum.PiedrillaVerde3, Piedras, new Rectangle ( 472, 246, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaVerde4, new FigureData ( FigureEnum.PiedrillaVerde4, Piedras, new Rectangle ( 472, 266, 20, 20 ), 0.5f ) );

			Figures.Add ( FigureEnum.PiedraCuadradaClara1, new FigureData ( FigureEnum.PiedraCuadradaClara1, Piedras, new Rectangle ( 0, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara2, new FigureData ( FigureEnum.PiedraCuadradaClara2, Piedras, new Rectangle ( 41, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara3, new FigureData ( FigureEnum.PiedraCuadradaClara3, Piedras, new Rectangle ( 82, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara4, new FigureData ( FigureEnum.PiedraCuadradaClara4, Piedras, new Rectangle ( 123, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara5, new FigureData ( FigureEnum.PiedraCuadradaClara5, Piedras, new Rectangle ( 164, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara6, new FigureData ( FigureEnum.PiedraCuadradaClara6, Piedras, new Rectangle ( 205, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara7, new FigureData ( FigureEnum.PiedraCuadradaClara7, Piedras, new Rectangle ( 246, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara8, new FigureData ( FigureEnum.PiedraCuadradaClara8, Piedras, new Rectangle ( 287, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara9, new FigureData ( FigureEnum.PiedraCuadradaClara9, Piedras, new Rectangle ( 328, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara10, new FigureData ( FigureEnum.PiedraCuadradaClara10, Piedras, new Rectangle ( 369, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaClara11, new FigureData ( FigureEnum.PiedraCuadradaClara11, Piedras, new Rectangle ( 410, 369, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaClara1, new FigureData ( FigureEnum.PiedrillaClara1, Piedras, new Rectangle ( 451, 369, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaClara2, new FigureData ( FigureEnum.PiedrillaClara2, Piedras, new Rectangle ( 451, 389, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaClara3, new FigureData ( FigureEnum.PiedrillaClara3, Piedras, new Rectangle ( 472, 369, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillaClara4, new FigureData ( FigureEnum.PiedrillaClara4, Piedras, new Rectangle ( 472, 389, 20, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalClara1, new FigureData ( FigureEnum.PiedraRectangularHorizontalClara1, Piedras, new Rectangle ( 164, 408, 80, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalClara2, new FigureData ( FigureEnum.PiedraRectangularHorizontalClara2, Piedras, new Rectangle ( 246, 408, 80, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalClara3, new FigureData ( FigureEnum.PiedraRectangularHorizontalClara3, Piedras, new Rectangle ( 164, 449, 80, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalClara4, new FigureData ( FigureEnum.PiedraRectangularHorizontalClara4, Piedras, new Rectangle ( 246, 449, 80, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalClara5, new FigureData ( FigureEnum.PiedraRectangularHorizontalClara5, Piedras, new Rectangle ( 368, 408, 80, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraRectangularHorizontalClara6, new FigureData ( FigureEnum.PiedraRectangularHorizontalClara6, Piedras, new Rectangle ( 368, 449, 80, 40 ), 0.5f ) );
			
			Figures.Add ( FigureEnum.PiedrillasVerticalRoja1, new FigureData ( FigureEnum.PiedrillasVerticalRoja1, Piedras, new Rectangle ( 451, 123, 20, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedrillasVerticalRoja2, new FigureData ( FigureEnum.PiedrillasVerticalRoja2, Piedras, new Rectangle ( 472, 123, 20, 40 ), 0.5f ) );
			
			Figures.Add ( FigureEnum.PiedraCuadradaAzul1, new FigureData ( FigureEnum.PiedraCuadradaAzul1, Piedras, new Rectangle ( 0, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul2, new FigureData ( FigureEnum.PiedraCuadradaAzul2, Piedras, new Rectangle ( 41, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul3, new FigureData ( FigureEnum.PiedraCuadradaAzul3, Piedras, new Rectangle ( 82, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul4, new FigureData ( FigureEnum.PiedraCuadradaAzul4, Piedras, new Rectangle ( 123, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul5, new FigureData ( FigureEnum.PiedraCuadradaAzul5, Piedras, new Rectangle ( 164, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul6, new FigureData ( FigureEnum.PiedraCuadradaAzul6, Piedras, new Rectangle ( 205, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul7, new FigureData ( FigureEnum.PiedraCuadradaAzul7, Piedras, new Rectangle ( 246, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul8, new FigureData ( FigureEnum.PiedraCuadradaAzul8, Piedras, new Rectangle ( 287, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul9, new FigureData ( FigureEnum.PiedraCuadradaAzul9, Piedras, new Rectangle ( 328, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul10, new FigureData ( FigureEnum.PiedraCuadradaAzul10, Piedras, new Rectangle ( 369, 492, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.PiedraCuadradaAzul11, new FigureData ( FigureEnum.PiedraCuadradaAzul11, Piedras, new Rectangle ( 410, 492, 40, 40 ), 0.5f ) );
			
			Figures.Add ( FigureEnum.DobleLadrilloOcre, new FigureData ( FigureEnum.DobleLadrilloOcre, Decorados, new Rectangle ( 0, 162, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.DobleLadrilloVerde, new FigureData ( FigureEnum.DobleLadrilloVerde, Decorados, new Rectangle ( 41, 162, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.DobleLadrilloVioleta, new FigureData ( FigureEnum.DobleLadrilloVioleta, Decorados, new Rectangle ( 82, 162, 40, 40 ), 0.5f ) );
			Figures.Add ( FigureEnum.LadrilloSimpleVerde, new FigureData ( FigureEnum.LadrilloSimpleVerde, Decorados, new Rectangle ( 41, 162, 40, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.LadrilloSimpleVerdePartido, new FigureData ( FigureEnum.LadrilloSimpleVerdePartido, Decorados, new Rectangle ( 41, 182, 40, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.LadrilloSimpleVioleta, new FigureData ( FigureEnum.LadrilloSimpleVioleta, Decorados, new Rectangle ( 82, 162, 40, 20 ), 0.5f ) );
			Figures.Add ( FigureEnum.LadrilloSimpleVioletaPartido, new FigureData ( FigureEnum.LadrilloSimpleVioletaPartido, Decorados, new Rectangle ( 82, 182, 40, 20 ), 0.5f ) );
		}
		
	}
}

