# 2D Projectile Moving

## Библиотека содержит в себе классы для управления движением проджектайлов в D2 пространстве

## BallisticData
<details>
<summary>BallisticData</summary>

### Изменяет Позицию и размер эмулируя полёт гранаты, с помощью AnimationCurve

### Демо:

![img.gif](img3.gif)

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

</details>

## AutoAimData
<details>
<summary>AutoAimData</summary>

### Изменяет позицию и поворот для автонаведения на таргет, если проджектайл находится в близи таргета.

### Пример:
```csharp
private void FixedUpdate()
{
    Vector2 newPos = obj.transform.position;
    Quaternion newRotation = obj.transform.rotation;
    
    AutoAimData.Update(autoAimData, Time.fixedDeltaTime, ref newPos, ref newRotation);

    obj.transform.position = newPos;
    obj.transform.rotation = newRotation;
}
```

</details>