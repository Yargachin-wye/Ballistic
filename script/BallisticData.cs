using System;
using UnityEngine;

namespace Ballistic
{
    [Serializable]
    public class BallisticData
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public Transform transform;
        public Vector2 startLocalScale;
        public float initialDistanceX;
        public float duration;
        public float elapsedTime;
        public AnimationCurve movementCurve;

        public static void OnBallisticUpdate(BallisticData ballistic, float deltaTime)
        {
            float t = ballistic.elapsedTime / ballistic.duration;
            if (t > 1) return;

            Vector2 currentPosition = Vector2.Lerp(ballistic.startPos, ballistic.endPos, t);

            float yOffset = ballistic.movementCurve.Evaluate(t);
            currentPosition.y += yOffset;

            float currentDistanceX = Mathf.Abs(Mathf.Abs(ballistic.endPos.x) - Mathf.Abs(currentPosition.y));
            float influenceFactor = ballistic.initialDistanceX > 0
                ? currentDistanceX / ballistic.initialDistanceX
                : 0;

            currentPosition.y += yOffset;
            ballistic.transform.position = currentPosition;

            float scaleFactor = ballistic.movementCurve.Evaluate(t);

            ballistic.transform.localScale =
                ballistic.startLocalScale + new Vector2(scaleFactor, scaleFactor);

            ballistic.elapsedTime += deltaTime;
            ballistic.transform.position = currentPosition;
        }
    }
}