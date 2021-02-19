using System;
using CocosSharp;
using CocosDenshion;
using OwlGame.Entities;

namespace OwlGame.Scenes
{     
    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 05/10/18
    //Description: This code is responsible for level object collisions.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public partial class GameScene
    {
        void PerformCollision(float seconds)
        {
            PerformPlayerVsMouseCollision();

            PerformPlayerVsLvlExitCollision();

            PerformPlayerVsEnvironmentCollision();

            PlayerVsDamageDealersCollision();

            PerformEnemiesVsEnvironmentCollision();
        }

        bool PerformPlayerVsMouseCollision() //this code is responsible what happens when the owl collides with a mouse.
        {
            bool grabbedAnyMice = false;

            // Reverse loop since items may get removed from the list
            for (int i = mice.Count - 1; i > -1; i--)
            {
                if (player.Intersects(mice[i]))
                {
                    var mouseToDestroy = mice[i];

                    DestroyMouse(mouseToDestroy);

                    if (LevelManager.Self.CurrentLevel == 5)
                    { CCSimpleAudioEngine.SharedEngine.PlayEffect("Letter"); }
                    //Sound Effect Donwloaded from: https://www.soundjay.com/page-flip-sounds-1.html

                    else
                    {
                        CCSimpleAudioEngine.SharedEngine.PlayEffect("MousePickup");
                    } //Sound Effect Donfloaded from: https://averageoutdoorsman.com/wild-game-downloads/mice-rats/?doing_wp_cron=1539573723.3573169708251953125000



                    grabbedAnyMice = true;
                }
            }

            if (grabbedAnyMice && mice.Count == 0) //opens exit of the current level if all mice are collected
            {
                // User got all the mice, so open the exit to the next level
                if (exit != null)
                {
                    exit.IsOpen = true;
                }
            }


            return grabbedAnyMice; //mice grabbed per level
        }

        void PerformPlayerVsLvlExitCollision() //this code is re
        {
            if (exit != null && exit.IsOpen && player.Intersects(exit))
            {
                CCSimpleAudioEngine.SharedEngine.PlayEffect("lvlExit"); //Sound Effect Donfloaded from: https://opengameart.org/content/completion-sound
                try
                {
                    bool isLastLevel = (LevelManager.Self.CurrentLevel + 1 == LevelManager.Self.NumberOfLevels);

                    if (isLastLevel)
                    {
                        GameAppDelegate.GoToLevelSelectScene();
                    }
                    else
                    {
                        DestroyLevel();
                        LevelManager.Self.CurrentLevel++;
                        GoToLevel(LevelManager.Self.CurrentLevel);
                    }
                }
                catch (Exception e)
                {
                    int m = 3;
                }
            }
        }

        void PerformPlayerVsEnvironmentCollision() //this code executes when owl collides whith level trrain.
        {
            CCPoint positionBeforeCollision = player.Position;
            CCPoint reposition = CCPoint.Zero;
            if (levelCollision.PerformCollisionAgainst(player))
            {
                reposition = player.Position - positionBeforeCollision;
            }
            player.ReactToCollision(reposition);
        }

        void PlayerVsDamageDealersCollision() //this code executes when players collides with an object which is dangerous for. (the owl vs snake).
        {
            for (int i = 0; i < damageDealers.Count; i++)
            {
                if (player.BoundingBoxWorld.IntersectsRect(damageDealers[i].BoundingBoxWorld))
                {
                    HandlePlayerDeath();
                    break;
                }
            }
        }

        void PerformEnemiesVsEnvironmentCollision() //this code executes when owl collides with an enemy.
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];
                CCPoint positionBeforeCollision = enemy.Position;
                CCPoint reposition = CCPoint.Zero;

                if (levelCollision.PerformCollisionAgainst(enemy))
                {
                    reposition = enemy.Position - positionBeforeCollision;
                }

                enemy.ReactToCollision(reposition);
            }
        }

    }
}

