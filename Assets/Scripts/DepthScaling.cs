using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthScaling : MonoBehaviour
{
    public float sizeDepthFactor = 1;
    public float speedDepthFactor = 1;
    public float zenithY;

    Vector3 originalScale;

    PlayerMovementController player;
    Vector2 originalPlayerSpeed;

    void Start() 
    {
        originalScale = transform.localScale;
        player = GetComponent<PlayerMovementController>();
        originalPlayerSpeed = player.movementSpeed;
    }

    void Update()
    {
        float yDiff = zenithY - transform.position.y;
        transform.localScale = originalScale*yDiff*sizeDepthFactor;
        player.movementSpeed = originalPlayerSpeed*yDiff*speedDepthFactor;
    }
}
