using System;
using CocosSharp;
using System.Collections.Generic;
using OwlGame.Entities.Data;
using System.Linq;
using CocosDenshion;
using OwlGame.Entities;
using OwlGame.ContentRuntime.Animations;


namespace OwlGame.Entities
{    
    //Student Num: c3260058
    //Date: 07/10/18
    //Description: This code allows player movement ie. left and right and also gives player walking animations.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public enum LeftOrRight
    {
        Left,
        Right
    }

    public class Player : PhysicsEntity
    {
        readonly Animation walkLeftAnimation;
        readonly Animation walkRightAnimation;

        public bool IsOnGround //Checks if player is on the ground
        {
            get;
            private set;
        }

        public Player() //Method for player animation 
        {
            LoadAnimations("Content/animations/playeranimation.achx");

            walkLeftAnimation = animations.Find(item => item.Name == "WalkLeft");
            walkRightAnimation = animations.Find(item => item.Name == "WalkRight");

            CurrentAnimation = walkLeftAnimation;
        }


        public void PerformActivity(float seconds)
        {
            ApplyMovementValues(seconds);

            AssignAnimation();

            this.VelocityY = System.Math.Max(this.VelocityY, PlayerMovementCoefficients.MaxFallingSpeed);
        }

        private void AssignAnimation()
        {
            if (VelocityX > 0)
            {
                CurrentAnimation = walkRightAnimation;
            }
            else if (VelocityX < 0)
            {
                CurrentAnimation = walkLeftAnimation;
            }
            // if 0 do nothing
        }

        public void ApplyInput(float horizontalMovementRatio, bool jumpPressed) //this is for jumping, in owl context it's going to be slightly the same.
        {
            AccelerationY = PlayerMovementCoefficients.GravityAcceleration;

            VelocityX = horizontalMovementRatio * PlayerMovementCoefficients.MaxHorizontalSpeed;

            if (jumpPressed && IsOnGround)
            {
                PerformJump();
            }
        }

        private void PerformJump() //Method for "jumping" itself, but in the owl context it would be flying up and down rapidly.
        {
            CCSimpleAudioEngine.SharedEngine.PlayEffect("FlyUp"); //Sound Effect Donwloaded From: http://soundbible.com/682-Swoosh-1.html
            VelocityY = PlayerMovementCoefficients.JumpVelocity;
        }

        public void ReactToCollision(CCPoint reposition)
        {
            IsOnGround = reposition.Y > 0;

            ProjectVelocityOnSurface(reposition);
        }
    }
}
