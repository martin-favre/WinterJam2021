using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float initialAnimationFrequency;
    float animationFrequency;
    public Sprite[] sprites;
    int spriteIndex = 0;
    SpriteRenderer spriteRenderer;
    float lastSpriteReplaceTime;
    SimpleObserver<Vector2> movementObserver;
    PlayerMovementController playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
        movementObserver = new SimpleObserver<Vector2>(playerMovement, movement =>
        {
            Vector2 maxMovement = playerMovement.MaxPossibleMovement;
            float relativeMovement = movement.magnitude / maxMovement.magnitude;
            if (relativeMovement > 0.0001f)
            {
                animationFrequency = 1 / (initialAnimationFrequency * relativeMovement);
                if (Mathf.Sign(movement.x) != Mathf.Sign(transform.localScale.x))
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                animationFrequency = 10000;
            }


        });
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastSpriteReplaceTime = Time.time;
    }

    void Update()
    {
        if (lastSpriteReplaceTime + animationFrequency < Time.time)
        {
            spriteRenderer.sprite = GetNextSprite();
            lastSpriteReplaceTime = Time.time;
        }
    }

    private Sprite GetNextSprite()
    {
        if (spriteIndex >= sprites.Length) spriteIndex = 0;
        Sprite retval = sprites[spriteIndex];
        spriteIndex++;
        return retval;
    }
}
