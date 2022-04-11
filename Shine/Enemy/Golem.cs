using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tales_of_Condoria.Shine.Animations;

namespace Tales_of_Condoria.Shine.Enemy
{
    public class Golem : DrawableGameComponent
    {
        public float Health { get; set; }

        public readonly string Name;

        public readonly AnimationController AnimationController;
        public readonly Animation Idle;

        public int Direction;

        private readonly SpriteBatch spriteBatch;
        private Vector2 position;

        private readonly float moveSpeed = 1.5f;

        private readonly GameLoop gameLoop;


        public Golem(Game game, SpriteBatch spriteBatch, string name, Vector2 spawnPos) : base(game)
        {
            gameLoop = (GameLoop)game;

            this.spriteBatch = spriteBatch;

            this.Name = name;
            this.position = spawnPos;
            this.Health = 100;

            this.Idle = new Animation(gameLoop.Content.Load<Texture2D>("Animation/Enemy/Idle"), 4, 3, AnimationPlayback.Loop, 6);

            this.AnimationController = new AnimationController(gameLoop);
            this.AnimationController.CurrentAnimation = Idle;
            this.AnimationController.CurrentAnimation.SpriteEffects = SpriteEffects.FlipHorizontally;

            AnimationController.Restart();
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
               new Vector2(0.75f, 0.75f),
               AnimationController.CurrentAnimation.SpriteEffects,
               0);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        /*
        // Didnt finish
        public void ShootProjectile()
        {
            Tutorial.shoot.Play();
            this.arrow = new Arrow(gameLoop, arrowTexture, new Vector2(this.position.X + 85, this.position.Y + 145), Direction);

            this.gameLoop.Components.Add(arrow);
            this.arrow.Shoot();
        }*/

        /*
        public void Die()
        {
            this.AnimationController.SwapAnimation(Dying);
        }*/


        public void TakeDamage(float damage)
        {
            if (damage > 0.1f)
            {
                Health -= damage;
                if (Health < 0)
                {
                    Health = 0;
                    //Die();
                }
            }
        }


        public override void Update(GameTime gameTime)
        {

        }

    }
}
