using UnityEngine;

public class FrogPlayer : MonoBehaviour, SpawnedObject
{
    const float moveUnit = 0.8f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 nextPosition = rb.position + direction * moveUnit;
        var obstacle = Physics2D.OverlapCircle(nextPosition,0.25f,LayerMask.GetMask("Obstacle"));
        var wall = Physics2D.OverlapCircle(nextPosition, 0.25f, LayerMask.GetMask("Wall"));
        var log = Physics2D.OverlapCircle(nextPosition, 0.25f, LayerMask.GetMask("Log"));

        if (wall != null) { return; }
        if(log != null){
            transform.SetParent(log.transform);}
        else {
            transform.SetParent(null);
        }

        if(obstacle!=null && log == null)
        {
            GameManager.gm.DeathEvent();
        }
        transform.position = nextPosition;
    }

    public void KillFrog()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 3;
        DisableFrog();
    }

    public void DisableFrog()
    {
        GameManager.gm.OnDeadEvent -= KillFrog;
        GameManager.gm.OnWinEvent -= DisableFrog;
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.SetParent(null);
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Car") { GameManager.gm.DeathEvent(); }
    }

    public void InitializeObject()
    {
        GameManager.gm.OnDeadEvent += KillFrog;
        GameManager.gm.OnWinEvent += DisableFrog;
    }
}
