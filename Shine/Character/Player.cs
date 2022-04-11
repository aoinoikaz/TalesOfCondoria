using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Tales_of_Condoria.Scenes;
using Tales_of_Condoria.Shine.Animations;
using Tales_of_Condoria.Shine.Event;
using Tales_of_Condoria.Shine.Projectiles;

namespace Tales_of_Condoria.Shine.Character
{
    public class Player : DrawableGameComponent
    {
        public EventHandler<PlayerDiedEventArgs> OnDied;
        public float Health { get;  set; }

        public readonly string Name;

        private Arrow arrow;
        private readonly Texture2D arrowTexture;

        public readonly AnimationController AnimationController;
        public readonly Animation Idle;
        public readonly Animation RunningShoot;
        public readonly Animation Dying;
        public readonly Animation Running;

        public int Direction;

        private readonly SpriteBatch spriteBatch;
        private Vector2 position;

        private readonly float moveSpeed = 1.5f;

        bool wPressed;
        bool aPressed;
        bool sPressed;
        bool dPressed;
        bool shot;
        bool running;

        private readonly GameLoop gameLoop;

        public Player(Game game, SpriteBatch spriteBatch, string name, Vector2 spawnPos) : base(game)
        {
            gameLoop = (GameLoop)game;

            this.spriteBatch = spriteBatch;

            this.Name = name;
            this.position = spawnPos;
            this.Health = 100;

            this.RunningShoot = new Animation(gameLoop.Content.Load<Texture2D>("Animation/Player/RunShooting"), 3, 4, AnimationPlayback.Once, 3);
            this.Idle = new Animation(gameLoop.Content.Load<Texture2D>("Animation/Player/Idle"), 3, 6, AnimationPlayback.Loop, 3);
            this.Dying = new Animation(gameLoop.Content.Load<Texture2D>("Animation/Player/Dying"), 3, 4, AnimationPlayback.Once, 1);
            this.Running = new Animation(gameLoop.Content.Load<Texture2D>("Animation/Player/Run"), 3, 4, AnimationPlayback.Loop, 3);

            this.arrowTexture = gameLoop.Content.Load<Texture2D>("Objects/arrow");

            this.AnimationController = new AnimationController(gameLoop);
            this.AnimationController.CurrentAnimation = Idle;

            AnimationController.Restart();
        }


        public void ShootProjectile()
        {
            Tutorial.shoot.Play();
            this.arrow = new Arrow(gameLoop, arrowTexture, new Vector2(this.position.X + 85, this.position.Y + 145), Direction);

            this.gameLoop.Components.Add(arrow);
            this.arrow.Shoot();
        }

        public void TakeDamage(float damage)
        {
            if (damage > 0.1f)
            {
                Health -= damage;
                if (Health < 0)
                {
                    Health = 0;
                    Die();
                }
            }
        }


        public void Die()
        {
            this.AnimationController.SwapAnimation(Dying);
        }


        /// <summary>
        /// Draw the animations on
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

                spriteBatch.Draw(AnimationController.CurrentAnimation.SpriteSheet,
                   position,
                   AnimationController.CurrentAnimation.Frames[AnimationController.FrameIndex],
                   Color.White,
                   0,
                   Vector2.Zero,
                   Vector2.One,
                   AnimationController.CurrentAnimation.SpriteEffects,
                   0);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            #region Keyboard Letter: W
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                // Move the character as long as theyre holding it down
                this.position.Y -= moveSpeed;

                // If we just started pressing on the key and running, then start the animation.
                if (!wPressed && !running)
                {
                    wPressed = true;
                    running = true;
                    this.AnimationController.SwapAnimation(Running);
                }
                // If they pressed on the left button to shoot, we're running, and we haven't shot yet, start the run shoot animation
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && running && !shot)
                {
                    shot = true;
                    this.AnimationController.SwapAnimation(RunningShoot);
                    this.ShootProjectile();
                }
                // If were still running and shooting and we're at the end of the sprite sheet, force swap back to running animation
                else if (Mouse.GetState().LeftButton == ButtonState.Released && running && shot && AnimationController.FrameIndex == 11)
                {
                    shot = false;
                    this.AnimationController.SwapAnimation(Running);
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.W) && wPressed)
            {
                wPressed = false;
                running = false;
                shot = false;
                this.AnimationController.SwapAnimation(Idle);
            }
            #endregion Keyboard Letter: W


            #region Keyboard Letter: A
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.AnimationController.CurrentAnimation.SpriteEffects = SpriteEffects.FlipHorizontally;

                this.position.X -= moveSpeed;
                Direction = -1;

                if (!aPressed && !running)
                {
                    aPressed = true;
                    running = true;
                    this.AnimationController.SwapAnimation(Running);
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && running && !shot)
                {
                    shot = true;
                    this.AnimationController.SwapAnimation(RunningShoot);
                    this.ShootProjectile();
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released && running && shot && AnimationController.FrameIndex == 11)
                {
                    shot = false;
                    this.AnimationController.SwapAnimation(Running);
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.W) && aPressed)
            {
                aPressed = false;
                running = false;
                shot = false;
                this.AnimationController.SwapAnimation(Idle);
            }
            #endregion Keyboard Letter: A


            #region Keyboard Letter: S
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.position.Y += moveSpeed;

                if (!sPressed && !running)
                {
                    sPressed = true;
                    running = true;
                    this.AnimationController.SwapAnimation(Running);
                }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && running && !shot)
                {
                    shot = true;
                    this.AnimationController.SwapAnimation(RunningShoot);
                    this.ShootProjectile();
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released && running && shot && AnimationController.FrameIndex == 11)
                {
                    shot = false;
                    this.AnimationController.SwapAnimation(Running);
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A) && sPressed)
            {
                sPressed = false;
                running = false;
                shot = false;
                this.AnimationController.SwapAnimation(Idle);
            }
            #endregion Keyboard Letter: S


            #region Keyboard Letter: D
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.AnimationController.CurrentAnimation.SpriteEffects = SpriteEffects.None;

                this.position.X += moveSpeed;

                Direction = 1;


                if (!dPressed && !running)
                {
                    dPressed = true;
                    running = true;
                    this.AnimationController.SwapAnimation(Running);
                }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && running && !shot)
                {
                    shot = true;
                    this.AnimationController.SwapAnimation(RunningShoot);
                    this.ShootProjectile();
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released && running && shot && AnimationController.FrameIndex == 11)
                {
                    shot = false;
                    this.AnimationController.SwapAnimation(Running);
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A) && dPressed)
            {
                dPressed = false;
                running = false;
                shot = false;
                this.AnimationController.SwapAnimation(Idle);
            }
            #endregion Keyboard Letter: D


            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                TakeDamage(1);
            }

            base.Update(gameTime);
        }
    }
}
