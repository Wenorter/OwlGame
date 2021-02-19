using System;
using CocosSharp;
using System.Collections.Generic;

namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Date: 10/10/18
    //Description: This code allows player to have touch screen input (iewhen pressing on the screen sensors.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class TouchScreenInput : IDisposable
    {
        CCEventListenerTouchAllAtOnce touchListener;
        CCLayer owner;
        bool touchedOnRightSide = false;

        TouchScreenAnalogStick analogStick;

        public float HorizontalRatio //Horizontal ratio of an analog stick.
        {
            get
            {
                return analogStick.HorizontalRatio;
            }
        }

        public bool WasJumpPressed //Checks if the jump button has been pressed.
        {
            get;
            private set;
        }


        public TouchScreenInput(CCLayer owner)
        {
            this.owner = owner;

            analogStick = new TouchScreenAnalogStick();
            analogStick.Owner = owner;

            touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            owner.AddEventListener(touchListener);

        }

        private void HandleTouchesBegan(List<CCTouch> touches, CCEvent touchEvent) //Determines the touch location
        {
            foreach (var item in touches)
            {
                if (item.Location.X > owner.ContentSize.Center.X)
                {
                    touchedOnRightSide = true;
                }
            }
        }

        private void HandleTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            analogStick.DetermineHorizontalRatio(touches);

        }

        public void Dispose()
        {
            owner.RemoveEventListener(touchListener);
        }



        public void UpdateInputValues()
        {
            WasJumpPressed = touchedOnRightSide;
            touchedOnRightSide = false;
        }
    }
}


