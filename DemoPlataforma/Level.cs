using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DemoPlataforma
{
	public class Level : DrawableGameComponent
	{
	
		internal struct BlockData {
			public readonly Vector2 Position;
			public readonly Rectangle RectangleInWorld;
			public readonly FigureData FigureData;
			public readonly bool Bloquea;
			
			public BlockData ( Vector2 position, FigureData figureData, bool bloquea )
			{
				Position = position;
				FigureData = figureData;
				RectangleInWorld = new Rectangle ( (int) Position.X, (int) Position.Y, FigureData.Rect.Width, FigureData.Rect.Height );
				Bloquea = bloquea;
			}
			
			public BlockData ( float x, float y, FigureData figureData )
				: this ( new Vector2 ( x, y), figureData, true )
			{
			}
			
			public BlockData ( float x, float y, FigureData figureData, bool bloquea )
				: this ( new Vector2 ( x, y), figureData, bloquea )
			{
			}
		}
		
		private Vector2 _levelDimensions = new Vector2 ( 1280.0f, 960.0f );
		private Camera _currentCamera;
		
		private List<BlockData> _blocks = new List<BlockData> ();
		private bool [,] _wallMap;
		
		/// <summary>
		/// Mantiene el estado global del juego
		/// </summary>
		public Dictionary<string, object> Status = new Dictionary<string, object> ();
		
		public Level ( Game game ) : base ( game )
		{
			initializeLevel ();
		}
		
		#region Definiendo el "Universo" de forma cutre
		private void initializeLevel ()
		{
			
			#region Pantalla 1
			// ********************* PANTALLA 1 ********************* 
			_blocks.Add ( new BlockData ( 0, 0, TextureManager.Figures [ FigureEnum.FiguraEntrada ] ) );
			for ( int i = 0; i < 8; i ++ )
				_blocks.Add ( new BlockData ( 80 * i, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaVerde ], true ) );
			for ( int i = 0; i < 6; i = i + 2 )
			{
				_blocks.Add ( new BlockData ( 100, 20 + ( i * 40 ), TextureManager.Figures [ FigureEnum.PiedrillasVerticalRoja1 ] ) );
				_blocks.Add ( new BlockData ( 100, 20 + ( ( i + 1 ) * 40 ), TextureManager.Figures [ FigureEnum.PiedrillasVerticalRoja2 ] ) );
			}
			for ( int i = 0; i < 6; i++ )
				_blocks.Add ( new BlockData ( 120, 20 + ( i * 40 ), TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 160, 220, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			for ( int i = 0; i < 12; i++ )
				_blocks.Add ( new BlockData ( 160 + ( i * 40 ), 20, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 160, 60, TextureManager.Figures [ FigureEnum.SpiderWeb ], false ) );
			_blocks.Add ( new BlockData ( 160, 200, TextureManager.Figures [ FigureEnum.BriznaHierbaVerdeEsquinaIzquierda ], false ) );
			_blocks.Add ( new BlockData ( 280, 280, TextureManager.Figures [ FigureEnum.LadrilloSimpleVioleta ] ) );
			_blocks.Add ( new BlockData ( 320, 280, TextureManager.Figures [ FigureEnum.LadrilloSimpleVioleta ] ) );
			_blocks.Add ( new BlockData ( 280, 180, TextureManager.Figures [ FigureEnum.Columna1 ] ) );
			for ( int i = 0; i < 4; i++ )
				_blocks.Add ( new BlockData ( 480 , 140 + i * 40, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 440, 220, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre10 ] ) );
			_blocks.Add ( new BlockData ( 440, 260, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre11 ] ) );
			_blocks.Add ( new BlockData ( 460, 200, TextureManager.Figures [ FigureEnum.BriznaHierbaVerdeEsquinaDerecha ], false ) );
			_blocks.Add ( new BlockData ( 520, 260, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 560, 260, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 600, 260, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 600, 220, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 520, 240, TextureManager.Figures [ FigureEnum.BriznaHierbaVerdeEsquinaIzquierda ], false ) );
			_blocks.Add ( new BlockData ( 580, 240, TextureManager.Figures [ FigureEnum.BriznaHierbaVerdeEsquinaDerecha ], false ) );
			_blocks.Add ( new BlockData ( 600, 60, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			_blocks.Add ( new BlockData ( 600, 100, TextureManager.Figures [ FigureEnum.DobleLadrilloOcre ] ) );
			#endregion
			
			#region Patanlla 2
			// ********************* PANTALLA 2 *********************
			for ( int i = 0; i < 16; i++ )
				_blocks.Add ( new BlockData ( 640 + ( i * 40 ), 20, TextureManager.Figures [ FigureEnum.DobleLadrilloVerde ] ) );
			_blocks.Add ( new BlockData ( 640, 60, TextureManager.Figures [ FigureEnum.DobleLadrilloVerde ] ) );
			_blocks.Add ( new BlockData ( 640, 100, TextureManager.Figures [ FigureEnum.DobleLadrilloVerde ] ) );
			_blocks.Add ( new BlockData ( 640, 220, TextureManager.Figures [ FigureEnum.DobleLadrilloVerde ] ) );
			_blocks.Add ( new BlockData ( 640, 260, TextureManager.Figures [ FigureEnum.DobleLadrilloVerde ] ) );
			for ( int i = 0; i < 6; i++ ) 
				_blocks.Add ( new BlockData ( 1240, 60 + ( i * 40 ), TextureManager.Figures [ FigureEnum.DobleLadrilloVerde ] ) );
			for ( int i = 0; i < 5; i++ )
			{
				_blocks.Add ( new BlockData ( 1040 + ( i * 40 ), 200, TextureManager.Figures [ FigureEnum.LadrilloSimpleVerdePartido ] ) );
				_blocks.Add ( new BlockData ( 1040 + ( i * 40 ), 220, TextureManager.Figures [ FigureEnum.LadrilloSimpleVerde ] ) );
			}
			_blocks.Add ( new BlockData ( 680, 220, TextureManager.Figures [ FigureEnum.PiedrillaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 700, 220, TextureManager.Figures [ FigureEnum.PiedrillaOcre2 ] ) );
			_blocks.Add ( new BlockData ( 720, 220, TextureManager.Figures [ FigureEnum.PiedrillaOcre3 ] ) );
			_blocks.Add ( new BlockData ( 740, 220, TextureManager.Figures [ FigureEnum.PiedrillaOcre4 ] ) );
			_blocks.Add ( new BlockData ( 640, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaAzul ] ) );
			_blocks.Add ( new BlockData ( 720, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaAzul ] ) );
			_blocks.Add ( new BlockData ( 880, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaAzul ] ) );
			_blocks.Add ( new BlockData ( 960, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaAzul ] ) );
			_blocks.Add ( new BlockData ( 1040, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaAzul ] ) );
			_blocks.Add ( new BlockData ( 1200, 300, TextureManager.Figures [ FigureEnum.LadrilloSueloConHierbaAzul ] ) );
			_blocks.Add ( new BlockData ( 680, 260, TextureManager.Figures [ FigureEnum.MonteCalaveras ], false ) );
			_blocks.Add ( new BlockData ( 840, 140, TextureManager.Figures [ FigureEnum.PiedrillaClara4 ] ) );
			_blocks.Add ( new BlockData ( 860, 140, TextureManager.Figures [ FigureEnum.PiedrillaClara2 ] ) );
			_blocks.Add ( new BlockData ( 880, 140, TextureManager.Figures [ FigureEnum.PiedrillaClara1 ] ) );
			_blocks.Add ( new BlockData ( 900, 140, TextureManager.Figures [ FigureEnum.PiedrillaClara3 ] ) );
			for ( int i = 0; i < 6; i++ )
				_blocks.Add ( new BlockData ( 920 + ( i * 40 ), 120, TextureManager.Figures [ FigureEnum.DobleLadrilloVioleta ] ) );
			_blocks.Add ( new BlockData ( 920, 160, TextureManager.Figures [ FigureEnum.DobleLadrilloVioleta ] ) );
			_blocks.Add ( new BlockData ( 920, 200, TextureManager.Figures [ FigureEnum.DobleLadrilloVioleta ] ) );			
			_blocks.Add ( new BlockData ( 1220, 180, TextureManager.Figures [ FigureEnum.BriznaHierbaRojaEsquinaDerecha ], false ) );
			_blocks.Add ( new BlockData ( 980, 100, TextureManager.Figures [ FigureEnum.BriznaHierbaRojaEsquinaDerecha ], false ) );
			_blocks.Add ( new BlockData ( 1000, 100, TextureManager.Figures [ FigureEnum.BriznaHierbaRoja1 ], false ) );
			_blocks.Add ( new BlockData ( 1020, 100, TextureManager.Figures [ FigureEnum.BriznaHierbaRoja2 ], false ) );
			_blocks.Add ( new BlockData ( 1040, 100, TextureManager.Figures [ FigureEnum.BriznaHierbaRoja3 ], false ) );
			_blocks.Add ( new BlockData ( 1060, 100, TextureManager.Figures [ FigureEnum.BriznaHierbaRojaEsquinaIzquierda ], false ) );
			#endregion 
			
			#region Pantalla 3
			// Pantalla 3
			for ( int i = 0; i < 4; i++ )
				_blocks.Add ( new BlockData ( 640 + ( i * 40 ), 320, TextureManager.Figures [ FigureEnum.LadrilloSimpleVioletaPartido ] ) );
			for ( int i = 0; i < 6; i++ )
				_blocks.Add ( new BlockData ( 880 + ( i * 40 ), 320, TextureManager.Figures [ FigureEnum.LadrilloSimpleVioleta ] ) );
			_blocks.Add ( new BlockData ( 1200, 320, TextureManager.Figures [ FigureEnum.LadrilloSimpleVioleta ] ) );
			_blocks.Add ( new BlockData ( 1240, 320, TextureManager.Figures [ FigureEnum.LadrilloSimpleVioleta ] ) );
			_blocks.Add ( new BlockData ( 640, 340, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 680, 340, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre3 ] ) );
			_blocks.Add ( new BlockData ( 720, 340, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre2 ] ) );
			_blocks.Add ( new BlockData ( 880, 340, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre6 ] ) );
			_blocks.Add ( new BlockData ( 920, 340, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre9 ] ) );
			_blocks.Add ( new BlockData ( 1240, 340, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 640, 380, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre9 ] ) );
			_blocks.Add ( new BlockData ( 680, 380, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre4 ] ) );
			_blocks.Add ( new BlockData ( 880, 380, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre7 ] ) );
			_blocks.Add ( new BlockData ( 1120, 380, TextureManager.Figures [ FigureEnum.PiedraRectangularHorizontalOcre5 ] ) );
			_blocks.Add ( new BlockData ( 1200, 380, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre10 ] ) );
			_blocks.Add ( new BlockData ( 1240, 380, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre2 ] ) );
			_blocks.Add ( new BlockData ( 640, 420, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre7 ] ) );
			_blocks.Add ( new BlockData ( 680, 420, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre9 ] ) );
			_blocks.Add ( new BlockData ( 880, 420, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre10 ] ) );
			_blocks.Add ( new BlockData ( 1240, 420, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre8 ] ) );
			_blocks.Add ( new BlockData ( 640, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre9 ] ) );
			_blocks.Add ( new BlockData ( 880, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre4 ] ) );
			_blocks.Add ( new BlockData ( 920, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 960, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre10 ] ) );
			_blocks.Add ( new BlockData ( 1000, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre6 ] ) );
			_blocks.Add ( new BlockData ( 1040, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre10 ] ) );
			_blocks.Add ( new BlockData ( 1080, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre4 ] ) );
			_blocks.Add ( new BlockData ( 1120, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 1240, 460, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 640, 500, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre1 ] ) );
			_blocks.Add ( new BlockData ( 880, 500, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre5 ] ) );
			_blocks.Add ( new BlockData ( 920, 500, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre8 ] ) );
			_blocks.Add ( new BlockData ( 960, 500, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre3 ] ) );
			_blocks.Add ( new BlockData ( 1000, 500, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre7 ] ) );
			_blocks.Add ( new BlockData ( 1040, 500, TextureManager.Figures[ FigureEnum.PiedrillaVerde1 ] ) );
			_blocks.Add ( new BlockData ( 1060, 500, TextureManager.Figures[ FigureEnum.PiedrillaVerde2 ] ) );
			_blocks.Add ( new BlockData ( 1080, 500, TextureManager.Figures[ FigureEnum.PiedrillaVerde3 ] ) );
			_blocks.Add ( new BlockData ( 1100, 500, TextureManager.Figures[ FigureEnum.PiedrillaVerde4 ] ) );
			_blocks.Add ( new BlockData ( 1120, 500, TextureManager.Figures[ FigureEnum.PiedrillaVerde1 ] ) );
			_blocks.Add ( new BlockData ( 1140, 500, TextureManager.Figures[ FigureEnum.PiedrillaVerde2 ] ) );
			_blocks.Add ( new BlockData ( 1240, 500, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre4 ] ) );
			_blocks.Add ( new BlockData ( 640, 540, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre10 ] ) );
			_blocks.Add ( new BlockData ( 880, 540, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre11 ] ) );
			_blocks.Add ( new BlockData ( 920, 540, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre4 ] ) );
			_blocks.Add ( new BlockData ( 1200, 560, TextureManager.Figures [ FigureEnum.BriznaHierbaAzulEsquinaDerecha ], false ) );
			_blocks.Add ( new BlockData ( 1220, 560, TextureManager.Figures [ FigureEnum.BriznaHierbaAzul1 ], false ) );
			_blocks.Add ( new BlockData ( 1240, 540, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre2 ] ) );
			_blocks.Add ( new BlockData ( 680, 560, TextureManager.Figures [ FigureEnum.MonteCalaveras ], false ) );
			_blocks.Add ( new BlockData ( 760, 560, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre5 ] ) );
			_blocks.Add ( new BlockData ( 800, 560, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre6 ] ) );
			_blocks.Add ( new BlockData ( 840, 560, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre7 ] ) );
			_blocks.Add ( new BlockData ( 640, 580, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre4 ] ) );
			for ( int i = 0; i < 5; i++ )
				_blocks.Add ( new BlockData ( 680 + ( i * 40 ), 600, TextureManager.Figures [ FigureEnum.LadrilloSimpleVerde ] ) );
			_blocks.Add ( new BlockData ( 880, 580, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre9 ] ) );
			_blocks.Add ( new BlockData ( 1200, 580, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre11 ] ) );
			_blocks.Add ( new BlockData ( 1240, 580, TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre3 ] ) );
			for ( int i = 0; i < 4; i++ )
			{
				_blocks.Add ( new BlockData ( 640 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde1 ] ) ); 
				_blocks.Add ( new BlockData ( 660 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde2 ] ) ); 
				_blocks.Add ( new BlockData ( 680 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde3 ] ) ); 
				_blocks.Add ( new BlockData ( 700 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde4 ] ) ); 
			}
			_blocks.Add ( new BlockData ( 960, 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde1 ] ) ); 
			_blocks.Add ( new BlockData ( 980, 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde2 ] ) ); 
			_blocks.Add ( new BlockData ( 1080, 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde3 ] ) ); 
			_blocks.Add ( new BlockData ( 1100, 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde4 ] ) ); 
			for ( int i = 0; i < 2; i++ )
			{
				_blocks.Add ( new BlockData ( 1120 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde1 ] ) ); 
				_blocks.Add ( new BlockData ( 1140 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde2 ] ) ); 
				_blocks.Add ( new BlockData ( 1160 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde3 ] ) ); 
				_blocks.Add ( new BlockData ( 1180 + ( i * 80 ), 620, TextureManager.Figures [ FigureEnum.PiedrillaVerde4 ] ) ); 
			}
			
			#endregion
			
			#region Pantalla 4
			
			_blocks.Add ( new BlockData ( 640, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde1 ] ) ); 
			_blocks.Add ( new BlockData ( 680, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde2 ] ) ); 
			_blocks.Add ( new BlockData ( 720, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde3 ] ) ); 
			_blocks.Add ( new BlockData ( 760, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde4 ] ) ); 
			_blocks.Add ( new BlockData ( 800, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde5 ] ) ); 
			_blocks.Add ( new BlockData ( 840, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde6 ] ) ); 
			_blocks.Add ( new BlockData ( 880, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde7 ] ) ); 
			_blocks.Add ( new BlockData ( 920, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde8 ] ) ); 
			_blocks.Add ( new BlockData ( 960, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde9 ] ) ); 
			_blocks.Add ( new BlockData ( 1080, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde5 ] ) ); 
			_blocks.Add ( new BlockData ( 1120, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde10 ] ) ); 
			_blocks.Add ( new BlockData ( 1160, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde2 ] ) ); 
			_blocks.Add ( new BlockData ( 1200, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde1 ] ) ); 
			_blocks.Add ( new BlockData ( 1240, 640, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde7 ] ) ); 
			_blocks.Add ( new BlockData ( 640, 680, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde9 ] ) ); 
			_blocks.Add ( new BlockData ( 1200, 680, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde4 ] ) ); 
			_blocks.Add ( new BlockData ( 1240, 680, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde5 ] ) ); 
			_blocks.Add ( new BlockData ( 640, 720, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde3 ] ) ); 
			_blocks.Add ( new BlockData ( 1200, 720, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde8 ] ) ); 
			_blocks.Add ( new BlockData ( 1240, 720, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde6 ] ) ); 
			_blocks.Add ( new BlockData ( 640, 760, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde1 ] ) ); 
			_blocks.Add ( new BlockData ( 1200, 760, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde10 ] ) ); 
			_blocks.Add ( new BlockData ( 1240, 760, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde2 ] ) ); 
			_blocks.Add ( new BlockData ( 640, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde10 ] ) ); 
			_blocks.Add ( new BlockData ( 1200, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde9 ] ) ); 
			_blocks.Add ( new BlockData ( 1240, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde4 ] ) ); 
			_blocks.Add ( new BlockData ( 640, 840, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde6 ] ) ); 
			_blocks.Add ( new BlockData ( 640, 880, TextureManager.Figures [ FigureEnum.PiedraCuadradaVerde9 ] ) ); 

			_blocks.Add ( new BlockData ( 680, 780, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara1 ] ) ); 
			_blocks.Add ( new BlockData ( 720, 780, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara3 ] ) ); 
			_blocks.Add ( new BlockData ( 760, 780, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara5 ] ) ); 
			_blocks.Add ( new BlockData ( 800, 780, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara2 ] ) ); 
			_blocks.Add ( new BlockData ( 840, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara7 ] ) ); 
			_blocks.Add ( new BlockData ( 880, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara9 ] ) ); 
			_blocks.Add ( new BlockData ( 980, 840, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara3 ] ) ); 
			_blocks.Add ( new BlockData ( 1020, 840, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara1 ] ) ); 
			_blocks.Add ( new BlockData ( 1060, 840, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara10 ] ) ); 
			_blocks.Add ( new BlockData ( 1040, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara7 ] ) ); 
			_blocks.Add ( new BlockData ( 1080, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara2 ] ) ); 
			_blocks.Add ( new BlockData ( 1120, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara4 ] ) ); 
			_blocks.Add ( new BlockData ( 1160, 800, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara6 ] ) ); 

			_blocks.Add ( new BlockData ( 1020, 720, TextureManager.Figures [ FigureEnum.PiedraCuadradaClara1 ] ) ); 
			_blocks.Add ( new BlockData ( 1120, 680, TextureManager.Figures [ FigureEnum.SpiderWeb ], false ) ); 
			_blocks.Add ( new BlockData ( 1200, 840, TextureManager.Figures [ FigureEnum.PiedraJeroglifico1 ] ) ); 
			
			_blocks.Add ( new BlockData ( 640, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul6 ] ) ); 
			_blocks.Add ( new BlockData ( 680, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul9 ] ) ); 
			_blocks.Add ( new BlockData ( 720, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul5 ] ) ); 
			_blocks.Add ( new BlockData ( 760, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul2 ] ) ); 
			_blocks.Add ( new BlockData ( 800, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul8 ] ) ); 
			_blocks.Add ( new BlockData ( 840, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul6 ] ) ); 
			_blocks.Add ( new BlockData ( 880, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul4 ] ) ); 
			_blocks.Add ( new BlockData ( 920, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul10 ] ) ); 
			_blocks.Add ( new BlockData ( 960, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul3 ] ) ); 
			_blocks.Add ( new BlockData ( 1000, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul7 ] ) ); 
			_blocks.Add ( new BlockData ( 1040, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul5 ] ) ); 
			_blocks.Add ( new BlockData ( 1080, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul1 ] ) ); 
			_blocks.Add ( new BlockData ( 1120, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul6 ] ) ); 
			_blocks.Add ( new BlockData ( 1160, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul2 ] ) ); 
			_blocks.Add ( new BlockData ( 1200, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul4 ] ) ); 
			_blocks.Add ( new BlockData ( 1240, 920, TextureManager.Figures [ FigureEnum.PiedraCuadradaAzul3 ] ) ); 
			
			_blocks.Add ( new BlockData ( 680, 680,  TextureManager.Figures [ FigureEnum.PiedraCuadradaOcre11 ] ) );

			#endregion
			
			// -- Ahora se calcula, a partir de los bloques anteriores, el mapa de muros
			_wallMap = new bool [ (int) _levelDimensions.Y / 20, (int) _levelDimensions.X / 20 ];
			for(int i = 0; i < (int) _levelDimensions.Y / 20; i++ )
				for ( int j = 0; j < (int) _levelDimensions.X / 20; j++ )
					_wallMap [i,j] = false;
			foreach ( BlockData bd in _blocks )
				if ( bd.Bloquea ) 
				{
					int init_i = (int) bd.RectangleInWorld.Y / 20,
						init_j = (int) bd.RectangleInWorld.X / 20,
						end_i = (int) ( bd.RectangleInWorld.Y + bd.RectangleInWorld.Height ) / 20,
						end_j = (int) ( bd.RectangleInWorld.X + bd.RectangleInWorld.Width ) / 20; 
					
					if ( bd.RectangleInWorld.Height % 20 > 0 ) end_i++;
					if ( bd.RectangleInWorld.Width % 20 > 0 ) end_j++;

					for ( int i = init_i; i < end_i; i++ )
						for ( int j = init_j; j < end_j; j++ )
							_wallMap [i,j] = true;
				}
			
			// -- ya tenemos nuestro universo inicializado
					
		}
		#endregion
		
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch sb = (SpriteBatch) Game.Services.GetService ( typeof ( SpriteBatch ) );
			Rectangle currentView = _currentCamera.CurrentViewPort;
			
			foreach ( BlockData bd in _blocks )
			{
				if ( currentView.Intersects ( bd.RectangleInWorld ) )
				{
					// se comprueba si entra dentro del recuadro a pintar			
					float newX = bd.Position.X - currentView.X;
					float newY = bd.Position.Y - currentView.Y;
					
//					Nullable<Rectangle> sourceRect = bd.FigureData.Rect;
					// se comprueba si se sale por la parte inferior para reducir el alto del rectangulo de origen
//					if ( bd.RectangleInWorld.Y + bd.RectangleInWorld.Height > currentView.Y + currentView.Height )
//						sourceRect = new Rectangle ( bd.FigureData.Rect.X, bd.FigureData.Rect.Y, bd.FigureData.Rect.Width, 
//								bd.FigureData.Rect.Height - ( bd.RectangleInWorld.Y + bd.RectangleInWorld.Height - currentView.Height ) );

					sb.Draw (
						bd.FigureData.Container,
						new Vector2 ( newX, newY ),
						// sourceRect,
						bd.FigureData.Rect,
						Color.White,
						0.0f,
						Vector2.Zero,
						1.0f,
						SpriteEffects.None,
						bd.FigureData.Depth );
				}
			}
			
#if SHOW_WALLS
			int ini_i = currentView.Y / 20; if ( ini_i > 0 ) ini_i--;
			int ini_j = currentView.X / 20; if ( ini_j > 0 ) ini_j--;
			int fin_i = ( currentView.Y + currentView.Height ) / 20;
			int fin_j = ( currentView.X + currentView.Width ) / 20;
			FigureData marca = TextureManager.Figures [ FigureEnum.MarcaBloquePared ];
			for ( int i = ini_i; i <= fin_i; i++ )
				for ( int j = ini_j; j <= fin_j; j++ )
					if ( _wallMap [i,j] )
						sb.Draw ( 
						         marca.Container,
						         new Vector2 ( j * 20 - currentView.X, i * 20 - currentView.Y ),
						         marca.Rect,
						         Color.Yellow,
						         0.0f,
						         Vector2.Zero,
						         1.0f,
						         SpriteEffects.None,
						         marca.Depth );
#endif
			
			base.Draw (gameTime);
		}
		
		/// <summary>
		/// Devuelve las coordenadas de la pantalla.
		/// </summary>
		/// <returns>
		/// The screen coords.
		/// </returns>
		/// <param name='myUniverseCoords'>
		/// My universe coords.
		/// </param>
		public Vector2 GetScreenCoords ( Vector2 myUniverseCoords )
		{
			return new Vector2 ( 
				myUniverseCoords.X - _currentCamera.CurrentViewPort.X, 
				myUniverseCoords.Y - _currentCamera.CurrentViewPort.Y );
		}
		
		/// <summary>
		/// Sirve para que el sprite principal pregunte al "universo" si tiene que caer (o seguir cayendo)
		/// </summary>
		/// <returns>
		/// The to fall.
		/// </returns>
		/// <param name='viewPosition'>
		/// If set to <c>true</c> view position.
		/// </param>
		public bool HaveToFall ( Rectangle viewPosition )
		{

			// ahora hay que controlar que haya suelo sobre el que pisar
			int ini_j = viewPosition.X / 20;
			int fin_j = ini_j + viewPosition.Width / 20 + ( viewPosition.X % 20 > 0 ? 1 : 0 );
			int i = ( viewPosition.Y + viewPosition.Height ) / 20;
			bool fall = true;
			for ( int j = ini_j; j < fin_j; j++ )
				fall &= ! _wallMap [i,j];
		
			return fall;
		}
		
		/// <summary>
		/// Controla el movimiento relativo del personaje y devuelve cuanto debe moverse en relacion al universo y a la pantalla
		/// </summary>
		public Vector2 WantToMoveTo ( Rectangle viewPosition, Vector2 deltaMove )
		{


			Vector2 posicionPrevistaVertical = new Vector2 ( viewPosition.X, viewPosition.Y + deltaMove.Y );
			Vector2 posicionPrevistaHorizontal = new Vector2 ( viewPosition.X + deltaMove.X, viewPosition.Y );

			int ini_i = (int) posicionPrevistaVertical.Y / 20;
			int fin_i = ( (int) posicionPrevistaVertical.Y + viewPosition.Height ) / 20;
			int ini_j = (int) posicionPrevistaVertical.X / 20;
			int fin_j = ( (int) posicionPrevistaVertical.X + viewPosition.Width ) / 20;

			// el movimiento vertical
			if ( deltaMove.Y != 0 )
			{
				if ( posicionPrevistaVertical.X % 20 > 0 ) fin_j = fin_j + 1;
				
				// nos estamos moviendo para arriba
				if ( deltaMove.Y < 0 ) 
				{
					bool pared = false;
					for ( int j = ini_j; j < fin_j; j++ )
						pared |= _wallMap [ ini_i, j ];
					if ( pared )
					{
						int y_definitivo = viewPosition.Y;
						if ( y_definitivo % 20 > 0 )
							y_definitivo = ( y_definitivo / 20 ) * 20;
						posicionPrevistaVertical.Y = y_definitivo;
					}
				}
				else
				{
					bool pared = false;
					for ( int j = ini_j; j < fin_j; j++ )
						pared |= _wallMap [ fin_i, j ];
					if ( pared )
					{
						int y_definitivo = viewPosition.Y;
						if ( y_definitivo % 20 > 0 )
							y_definitivo = ( ( y_definitivo / 20 ) + 1 ) * 20;
						posicionPrevistaVertical.Y = y_definitivo;
					}
				}
			}
			
			posicionPrevistaHorizontal.Y = posicionPrevistaVertical.Y;
			ini_i = (int) posicionPrevistaHorizontal.Y / 20;
			fin_i = ( (int) posicionPrevistaHorizontal.Y + viewPosition.Height ) / 20;
			ini_j = (int) posicionPrevistaHorizontal.X / 20;
			fin_j = ( (int) posicionPrevistaHorizontal.X + viewPosition.Width ) / 20;			
			
			// se comprueba primero la componente horizontal
			if ( deltaMove.X != 0 )
			{
				fin_i = fin_i + ( posicionPrevistaHorizontal.Y % 20 > 0 ? 1 : 0 );
				// hacia la izquierda
				if ( deltaMove.X < 0 )
				{
					// si existe alguna pared, entonces no se permite avanzar del todo
					bool pared = false;
					for ( int i = ini_i; i < fin_i; i++ )
						pared |= _wallMap [i, ini_j];
					if ( pared )
					{
						int x_definitivo = viewPosition.X;
						if ( x_definitivo % 20 > 0 ) // aun se puede mover un poco
							x_definitivo = (x_definitivo / 20) * 20;
						posicionPrevistaHorizontal.X = x_definitivo;
					}
				}
				else
				{
					// si existe alguna pared, entonces no se permite avanzar del todo
					bool pared = false;
					for ( int i = ini_i; i < fin_i; i++ )
						pared |= _wallMap [i, fin_j];
					if ( pared )
					{
						int x_definitivo = viewPosition.X;
						if ( x_definitivo % 20 > 0 ) // aun se puede mover un poco
							x_definitivo = ( (x_definitivo / 20) + 1 ) * 20;
						posicionPrevistaHorizontal.X = x_definitivo;
					}
				}
			}
			
			return new Vector2 ( posicionPrevistaHorizontal.X, posicionPrevistaVertical.Y );
		}
		
		public Camera CurrentCamera
		{
			get { return _currentCamera; }
			set { _currentCamera = value; }
		}
		
	}
}

