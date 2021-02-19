using System;
using CocosSharp;


namespace OwlGame.TilemapClasses
{
    //Student Num: c3260058
    //Date: 04/10/18
    //Description: This code finds the location of tiles and returns it's width, height, bottom left coordinate.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    class RectDirections
    {
        public enum Directions
        {
            None = 0,
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8,
            All = 15
        }

        public struct RectWithDirection
        {
            public Directions Directions;

            public float Left;
            public float Bottom;
            public float Width;
            public float Height;

            public float CenterX
            {
                get
                {
                    return Left + Width / 2.0f;
                }
            }

            public float CenterY
            {
                get
                {
                    return Bottom + Height / 2.0f;
                }
            }

            public bool ContainsPoint(float x, float y)
            {
                return x >= Left &&
                    y >= Bottom &&
                    x <= Left + Width &&
                    y <= Bottom + Height;

            }

            public CCRect ToRect()
            {
                return new CCRect(Left, Bottom, Width, Height);
            }

        }
    }
}
