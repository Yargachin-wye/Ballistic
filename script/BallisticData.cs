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
        /// Обновляет позицию и масштаб объекта на основе баллистической траектории в мировых координатах.
        /// </summary>
        /// <param name="ballistic">Данные баллистической траектории.</param>
        /// <param name="deltaTime">Время, прошедшее с последнего обновления.</param>
        public static void UpdateWorldPosition(BallisticData ballistic, float deltaTime)
        {
            float t = ballistic.elapsedTime / ballistic.duration;
            if (t > 1) return;

            Vector2 currentPosition = Vector2.Lerp(ballistic.startPos, ballistic.endPos, t);
            
            currentPosition.y += ballistic.movementCurve.Evaluate(t);

            ballistic.transform.position = currentPosition;

            float scaleFactor = ballistic.movementCurve.Evaluate(t);

            ballistic.transform.localScale =
                ballistic.startLocalScale + new Vector2(scaleFactor, scaleFactor);

            ballistic.elapsedTime += deltaTime;
            ballistic.transform.position = currentPosition;
        }
        /// <summary>
        /// Обновляет позицию и масштаб объекта на основе баллистической траектории в локальных координатах.
        /// </summary>
        /// <param name="ballistic">Данные баллистической траектории.</param>
        /// <param name="deltaTime">Время, прошедшее с последнего обновления.</param>
        public static void UpdateLocalPosition(BallisticData ballistic, float deltaTime)
        {
            float t = ballistic.elapsedTime / ballistic.duration;
            if (t > 1) return;

            Vector2 currentPosition = Vector2.Lerp(ballistic.startPos, ballistic.endPos, t);
            
            currentPosition.y += ballistic.movementCurve.Evaluate(t);

            ballistic.transform.localPosition = currentPosition;

            float scaleFactor = ballistic.movementCurve.Evaluate(t);

            ballistic.transform.localScale =
                ballistic.startLocalScale + new Vector2(scaleFactor, scaleFactor);

            ballistic.elapsedTime += deltaTime;
            ballistic.transform.localPosition = currentPosition;
        }
    }
}