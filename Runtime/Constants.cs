using UnityEngine;
using UnityEngine.UI;

namespace CippSharp.Core.Containers
{
    public static class Constants
    {
        public const HideFlags underTheHoodFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        
        /// <summary>
        /// Unity's default name for script property.
        /// </summary>
        public const string ScriptSerializedPropertyName = "m_Script";

        /// <summary>
        /// Unity's default name for lightmap parameters property.
        /// </summary>
        public const string LightmapParametersSerializedPropertyName = "m_LightmapParameters";

        /// <summary>
        /// Unity's default name for gameObject's component property.
        /// </summary>
        public const string ComponentSerializedPropertyName = "m_Component";

        /// <summary>
        /// UnityEditor's default name for inspected objects local identfier. 
        /// </summary>
        public const string LocalIdentfierInFilePropertyName = "m_LocalIdentfierInFile";
        
        /// <summary>
        /// Unity's default name for enabled property.
        /// </summary>
        public const string EnabledPropertyName = nameof(Behaviour.enabled);

        /// <summary>
        /// Unity's default name for text property.
        /// </summary>
        public const string TextPropertyName = nameof(Text.text);

        
        /// <summary>
        /// Priority for MenuItems Methods, to create a separator in menu items you need to add 11 from other menu item
        /// </summary>
        public const int MenuItemMethodPriority = 1;
        
        /// <summary>
        /// Priority for MenuItems Wizards, to create a separator in menu items you need to add 11 from other menu item 
        /// </summary>
        public const int MenuItemWizardPriority = 12;
        
        /// <summary>
        /// Priority for MenuItems Windows, to create a separator in menu items you need to add 11 from other menu item
        /// </summary>
        public const int MenuItemWindowPriority = 23;
        
        
        /// <summary>
        /// VectorX.x
        /// </summary>
        public const string x = "x";
        
        /// <summary>
        /// VectorX.y
        /// </summary>
        public const string y = "y";

        /// <summary>
        /// VectorX.z
        /// </summary>
        public const string z = "z";

        /// <summary>
        /// Close word
        /// </summary>
        public const string Close = "Close";
        
        /// <summary>
        /// Select word
        /// </summary>
        public const string Select = "Select";

        /// <summary>
        /// CippSharp's missing script.
        /// </summary>
        public const string MissingScriptName = "MissingScript";

        /// <summary>
        /// Unity serialized property array element part path.
        /// </summary>
        public const string Array = "Array";

        /// <summary>
        /// Unity's transform component name
        /// </summary>
        public const string TransformComponentName = nameof(Transform);
        

        /// <summary>
        /// Debug word / script section
        /// </summary>
        public const string Debug = "Debug";
        
        /// <summary>
        /// Functionality word / script section
        /// </summary>
        public const string Functionality = "Functionality";
        
        /// <summary>
        /// Settings word / script section
        /// </summary>
        public const string Settings = "Settings";
        
        /// <summary>
        /// Commands word / script section
        /// </summary>
        public const string Commands = "Commands";
        
        /// <summary>
        /// References word / script section
        /// </summary>
        public const string References = "References";
        
        /// <summary>
        /// Infos word / script section
        /// </summary>
        public const string Infos = "Infos";
        
        /// <summary>
        /// Events word / script section
        /// </summary>
        public const string Events = "Events";


        /// <summary>
        /// Initialize method name for some classes
        /// </summary>
        public const string InitializeMethodName = "Initialize";

    }
}
