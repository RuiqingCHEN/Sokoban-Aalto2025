using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Detection Settings")]
    public LayerMask detectLayer;
    public Color endColor;
    Color originColor;
    GameManager gameManager;

    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
        gameManager = Object.FindFirstObjectByType<GameManager>();

    }
    public bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f, detectLayer);
        if(!hit || (hit.collider != null && hit.collider.CompareTag("Target")))
        {
            transform.Translate(dir);
            return true;
        }  
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            gameManager.finishedBoxs++;
            gameManager.CheckFinish();
            GetComponent<SpriteRenderer>().color = endColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Target"))
        {
            gameManager.finishedBoxs--;
            GetComponent<SpriteRenderer>().color = originColor;
        }
    }
}
