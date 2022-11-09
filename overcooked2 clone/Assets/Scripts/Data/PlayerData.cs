using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject //this governs all variables that needs quick access
{
    
    [Header("Move State")]
    public float movementVelocity = 10f;
}
