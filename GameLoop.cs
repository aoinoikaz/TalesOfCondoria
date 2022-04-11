using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tales_of_Condoria.Scenes;

namespace Tales_of_Condoria
{
    public class GameLoop : Game
    {
        public static Vector2 Stage;
        public SpriteBatch SpriteBatch;

        private readonly GraphicsDeviceManager Graphics;

        public GameLoop()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = 1200;
            Graphics.PreferredBackBufferHeight = 800;
            Graphics.ApplyChanges();
            Stage = new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            SceneManager.Instance.Init(this);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Menu menu = new Menu(this, "Menu");
            Tutorial tutorialIsland = new Tutorial(this, "Tutorial Island");

            SceneManager.Instance.AddScene(menu);
            SceneManager.Instance.AddScene(tutorialIsland);

            menu.Show();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            base.Draw(gameTime);
        }
    }
}
