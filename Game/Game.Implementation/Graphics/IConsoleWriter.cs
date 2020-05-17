using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Graphics
{
	public interface IConsoleWriter
	{
		int Width { get; }
		int Height { get; }

		void BeginRender();
		void EndRender();

		void PrintChar(float x, float y, char character, int zIndex);
		void PrintChars(float leftX, float rightX, float bottomY, float topY,
			char character, int zIndex);
		void PrintText(float x, float y, string text, int zIndex);

		void PrintCharInPosition(int positionX, int positionY, char character, int zIndex);
		void PrintCharsInPosition(int leftPositionX, int rightPositionX,
			int bottomPositionY, int topPositionY, char character, int zIndex);
		void PrintTextInPosition(int positionX, int positionY, string text, int zIndex);
		void PrintTextInPositionCenter(int positionX, int positionY, string text, int zIndex);
		void PrintTextInPositionCenterInBox(int positionCenterX, int positionY,
			string text,char boxChar, int zIndex);

		void SetCameraCenter(float x, float y);
	}
}
