using System;
using CocosSharp;
using CocosDenshion;
using System.Collections.Generic;
using OwlGame.TilemapClasses;
using OwlGame;
using OwlGame.Entities;
using OwlGame.Input;

namespace OwlGame.Scenes
{   //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 05/10/18
    //Description: This code is for Game Scene where all gameplay happens.
    //Tutorial Used: Coin Time Demo in Microsoft Website,
    public partial class GameScene : CCScene
    {
        CCLayer gameplayLayer;
        CCLayer hudLayer;

        CCTileMap currentLevel;
        LevelCollision levelCollision;
        CCTileMapLayer backgroundLayer;

        TouchScreenInput touchScreen;
        IGameController controller;


        Player player;
        lvlExit exit;

      

        List<IDamageDealer> damageDealers = new List<IDamageDealer>();
        List<Enemy> enemies = new List<Enemy>();
        List<Mouse> mice = new List<Mouse>();

        public GameScene(CCWindow mainWindow) : base(mainWindow)
        {
            

            PlatformInit(); //initializes the amazon game controller

            CreateLayers(); //creates the gamplay layer

            CreateHud(); //for back arrow in the left right corner, so you can go to level select scene and exit gameplay.

            GoToLevel(LevelManager.Self.CurrentLevel); //creates level layout,collision and entities!

            Schedule(PerformActivity); //Loads player controls, so it takes a certain input and executes it!

            /*Observation:  All Method calls are using entity names i haven't loaded the level so it gives null. And if it gives null game crashes.*/
        }

        partial void PlatformInit();

        private void CreateHud()
        {


            var backButton = new Button(hudLayer);
            backButton.ButtonStyle = ButtonStyle.LeftArrow;
            backButton.Clicked += HandleBackClicked;
            backButton.PositionX = 100;
            backButton.PositionY = ContentSize.Height - 100;
            hudLayer.AddChild(backButton);

        }

        private void HandleBackClicked(object sender, EventArgs args)
        {
            GameAppDelegate.GoToLevelSelectScene();
        }

        private void PerformActivity(float seconds)
        {
            player.PerformActivity(seconds); /*ERROR IS HERE!!!!!*/ /*Edit: No more errors!*/

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].PerformActivity(seconds);
            }

            ApplyInput(seconds); /*ERROR IS HERE!!!*/ /*Edit: No more errors!*/

            PerformCollision(seconds); //ERROR IS HERE!!!! /*Edit: No more errors!*/

            PerformScrolling(); /*ERROR IS HERE!!!!*/ /*Edit: No more errors!*/


        }


        private void PerformScrolling()
        {
            float effectivePlayerX = player.PositionX;

            // Effective values limit the scorlling beyond the level's bounds
            effectivePlayerX = System.Math.Max(player.PositionX, this.ContentSize.Center.X);
            float levelWidth = currentLevel.TileTexelSize.Width * currentLevel.MapDimensions.Column;
            effectivePlayerX = System.Math.Min(effectivePlayerX, levelWidth - this.ContentSize.Center.X);

            float effectivePlayerY = player.PositionY;
            float levelHeight = currentLevel.TileTexelSize.Height * currentLevel.MapDimensions.Row;
            effectivePlayerY = System.Math.Min(player.Position.Y, levelHeight - this.ContentSize.Center.Y);
            // We don't want to limit the scrolling on Y - instead levels should be large enough
            // so that the view never reaches the bottom. This allows the user to play
            // with their thumbs without them getting in the way of the game.

            float positionX = -effectivePlayerX + this.ContentSize.Center.X;
            float positionY = -effectivePlayerY + this.ContentSize.Center.Y;

            gameplayLayer.PositionX = positionX;
            gameplayLayer.PositionY = positionY;

            // We don't want the background to scroll, 
            // so we'll make it move the opposite direction of the rest of the tilemap:
            if (backgroundLayer != null)
            {
                backgroundLayer.PositionX = -positionX;
                backgroundLayer.PositionY = -positionY;
            }

            currentLevel.TileLayersContainer.PositionX = positionX;
            currentLevel.TileLayersContainer.PositionY = positionY;
        }

        private void CreateLayers() 
        {
            gameplayLayer = new CCLayer();
            this.AddChild(gameplayLayer);

            hudLayer = new CCLayer();
            this.AddChild(hudLayer);
        }


        private void GoToLevel(int LevelNumber)
        {
            if (LevelManager.Self.CurrentLevel == 0)
            {
                CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("01BurnedLands", loop: true);
            }

            else if (LevelManager.Self.CurrentLevel == 1)
            {
                CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("02Desert", loop: true);
            }

            else if (LevelManager.Self.CurrentLevel == 2)
            {
                CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("03Ocean", loop: true);
            }

            else if (LevelManager.Self.CurrentLevel == 3)
            {
                CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("04SnowMountain", loop: true);
            }

            else if (LevelManager.Self.CurrentLevel == 4)
            {
                CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("05Valley", loop: true);
            }

            else if (LevelManager.Self.CurrentLevel == 5)
            {
                CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("06HedwigsTheme", loop: true);
            }

            LoadLevel(LevelNumber); //works!

            CreateCollision(); //works!

            ProcessTileProperties(); // works! I forgot to add mastersheet.png in the VS file directory and it was crashing... rest in peace my 5 hours of finding that out.


        }

        private void CreateCollision()
        {
            levelCollision = new LevelCollision();
            levelCollision.PopulateFrom(currentLevel);
        }

        private void LoadLevel(int levelNumber)
        {
            currentLevel = new CCTileMap("level" + levelNumber + ".tmx");
            currentLevel.Antialiased = false;
            backgroundLayer = currentLevel.LayerNamed("Background");


            // CCTileMap is a CCLayer, so we'll just add it under all entities
            this.AddChild(currentLevel);

            // put the game layer after
            this.RemoveChild(gameplayLayer);
            this.AddChild(gameplayLayer);

            this.RemoveChild(hudLayer);
            this.AddChild(hudLayer);
        }

        private void ProcessTileProperties()
        {
            TileMapPropertyFinder finder = new TileMapPropertyFinder(currentLevel);
            foreach (var propertyLocation in finder.GetPropertyLocations())
            {
                var properties = propertyLocation.Properties;
                if (properties.ContainsKey("EntityType")) /*Determines entity type ie. Player, Mouse*/
                {
                    float worldX = propertyLocation.WorldX;
                    float worldY = propertyLocation.WorldY;


                    if (properties.ContainsKey("YOffset")) /*Determines Y-axis pixel offset*/
                    {
                        string yOffsetAsString = properties["YOffset"];
                        float yOffset = 0;
                        float.TryParse(yOffsetAsString, out yOffset);
                        worldY += yOffset;
                    }

                    bool created = TryCreateEntity(properties["EntityType"], worldX, worldY);

                    if (created)
                    {
                        propertyLocation.Layer.RemoveTile(propertyLocation.TileCoordinates);
                    }

                    else if (properties.ContainsKey("RemoveMe")) /*Removes tiles*/
                    {
                        propertyLocation.Layer.RemoveTile(propertyLocation.TileCoordinates);
                    }
                }
            }

            touchScreen = new TouchScreenInput(gameplayLayer); ///ERROR FREE CLASS, ALL GOOD
        }

        private bool TryCreateEntity(string entityType, float worldX, float worldY)
        {
            CCNode entityAsNode = null;

            switch (entityType)
            {
                case "Player":
                    player = new Player();
                    entityAsNode = player;
                    break;
                case "Mouse":
                    Mouse mouse = new Mouse();
                    entityAsNode = mouse;
                    mice.Add(mouse);
                    break;
                case "Door":
                    exit = new lvlExit();
                    entityAsNode = exit;
                    break;
                case "Spikes":
                    var spikes = new Obstacle();
                    this.damageDealers.Add(spikes);
                    entityAsNode = spikes;
                    break;
                case "Enemy":
                    var enemy = new Enemy();
                    this.damageDealers.Add(enemy);
                    this.enemies.Add(enemy);
                    entityAsNode = enemy;
                    break;
            }

            if (entityAsNode != null)
            {
                entityAsNode.PositionX = worldX;
                entityAsNode.PositionY = worldY;
                gameplayLayer.AddChild(entityAsNode);
            }

            return entityAsNode != null;
        }

        private void ApplyInput(float seconds)
        {
            if (controller != null && controller.IsConnected)
            {
                controller.UpdateInputValues();
                player.ApplyInput(controller.HorizontalRatio, controller.JumpPressed);

                if (controller.BackPressed)
                {
                    GameAppDelegate.GoToLevelSelectScene();
                }
            }
            else
            {
                touchScreen.UpdateInputValues();
                player.ApplyInput(touchScreen.HorizontalRatio, touchScreen.WasJumpPressed);
            }
        }

        private void DestroyMouse(Mouse mouseToDestroy)
        {
            mice.Remove(mouseToDestroy);
            gameplayLayer.RemoveChild(mouseToDestroy);
            mouseToDestroy.Dispose();
        }

        private void DestroyDamageDealer(IDamageDealer damageDealer)
        {
            damageDealers.Remove(damageDealer);
            if (damageDealer is CCNode)
            {
                var asNode = damageDealer as CCNode;
                gameplayLayer.RemoveChild(asNode);
                asNode.Dispose();
            }
        }

        private void DestroyLevel()
        {
            gameplayLayer.RemoveChild(player);
            player.Dispose();

            gameplayLayer.RemoveChild(exit);
            exit.Dispose();

            for (int i = mice.Count - 1; i > -1; i--)
            {
                var mouseToDestroy = mice[i];
                DestroyMouse(mouseToDestroy);
            }

            for (int i = damageDealers.Count - 1; i > -1; i--)
            {
                var damageDealer = damageDealers[i];

                DestroyDamageDealer(damageDealer);
            }

            // We can just clear the list - the contained entities are destroyed as damage dealers:
            enemies.Clear();

            touchScreen.Dispose();


            this.RemoveChild(currentLevel);
            currentLevel.Dispose();

            

        }

        private void HandlePlayerDeath()
        {
            CCSimpleAudioEngine.SharedEngine.PlayEffect("OwlDeath"); /*Donwloaded from: http://www.wavlist.com/soundfx/012/*/
                                                                     //DestroyLevel(); //this doesn't work and it makes the game crash.  
            GameAppDelegate.GoToDeathScene();
        }
    }
}

