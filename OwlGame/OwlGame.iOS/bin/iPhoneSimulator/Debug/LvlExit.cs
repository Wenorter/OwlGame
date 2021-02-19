using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;

namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 08/10/18
    //Description: This code is for the level Exit. When the collision occurs it redirects player to another level.
    //Tutorial Used: Coin Time Demo in Microsoft Website
    //lvl exit is a transparent sprite and used only to indicate end of the level, therefore no animations needed to it's implementation.


    public class lvlExit : AnimatedSpriteEntity
    {
        bool isOpen = false;

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set
            {
                isOpen = value;
                UpdateToIsOpen();
            }
        }


        public lvlExit()
        {
            LoadAnimations("Content/animations/burnedexitanimation.achx");

            CurrentAnimation = animations[0];
        }

        void UpdateToIsOpen()
        {
            if (isOpen)
            {
                IsLoopingAnimation = false;
                CurrentAnimation = animations.Find(item => item.Name == "Open");
            }
            else
            {
                CurrentAnimation = animations.Find(item => item.Name == "Closed");
            }
        }

    }
}

