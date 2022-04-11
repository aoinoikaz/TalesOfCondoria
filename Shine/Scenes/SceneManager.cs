namespace Tales_of_Condoria.Scenes
{
    /// <summary>
    ///  This scene manager class will handle object management and scene navigation
    ///  This class is a singleton instance, so that it can be used throughout the game, only once. We use the classic singleton pattern
    ///  to initialize ITSELF, then a custom init function to pass additional data for init if needed.
    /// </summary>
    public class SceneManager
    {
        public int Count { get; private set; }

        public GameLoop GameLoop { get; private set; }

        // Static singleton instance. Will manually create this object if it doesn't already exist
        private static SceneManager instance;
        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// This method simply initializes the scene manager. We must call this before we call base.Initialize in GameLoop. 
        /// </summary>
        /// <param name="gameLoop"></param>
        public void Init(GameLoop gameLoop)
        {
            GameLoop = gameLoop;
            Count = 0;
        }

        /// <summary>
        /// This method simply adds a scene to the game component list
        /// </summary>
        /// <param name="scene">The scene to be added</param>
        public void AddScene(Scene scene)
        {
            GameLoop.Components.Add(scene);
        }

        /// <summary>
        /// This method simply removes a scene from the game component list
        /// </summary>
        /// <param name="scene">The scene to be removed</param>
        public void RemoveScene(Scene scene)
        {
            for (int i = GameLoop.Components.Count; i >= 0; i++)
            {
                if (GameLoop.Components[i] == scene)
                {
                    GameLoop.Components.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// This function returns a scene thats specified by scene id
        /// </summary>
        /// <param name="index">The id of the scene (the system will automatically assign a scene this id when the object is constructed</param>
        /// <returns>The scene corresponding to the id</returns>
        public Scene GetSceneAtIndex(int index)
        {
            for (int i = 0; i < GameLoop.Components.Count; i++)
            {
                if (GameLoop.Components[i] is Scene scene)
                {
                    if (scene.Id == index)
                    { 
                        return scene;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// This function returns a scene thats specified by scene name
        /// </summary>
        /// <param name="name">The name of the scene</param>
        /// <returns>The scene corresponding to the name</returns>
        public Scene GetSceneByName(string name)
        {
            for (int i = 0; i < GameLoop.Components.Count; i++)
            {
                if (GameLoop.Components[i] is Scene scene)
                {
                    if (scene.Name.Equals(name))
                    { 
                        return scene;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// This function simply assigns a unique id to a scene
        /// </summary>
        /// <returns>The unique id</returns>
        public int GetUniqueId()
        {
            return Count++;
        }
    }
}
