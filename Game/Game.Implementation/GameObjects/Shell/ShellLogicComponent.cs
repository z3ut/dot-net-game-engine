using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Shell
{
	public class ShellLogicComponent : IGameComponent
	{
		public object Clone()
		{
			return new ShellLogicComponent();
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{
		}

		public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is IntersectionEvent)
			{
				var intersectonEvent = gameEvent as IntersectionEvent;

				var intersectionObject = intersectonEvent.IntersectionObject;

				intersectionObject.HandleEvent(gameWorld, new ShellHitEvent());

				gameWorld.Destroy(gameObject);
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
