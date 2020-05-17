using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Shared.Intersections
{
	public interface ILineIntersectionResolver
	{
		bool IsIntersect(float rectLeftX, float rectBotY, float rectRightX, float rectTopY,
			float lineX1, float lineY1, float lineX2, float lineY2);
	}
}
