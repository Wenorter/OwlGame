using System;
using CocosDenshion;
using CocosSharp;
using OwlGame.Entities;

namespace OwlGame.Scenes
{
    public class HowToPlayScene : CCScene
    {
        CCLayer mainLayer;
        CCSprite background;
        CCSprite howToImage;

        CCDrawNode labelBackground;
        CCLabel howToLabel;
        Button backButton;





        public HowToPlayScene(CCWindow mainWindow) : base(mainWindow)
        {
            CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("05Valley", loop: true);

            CreateSnowFall();

            CreateLayer();

            CreateBackground();

            CreateHowToImage();

            CreateHowtoTitle();

            CreateHowtoLabel();

            CreateBackButton();
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

        private void CreateBackground()
        {
            background = new CCSprite("ui/background.png");
            background.PositionX = ContentSize.Center.X;
            background.PositionY = ContentSize.Center.Y;
            background.IsAntialiased = false;
            mainLayer.AddChild(background);
        }

        private void CreateHowToImage()
        {
            howToImage = new CCSprite("ui/howtoplay.png");
            // Make this positioned by its center:
            howToImage.AnchorPoint = new CCPoint(.5f, .5f);

            howToImage.PositionX = ContentSize.Center.X;
            howToImage.PositionY = ContentSize.Center.Y;
            howToImage.Scale = 1.2f;
            howToImage.IsAntialiased = false;
            mainLayer.AddChild(howToImage);

        }

        private void CreateHowtoTitle()
        {
            
            const float verticalDistanceFromEdge = 100;

            howToLabel = new CCLabel(
                "Catch All The Mice",
                "hazelgrace.ttf", 48, CCLabelFormat.SystemFont);
          
            howToLabel.Scale = 1.0f;
            howToLabel.PositionX = ContentSize.Center.X;
            howToLabel.PositionY = ContentSize.Height - verticalDistanceFromEdge;
            howToLabel.IsAntialiased = false;

            mainLayer.AddChild(howToLabel);
        }

        private void CreateHowtoLabel()
        {
            float backgroundWidth = howToImage.ScaledContentSize.Width;
            const float backgroundHeight = 36;

            labelBackground = new CCDrawNode();

            var rect = new CCRect(
                -backgroundWidth / 2.0f, -backgroundHeight / 2.0f,
                backgroundWidth, backgroundHeight);
            labelBackground.DrawRect(
                rect, CCColor4B.Black);            
            labelBackground.PositionX = ContentSize.Center.X;
            labelBackground.PositionY = 74;
            mainLayer.AddChild(labelBackground);


            howToLabel = new CCLabel(
                "The left side of the screen is used for movement. Right side of the screen is used for flying up. Note that the owl will descend after few seconds!",
                "fonts/Aldrich-Regular.ttf", 48, CCLabelFormat.SystemFont);
            howToLabel.PositionX = ContentSize.Center.X;
            howToLabel.Scale = .45f;
            howToLabel.PositionY = 74;
            howToLabel.HorizontalAlignment = CCTextAlignment.Center;
            howToLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
            howToLabel.IsAntialiased = false;
            
            mainLayer.AddChild(howToLabel);
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
            
            OwlGame.GameAppDelegate.GoToLevelSelectScene();
        }

    }
}
