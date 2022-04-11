using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tales_of_Condoria.Shine.UI
{
    /// <summary>
    /// This class represents an image UI element. It is a generic element and can be re used for multiple different components.
    /// </summary>
    public class Image : DrawableGameComponent
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D texture;
        private readonly Rectangle destination;

        /// <summary>
        /// Construct this image 
        /// </summary>
        /// <param name="game">Reference to the game loop</param>
        /// <param name="spriteBatch">Reference to the sprite batch</param>
        /// <param name="texture">Loaded texture from content pipeline</param>
        /// <param name="destination">A rectangle where the image will be rendered</param>
        public Image(Game game, SpriteBatch spriteBatch, Texture2D texture, Rectangle destination) : base(game)
        {
            this.texture = texture;
            this.destination = destination;
            this.spriteBatch = spriteBatch;
        }

        /// <summary>
        /// This method will draw this specific image.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destination, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// This method will update this specific component per frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This method will show this component
        /// </summary>
        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        /// <summary>
        /// This method will hide this component
        /// </summary>
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }
    }
}
