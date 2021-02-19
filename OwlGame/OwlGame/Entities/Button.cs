using System;
using System.Linq;
using CocosSharp;
using System.Collections.Generic;
using CocosDenshion;

namespace OwlGame.Entities
{
    //Student Num: c3260058
    //Date: 23/10/18
    //Description: This code is for determine button behavour on the LevelSelectScene.cs
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public enum ButtonStyle
    {
        LevelSelect,
        HelpButton,
        TitleButton,
        LeftArrow,
        RightArrow
    }


    public class Button : CCNode
    {
        CCSprite sprite;
        CCLabel label;
        CCLayer owner;

        ButtonStyle buttonStyle;
        public ButtonStyle ButtonStyle
        {
            get
            {
                return buttonStyle;
            }
            set
            {
                buttonStyle = value;

                switch (buttonStyle)
                {
                    case ButtonStyle.LevelSelect:
                        sprite.Texture = new CCTexture2D("ui/buttonup.png");
                        sprite.FlipX = false;
                        break;
                    case ButtonStyle.HelpButton:
                        sprite.Texture = new CCTexture2D("");
                        break;
                    case ButtonStyle.TitleButton:
                        sprite.Texture = new CCTexture2D("");
                        break;
                    case ButtonStyle.LeftArrow:
                        sprite.Texture = new CCTexture2D("ui/arrowup.png");
                        sprite.FlipX = true;
                        break;
                    case ButtonStyle.RightArrow:
                        sprite.Texture = new CCTexture2D("ui/arrowup.png");

                        sprite.FlipX = false;
                        break;
                }

                sprite.IsAntialiased = false;
                sprite.TextureRectInPixels =
                    new CCRect(0, 0,
                    sprite.Texture.PixelsWide,
                    sprite.Texture.PixelsHigh);
            }
        }

        int levelNumber;

        public event EventHandler Clicked;

        public int LevelNumber //reads level number and adds on on the label.
        {
            get
            {
                return levelNumber; 
            }
            set
            {
                levelNumber = value;

                label.Text = levelNumber.ToString();
            }
        }

        public string Text 
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        public Button(CCLayer layer) //adds sprites to the button.
        {
            // Give it a default texture, may get changed in ButtonStyle
            sprite = new CCSprite("ui/buttonup.png");
            sprite.IsAntialiased = false;
            this.AddChild(sprite);

            label = new CCLabel("", "hazelgrace.ttf", 60, CCLabelFormat.SystemFont); //This used to be Aldrich-Regular.ttf, but I want to use different font that fits the game artstyle.
            label.IsAntialiased = false;
            this.AddChild(label);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            layer.AddEventListener(touchListener);

        }

        private void HandleTouchesBegan(List<CCTouch> touches, CCEvent touchEvent) //code for when the button is touched by user.
        {
            if (this.Visible)
            {
                // did the user actually click within the CCSprite bounds?
                var firstTouch = touches.FirstOrDefault();

                if (firstTouch != null)
                {

                    bool isTouchInside = sprite.BoundingBoxTransformedToWorld.ContainsPoint(firstTouch.Location);

                    if (isTouchInside && Clicked != null)
                    {
                        Clicked(this, null);
                    }
                }
            }
        }

        public void OnClicked()
        {

            if (Clicked != null)
            {
                Clicked(this, null);
            }
        }
    }
}
