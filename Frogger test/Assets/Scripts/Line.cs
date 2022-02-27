using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private bool hasBeenReached;
    [SerializeField] bool isWinLine;

    private void Start()
    {
        GameManager.gm.OnWinEvent += ResetState;
    }

    private void ResetState()
    {
        hasBeenReached = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasBeenReached && collision.tag == "Player")
        {
            hasBeenReached = true;
            GameManager.gm.Score += 10;
            if (isWinLine)
            {
                GameManager.gm.Score += 100;
                GameManager.gm.WinEvent();
            }
        }
    }

    private void OnDisable()
    {
        GameManager.gm.OnWinEvent -= ResetState;
    }
}
