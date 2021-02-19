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
    //Description: This scene allows player to select levels.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public partial class LevelSelectScene : CCScene
    {
        int pageNumber;
        CCLayer mainLayer;
        CCSprite background;
        CCSprite controllerHighlight;
        Button highlightTarget;

        Button navigateLeftButton;
        Button navigateRightButton;
        Button howToButton;
        Button backButton;

        IMenuController menuController;

        List<Button> levelButtons = new List<Button>();
        List<Button> highlightableObjects = new List<Button>();

        const float levelButtonSpacing = 216;

        public LevelSelectScene(CCWindow mainWindow) : base(mainWindow)
        {
            CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("OwlSong", loop: true);

            PlatformInit(); //initialising platform

            CreateSnowFall();

            CreateLayer();

            CreateBackground();

            CreateLogo();

            CreateLevelButtons();

            CreateNavigationButtons();

            CreateHowToPlayButton();

            CreateControllerHighlight();

            CreateBackButton();

            RefreshHighlightableObjects();

            Schedule(PerformActivity);
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

        private void CreateHowToPlayButton()
        {
            howToButton = new Button(mainLayer);
            howToButton.ButtonStyle = ButtonStyle.HelpButton;
            howToButton.PositionX = ContentSize.Center.X;
            howToButton.PositionY = 100;
            howToButton.Name = "HelpButton";
            howToButton.Text = "How To Play";
            howToButton.Clicked += HandleHelpClicked;

            mainLayer.AddChild(howToButton);

        }

        private void HandleHelpClicked(object sender, EventArgs args)
        {

            OwlGame.GameAppDelegate.GoToHowToScene();
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
            background = new CCSprite("ui/logobig.png");
            background.PositionX = ContentSize.Center.X;
            const float offsetFromMiddle = 360;
            background.PositionY = ContentSize.Center.Y + offsetFromMiddle;
            background.IsAntialiased = false;
            mainLayer.AddChild(background);
        }

        private void CreateNavigationButtons() //creates navigation buttons, they are not really required 
        {
            const float horizontalDistanceFromEdge = 70;
            const float verticalDistanceFromEdge = 70;

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
            navigateLeftButton.Visible = pageNumber > 0;

            navigateRightButton.Visible = (1 + pageNumber) * 6 < LevelManager.Self.NumberOfLevels;


        }


        private void HandleNavigateLeft(object sender, EventArgs args)
        {
            pageNumber--;
            UpdateNavigationButtonVisibility();

            DestroyLevelButtons();
            CreateLevelButtons();
            RefreshHighlightableObjects();
        }


        private void HandleNavigateRight(object sender, EventArgs args)
        {
            pageNumber++;
            UpdateNavigationButtonVisibility();

            DestroyLevelButtons();
            CreateLevelButtons();
            RefreshHighlightableObjects();
        }


        private void CreateLayer()
        {
            mainLayer = new CCLayer();
            this.AddChild(mainLayer);

        }

        private void CreateLevelButtons()
        {
            const int buttonsPerPage = 6;
            int levelIndex0Based = buttonsPerPage * pageNumber;

            int maxLevelExclusive = System.Math.Min(levelIndex0Based + 6, LevelManager.Self.NumberOfLevels);
            int buttonIndex = 0;

            float centerX = this.ContentSize.Center.X;
            const float topRowOffsetFromCenter = 16;
            float topRowY = this.ContentSize.Center.Y + topRowOffsetFromCenter;

            for (int i = levelIndex0Based; i < maxLevelExclusive; i++)
            {
                var button = new Button(mainLayer);

                // Make it 1-based for non-programmers
                button.LevelNumber = i + 1;

                button.ButtonStyle = ButtonStyle.LevelSelect;

                button.PositionX = centerX - levelButtonSpacing + (buttonIndex % 3) * levelButtonSpacing;
                button.PositionY = topRowY - levelButtonSpacing * (buttonIndex / 3);
                button.Name = "LevelButton" + i;
                button.Clicked += HandleButtonClicked;
                levelButtons.Add(button);
                mainLayer.AddChild(button);

                buttonIndex++;
            }
        }

        private void CreateBackButton()
        {
      
            const float verticalDistanceFromEdge = 100;

            backButton = new Button(mainLayer);
            backButton.ButtonStyle = ButtonStyle.LeftArrow;
            backButton.PositionX = verticalDistanceFromEdge;
            backButton.PositionY = ContentSize.Height - verticalDistanceFromEdge;
            backButton.Name = "backButton";
            backButton.Clicked += HandleBackClicked;
            mainLayer.AddChild(backButton);

        }

        private void HandleBackClicked(object sender, EventArgs args)
        {

            OwlGame.GameAppDelegate.GoToTitleScene();
        }

        private void DestroyLevelButtons()
        {
            for (int i = levelButtons.Count - 1; i > -1; i--)
            {
                mainLayer.RemoveChild(levelButtons[i]);
                levelButtons[i].Dispose();
            }

            levelButtons.Clear();
        }

        private void HandleButtonClicked(object sender, EventArgs args)
        {
            // levelNumber is 1-based, so subtract 1:
            var levelIndex = (sender as Button).LevelNumber - 1;
            
            CCSimpleAudioEngine.SharedEngine.PlayEffect("Button");
            LevelManager.Self.CurrentLevel = levelIndex;

            GameAppDelegate.GoToGameScene();
        }


    }
}