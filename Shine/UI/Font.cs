using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tales_of_Condoria.Shine.UI
{
    public class Font : DrawableGameComponent
    {
        public string Message;
        public Vector2 Position;

        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont font;
        private readonly Color color;

        /// <summary>
        /// Construct the object with a starting message, location and color
        /// </summary>
        /// <param name="game">Reference to the game loop</param>
        /// <param name="spriteBatch">Reference to the sprite batch</param>
        /// <param name="font">A loaded asset from content pipeline(sprite font)</param>
        /// <param name="message">The text to be displayed</param>
        /// <param name="position">A vector2 position where the font will be rendered</param>
        /// <param name="color">The color of the font</param>
        public Font(Game game, SpriteBatch spriteBatch, SpriteFont font, string message, Vector2 position, Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.color = color;

            this.Message = message;
            this.Position = position;
        }

        /// <summary>
        /// This method simply draws this font component to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, this.Message, this.Position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// This method simply shows this font
        /// </summary>
        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        /// <summary>
        /// This method simply hides this font
        /// </summary>
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }
    }
}
