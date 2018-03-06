using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoPlataforma
{
	public class EnemiesManager : DrawableGameComponent
	{
		
		private Rectangle _currentRect = Rectangle.Empty;
		private List<EnemyBase> _enemies = new List<EnemyBase> ();
		private List<EnemyBase> _activeEnemies = new List<EnemyBase> ();
		private Level _universe;

		public EnemiesManager ( Game game, Level universe ) : base ( game )
		{
			_universe = universe;
			initializeEnemies ();
		}
		
		/// <summary>
		/// Crea un array con los enemigos que hay en el universo
		/// </summary>
		private void initializeEnemies ()
		{
			
			#region Pantalla 1
			_enemies.Add ( new EnemyGotaAcido ( 
				Game, 
				_universe,
				EnemyGotaAcido.GOTA_AZUL, 
				new Vector2 ( 300, 60 ), 
				140,
				new TimeSpan ( TimeSpan.TicksPerSecond / 3 ),
				new TimeSpan ( TimeSpan.TicksPerSecond / 5 ),
				new TimeSpan ( TimeSpan.TicksPerSecond / 30 ),
				new TimeSpan ( TimeSpan.TicksPerSecond / 12 ) ) );
			
			_enemies.Add ( new EnemyHorizontal (
				Game,
				_universe,
				EnemyHorizontal.TIPO_ALFOMBRA_AMARILLA,
				510,
				80,
				380,
				new TimeSpan ( TimeSpan.TicksPerSecond / 20 ) ) );
			
			_enemies.Add ( new EnemyVertical (
				Game,
				_universe,
				EnemyVertical.TIPO_CUADRADO_ROJO,
				560,
				220,
				90,
				new TimeSpan ( TimeSpan.TicksPerSecond / 15 ) ) );
			#endregion
			
			#region Pantalla 2
			_enemies.Add ( new EnemyHorizontal (
				Game,
				_universe,
				EnemyHorizontal.TIPO_ESTRELLA_AZUL,
				870,
				80,
				740,
				new TimeSpan( TimeSpan.TicksPerSecond / 50 ) ) );
			
			_enemies.Add ( new EnemyVertical ( 
				Game,
				_universe,
				EnemyVertical.TIPO_BARRAS_AMARILLAS,
				880,
				260,
				200,
				new TimeSpan ( TimeSpan.TicksPerSecond / 12 ) ) );
			
			_enemies.Add ( new EnemyHorizontal (
				Game,
				_universe,
				EnemyHorizontal.TIPO_SERPIENTE_VERDE,
				940,
				260,
				1090,
				new TimeSpan ( TimeSpan.TicksPerSecond / 25 ) ) );
			
			#endregion
			
			#region Pantalla 3
			_enemies.Add ( new EnemyEmpalamiento (
				Game,
				_universe,
				800,
				480 ) );
			
			_enemies.Add ( new EnemyHorizontal (
				Game,
				_universe,
				EnemyHorizontal.TIPO_TORNILLO_AZUL,
				1040,
				380,
				1080,
				new TimeSpan ( TimeSpan.TicksPerSecond / 40 ) ) );
			
			_enemies.Add ( new EnemyHorizontal (
				Game,
				_universe,
				EnemyHorizontal.TIPO_ARANA_PATAS_VERDES,
				920,
				420,
				1120,
				new TimeSpan ( TimeSpan.TicksPerSecond / 24 ) ) );
			
			_enemies.Add ( new EnemyHorizontal (
				Game,
				_universe,
				EnemyHorizontal.TIPO_MOMIA_GRIS,
				1080,
				580,
				1160,
				new TimeSpan ( TimeSpan.TicksPerSecond / 13 ) ) );
			
			#endregion 
			
			#region Pantalla 4
			_enemies.Add ( new EnemyGotaAcido ( 
				Game, 
				_universe,
				EnemyGotaAcido.GOTA_AMARILLA, 
				new Vector2 ( 800, 680 ), 
				740,
				new TimeSpan ( TimeSpan.TicksPerSecond / 30 ),
				new TimeSpan ( TimeSpan.TicksPerSecond / 6 ),
				new TimeSpan ( TimeSpan.TicksPerSecond / 40 ),
				new TimeSpan ( TimeSpan.TicksPerSecond / 18 ) ) );
			
			_enemies.Add ( new EnemyVertical (
				Game,
				_universe,
				EnemyVertical.TIPO_CUADRADO_ROJO,
				940,
				700,
				820,
				new TimeSpan ( TimeSpan.TicksPerSecond / 40 ) ) );
			

			#endregion 
		}
		
		/// <summary>
		/// Comprueba las colisiones contra los enemigos
		/// </summary>
		/// <returns>
		/// The colision.
		/// </returns>
		/// <param name='player'>
		/// If set to <c>true</c> player.
		/// </param>
		public bool CheckColision ( Sprite player )
		{
			int enemigosActivos = _activeEnemies.Count;
			for ( int i = 0; i < enemigosActivos; i++ )
				if ( _activeEnemies [ i ].CollisionRectangle.Intersects ( player.SensibleArea ) )
					return true;
			return false;
		}
		
		public override void Update ( GameTime gameTime )
		{
			// se comprueba si la camara ha cambiado de posicion 
			Rectangle nuevoRect = _universe.CurrentCamera.CurrentViewPort;
			if ( nuevoRect != _currentRect )
			{
				updateListaEnemigosActivos ( nuevoRect );
				_currentRect = nuevoRect;
			}
			
			foreach ( EnemyBase enemy in _activeEnemies )
				enemy.Update ( gameTime );
			
			base.Update ( gameTime );
		}
		
		public override void Draw (GameTime gameTime)
		{
			foreach ( EnemyBase enemy in _activeEnemies )
				enemy.Draw ( gameTime );
			
			base.Draw (gameTime);
		}
		
		private void updateListaEnemigosActivos ( Rectangle nuevoRect )
		{
			List<EnemyBase> nuevaLista = new List<EnemyBase> ();
			Rectangle checkRect = new Rectangle ( nuevoRect.X - 300, nuevoRect.Y - 200, nuevoRect.Width + 600, nuevoRect.Height + 400 );
			foreach ( EnemyBase enemy in _enemies )
				if ( enemy.ActionRectangle.Intersects ( checkRect ) )
					nuevaLista.Add ( enemy );
			_activeEnemies.Clear ();
			_activeEnemies = nuevaLista;
		}
		
	}
}

