using System;

namespace OwlGame
{   //Student Num: c3260058
    //Date: 10/10/18
    //Description: This code ais for Game Controller Interface.
    //Tutorial Used: Coin Time Demo in Microsoft Website
    public interface IGameController
    {
        bool IsConnected { get; }

        float HorizontalRatio { get; }

        bool JumpPressed { get; }

        bool BackPressed { get; }

        void UpdateInputValues();
    }
}
