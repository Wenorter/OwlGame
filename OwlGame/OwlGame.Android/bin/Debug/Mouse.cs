using OwlGame.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwlGame.Entities
{   
    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 05/10/18
    //Description: This code is for the enemy class. This is an unique entity, which would deal damage to the owl when beeing touched.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class Mouse : AnimatedSpriteEntity
    {
        public Mouse ()
        {

            if (LevelManager.Self.CurrentLevel == 5)
            { LoadAnimations("Content/animations/letteranimation.achx"); }

            else
            {
                LoadAnimations("Content/animations/mouseanimation.achx");
            }

            CurrentAnimation = animations[0];
        }
    }
}