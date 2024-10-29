using System;
using UnityEngine;

namespace Ballistic.script
{
    /// <summary>
    /// Класс предназначен для управления поведением объекта для автонаведения.
    /// </summary>
    [Serializable]
    public class AutoAimData
    {
        /// <summary>
        /// Позиция цели, к которой производится прицеливание.
        /// </summary>
        public Vector2 targetPosition; 
        /// <summary>
        /// Максимальное расстояние, на котором происходит автоматическое прицеливание.
        /// </summary>
        public float autoAimingDistance; 
        /// <summary>
        /// Скорость перемещения объекта к цели.
        /// </summary>
        public float speed;
        /// <summary>
        /// Скорость поворота объекта для наведения на цель.
        /// </summary>
        public float turningSpeed;
        
        /// <summary>
        /// Метод Update обновляет состояние объекта.
        /// </summary>
        /// <param name="data">Данные баллистической траектории.</param>
        /// <param name="deltaTime">Время, прошедшее с последнего обновления.</param>
        /// <param name="refPosition">Обновляемая позиция.</param>
        /// <param name="refRotation">Обновляемый поворота.</param>
        public static void Update(
            AutoAimData data,
            float deltaTime,
            ref Vector2 refPosition,
            ref Quaternion refRotation)
        {
            Vector2 current2DPosition = new Vector2(refPosition.x, refPosition.y);
            float distanceToTarget = Vector2.Distance(current2DPosition, data.targetPosition);

            if (distanceToTarget > data.autoAimingDistance)
            {
                refPosition += (Vector2)(refRotation * Vector2.up * data.speed * deltaTime);
            }
            else
            {
                Vector2 directionToTarget = (data.targetPosition - refPosition).normalized;
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                float proximityFactor = 1 - (distanceToTarget / data.autoAimingDistance);
                float dynamicTurningSpeed = data.turningSpeed * Mathf.Lerp(1, 5, proximityFactor);
                refRotation = Quaternion.RotateTowards(refRotation, targetRotation, dynamicTurningSpeed * deltaTime);
                refPosition += (Vector2)(refRotation * Vector2.up * data.speed * deltaTime);
            }
        }
    }
}