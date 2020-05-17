using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization.SerializedGameObjectData
{
	public class PlayerData
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public int Speed { get; set; }

		public char DisplayCharLeft { get; set; }
		public char DisplayCharTop { get; set; }
		public char DisplayCharRight { get; set; }
		public char DisplayCharBot { get; set; }
	}
}
