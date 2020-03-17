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
    public static readonly string LASER_GEYSER_BOX = "LaserGeyserBox";
    public static readonly string LASER_GEYSER = "LaserGeyser";
    
    public static readonly string TROJAN_HORSE = "TrojanHorse";
    public static readonly string PLAYER_BALL = "PlayerBall";

    public static readonly string BOMB = "Bomb";
    public static readonly string BOMB_BOX = "BombBox";

}

// Tags for player attacks
public struct HitboxPool
{
    public static readonly string TESTBOX = "Testbox";
}
