using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using Game.Implementation.GameObjects.Player.Events;
using Game.Implementation.GameObjects.WinPlatform.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.GameResult
{
	public class GameResultLogicComponent : IGameComponent
	{
		public object Clone()
		{
			return new GameResultLogicComponent();
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is PlayerKilled)
			{
				gameWorld.SendEvent(new GameEnd());
			}

			if (gameEvent is PlayerReachedWinPlatform)
			{
				gameWorld.SendEvent(new GameEnd());
			}
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void Update(IGameObject gameObject, IGameWorld gameWorld)
		{

		}
	}
}
