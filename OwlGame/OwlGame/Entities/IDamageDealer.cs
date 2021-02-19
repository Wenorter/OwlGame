using System;
using CocosSharp;

namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Date: 08/10/18
    //Description: This code is an interface for the damage dealing for the player entity.
    //Tutorial Used: https://www.youtube.com/watch?v=aQ8YkJrAbzE

    public interface IDamageDealer
    {
        CCRect BoundingBoxWorld { get; }
    }
}

