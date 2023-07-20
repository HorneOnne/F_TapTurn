using UnityEngine.SceneManagement;

namespace TapTurn
{
    public static class Loader
    {
        public enum Scene
        {
            _0_MainMenuScene,
            _1_GameOverScene,
            Level_01,
            Level_02,
            Level_03,
        }

        private static Scene targetScene;

        public static void Load(Scene targetScene, System.Action afterLoadScene = null)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Loader.targetScene.ToString());
            FadeTransition.Instance.FadeOut(afterLoadScene);
        }

        public static void LoadLevel(int level, System.Action afterLoadScene = null)
        {
            Loader.targetScene = GetSceneLevel(level);
            SceneManager.LoadScene(Loader.targetScene.ToString());
            FadeTransition.Instance.FadeOut(afterLoadScene);
        }


        public static Scene GetSceneLevel(int level)
        {
            switch (level)
            {
                default: return Scene.Level_01;
                case 1: return Scene.Level_01;
                case 2: return Scene.Level_02;
                case 3: return Scene.Level_03;
            }
        }
    }
}
