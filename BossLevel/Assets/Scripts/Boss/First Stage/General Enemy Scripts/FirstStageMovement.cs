using UnityEngine;

public class FirstStageMovement : MonoBehaviour
{
    
    public bool IsFlipped { get; private set; }
    
    // Flip the boss to face the player
    
    public void FlipToPlayer(Transform playerTransform)
    {
        var flipped = transform.localScale;
        flipped.z *= -1f;
        if (playerTransform.position.x < transform.position.x && !IsFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            IsFlipped = true;
        }
        else if (playerTransform.position.x > transform.position.x && IsFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            IsFlipped = false;
        }
    }
}
