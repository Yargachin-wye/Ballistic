using System;
using UnityEngine;

namespace Ballistic
{
    [Serializable]
    public class BallisticData
    {
        /// <summary>
        /// Начальная позиция объекта.
        /// </summary>
        public Vector2 startPos;

        /// <summary>
        /// Конечная позиция объекта.
        /// </summary>
        public Vector2 endPos;

        /// <summary>
        /// Трансформ объекта, который будет перемещаться.
        /// </summary>
        public Transform transform;

        /// <summary>
        /// Начальный локальный масштаб объекта.
        /// </summary>
        public Vector2 startLocalScale;

        /// <summary>
        /// Начальное расстояние по оси X между начальной и конечной позициями.
        /// </summary>
        public float initialDistanceX;

        /// <summary>
        /// Общая продолжительность движения.
        /// </summary>
        public float duration;

        /// <summary>
        /// Прошедшее время с начала движения.
        /// </summary>
        [HideInInspector] public float elapsedTime = 0;

        /// <summary>
        /// Кривая анимации, определяющая траекторию движения по оси Y.
        /// </summary>
        public AnimationCurve movementCurve;

        /// <summary>
        /// Обновляет позицию и масштаб объекта на основе баллистической траектории.
        /// </summary>
        /// <param name="ballistic">Данные баллистической траектории.</param>
        /// <param name="deltaTime">Время, прошедшее с последнего обновления.</param>
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