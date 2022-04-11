using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using Tales_of_Condoria.Shine.Character;
using Tales_of_Condoria.Shine.Enemy;
using Tales_of_Condoria.Shine.Event;
using Tales_of_Condoria.Shine.UI;

namespace Tales_of_Condoria.Scenes
{
    public class Tutorial : Scene
    {
        public readonly Image Background;

        public readonly Player player;

        public readonly Font healthBar;

        public static SoundEffect shoot;

        public readonly Golem golem;

        float previousHealth;

        public Tutorial(Game game, string name) : base(game, name)
        {
            GameLoop gameLoop = (GameLoop)game;

            // Background
            Texture2D backgroundImage = gameLoop.Content.Load<Texture2D>("Backgrounds/tutorial_island");

            Background = new Image(gameLoop, gameLoop.SpriteBatch, backgroundImage,
                new Rectangle(0, 0,
                (int)GameLoop.Stage.X,
                (int)GameLoop.Stage.Y));

            // health bar text
            SpriteFont healthBarFont = gameLoop.Content.Load<SpriteFont>("Fonts/healthBar");
            string msg = "HP: 100 / 100";
            healthBar = new Font(gameLoop, gameLoop.SpriteBatch, healthBarFont, msg, new Vector2(
                GameLoop.Stage.X / 2 - healthBarFont.MeasureString(msg).X / 2 - 450, GameLoop.Stage.Y / 2 - healthBarFont.MeasureString(msg).Y / 2 - 350), Color.Black);

            Vector2 spawnPos = new Vector2(GameLoop.Stage.X / 2 - 100, GameLoop.Stage.Y / 2 - 100);
            player = new Player(gameLoop, gameLoop.SpriteBatch, "Aoinoikaz", spawnPos);
            player.OnDied += OnPlayerDied;

            Vector2 golemSpawnPos = new Vector2(GameLoop.Stage.X / 2 + 200 , GameLoop.Stage.Y / 2 - 150);
            golem = new Golem(gameLoop, gameLoop.SpriteBatch, "Nevarious the Golem", golemSpawnPos);


            this.GameObjects.Add(Background);
            this.GameObjects.Add(healthBar);

            this.GameObjects.Add(player);
            this.GameObjects.Add(player.AnimationController);

            this.GameObjects.Add(golem);
            this.GameObjects.Add(golem.AnimationController);

            shoot = gameLoop.Content.Load<SoundEffect>("Sounds & FX/shoot");
        }


        private void OnPlayerDied(object sender, PlayerDiedEventArgs e)
        {
            Player p = (Player)sender;
            Debug.WriteLine("OnPlayerDied: " + p.Health);
        }


        public override void Update(GameTime gameTime)
        {
            if (previousHealth != player.Health)
            {
                healthBar.Message = "HP: " + player.Health + " / 100";
                previousHealth = player.Health;
            }

            base.Update(gameTime);
        }
    }
}
