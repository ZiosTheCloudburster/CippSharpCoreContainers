#if UNITY_EDITOR
using UnityEditor;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Simple edit of scripting define symbols in player settings
    /// </summary>
    public static class PreprocessorDirectivesUtils
    {
        /// <summary>
        /// Semicolon its used as separator
        /// </summary>
        private const string separator = ";";
        
        /// <summary>
        /// Adds a script define symbol to player settings in editor
        /// </summary>
        /// <param name="define"></param>
        public static void AddDirective(string define)
        {
            if (string.IsNullOrEmpty(define))
            {
                return;
            }
            
            BuildTargetGroup currentGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentGroup);
            if (defineSymbols.Contains(define))
            {
                return;
            }

            if (!defineSymbols.EndsWith(separator))
            {
                defineSymbols += separator;
            }

            defineSymbols += define;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentGroup, defineSymbols);
        }

        /// <summary>
        /// Remove a script define symbol from player settings in editor
        /// </summary>
        /// <param name="define"></param>
        public static void RemoveDirective(string define)
        {
            if (string.IsNullOrEmpty(define))
            {
                return;
            }
            
            BuildTargetGroup currentGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentGroup);
            if (!defineSymbols.Contains(define))
            {
                return;
            }

            if (defineSymbols.Contains(define + separator))
            {
                defineSymbols = defineSymbols.Replace(define + separator, string.Empty);
            }
            else if (defineSymbols.Contains(define))
            {
                defineSymbols = defineSymbols.Replace(define, string.Empty);
            }
            else
            {
                //what case is this? :D
            }
            
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentGroup, defineSymbols);
        }

        /// <summary>
        /// Check if target define is contained in Player Settings
        /// </summary>
        /// <param name="define"></param>
        /// <returns></returns>
        public static bool Contains(string define)
        {  
            BuildTargetGroup currentGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentGroup);
            return defineSymbols.Contains(define);
        }
    }
}
#endif