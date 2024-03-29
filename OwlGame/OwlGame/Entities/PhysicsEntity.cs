﻿using System;
using CocosSharp;


namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Date: 06/10/18
    //Description: This code is game physisc which determines the game entity sprite velocity and acceleratin.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class PhysicsEntity : AnimatedSpriteEntity
    {
        protected float VelocityX;
        protected float VelocityY;

        protected float AccelerationX;
        protected float AccelerationY;

        public PhysicsEntity()
        {
        }


        protected void ApplyMovementValues(float seconds)
        {
            float halfSecondsSquared = (seconds * seconds) / 2.0f;

            this.PositionX +=
                this.VelocityX * seconds + this.AccelerationX * halfSecondsSquared;
            this.PositionY +=
                this.VelocityY * seconds + this.AccelerationY * halfSecondsSquared;

            this.VelocityX += this.AccelerationX * seconds;
            this.VelocityY += this.AccelerationY * seconds;

        }

        protected void ProjectVelocityOnSurface(CCPoint reposition)
        {
            if (reposition.X != 0 || reposition.Y != 0)
            {
                var repositionNormalized = reposition;
                repositionNormalized.Normalize();

                CCPoint velocity = new CCPoint(VelocityX, VelocityY);

                var dot = CCPoint.Dot(velocity, repositionNormalized);
                // falling into the collision, rather than out of
                if (dot < 0)
                {
                    velocity -= repositionNormalized * dot;

                    VelocityX = velocity.X;
                    VelocityY = velocity.Y;
                }
            }
        }

    }
}
