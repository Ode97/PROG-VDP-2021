using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    //LAYER
    public const int PLAYER_LAYER = 3;
    public const int ENEMY_LAYER = 9;
    public const int FOOD_LAYER = 7;
    public const int OBSTACLE_LAYER = 6;
    public const int VIEW_LAYER = 8;
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
    public const float LOW_PROTECTION = -3;
    public const float MID_PROTECTION = -10;
    public const float HIGH_PROTECTION = -20;
    public const float LOW_DMG = +5;
    public const float HIGH_DMG = +15;
    public const float LOW_RATE = +2;
    public const float HIGH_RATE = +5;
}
