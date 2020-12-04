using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Framework.Generics.Editor
{
    public static class HardwiredWriter
    {
        private const string BricksBucketNameSpace = "Framework.Generics";
        private const string DynRefClassName = "DynReference";
        private const string RegisteredTypesClassName = "Hardwired";
        private const string Extension = ".cs";
        
        /// <summary>
        /// Writer for AbstractHardwired.cs
        /// </summary>
        private static StreamWriter _writer;

        public static void CreateClasses(string localPath,  string nameSpace)
        {
            var hardwiredPath =
                 "/" + localPath + "/" +
                RegisteredTypesClassName +
                Extension;
            
            if (!File.Exists(hardwiredPath))
            {
                ConstructPath(hardwiredPath);
            }
            
            _writer?.Close();
            _writer = null;
            _writer = new StreamWriter(
                Application.dataPath + hardwiredPath,
                false
            );

            var content = Template
                .Replace(
                    "{OLD_NAMESPACE}", BricksBucketNameSpace)
                .Replace("{NEW_NAMESPACE}", nameSpace)
                .Replace(
                    "{REGISTRY_CLASS_NAME}", RegisteredTypesClassName)
                .Replace(
                    "{REFERENCE_CLASS_NAME}", DynRefClassName)
                .Replace("{PATH}", localPath)
                .Replace("{DATE}", $"{DateTime.Now:F}")
                .Replace("{TYPES}", string.Empty)
                .Replace("{ACTIONS}", string.Empty)
                .Replace("{FUNCTIONS}", string.Empty);

            _writer.Write(content);
            _writer.Close();
            
            var editorPath = 
                "/" + localPath + "/Editor/" +
                DynRefClassName + "Drawer" +
                Extension;
            
            
            if (!File.Exists(editorPath))
            {
                ConstructPath(editorPath);
            }

            _writer?.Close();
            _writer = null;
            _writer = new StreamWriter(
                Application.dataPath + editorPath,
                false
            );

            content = EditorTemplate
                .Replace(
                    "{OLD_NAMESPACE}", BricksBucketNameSpace)
                .Replace("{NEW_NAMESPACE}", nameSpace)
                .Replace(
                    "{REFERENCE_CLASS_NAME}", DynRefClassName);

            _writer.Write(content);
            _writer.Close();
            
            AssetDatabase.Refresh();
        }

        private static void ConstructPath(string localPath)
        {
            var directories = ("Assets/" + localPath).Split('/');
            var limit = directories.Length > 0
                    ? (directories[directories.Length - 1].Contains(".")
                        ? directories.Length - 1
                        : directories.Length)
                    : 0;

            var path = string.Empty;
            for (var i = 0; i < limit; i++)
            {
                path += directories[i];

                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }
                
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path += "/";
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="hardwiredClass"></param>
        public static void RegisterType(
            Type type,
            IHardwiredRegistry hardwiredClass)
        {
            
            if (string.IsNullOrWhiteSpace(hardwiredClass.Path))
            {
                return;
            }

            //  Local Variables.
            var registeredTypes = new List<Type>();

            //  Saving current types list.
            foreach (var oldType in hardwiredClass.Array)
            {
                registeredTypes.Add(oldType);
            }

            if (type != null && !registeredTypes.Contains(type))
                registeredTypes.Add(type);
            registeredTypes.Sort((x, y) => string.Compare(
                x.FullName,
                y.FullName,
                StringComparison.InvariantCulture
            ));
            
            //  Initializing a new writer.
            var path =
                Application.dataPath + "/" +
                hardwiredClass.Path + "/" +
                RegisteredTypesClassName +
                Extension;
            
            _writer?.Close();
            _writer = null;
            _writer = new StreamWriter(
                path,
                false
            );

            var oldNamespace = BricksBucketNameSpace;
            var registryClassName = RegisteredTypesClassName;
            var referenceClassName = DynRefClassName;
            var content = Template
                .Replace("{OLD_NAMESPACE}",oldNamespace)
                .Replace("{NEW_NAMESPACE}", hardwiredClass.NameSpace)
                .Replace("{REGISTRY_CLASS_NAME}", registryClassName)
                .Replace("{REFERENCE_CLASS_NAME}", referenceClassName)
                .Replace("{PATH}", hardwiredClass.Path)
                .Replace("{DATE}", $"{DateTime.Now:F}")
                .Replace("{TYPES}", GetTypes(registeredTypes))
                .Replace("{ACTIONS}", GetSetDictionary(registeredTypes))
                .Replace("{FUNCTIONS}", GetGGetDictionary(registeredTypes));

            _writer.Write(content);
            _writer.Close();
            
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredTypes"></param>
        /// <returns></returns>
        private static string GetTypes(IReadOnlyList<Type> registeredTypes)
        {
            var typesList = string.Empty;
            for (var i = 0; i < registeredTypes.Count; i++)
            {
                typesList += string.Format(TypeElement,
                    registeredTypes[i]);
                if (i < registeredTypes.Count - 1)
                    typesList += ",\n";
            }

            return typesList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredTypes"></param>
        /// <returns></returns>
        private static string GetSetDictionary(
            IReadOnlyList<Type> registeredTypes)
        {
            var setDictionary = string.Empty;

            for (var i = 0; i < registeredTypes.Count; i++)
            {

                var subContent = string.Empty;
                var propertiesInfo = registeredTypes[i].GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                );
                var propertiesToAdd = 0;
                foreach (var propertyInfo in propertiesInfo)
                    if (
                        propertyInfo.CanRead &&
                        propertyInfo.CanWrite &&
                        !propertyInfo.IsDefined(typeof(ObsoleteAttribute), true)
                    )
                        propertiesToAdd++;
                var propertiesAdded = 0;

                //  Writing Properties.
                for (var j = 0; j < propertiesInfo.Length; j++)
                {
                    if (!propertiesInfo[j].CanRead ||
                        !propertiesInfo[j].CanWrite || propertiesInfo[j]
                            .IsDefined(typeof(ObsoleteAttribute), true))
                        continue;
                    subContent += ActionTemplate
                        .Replace("{PROPERTY_NAME}", propertiesInfo[j].Name)
                        .Replace("{OBJECT_NAME}", registeredTypes[i].ToString())
                        .Replace("{PROPERTY_TYPE}",
                            propertiesInfo[j].PropertyType.ToString());
                    
                    propertiesAdded++;
                    if (propertiesAdded < propertiesToAdd)
                        subContent += ",";
                }

                //  Writing Region Component.
                setDictionary += ActionRegionTemplate
                    .Replace("{OBJECT_NAME}", registeredTypes[i].ToString())
                    .Replace("{ACTIONS}", subContent);
                
                if (i < registeredTypes.Count - 1)
                    setDictionary += ",\n";
            }

            return setDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredTypes"></param>
        /// <returns></returns>
        private static string GetGGetDictionary(
            IReadOnlyList<Type> registeredTypes)
        {
            var getDictionary = string.Empty;
            for (int i = 0; i < registeredTypes.Count; i++)
            {

                var subContent = string.Empty;
                var propertiesInfo = registeredTypes[i].GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                );
                var propertiesToAdd = 0;
                foreach (PropertyInfo propertyInfo in propertiesInfo)
                    if (
                        propertyInfo.CanRead &&
                        propertyInfo.CanWrite &&
                        !propertyInfo.IsDefined(typeof(ObsoleteAttribute), true)
                    )
                        propertiesToAdd++;
                var propertiesAdded = 0;

                //  Writing Properties.
                for (var j = 0; j < propertiesInfo.Length; j++)
                {
                    if (!propertiesInfo[j].CanRead ||
                        !propertiesInfo[j].CanWrite || propertiesInfo[j]
                            .IsDefined(typeof(ObsoleteAttribute), true))
                        continue;
                    
                    subContent += FunctionTemplate
                        .Replace("{PROPERTY_NAME}", propertiesInfo[j].Name)
                        .Replace("{OBJECT_NAME}", registeredTypes[i].ToString());
                    
                    propertiesAdded++;
                    if (propertiesAdded < propertiesToAdd)
                        subContent += ",";
                }

                //  Writing Region Component.
                getDictionary += FunctionRegionTemplate
                    .Replace("{OBJECT_NAME}", registeredTypes[i].ToString())
                    .Replace("{FUNCTIONS}", subContent);
                    
                if (i < registeredTypes.Count - 1)
                    getDictionary += ",\n";
            }

            return getDictionary;
        }

        #region Private Consts

        private const string EditorTemplate = @"
using UnityEditor;

namespace {NEW_NAMESPACE}
{
    [CustomPropertyDrawer(typeof({REFERENCE_CLASS_NAME}))]
    public class {REFERENCE_CLASS_NAME}Drawer :
        {OLD_NAMESPACE}.Editor.DynRefDrawer <{REFERENCE_CLASS_NAME}>{ }
}
";

        /// <summary>
        /// 
        /// </summary>
        private const string Template = @"
using System;
using System.Collections.Generic;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable BuiltInTypeReferenceStyle
// ReSharper disable UnusedMember.Local
// ReSharper disable RedundantNameQualifier

namespace {NEW_NAMESPACE}
{
	/// <summary>
	/// Registered types.
	/// 
	/// Since iOS cannot support System.Reflection, AbstractReference has to
	/// have this static class to cast values.
	/// 
	/// 
	/// <autogenerated>
	/// 
	/// This code was generated by a tool.
	/// Changes to this file may cause incorrect behavior and will
	/// be lost if the code is regenerated.
	/// 
	/// </autogenerated>
	/// 
	/// ----------------------------------------------------------
	/// Code generated on {DATE}
	/// ----------------------------------------------------------
	///
	/// By Javier García.
	/// </summary>
    public sealed class {REGISTRY_CLASS_NAME} : {OLD_NAMESPACE}.AbstractHardwired
	{
		/// <inheritdoc cref=""{OLD_NAMESPACE}.AbstractHardwired.NameSpace""/>
		public override string NameSpace => ""{NEW_NAMESPACE}"";

        /// <inheritdoc cref=""{OLD_NAMESPACE}.AbstractHardwired.Path""/>
        public override string Path => ""{PATH}"";

		/// <inheritdoc cref=""{OLD_NAMESPACE}.AbstractHardwired.TypesList""/>
        protected override List<Type> TypesList { get; } = new List<Type>
		{
            {TYPES}
        };

		/// <inheritdoc cref=""{OLD_NAMESPACE}.AbstractHardwired.Set""/>
        protected override
			Dictionary<string, Dictionary<string, Action<object, object>>> Set
		{
			get;
		} = new Dictionary<string, Dictionary<string, Action<object, object>>>
        {
            {ACTIONS}
		};

		/// <inheritdoc cref=""{OLD_NAMESPACE}.AbstractHardwired.Get""/>
        protected override
			Dictionary<string, Dictionary<string, Func<object, object>>> Get
		{
			get;
		} = new Dictionary<string, Dictionary<string, Func<object, object>>>
		{
            {FUNCTIONS}
        };
	}

	[System.Serializable]
	public sealed class {REFERENCE_CLASS_NAME} :
		{OLD_NAMESPACE}.AbstractReference<{REGISTRY_CLASS_NAME}>
	{
		private static {REGISTRY_CLASS_NAME} _hardwired = new {REGISTRY_CLASS_NAME}();
        
		public override {OLD_NAMESPACE}.IHardwiredRegistry Hardwired => 
			_hardwired ?? (_hardwired = new {REGISTRY_CLASS_NAME}());
	}
}";
        
        /// <summary>
        /// 
        /// </summary>
        private const string TypeElement = @"
            typeof({0})";
        
        /// <summary>
        /// 
        /// </summary>
        private const string ActionRegionTemplate = @"                
            {
				#region {OBJECT_NAME}

                ""{OBJECT_NAME}"",
                new Dictionary<string, Action<object, object>>
                {
                    {ACTIONS}
                }

                #endregion
            }";
        
        /// <summary>
        /// 
        /// </summary>
        private const string ActionTemplate = @"
                    {
                        ""{PROPERTY_NAME}"",
                        (component, value) =>
                            (component as {OBJECT_NAME}).{PROPERTY_NAME} =
                                ({PROPERTY_TYPE}) value
                    }";
        
        /// <summary>
        /// 
        /// </summary>
        private const string FunctionRegionTemplate = @"                
            {
				#region {OBJECT_NAME}

                ""{OBJECT_NAME}"",
                new Dictionary<string, Func<object, object>>
                {
                    {FUNCTIONS}
                }

                #endregion
            }";
        
        /// <summary>
        /// 
        /// </summary>
        private const string FunctionTemplate = @"
                    {
                        ""{PROPERTY_NAME}"",
                        (component) =>
                            (component as {OBJECT_NAME}).{PROPERTY_NAME}
                    }";

        #endregion
        
        
    }
}