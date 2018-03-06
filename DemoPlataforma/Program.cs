
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endregion

namespace DemoPlataforma
{
	[Register("AppDelegate")]
	class Program : MonoGameProgram
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Fun begins..
			MonoGameGame = new DemoPlataformaGame ();
			MonoGameGame.Run ();
			
			return true;
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

