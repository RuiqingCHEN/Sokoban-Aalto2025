using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir;
    public LayerMask detectLayer;

    // Sound Effect
    private bool playingFootsteps = false;
    private bool isMoving = false;
    public float footstepSpeed = 0.5f;

    // 当前可交互的猫
    private Dino currentDino; 

    void Update()
    {
        if (PauseController.IsGamePaused || GameManager.HasWon)
        {
            StopFootsteps();
            return;
        }    
        isMoving = false;
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            moveDir = Vector2.right;
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            moveDir = Vector2.left;
        }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            moveDir = Vector2.up;
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            moveDir = Vector2.down;
        }

        if (moveDir != Vector2.zero)
        {
            if (CanMoveToDir(moveDir))
            {
                Move(moveDir);
                isMoving = true;
            }
        }

        if (isMoving && !playingFootsteps)
        {
            StartFootsteps();
        }
        else if (!isMoving && playingFootsteps)
        {
            StopFootsteps();
        }
        
        moveDir = Vector2.zero;
    }

    bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, detectLayer);

        if (!hit || (hit.collider != null && hit.collider.CompareTag("Target")))
            return true;
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
                return hit.collider.GetComponent<Box>().CanMoveToDir(dir);
            else if (hit.collider.GetComponent<Food>() != null)
                return hit.collider.GetComponent<Food>().CanMoveToDir(dir);
        }
        return false;
    }

    void Move(Vector2 dir)
    {
        StartFootsteps();
        transform.Translate(dir);
    }
    
    // Sound Effect
    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footstepSpeed);
    }
    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }
    void PlayFootstep()
    {
        SoundEffectManager.Play("Footstep", true);
    }
}
