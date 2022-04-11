using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tales_of_Condoria.Scenes
{
    /// <summary>
    /// This abstract class is the base of all scenes
    /// </summary>
    public abstract class Scene : DrawableGameComponent
    {
        public List<GameComponent> GameObjects { get; set; }
        public bool IsHidden { get; private set; }

        public readonly string Name;
        public readonly int Id;

        /// <summary>
        /// Construct a scene with a name
        /// </summary>
        /// <param name="game">Reference to the main game loop</param>
        /// <param name="name">The name of the scene</param>
        public Scene(Game game, string name) : base(game)
        {
            // Setup metadata 
            this.Id = SceneManager.Instance.GetUniqueId();
            this.Name = name;

            GameObjects = new List<GameComponent>();

            // Default the scene to hidden
            Hide();
        }

        /// <summary>
        /// This function draws all components in the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in GameObjects)
            {
                if (item is DrawableGameComponent comp)
                {
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// This method updates game components every frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in GameObjects)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This method simply shows the current scene
        /// </summary>
        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
            this.IsHidden = false;
        }

        /// <summary>
        /// This method simply hides the current scene
        /// </summary>
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
            this.IsHidden = true;
        }
    }
}
