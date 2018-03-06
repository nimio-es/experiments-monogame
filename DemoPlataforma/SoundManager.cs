using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace DemoPlataforma
{
	public static class SoundManager
	{
		
		private static SoundEffect musicaJuego;
		private static SoundEffectInstance musicaInstance = null;
		private static SoundEffect walkingEffect;
		private static SoundEffectInstance walkingInstance = null;
		private static SoundEffect fallingEffect;
		private static SoundEffectInstance fallingInstance = null;
		private static SoundEffect jumpingEffect;
		private static SoundEffectInstance jumpingInstance = null;
		private static SoundEffect gotaEffect;
		private static SoundEffect tesoroEffect;
		private static SoundEffect gritoEffect;
		
		public static void Initialize ( ContentManager content )
		{
			musicaJuego = content.Load<SoundEffect> ( "Sounds/musicajuego.mp3" );
			walkingEffect = content.Load<SoundEffect> ( "Sounds/camina.wav" );
			fallingEffect = content.Load<SoundEffect> ( "Sounds/caida.wav" );
			jumpingEffect = content.Load<SoundEffect> ( "Sounds/salto.wav" );
			gotaEffect = content.Load<SoundEffect> ( "Sounds/gota.wav" );
			tesoroEffect = content.Load<SoundEffect> ( "Sounds/tesoro.wav" );
			gritoEffect = content.Load<SoundEffect> ( "Sounds/grito.wav" );
		}
		
		public static void PlayMusica ()
		{
			musicaInstance = musicaJuego.CreateInstance ();
			musicaInstance.Volume = 0.2f;
			musicaInstance.IsLooped = true;
			musicaInstance.Play ();
		}
		
		public static bool IsPlayingMusic { get { return ( musicaInstance != null ); } }
		
		public static void PlayWalking ()
		{
			walkingInstance = walkingEffect.CreateInstance ();
			walkingInstance.Volume = 0.3f;
			walkingInstance.IsLooped = true;
			walkingInstance.Play (); 
		}
		
		public static void StopWalking ()
		{
			if ( walkingInstance != null )
			{
				walkingInstance.Stop ();
				walkingInstance.Dispose ();
				walkingInstance = null; 
			}
		}
		
		public static void PlayFalling ()
		{
			fallingInstance = fallingEffect.CreateInstance ();
			fallingInstance.Volume = 0.3f;
			fallingInstance.IsLooped = true;
			fallingInstance.Play ();
		}
		
		public static void StopFalling ()
		{
			if ( fallingInstance != null )
			{
				fallingInstance.Stop ();
				fallingInstance.Dispose ();
				fallingInstance = null;
			}
		}
		
		public static void PlayJumping ()
		{
			if ( jumpingInstance == null )
			{
				jumpingInstance = jumpingEffect.CreateInstance ();
				jumpingInstance.Volume = 0.3f;
				jumpingInstance.IsLooped = true;
				jumpingInstance.Play ();
			}
		}
		
		public static void StopJumping ()
		{
			if ( jumpingInstance != null )
			{
				jumpingInstance.Stop ();
				jumpingInstance.Dispose ();
				jumpingInstance = null;
			}
		}
		
		public static void PlayGota ()
		{
			gotaEffect.Play ( 0.3f, 0.0f, 0.0f );
		}
		
		public static void PlayTesoro ()
		{
			tesoroEffect.Play ( 0.3f, 0.0f, 0.0f );
		}
		
		public static void PlayGrito ()
		{
			gritoEffect.Play ( 0.3f, 0.0f, 0.0f );
		}
	}
}

