using System;
using System.Linq;
using CocosSharp;
using OwlGame.Entities.Data;
using OwlGame.ContentRuntime.Animations;
using OwlGame.Entities;

namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 05/10/18
    //Description: This code is for the enemy class. This is an unique entity, which would deal damage to the owl when beeing touched.
    //Tutorial Used: Coin Time Demo in Microsoft Website


    public class Enemy : PhysicsEntity, IDamageDealer
    {
        LeftOrRight directionFacing = LeftOrRight.Left;

        readonly Animation walkLeftAnimation;
        readonly Animation walkRightAnimation;

        public Enemy()
        {
            
            if (LevelManager.Self.CurrentLevel == 0)
            { LoadAnimations("Content/animations/enemyanimation.achx"); }
            else if (LevelManager.Self.CurrentLevel == 1)
            { LoadAnimations("Content/animations/scorpanimation.achx"); }
            else if (LevelManager.Self.CurrentLevel == 2)
            { LoadAnimations("Content/animations/crabanimation.achx"); }
            else if (LevelManager.Self.CurrentLevel == 3)
            { LoadAnimations("Content/animations/hawkanimation.achx"); }
            else 
            { LoadAnimations("Content/animations/enemyanimation.achx"); }


            CurrentAnimation = animations[0];

            walkLeftAnimation = animations.Find(item => item.Name == "WalkLeft");
            walkRightAnimation = animations.Find(item => item.Name == "WalkRight");

            this.VelocityX = 0;

            this.AccelerationY = PlayerMovementCoefficients.GravityAcceleration;
        }

        public void PerformActivity(float seconds)
        {
            PerformAi();

            AssignAnimation();

            ApplyMovementValues(seconds);
        }

        private void AssignAnimation()
        {
            switch (directionFacing)
            {
                case LeftOrRight.Left:
                    this.CurrentAnimation = walkLeftAnimation;
                    break;
                case LeftOrRight.Right:
                    this.CurrentAnimation = walkRightAnimation;
                    break;
            }
        }

        private void PerformAi()
        {
            switch (directionFacing)
            {
                case LeftOrRight.Left:
                    this.VelocityX = -EnemyMovementCoefficients.MaxHorizontalSpeed;
                    break;
                case LeftOrRight.Right:
                    this.VelocityX = EnemyMovementCoefficients.MaxHorizontalSpeed;
                    break;
            }
        }

        public void ReactToCollision(CCPoint reposition)
        {
            ProjectVelocityOnSurface(reposition);

            // account for floating point error:
            const float epsilon = .0001f;

            if (System.Math.Abs(reposition.X) > epsilon)
            {
                if (reposition.X > 0 && directionFacing == LeftOrRight.Left)
                {
                    directionFacing = LeftOrRight.Right;
                }
                else if (reposition.X < 0 && directionFacing == LeftOrRight.Right)
                {
                    directionFacing = LeftOrRight.Left;
                }
            }
        }
    }
}
