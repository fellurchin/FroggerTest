using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MovingEntity, SpawnedObject
{
    const float baseWidth = 0.35f;
    const float baseHeight = 0.65f;
    [Min(2)] public int maxSize;

    public override void InitializeObject()
    {
        float logSize = baseHeight + baseHeight * Random.Range(1, maxSize);
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(baseWidth, logSize);
    }
}
