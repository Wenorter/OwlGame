using System;
using CocosSharp;

namespace OwlGame.ContentRuntime.Animations
{
    //Student Num: c3260058
    //Date: 10/10/18
    //Description: This code is responsible for sprite animation framework.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class AnimationFrame
    {
        public CCRect SourceRectangle { get; set; }
        public TimeSpan Duration { get; set; }
        public bool FlipHorizontal { get; set; }
    }
}
