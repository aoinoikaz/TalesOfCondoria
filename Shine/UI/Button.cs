using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tales_of_Condoria.Shine.UI
{
    public class Button : DrawableGameComponent
    {
        public EventHandler<ButtonClickEventArgs> OnClick;
        public readonly Font Font;

        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D primaryImage;
        private readonly Rectangle destination;
        private bool clicked;

        /// <summary>
        /// This constructor allows for button creation without a font. Use this if you want a button that already has graphics on the image
        /// </summary>
        /// <param name="game">Reference to our main game loop</param>
        /// <param name="spriteBatch">Reference to our main sprite batch</param>
        /// <param name="primaryImage">The primary image that will be rendered on the button</param>
        /// <param name="destination">Destination rectangle of the button</param>
        public Button(Game game, SpriteBatch spriteBatch, Texture2D primaryImage, Rectangle destination) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.primaryImage = primaryImage;
            this.destination = destination;
        }

        /// <summary>
        /// This constructor allows for button creation WITH a font. Use this if your buttons don't have text on them. In this case, I'm using this because
        /// my button images suck!
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="primaryImage"></param>
        /// <param name="text"></param>
        /// <param name="fontName"></param>
        /// <param name="destination"></param>
        public Button(Game game, SpriteBatch spriteBatch, Texture2D primaryImage, string text, string fontName, Rectangle destination) : base(game)
        {
            GameLoop gameLoop = (GameLoop)game;

            this.spriteBatch = spriteBatch;
            this.primaryImage = primaryImage;
            this.destination = destination;

            // Create the font which will be placed inside the button
            SpriteFont spriteFont = gameLoop.Content.Load<SpriteFont>(fontName);
            
            Vector2 center = new Vector2(destination.Center.X - spriteFont.MeasureString(text).X / 2, destination.Center.Y - spriteFont.MeasureString(text).Y / 2);
            this.Font = new Font(gameLoop, gameLoop.SpriteBatch, spriteFont, text, center, Color.White);
        }

        /// <summary>
        /// This method will check to see if a mouse click is within THIS current buttons' bounds. If it is, it will omit an
        /// OnClick event which can be subscribed to from the calling scope
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            // If left button was clicked once
            if (mouseState.LeftButton == ButtonState.Pressed && !clicked)
            {
                // If the position of our mouse click is within the bounds of our button
                if (destination.Contains(mousePosition))
                {
                    // Set our flag or else itll continue to be called
                    clicked = true;
                }
            }
            // Now, we've simulataed a mouse OnClick. Ideally, we'd create full states for mouse clicks, and omit the events
            // accordingly to the current mouse state, but no time for that atm.
            else if (mouseState.LeftButton == ButtonState.Released && clicked)
            {
                // Reset state
                clicked = false;

                // If the event has been subscribed too, then invoke it
                if (OnClick != null)
                {
                    OnClick.Invoke(this, new ButtonClickEventArgs());
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This method simply draws the button to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(primaryImage, destination, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// This method simply shows the button, as well as any nested types (eg. a font if the button was constructed with font parameters)
        /// </summary>
        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;

            if (Font != null)
            {
                Font.Visible = true;
                Font.Enabled = true;
            }
        }

        /// <summary>
        /// This method simply hides the button, as well as any nested types (eg. a font if the button was constructed with font parameters)
        /// </summary>
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;

            if (Font != null)
            {
                Font.Visible = false;
                Font.Enabled = false;
            }
        }
    }
}
