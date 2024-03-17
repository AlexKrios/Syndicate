#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Syndicate.Utils
{
    [InitializeOnLoad]
    public class HelpersUtil
    {
        private const string PreloaderPath = "Assets/Project/Scenes/Preloader.unity";
        private const string HubPath = "Assets/Project/Scenes/Hub.unity";

        static HelpersUtil()
        {
            EditorApplication.update = Update;
        }

        [MenuItem("Helper/Open Preloader", false, 1)]
        public static void OpenLauncher()
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene(PreloaderPath);
        }
        
        [MenuItem("Helper/Open Hub", false, 1)]
        public static void OpenHub()
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene(HubPath);
        }

        [MenuItem("Helper/Run Preloader", false, 101)]
        public static void RunPreloader()
        {
            if (!EditorApplication.isPlaying)
            {
                PlayerPrefs.SetString("restoreScenePath", SceneManager.GetActiveScene().path);
                EditorSceneManager.SaveOpenScenes();
                EditorSceneManager.OpenScene(PreloaderPath);
                PlayerPrefs.SetInt("needRestore", 1);
                EditorApplication.isPlaying = true;
            }
        }
        
        private static void Update()
        {
            if (!EditorApplication.isPlaying &&
                !EditorApplication.isPlayingOrWillChangePlaymode &&
                PlayerPrefs.GetInt("needRestore") == 1)
            {
                PlayerPrefs.SetInt("needRestore", 0);
                EditorSceneManager.OpenScene(PlayerPrefs.GetString("restoreScenePath"));
            }
        }

        [MenuItem("Helper/Reset PlayerPrefs", false, 201)]
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif