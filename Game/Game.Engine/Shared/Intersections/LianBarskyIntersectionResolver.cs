using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Shared.Intersections
{
    // Liang–Barsky algorithm
    // https://en.wikipedia.org/wiki/Liang%E2%80%93Barsky_algorithm
    public class LianBarskyIntersectionResolver : ILineIntersectionResolver
	{
        public bool IsIntersect(float rectLeftX, float rectBotY,
            float rectRightX, float rectTopY,
            float lineX1, float lineY1, float lineX2, float lineY2)
        {
            float p1 = -(lineX2 - lineX1);
            float p2 = -p1;
            float p3 = -(lineY2 - lineY1);
            float p4 = -p3;

            float q1 = lineX1 - rectLeftX;
            float q2 = rectRightX - lineX1;
            float q3 = lineY1 - rectBotY;
            float q4 = rectTopY - lineY1;

            var posarr = new float[5];
            var negarr = new float[5];
            int posind = 1, negind = 1;
            posarr[0] = 1;
            negarr[0] = 0;

            if ((p1 == 0 && q1 < 0) || (p2 == 0 && q2 < 0) || (p3 == 0 && q3 < 0) || (p4 == 0 && q4 < 0))
            {
                // Line is parallel to clipping window
                return false;
            }
            if (p1 != 0)
            {
                float r1 = q1 / p1;
                float r2 = q2 / p2;
                if (p1 < 0)
                {
                    negarr[negind++] = r1; // for negative p1, add it to negative array
                    posarr[posind++] = r2; // and add p2 to positive array
                }
                else
                {
                    negarr[negind++] = r2;
                    posarr[posind++] = r1;
                }
            }
            if (p3 != 0)
            {
                float r3 = q3 / p3;
                float r4 = q4 / p4;
                if (p3 < 0)
                {
                    negarr[negind++] = r3;
                    posarr[posind++] = r4;
                }
                else
                {
                    negarr[negind++] = r4;
                    posarr[posind++] = r3;
                }
            }

            float rn1, rn2;

            rn1 = MaxFromFirstElements(negarr, negind); // maximum of negative array
            rn2 = MinFromFirstElements(posarr, posind); // minimum of positive array

            if (rn1 > rn2)
            {
                // Line is outside the clipping window
                return false;
            }

            return true;
        }

        // this function gives the maximum
        float MaxFromFirstElements(float[] arr, int n)
        {
            float m = 0;
            for (int i = 0; i < n; ++i)
                if (m < arr[i])
                    m = arr[i];
            return m;
        }

        // this function gives the minimum
        float MinFromFirstElements(float[] arr, int n)
        {
            float m = 1;
            for (int i = 0; i < n; ++i)
                if (m > arr[i])
                    m = arr[i];
            return m;
        }
    }
}
