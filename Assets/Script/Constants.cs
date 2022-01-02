using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public const int TRAP_DMG = 10;
    public const int FIRST_ATTK_DMG = 10;
    public const int BULLET_VELOCITY = 1;
    public const int BOMB_TIMING = 3;
    public const int BOMB_DISTANCE_EXPLOSION = 5;
    public const int BOMB_DMG_EXPLOSION = 10;
    public const int ELECTRIC_BULLET_AREA_EFFECT = 8;

    //LAYER
    public const int PLAYER_LAYER = 3;
    public const int ENEMY_LAYER = 9;
    public const int FOOD_LAYER = 7;
    public const int OBSTACLE_LAYER = 6;
    public const int TRAP_LAYER = 8;
    public const int PLAYER_BULLET_LAYER = 11;
    public const int ENEMY_BULLET_LAYER = 10;

    //UPGRADE STATS
    public const float LOW_FOV = +10;
    public const float MID_FOV = +30;
    public const float HIGH_FOV = +50;
    public const float LOW_LOOKAHEAD = +1;
    public const float MID_LOOKAHEAD = +2;
    public const float HIGH_LOOKAHEAD = +3;
    public const float LOW_SPEED = +2;
    public const float MID_SPEED = +5;
    public const float HIGH_SPEED = +10;
    public const float LOW_MOV_PRECISION = -1;
    public const float MID_MOV_PRECISION = -3;
    public const float HIGH_MOV_PRECISION = -5;
    public const float LOW_PROTECTION = 0.8f;
    public const float MID_PROTECTION = 0.6f;
    public const float HIGH_PROTECTION = 0.4f;
    public const float LOW_DMG = +5;
    public const float HIGH_DMG = +15;
    public const float LOW_RATE = +2;
    public const float HIGH_RATE = +5;
}
