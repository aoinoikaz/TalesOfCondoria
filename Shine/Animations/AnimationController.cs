using Microsoft.Xna.Framework;

namespace Tales_of_Condoria.Shine.Animations
{
    public class AnimationController : GameComponent
    {
        public Animation CurrentAnimation { get; set; }
        public bool IsAnimating { get; private set; }
        public int FrameIndex { get; private set; }
        public int DelayCounter { get; private set; }

        public AnimationController(Game game) : base(game) {}

        /// <summary>
        /// This method will update animation for the specified animation data structure.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (IsAnimating)
            {
                // Increase our delay counter
                DelayCounter++;

                // Move to new frame if counter threshold hit
                if (DelayCounter > CurrentAnimation.Delay)
                {
                    // Calculate highest bound of sprite sheet
                    int highestBound = (CurrentAnimation.Row * CurrentAnimation.Column) - 1;

                    // Increment the frame index if we aren't at the last frame
                    if( (FrameIndex + 1) <= highestBound)
                    { 
                        FrameIndex++;
                    }
                    
                    // If playback mode is set to loop and we're at the last frame , reset it 
                    if (CurrentAnimation.Playback == AnimationPlayback.Loop && FrameIndex == highestBound)
                    {
                        FrameIndex = 0;
                    }
                    DelayCounter = 0;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This method simply swaps the current animation
        /// </summary>
        /// <param name="animation"></param>
        public void SwapAnimation(Animation animation)
        {
            // Only reset the frame index is the animation we're swapping isn't the current one
            if (this.CurrentAnimation != animation)
            {
                this.FrameIndex = 0;
            }
            this.CurrentAnimation = animation;
        }


        /// <summary>
        /// This method will start the animator
        /// </summary>
        public void Restart()
        {
            IsAnimating = true;
            FrameIndex = 0;
            DelayCounter = 0;
        }
    }
}
