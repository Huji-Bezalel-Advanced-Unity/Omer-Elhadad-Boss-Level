using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement State")]
    public float movementSpeed = 10f;
    
    [Header("Jump State")]
    public float jumpSpeed = 5f;
    
    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Dash State")]
    public float dashSpeed = 15f;
    public float dashDuration = 3f;
    public float dashCooldown = 1f; // Time before the player can dash again
}
