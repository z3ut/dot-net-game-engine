using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Shell
{
	public class FireEvent
	{
		public float Direction { get; set; }

		public FireEvent(float direction)
		{
			Direction = direction;
		}
	}
}
