using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization
{
	public class GameObjectSerialized
	{
		public string GameObjectType { get; set; }

		public float X { get; set; }
		public float Y { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }
		public float Azimuth { get; set; }
	}
}
