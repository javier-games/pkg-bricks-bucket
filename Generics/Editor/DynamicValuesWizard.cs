using UnityEditor;
using UnityEngine;

namespace Framework.Generics
{
    public class DynamicValuesWizard : ScriptableWizard
    {
        [MenuItem("Tools/BricksBucket/Create Custom Dynamic Values")]
        public static void CreateWizard()
        {
            DisplayWizard<DynamicValuesWizard>(
                "Create Custom Dynamic Values",
                "Create",
                "Cancel"
            );
        }

        [SerializeField]
        private string path = "";
        
        [SerializeField]
        private string folderName = "Custom Values";

        [SerializeField]
        private string nameSpace = "MyNameSpace";
        
        
        public const string BricksBucketNameSpace = "Framework.Generics";
        public const string DynVarClassName = "DynVariable";
        public const string DynRefClassName = "AbstractReference";
        public const string RegisteredTypesClassName = "AbstractHardwired";
        public const string Extension = ".cs";

        
        public void OnWizardCreate()
        {
            
        }
        
        
    }
}