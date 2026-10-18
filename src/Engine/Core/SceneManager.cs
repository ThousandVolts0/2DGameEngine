namespace Engine.Core
{
    public class SceneManager
    {
        public Scene? ActiveScene { get; private set; }
        public event Action<Scene>? SceneActivated;
        public event Action<Scene>? SceneDeactivated;

        public SceneManager(Scene activeScene)
        {
            SetActiveScene(activeScene);
        }

        public void SetActiveScene(Scene scene)
        {
            if (ActiveScene != null)
                SceneDeactivated?.Invoke(ActiveScene);

            ActiveScene = scene;

            SceneActivated?.Invoke(ActiveScene);
        }
    }
}
