using System;

using Microsoft.Xna.Framework;

namespace DemoPlataforma
{
	
	/// <summary>
	/// Se crea para controlar el movimiento de la camara siguiendo al personaje en la pantalla.
	/// Por defecto, la camara base es estatica y no tiene en cuenta la posicion del jugador, 
	/// unicamente recorre desde el punto actual hasta el punto de arranque y alli se queda
	/// </summary>
	public class Camera
	{
		
		// En coordenadas universales
		public readonly uint Id;
		private Vector2 _startPoint = Vector2.Zero, 
						_endPoint = Vector2.Zero, 
						_currentPosition = Vector2.Zero,
						_delta;
		private int _numSteps = 30;
		private int _currentStep = 0;
		
		public Camera ( uint id ) 
		{
			Id = id;
		}
		
		/// <summary>
		/// Modifica la posicion de la camara conforme se necesite
		/// </summary>
		public virtual void MoveCamera ( Level universe, Sprite player )
		{
			// Posiciona la camara en 10 pasos
			if ( _currentStep <= _numSteps )
			{
				if ( ++_currentStep >= _numSteps )
					_currentPosition = _endPoint;
				else 
					_currentPosition = _currentPosition + _delta;
			}
		}
		
		public virtual void TransitionFromCamera ( Camera oldCamera )
		{
			this.StartPoint = oldCamera.CurrentPosition;
		}
		
		public Vector2 StartPoint
		{ 
			get { return _startPoint; } 
			set 
			{ 
				_startPoint = value; 
				_currentPosition = value;
				_currentStep = 0;
				calculateDeltas ();
			} 
		}
		
		public Vector2 EndPoint 
		{
			get { return _endPoint; }
			set 
			{ 
				_endPoint = value;
				_currentPosition = _startPoint;
				_currentStep = 0;
				calculateDeltas ();
			}
		}
		
		public int NumSteps 
		{
			get { return _numSteps; }
			set 
			{ 
				_numSteps = value;
				calculateDeltas ();
			}
		}
		
		public Vector2 CurrentPosition
		{
			get { return _currentPosition; }
		}
		
		public Rectangle CurrentViewPort 
		{
			get { return new Rectangle ( (int) _currentPosition.X, (int) _currentPosition.Y, 480, 240 ); }
		}
		
		public void calculateDeltas ()
		{
			float deltaX = ( _endPoint.X - _startPoint.X ) / _numSteps;
			float deltaY = ( _endPoint.Y - _startPoint.Y ) / _numSteps;
			_delta = new Vector2 ( deltaX, deltaY );
		}
	}
}

