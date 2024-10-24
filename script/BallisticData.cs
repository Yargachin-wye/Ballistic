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
        /// Обновляет позицию и масштаб объекта на основе баллистической траектории в локальных координатах.
        /// </summary>
        /// <param name="ballistic">Данные баллистической траектории.</param>
        /// <param name="deltaTime">Время, прошедшее с последнего обновления.</param>
        /// <param name="refPosition">Обновляемя позиция.</param>
        /// <param name="refLocalScale">Обновляемый Scale.</param>
        public static void UpdateLocalPosition(
            BallisticData ballistic,
            float deltaTime,
            ref Vector3 refPosition,
            ref Vector3 refLocalScale)
        {
            float t = ballistic.elapsedTime / ballistic.duration;
            if (t > 1) return;
            Vector2 currentPosition = Vector2.Lerp(ballistic.startPos, ballistic.endPos, t);
            currentPosition.y += ballistic.movementCurve.Evaluate(t);
            float scaleFactor = ballistic.movementCurve.Evaluate(t);
            ballistic.elapsedTime += deltaTime;

            refPosition = currentPosition;
            refLocalScale = ballistic.startLocalScale + new Vector2(scaleFactor, scaleFactor);
        }
    }
}