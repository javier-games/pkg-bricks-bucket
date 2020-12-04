using UnityEditor;
using UnityEngine;

namespace Framework.Generics.Editor
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
        
        public void OnWizardCreate()
        {
            HardwiredWriter.CreateClasses(path + folderName, nameSpace);
        }
    }
}