using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace OwlGame.ContentLoading.Animations
{
    //Student Num: c3260058
    //Date: 10/10/18
    //Description: Creates a list of saved animation frames.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class AnimationChainSave
    {
        public string Name;

        [XmlElementAttribute("Frame")]
        public List<AnimationFrameSave> Frames = new List<AnimationFrameSave>();

    }
}
