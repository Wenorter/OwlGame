using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace OwlGame.ContentLoading.Animations
{
    //Student Num: c3260058
    //Date: 10/10/18
    //Description: This code is responsible for sprite animation sequences. 
    //Tutorial Used: Coin Time Demo in Microsoft Website

    [XmlType("AnimationChainArraySave")]
    public class AnimationChainListSave
    {
        #region Fields
        [XmlElementAttribute("AnimationChain")]
        public List<AnimationChainSave> AnimationChains;
        #endregion
    }

}

