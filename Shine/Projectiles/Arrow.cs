using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tales_of_Condoria.Shine.Projectiles
{
    // Didnt finish
   public class Arrow : DrawableGameComponent
   {
        public readonly Rectangle Destination;
        public Vector2 Position;

        public float Speed = 8.0f;

        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D texture;

        private bool shot;

        private float currentTime = 3.0f;
        private const float MAX_TIME = 3.0f;

        private GameLoop gameLoop;

        private int direction;

        public Arrow(Game game, Texture2D texture, Vector2 position, int direction) : base(game)
        {
            gameLoop = (GameLoop)game;

            this.Position = position;
            this.direction = direction;

            this.texture = texture;
            this.spriteBatch = gameLoop.SpriteBatch;
        }

        public void Shoot()
        {
            this.shot = true;
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero, Vector2.One, direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            if (shot)
            {
                this.Position.X += Speed * direction;

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentTime -= elapsed;

                if (currentTime < 0)
                {
                    currentTime = MAX_TIME;

                    if (gameLoop.Components.Remove(this))
                    { 
                        Hide();
                    }
                }
            }
            base.Update(gameTime);
        }

        void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }
    }
}
