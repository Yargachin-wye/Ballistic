# Ballistic System

## Moving Transform using AnimationCurve

### Example:

```csharp
using Ballistic;
using UnityEngine;

public class BallisticExample : MonoBehaviour
{
    [SerializeField] private BallisticData ballistic;

    private void FixedUpdate()
    {
        BallisticData.OnBallisticUpdate(ballistic, Time.fixedDeltaTime);
    }
}