using OwlGame.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Date: 06/10/18
    //Description: This code is for the obstacle class. This is an unique entity, which would deal damage to the owl when beeing touched.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class Obstacle : AnimatedSpriteEntity, IDamageDealer
    {
        public Obstacle ()
        {
            LoadAnimations("Content/animations/obstacleanimation.achx"); 

            CurrentAnimation = animations[0];
        }

    }
}
