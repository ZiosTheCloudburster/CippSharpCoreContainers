using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Holds references to prefabs through keys 
    /// </summary>
    [CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/Templates Holder Asset")]
    public class TemplatesHolderAsset : AListDataAsset<StringObjectPair>
    {
        /// <summary>
        /// Path separator
        /// </summary>
        public const char separator = '/';
        /// <summary>
        /// Palettes with no name have this
        /// </summary>
        public const string DefaultGroupIdentifierName = "Default";
        
        /// <summary>
        /// A nicer name for logs
        /// </summary>
        private static readonly string LogName = StringUtils.LogName(typeof(TemplatesHolderAsset));

        /// <summary>
        /// Loaded Templates Holders
        /// </summary>
        private static readonly List<TemplatesHolderAsset> loadedTemplates = new List<TemplatesHolderAsset>();
        
        /// <summary>
        /// This Holder identifier
        /// </summary>
        [SerializeField]private string identifier = "";

        /// <summary>
        /// Save this TemplatesHolderAsset to loadedTemplates
        /// </summary>
        public void Save()
        {
            EnsureInitialization();
            
            ArrayUtils.AddIfNew(loadedTemplates, this);

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        [UnityEditor.InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            var prevLoadedTemplates = loadedTemplates;
            loadedTemplates.Clear();
            loadedTemplates.AddRange(Resources.LoadAll<TemplatesHolderAsset>(string.Empty));
            if (!ArrayUtils.IsNullOrEmpty(loadedTemplates))
            {
                foreach (var prevLoadedTemplate in prevLoadedTemplates)
                {
                    if (prevLoadedTemplate != null)
                    {
                        ArrayUtils.AddIfNew(loadedTemplates, prevLoadedTemplate);
                    }
                }
            }
        }

        private static void EnsureInitialization()
        {
            if (ArrayUtils.IsNullOrEmpty(loadedTemplates))
            {
                Initialize();
            }
        }

        /// <summary>
        /// Retrieve template by key
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetTemplate<T>(string key) where T : Object
        {
            EnsureInitialization();

            string[] split = key.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
            if (ArrayUtils.IsNullOrEmpty(split))
            {
                Debug.LogError(LogName+$"{nameof(GetTemplate)} key {key} is invalid!");
                return null;
            }
            
            foreach (var loadedTemplate in loadedTemplates)
            {
                var loadedIdentifier = loadedTemplate.identifier;
                if (loadedIdentifier != split[0] && (!string.IsNullOrEmpty(loadedIdentifier) || split[0] != DefaultGroupIdentifierName))
                {
                    continue;
                }
                
                var pairKey = key.TrimStart((split[0] + separator).ToCharArray());
                foreach (var pair in loadedTemplate.list)
                {
                    if (pair.Key != pairKey)
                    {
                        continue;
                    }

                    return CastUtils.ToOrDefault<T>(typeof(T) == typeof(GameObject) ? GameObjectUtils.From(pair.Value) : pair.Value);
                }
            }

            return null;
        }
       
        /// <summary>+
        /// Instantiate the template and retrieve the instance
        /// </summary>
        /// <returns></returns>
        public static T InstantiateTemplate<T>(string key) where T : Object
        {
            var template = GetTemplate<T>(key);
            Object instance = null;
            if (template != null)
            {
                instance = Instantiate(template);
            }
            
            return CastUtils.ToOrDefault<T>(typeof(T) == typeof(GameObject) ? GameObjectUtils.From(instance) : instance);
        }

        /// <summary>
        /// Retrieve a list of all stored keys
        /// </summary>
        /// <returns></returns>
        public static List<string> GetKeys()
        {
            EnsureInitialization();
            
            List<string> /*theblack*/keys = new List<string>();
            keys.Add("None");
            foreach (var loadedTemplate in loadedTemplates)
            {
                var templateIdentifier = string.IsNullOrEmpty(loadedTemplate.identifier) ? DefaultGroupIdentifierName : loadedTemplate.identifier;
                foreach (var pair in loadedTemplate.list)
                {
                    var pairKey = pair.Key;
                    if (!string.IsNullOrEmpty(pairKey))
                    {
                        ArrayUtils.AddIfNew(keys, $"{templateIdentifier}{separator}{pairKey}");
                    }
                }
            }

            return keys;
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomEditor(typeof(TemplatesHolderAsset), true)]
        public class TemplatesHolderAssetEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                
                EditorGUILayoutUtils.DrawHeader("Commands:");
                EditorGUILayoutUtils.DrawMiniButton("Save", () =>
                {
                    if (target is TemplatesHolderAsset templatesHolderAsset)
                    {
                        templatesHolderAsset.Save();
                    }
                });
            }

            private void OnDisable()
            {
                if (target is TemplatesHolderAsset templatesHolderAsset)
                {
                    templatesHolderAsset.Save();
                }
            }
        }
#endif
        #endregion
        
    }
}

    