using System;

namespace OwlGame.Entities.Data
{
    //Student Num: c3260058
    //Date: 04/10/18
    //Description: This counts the speed of Enemy Entity Movements in the comparison to The Player Class.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class EnemyMovementCoefficients
    {
        // This should be slightly slower than the player
        public const float MaxHorizontalSpeed = 100;
        public const float GravityAcceleration = -390;
        public const float JumpVelocity = 240;
        public const float MaxFallingSpeed = -160;
    }
}