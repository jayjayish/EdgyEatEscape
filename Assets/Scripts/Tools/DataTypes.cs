using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tags for other characters and interactable objects
public struct Tags
{
    public static readonly string PLAYER = "Player";
    public static readonly string ENEMY = "Enemy";
    public static readonly string SOLID_OBSTACLE = "SolidObstacle";
    public static readonly string RESIDENT = "Resident";
}

public struct Pool
{
    public static readonly string HORIZONTAL_ENEMY_BULLET = "HorizontalEnemyBullet";
}

// Tags for player attacks
public struct HitboxPool
{
    public static readonly string TESTBOX = "Testbox";
}
