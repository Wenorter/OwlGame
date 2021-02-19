using System;
using System.Linq;
using CocosSharp;
using Microsoft.Xna.Framework;
using OwlGame.Entities;
using System.Collections.Generic;
using OwlGame;
using OwlGame.Input;
using CocosDenshion;

namespace OwlGame.Scenes
{
    //Student Num: c3260058
    //Date: 26/10/18
    //Description: This scene contains information on how to control the owl.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public partial class TitleScene : CCScene
    {
        int pageNumber;
        CCLayer mainLayer;
        CCSprite background;
        CCSprite controllerHighlight;
        Button highlightTarget;

        Button navigateLeftButton;
        Button navigateRightButton;

        IMenuController menuController;

        List<Button> levelButtons = new List<Button>();
        List<Button> titleButton = new List<Button>();

        List<Button> highlightableObjects = new List<Button>();

        const float levelButtonSpacing = 216;

        public TitleScene(CCWindow mainWindow) : base(mainWindow)
        {
            CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("OwlSong", loop: true);
            // Make the audio a little quieter:
            CCSimpleAudioEngine.SharedEngine.EffectsVolume = .8f;

           
            PlatformInit(); //initialising platform

            CreateSnowFall();

            CreateLayer();

            CreateBackground();

            CreateLogo();

            CreateMainTitleButton();

            CreateNavigationButtons();

            CreateControllerHighlight();

            RefreshHighlightableObjects();

            Schedule(PerformActivity);

            
        }


        private void RefreshHighlightableObjects()
        {
            highlightableObjects.Clear();

            var visibleButtons = levelButtons.Where(item => item.Visible);

            highlightableObjects.AddRange(visibleButtons);

            if (navigateLeftButton.Visible)
            {
                highlightableObjects.Add(navigateLeftButton);
            }

        }

        partial void PlatformInit();

        private void CreateBackground() //reads sprites from image/ui folder
        {
            background = new CCSprite("ui/background.png");
            background.PositionX = ContentSize.Center.X;
            background.PositionY = ContentSize.Center.Y;
            background.IsAntialiased = false;
            mainLayer.AddChild(background);
        }

        private void CreateLogo()
        {
            background = new CCSprite("ui/logo.png");
            background.PositionX = ContentSize.Center.X;
            const float offsetFromMiddle = 100;
            background.PositionY = ContentSize.Center.Y + offsetFromMiddle;
            background.IsAntialiased = false;
            mainLayer.AddChild(background);
        }

        private void CreateNavigationButtons() //creates navigation buttons, they are not really required 
        {
            const float horizontalDistanceFromEdge = 144;
            const float verticalDistanceFromEdge = 112;

            navigateLeftButton = new Button(mainLayer);
            navigateLeftButton.ButtonStyle = ButtonStyle.LeftArrow;
            navigateLeftButton.PositionX = horizontalDistanceFromEdge;
            navigateLeftButton.PositionY = verticalDistanceFromEdge;
            navigateLeftButton.Name = "NavigateLeftButton";
            navigateLeftButton.Clicked += HandleNavigateLeft;
            mainLayer.AddChild(navigateLeftButton);

            navigateRightButton = new Button(mainLayer);
            navigateRightButton.ButtonStyle = ButtonStyle.RightArrow;
            navigateRightButton.PositionX = ContentSize.Width - horizontalDistanceFromEdge;
            navigateRightButton.PositionY = verticalDistanceFromEdge;
            navigateRightButton.Name = "NavigateLeftButton";
            navigateRightButton.Clicked += HandleNavigateRight;

            mainLayer.AddChild(navigateRightButton);

            UpdateNavigationButtonVisibility();
        }

        private void CreateControllerHighlight()
        {
            controllerHighlight = new CCSprite("ui/controllerhighlight.png");
            controllerHighlight.IsAntialiased = false;
            // make this invisible, it will be turned on if any controllers are connected:
            controllerHighlight.Visible = false;


            mainLayer.AddChild(controllerHighlight);
            controllerHighlight.ZOrder = 1;
        }

        private void PerformActivity(float seconds)
        {
            if (menuController != null)
            {
                menuController.UpdateInputValues();
                controllerHighlight.Visible = menuController.IsConnected;
            }


            if (controllerHighlight.Visible)
            {
                if (highlightTarget == null)
                {
                    MoveHighlightTo(highlightableObjects[0]);
                }

                // for simplicity we'll just allow left/right movement, no up/down movement
                if (menuController.MovedLeft)
                {
                    if (highlightTarget == highlightableObjects[0])
                    {
                        MoveHighlightTo(highlightableObjects.Last());
                    }
                    else
                    {
                        var index = highlightableObjects.IndexOf(highlightTarget);
                        MoveHighlightTo(highlightableObjects[index - 1]);
                    }
                }
                if (menuController.MovedRight)
                {
                    if (highlightTarget == highlightableObjects.Last())
                    {
                        MoveHighlightTo(highlightableObjects[0]);
                    }
                    else
                    {
                        var index = highlightableObjects.IndexOf(highlightTarget);
                        MoveHighlightTo(highlightableObjects[index + 1]);
                    }
                }

                if (menuController.SelectPressed && highlightTarget != null)
                {
                    highlightTarget.OnClicked();
                }
            }
        }

        private void MoveHighlightTo(Button node)
        {
            controllerHighlight.Position = node.Position;
            highlightTarget = node;
        }

        private void UpdateNavigationButtonVisibility()
        {
            navigateLeftButton.Visible = false;

            navigateRightButton.Visible = false;
        }


        private void HandleNavigateLeft(object sender, EventArgs args)
        {
            pageNumber--;
            UpdateNavigationButtonVisibility();

            DestroyMainTitleButton();
            CreateMainTitleButton();
            RefreshHighlightableObjects();
        }


        private void HandleNavigateRight(object sender, EventArgs args)
        {
            pageNumber++;
            UpdateNavigationButtonVisibility();

            DestroyMainTitleButton();
            CreateMainTitleButton();
            RefreshHighlightableObjects();
        }

        private void CreateSnowFall() //Tutorial: https://codemilltech.com/making-it-snow-xamarin-forms-and-cocossharp-and-particles/
        {
            CCParticleSnow snow;
            snow = new CCParticleSnow(new CCPoint(ContentSize.Width, 20));
            snow.Position = new CCPoint(ContentSize.Width / 2, ContentSize.Height + 1);
            snow.StartColor = new CCColor4F(CCColor4B.White);
            snow.EndColor = new CCColor4F(CCColor4B.LightGray);
            snow.StartSize = 10f;
            snow.StartSizeVar = 2f;
            snow.EndSize = 8f;
            snow.EndSizeVar = 1f;
            snow.Speed = 2f;
            snow.Gravity = new CCPoint(0.5f, -2);
            snow.EmissionRate = 2.5f;
            snow.Life = 50f;
        }

        private void CreateLayer()
        {
            mainLayer = new CCLayer();
            this.AddChild(mainLayer);

        }

        private void CreateMainTitleButton()
        {
            

            float centerX = this.ContentSize.Center.X;
            const float topRowOffsetFromCenter = -200;
            float topRowY = this.ContentSize.Center.Y + topRowOffsetFromCenter;

            var button = new Button(mainLayer);

            // Make it 1-based for non-programmers

          
            button.Text = "Start Game";
            button.ButtonStyle = ButtonStyle.TitleButton;

            button.PositionX = centerX;
            button.PositionY = topRowY;
            button.Name = "Start Game";
            button.Clicked += HandleButtonClicked;
            titleButton.Add(button);
            mainLayer.AddChild(button);
            
        }

        private void DestroyMainTitleButton()
        {
    
            
            titleButton.Clear();
        }

        private void HandleButtonClicked(object sender, EventArgs args)
        {
           
            GameAppDelegate.GoToLevelSelectScene();
        }

    }
}
