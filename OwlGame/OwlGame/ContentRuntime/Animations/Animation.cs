using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using OwlGame.ContentLoading.Animations;

namespace OwlGame.ContentRuntime.Animations
{  
    //Student Num: c3260058
    //Date: 10/10/18
    //Description: This code is responsible for sprite animation sequences during the game running time.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class Animation
    {
        // The frames in this animation
        List<AnimationFrame> frames = new List<AnimationFrame>();
        
        public IEnumerable<AnimationFrame> Frames
        {
            get
            {
                return frames;
            }
        }

        public string Name
        {
            get;
            set;
        }

        // The length of the entire animation
        public TimeSpan Duration
        {
            get
            {
                double totalSeconds = 0;
                foreach (var frame in frames)
                {
                    totalSeconds += frame.Duration.TotalSeconds;
                }

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public static Animation FromAnimationSave(AnimationChainSave animationSave)
        {
            Animation toReturn = new Animation
            {
                Name = animationSave.Name
            };

            foreach (var frame in animationSave.Frames)
            {
                CCRect rectangle;

                rectangle = new CCRect(
                    frame.LeftCoordinate,
                    frame.TopCoordinate,
                    frame.RightCoordinate - frame.LeftCoordinate,
                    frame.BottomCoordinate - frame.TopCoordinate);

                var duration = TimeSpan.FromSeconds(frame.FrameLength);

                toReturn.AddFrame(rectangle, duration, flipHorizontal: frame.FlipHorizontal);
            }

            return toReturn;
        }

        private AnimationFrame GetAnimationFrameAtTime(TimeSpan timeSpan)
        {
            AnimationFrame currentFrame = null;

            // See if we can find the frame
            TimeSpan accumulatedTime = new TimeSpan(0);
            foreach (var frame in frames)
            {
                if (accumulatedTime + frame.Duration >= timeSpan)
                {
                    currentFrame = frame;
                    break;
                }
                else
                {
                    accumulatedTime += frame.Duration;
                }
            }

            // If no frame was found, then try the last frame, 
            // just in case timeIntoAnimation somehow exceeds Duration
            if (currentFrame == null)
            {
                currentFrame = frames.LastOrDefault();
            }

            return currentFrame;
        }

        public CCRect GetRectangleAtTime(TimeSpan timeSpan)
        {
            var currentFrame = GetAnimationFrameAtTime(timeSpan);

            // If we found a frame, return its rectangle, otherwise
            // return an empty rectangle (one with no width or height)
            if (currentFrame != null)
            {
                return currentFrame.SourceRectangle;
            }
            else
            {
                return CCRect.Zero;
            }
        }

        public bool GetIfFlippedHorizontallyAtTime(TimeSpan timeSpan)
        {
            var currentFrame = GetAnimationFrameAtTime(timeSpan);

            if (currentFrame != null)
            {
                return currentFrame.FlipHorizontal;
            }
            else
            {
                return false;
            }
        }

        // Adds a single frame to this animation.
        public void AddFrame(CCRect rectangle, TimeSpan duration, bool flipHorizontal = false)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration,
                FlipHorizontal = flipHorizontal
            };

            frames.Add(newFrame);
        }
    }
}
