using Game.Engine.Components;
using Game.Engine.Core;
using Game.Implementation.GameObjects.Player.Events;
using Game.Implementation.GameObjects.WinPlatform.Events;
using Game.Implementation.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.GameResult
{
	public class GameResultGraphicComponent : IGameComponent
	{
		private readonly IConsoleWriter _consoleWriter;
		private GameResult gameResult = GameResult.None;

		private enum GameResult
		{
			None,
			Win,
			Lose
		}

		public GameResultGraphicComponent(IConsoleWriter consoleWriter)
		{
			_consoleWriter = consoleWriter;
		}

		public object Clone()
		{
			return new GameResultGraphicComponent(_consoleWriter);
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is PlayerKilled)
			{
				gameResult = GameResult.Lose;
			}

			if (gameEvent is PlayerReachedWinPlatform)
			{
				gameResult = GameResult.Win;
			}
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			if (gameResult == GameResult.Lose)
			{
				PrintMessageInBox("You Lose!");
			}

			if (gameResult == GameResult.Win)
			{
				PrintMessageInBox("You Win!");
			}
		}

		private void PrintMessageInBox(string message)
		{
			_consoleWriter.PrintTextInPositionCenterInBox(_consoleWriter.Width / 2,
				_consoleWriter.Height / 2, message, ' ', 9999);
		}
	}
}
