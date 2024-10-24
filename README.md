# Ballistic System

## Изменяет Transform имулируя полёт гранаты, с помощью AnimationCurve

### Пример:

```csharp
using System;
using Ballistic;
using UnityEngine;

public class BallisticExample : MonoBehaviour
{
    [SerializeField] private BallisticData ballistic;
    [SerializeField] private Transform transform;

    private void Start()
    {
        ballistic.Init();
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        BallisticData.UpdatePosition(ballistic, Time.fixedDeltaTime, ref pos, ref scale);

        transform.localPosition = pos;
        transform.localScale = scale;
    }
}
```
### Кривая должны выглядить примерно так:
![img.png](img.png)
### Параметры должны выглядить примерно так:
![img.png](img2.png)
### Демо:
![img.gif](img3.gif)