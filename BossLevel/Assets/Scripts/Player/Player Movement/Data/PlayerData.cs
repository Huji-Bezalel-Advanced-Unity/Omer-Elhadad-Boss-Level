using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement State")]
    public float movementSpeed = 10f;
    
    [Header("Jump State")]
    public float jumpSpeed = 5f;
    public float baseGravityScale = 7f;
    
    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Dash State")]
    public float dashSpeed = 15f;
    public float dashCooldown = 1f; // Time before the player can dash again
    
    [Header("In Air State")]
    public float inAirGravityScale = 13; // Gravity scale when the player is in the air
}
