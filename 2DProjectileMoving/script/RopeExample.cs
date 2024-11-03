using System;
using System.Collections.Generic;
using Ballistic._2DProjectileMoving.script;
using Unity.VisualScripting;
using UnityEngine;

namespace TestScripts
{
    public class RopeExample : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform target;
        [SerializeField] private Transform projectile;
        [SerializeField] private float speed;
        [Range(0.01f, 4)] [SerializeField] private float startWaveSize = 2;
        [SerializeField] [Range(1, 50)] private float wavesProgressionSpeed = 2;

        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private LineRenderer lineRend;

        [SerializeField] private RopeData rope;

        private float _waveSize = 0;
        private List<Vector2> rope2DPhisics;
        private float elapsedTime = 0f;
        [SerializeField] private float lerpDuration = 1f; // Время, за которое точки должны достичь целевых позиций

        public int segmentsPerInterval = 100;

        private void Start()
        {
            _waveSize = startWaveSize;
            rope.StartBetween2Positions(start.position, target.position);
            rope2DPhisics = rope.UpdateRopePhysics();
            float totalLength = CalculateCurveLength();
            rope.segmentLength = totalLength / rope2DPhisics.Count;
            Debug.Log("Длина линии AnimationCurve: " + totalLength);
        }

        float CalculateCurveLength()
        {
            float length = 0f;
            Vector2 endP = target.position;
            Vector2 startP = start.position;
            Vector2 startToEnd = endP - startP;
            Vector2 last = Vector2.zero;
            for (int i = 0, j = rope2DPhisics.Count - 1; i < rope2DPhisics.Count; i++, j--)
            {
                float delta = i / (rope2DPhisics.Count - 1f);
                float waveEval = animationCurve.Evaluate(delta);
                Vector2 offset = startToEnd.normalized * waveEval * _waveSize;
                if (i == 0)
                {
                    last = Vector2.Lerp(endP, startP, delta) + offset;
                }
                else
                {
                    Vector2 targetPosition = Vector2.Lerp(endP, startP, delta) + offset;
                    length += Vector2.Distance(last, targetPosition);
                    last = targetPosition;
                }
            }

            return length;
        }

        private void FixedUpdate()
        {
            _waveSize = Mathf.Max(0, _waveSize - Time.fixedDeltaTime * wavesProgressionSpeed);
            rope.SetFirstSectionPos(start.position);
            rope.SetLastSectionPos(target.position);
            rope2DPhisics = rope.UpdateRopePhysics();
            DrawRopeWaves();
            elapsedTime += Time.fixedDeltaTime;
        }

        private void DrawRopeWaves()
        {
            Vector2 endP = target.position;
            Vector2 startP = start.position;
            Vector2 startToEnd = endP - startP;
            lineRend.positionCount = rope2DPhisics.Count;
            for (int i = 0, j = rope2DPhisics.Count - 1; i < rope2DPhisics.Count; i++, j--)
            {
                float delta = i / (lineRend.positionCount - 1f);
                float waveEval = animationCurve.Evaluate(delta);
                Vector2 offset = Vector2.Perpendicular(startToEnd).normalized * waveEval * _waveSize;
                Vector2 targetPosition = Vector2.Lerp(endP, startP, delta) + offset;

                Vector2 lerpedPosition = Vector2.Lerp(targetPosition, rope2DPhisics[j], elapsedTime / lerpDuration);

                lineRend.SetPosition(i, lerpedPosition);
            }
        }
    }
}