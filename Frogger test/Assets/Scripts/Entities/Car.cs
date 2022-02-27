using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MovingEntity, SpawnedObject
{
    public override void InitializeObject()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1);
    }
}