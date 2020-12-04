using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable FormatStringProblem

namespace Framework.Generics
{
    public static class Registrator
    {
        /// <summary>
        /// Writer for RegisteredTypes.cs
        /// </summary>
        private static StreamWriter _writer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="registeredTypesClass"></param>
        public static void RegisterType(
            Type type,
            RegisteredTypes registeredTypesClass)
        {
            if (string.IsNullOrWhiteSpace(registeredTypesClass.Path))
            {
                Debug.Log(registeredTypesClass.NameSpace);
                return;
            }
            
            //  Local Variables.
            var registeredTypes = new List<Type>();

            //  Saving current types list.
            foreach (var oldType in registeredTypesClass.Array)
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
                registeredTypesClass.Path + "/" +
                DynamicValuesWizard.DynVarClassName +
                DynamicValuesWizard.Extension;
            
            _writer?.Close();
            _writer = null;
            _writer = new StreamWriter(
                path,
                false
            );

            var content = string.Format(
                Template,
                DynamicValuesWizard.BricksBucketNameSpace,
                registeredTypesClass.NameSpace,
                $"{DateTime.Now:F}",
                GetTypes(registeredTypes),
                GetSetDictionary(registeredTypes),
                GetGGetDictionary(registeredTypes)
            );

            _writer.Write(content);
            _writer.Close();
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
                    subContent += string.Format(
                        ActionTemplate,
                        propertiesInfo[j].Name,
                        registeredTypes[i],
                        propertiesInfo[j].Name,
                        propertiesInfo[j].PropertyType
                    );
                    propertiesAdded++;
                    if (propertiesAdded < propertiesToAdd)
                        subContent += ",";
                }

                //  Writing Region Component.
                setDictionary += string.Format(
                    ActionRegionTemplate,
                    registeredTypes[i],
                    registeredTypes[i],
                    subContent
                );
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
                    subContent += string.Format(
                        FunctionTemplate,
                        propertiesInfo[j].Name,
                        registeredTypes[i],
                        propertiesInfo[j].Name
                    );
                    propertiesAdded++;
                    if (propertiesAdded < propertiesToAdd)
                        subContent += ",";
                }

                //  Writing Region Component.
                getDictionary += string.Format(
                    FunctionRegionTemplate,
                    registeredTypes[i],
                    registeredTypes[i],
                    subContent
                );
                if (i < registeredTypes.Count - 1)
                    getDictionary += ",\n";
            }

            return getDictionary;
        }

        #region Private Consts

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

namespace {1}
{
	/// <summary>
	/// Registered types.
	/// 
	/// Since iOS cannot support System.Reflection, DynRef has to
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
	/// Code generated on {2}
	/// ----------------------------------------------------------
	///
	/// By Javier García.
	/// </summary>
    public sealed class RegisteredTypes : {0}.RegisteredTypes
	{
		/// <inheritdoc cref=""{0}.RegisteredTypes.Path""/>
		public new readonly string NameSpace = ""{1}"";

        /// <inheritdoc cref=""{0}.RegisteredTypes.Path""/>
        public new readonly string Path = ""LISTA"";

		/// <inheritdoc cref=""{0}.RegisteredTypes.TypesList""/>
        private new static readonly List<Type> TypesList = new List<Type>
            {
                {3}
            };


		/// <inheritdoc cref=""{0}.RegisteredTypes.Set""/>
        private new static readonly
			Dictionary<stringValue, Dictionary<stringValue, Action<object, object>>> Set =
				new Dictionary<stringValue,
					Dictionary<stringValue, Action<object, object>>>
                {
                    {4}
				};

		/// <inheritdoc cref=""{0}.RegisteredTypes.Get""/>
        private new static readonly
			Dictionary<stringValue, Dictionary<stringValue, Func<object, object>>> Get =
				new Dictionary<stringValue, Dictionary<stringValue, Func<object, object>>>
                {
                    {5}
                };
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
        private const string ActionRegionTemplate =
            @"                
                    {
						#region {0}

                        ""{1}"",
                        new Dictionary<stringValue, Action<object, object>>
                        {
                            {2}
                        }

                        #endregion
                    }";
        
        /// <summary>
        /// 
        /// </summary>
        private const string ActionTemplate =
            @"
                            {
                                ""{0}"",
                                (component, value) => (component as {1}).{2} =
                                    ({3}) value
                            }";
        
        /// <summary>
        /// 
        /// </summary>
        private const string FunctionRegionTemplate =
            @"                
                    {
						#region {0}

                        ""{1}"",
                        new Dictionary<stringValue, Func<object, object>>
                        {
                            {2}
                        }

                        #endregion
                    }";
        
        /// <summary>
        /// 
        /// </summary>
        private const string FunctionTemplate =
            @"
                            {
                                ""{0}"",
                                (component) => (component as {1}).{2}
                            }";

        #endregion
        
        
    }
}