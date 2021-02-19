using System;
using CocosSharp;
using OwlGame.Input;

namespace OwlGame.Scenes
{
    public partial class GameScene 
    {
        //Student Num: c3260058
        //Student Name: Kira Khritosova
        //Date: 12/10/18
        //Description: This code is for initializing controller in the game mode.
        //Tutorial Used: Coin Time Demo in Microsoft Website

        partial void PlatformInit()
        {
            this.controller = new AmazonFireGameController(); //inializes the Amazon Fire Game Conroller in Game Scene.cs
        }
    }
}