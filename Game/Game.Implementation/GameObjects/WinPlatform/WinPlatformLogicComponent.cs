using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.WinPlatform.Events;
using Game.Implementation.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.WinPlatform
{
	public class WinPlatformLogicComponent : IGameComponent
	{
		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGeometryMathService _geometryMathService;

		public WinPlatformLogicComponent(IGameObjectLocator gameObjectLocator,
			IGeometryMathService geometryMathService)
		{
			_gameObjectLocator = gameObjectLocator;
			_geometryMathService = geometryMathService;
		}

		public object Clone()
		{
			return new WinPlatformLogicComponent(_gameObjectLocator, _geometryMathService);
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
			var player = _gameObjectLocator.GetPlayer();

			if (_geometryMathService.IsIntersects(gameObject.Boundary, player.Boundary))
			{
				gameWorld.SendEvent(new PlayerReachedWinPlatform());
			}
		}
	}
}
