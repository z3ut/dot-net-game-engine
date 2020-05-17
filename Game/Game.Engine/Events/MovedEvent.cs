using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Events
{
	public class MovedEvent
	{
		public float NewX { get; set; }
		public float NewY { get; set; }

		public MovedEvent(float newX, float newY)
		{
			NewX = newX;
			NewY = newY;
		}
	}
}
