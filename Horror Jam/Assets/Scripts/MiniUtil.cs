using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BelowDeck.MiniUtil
{
    public static class MiniUtil
    {
        public static float DistanceNoY(Vector3 firstVector, Vector3 secondVector)
        {
            firstVector.y = 0;
            secondVector.y = 0;
            return Vector3.Distance(firstVector, secondVector);
        }
    }
}
