using System;
using System.Collections.Generic;
using CocosSharp;

namespace OwlGame.TilemapClasses { 

    //Student Num: c3260058
    //Date: 04/10/18
    //Description: This code finds the properties stored in the .tmx files and tile coordinates.
    //Tutorial Used: Coin Time Demo in Microsoft Website

public struct PropertyLocation
    {
        public CCTileMapLayer Layer;
        public CCTileMapCoordinates TileCoordinates;

        public float WorldX;
        public float WorldY;
        public Dictionary<string, string> Properties;
    }
}
