using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tales_of_Condoria.Shine.Animations
{
    /// <summary>
    /// This data structure makes up an animation, and is used in conjunction with the animation controller
    /// </summary>
    public class Animation
    {
        public List<Rectangle> Frames { get; set; }
        public SpriteEffects SpriteEffects { get; internal set; }

        public readonly Texture2D SpriteSheet;
        public readonly AnimationPlayback Playback;

        public readonly int Row;
        public readonly int Column;
        public readonly int Delay;

        private readonly Vector2 dimension;

        /// <summary>
        /// Construct an animation object 
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet (assuming its packed correctly) to animate</param>
        /// <param name="row">The amount of rows in the sprite sheet</param>
        /// <param name="column">The amount of columns in the sprite sheet</param>
        /// <param name="playback">Animation playback settings.</param>
        /// <param name="delay">Delay in between frames.</param>
        public Animation(Texture2D spriteSheet, int row, int column, AnimationPlayback playback, int delay, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            this.SpriteSheet = spriteSheet;
            this.Row = row;
            this.Column = column;

            this.Playback = playback;
            this.Delay = delay;
            this.dimension = new Vector2(spriteSheet.Width / Column, spriteSheet.Height / Row);

            CreateFrames();
        }

        /// <summary>
        /// This method calculates the sprite sheets dimensions which the animation controller will use to properly render frames
        /// </summary>
        private void CreateFrames()
        {
            Frames = new List<Rectangle>();
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    Frames.Add(r);
                }
            }
        }
    }
}
