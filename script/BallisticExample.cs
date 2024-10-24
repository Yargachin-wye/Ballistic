using UnityEngine;

namespace Ballistic.script
{
    public class BallisticExample : MonoBehaviour
    {
        [SerializeField] private BallisticData ballistic;
        private void FixedUpdate()
        {
            BallisticData.OnBallisticUpdate(ballistic, Time.fixedDeltaTime);
        }
    }
}