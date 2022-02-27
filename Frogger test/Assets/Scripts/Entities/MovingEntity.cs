using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : MonoBehaviour, SpawnedObject
{
    Rigidbody2D rb;
    [SerializeField] float speed;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
    }

    public abstract void InitializeObject();
}
