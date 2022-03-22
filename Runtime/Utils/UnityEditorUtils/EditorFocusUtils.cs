#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static class EditorFocusUtils
    {
        [MenuItem("GameObject/Focus/Scene")]
        public static void SceneFocus()
        {
            try
            {
                if (SceneView.sceneViews.Count > 0)
                {
                    SceneView sceneView = (SceneView) SceneView.sceneViews[0];
                    sceneView.Focus();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        [MenuItem("GameObject/Focus/Game %g")]
        public static void GameFocus()
        {
            try
            {
                System.Reflection.Assembly assembly = typeof(EditorWindow).Assembly;
                Type type = assembly.GetType("UnityEditor.GameView");
                EditorWindow gameView = EditorWindow.GetWindow(type);
                gameView.Focus();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
#endif
