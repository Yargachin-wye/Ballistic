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
        /// Общая продолжительность движения.
        /// </summary>
        public float duration;

        /// <summary>
        /// Прошедшее время с начала движения.
        /// </summary>
        [HideInInspector] public float elapsedTime;

        /// <summary>
        /// Кривая анимации, определяющая траекторию движения по оси Y.
        /// </summary>
        public AnimationCurve movementCurve;

        [HideInInspector] public float scaleFactor;

        /// <summary>
        /// Обязательная инициализация.
        /// </summary>
        public void Init()
        {
            elapsedTime = 0;
            
            Vector2 direction = endPos - startPos;
            float x = Mathf.Abs(direction.x);
            float y = Mathf.Abs(direction.y);
            if (x + y != 0) scaleFactor = y / (x + y);
            else scaleFactor = 1;
        }

        /// <summary>
        /// Обновляет позицию и масштаб объекта на основе баллистической траектории в локальных координатах.
        /// </summary>
        /// <param name="ballistic">Данные баллистической траектории.</param>
        /// <param name="deltaTime">Время, прошедшее с последнего обновления.</param>
        /// <param name="refPosition">Обновляемая позиция.</param>
        /// <param name="refLocalScale">Обновляемый Scale.</param>
        public static void UpdatePosition(
            BallisticData ballistic,
            float deltaTime,
            ref Vector3 refPosition,
            ref Vector3 refLocalScale)
        {
            float t = ballistic.elapsedTime / ballistic.duration;
            if (t > 1) return;
            Vector2 currentPosition = Vector2.Lerp(ballistic.startPos, ballistic.endPos, t);
            currentPosition.y += ballistic.movementCurve.Evaluate(t) * 2;

            float scaleFactor = ballistic.movementCurve.Evaluate(t);
            ballistic.elapsedTime += deltaTime;

            refPosition = currentPosition;
            refLocalScale = ballistic.startLocalScale + new Vector2(scaleFactor, scaleFactor) * ballistic.scaleFactor;
        }
    }
}