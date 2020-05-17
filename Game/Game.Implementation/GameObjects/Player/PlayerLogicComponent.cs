using Game.Engine.Commands;
using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Player.Events;
using Game.Implementation.GameObjects.Shared.Commands;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Player
{
	public class PlayerLogicComponent : IGameComponent
	{
		private readonly IGameCommand _fireCommand;
		private readonly IConsoleWriter _consoleWriter;
		private readonly IGeometryMathService _geometryMathService;

		public PlayerLogicComponent(IGameCommand fireCommand,
			IConsoleWriter consoleWriter, IGeometryMathService geometryMathService)
		{
			_fireCommand = fireCommand;
			_consoleWriter = consoleWriter;
			_geometryMathService = geometryMathService;
		}

		public object Clone()
		{
			return new PlayerLogicComponent(_fireCommand,
				_consoleWriter, _geometryMathService);
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{
		}

		public void HandleEvent(IGameObject gameObject,
			IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is FireEvent)
			{
				var fireEvent = gameEvent as FireEvent;

				var (fireX, fireY) = _geometryMathService
					.PointOnBoundary(gameObject, fireEvent.Direction);

				var fireCommandOptions =
					new FireCommandOptions(fireX, fireY, fireEvent.Direction);
				_fireCommand.Do(gameObject, gameWorld, fireCommandOptions);
			}

			if (gameEvent is ShellHitEvent)
			{
				gameWorld.SendEvent(new PlayerKilled());
			}

			if (gameEvent is MovedEvent)
			{
				var movedEvent = gameEvent as MovedEvent;
				_consoleWriter.SetCameraCenter(movedEvent.NewX, movedEvent.NewY);
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
