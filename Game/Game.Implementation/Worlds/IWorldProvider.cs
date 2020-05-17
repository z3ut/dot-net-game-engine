using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Worlds
{
	public interface IWorldProvider
	{
		IEnumerable<WorldDescription> GetWorldDescriptions();
		IGameWorld GetWorld(int worldDescriptionId);
	}
}
