using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Shell
{
	public class ShellInputComponent : IGameComponent
	{
		public object Clone()
		{
			return new ShellInputComponent();
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{
		}

		public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{
		}

		public void Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			gameObject.HandleEvent(gameWorld, new MoveEvent(gameObject.Azimuth));
		}
	}
}
