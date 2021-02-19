using System;

namespace OwlGame.Input
{    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 10/10/18
    //Description: This code ais for Game Controller Interface.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public interface IMenuController
    {
        bool IsConnected { get; }

        bool MovedLeft { get; }
        bool MovedRight { get; }

        bool SelectPressed { get; }

        void UpdateInputValues();
    }
}
