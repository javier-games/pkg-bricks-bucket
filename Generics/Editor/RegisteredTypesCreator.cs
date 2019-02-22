﻿using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace Framework.Generics {

    #if UNITY_EDITOR
    /// <summary>
    /// Registered types creator.
    ///
    /// This script helps to register a new type in the RegisteredTypes.cs.
    /// By Javier García.
    /// </summary>
    public static class RegisteredTypesCreator {

        /// <summary> Writer for RegisteredTypes.cs </summary>
        private static StreamWriter writer;

        /// <summary> Registers a new type. </summary>
        /// <param name="type">Type.</param>
        public static void RegisterType (Type type) {


            //  Local Variables.
            List<Type> registeredTypes = new List<Type> ();
            PropertyInfo [] propertiesInfo = null;
            string localPath = string.Empty;
            string content = string.Empty;
            string subcontent = string.Empty;
            int propertiesToAdd = 0;
            int propertiesAdded = 0;


            //  Saving current types list.
            foreach (Type oldType in RegisteredTypes.Array) {
                registeredTypes.Add (oldType);
            }
            if (type != null && !registeredTypes.Contains (type))
                registeredTypes.Add (type);
            registeredTypes.Sort ((x, y) => string.Compare (
                x.FullName,
                y.FullName,
                StringComparison.CurrentCulture
            ));


            //  Initializing a new writer.
            if (writer != null)
                writer.Close ();
            writer = null;
            localPath = UnityEngine.Application.dataPath + "/Scripts/Framework/Generics/RegisteredTypes.cs";
            writer = new StreamWriter (localPath, false);


            #region Summary

            writer.WriteLine (System.String.Concat (
                "using System;",
                "\nusing System.Collections.Generic;",
                "\n",
                "\n/// <summary>",
                "\n/// Registered types.",
                "\n/// ",
                "\n/// Since iOS cannot support System.Reflection, DynRef has to",
                "\n/// have this static class to cast values.",
                "\n/// ",
                "\n/// ",
                "\n/// <autogenerated>",
                "\n/// ",
                "\n/// This code was generated by a tool.",
                "\n/// Changes to this file may cause incorrect behavior and will",
                "\n/// be lost if the code is regenerated.",
                "\n/// ",
                "\n/// </autogenerated>",
                "\n/// ",
                "\n/// ----------------------------------------------------------"
            ));
            writer.Write (System.String.Concat (
                "/// Code generated on ", string.Format ("{0:F}", DateTime.Now)
            ));
            writer.Write (System.String.Concat (
                "\n/// ----------------------------------------------------------",
                "\n///",
                "\n/// By Javier García.",
                "\n/// </summary>"
            ));

            #endregion


            //  Initializing Class
            writer.WriteLine ("\npublic static class RegisteredTypes {");


            #region Types List

            writer.WriteLine (System.String.Concat (
                "\n\t/// <summary> Quickly view of all registered types. </summary>",
                "\n\tprivate static readonly List<Type> _typesList = new List<Type>{"
            ));
            content = string.Empty;
            for (int i = 0; i < registeredTypes.Count; i++) {
                content += System.String.Concat (
                    "\t\ttypeof(", registeredTypes [i].ToString (), ")"
                );
                if (i < registeredTypes.Count - 1)
                    content += ",\n";
            }
            writer.Write (System.String.Concat (content, "\n\t};"));

            #endregion


            #region Set Dictionary

            writer.WriteLine (System.String.Concat (
                "\n\n\t/// <summary> Dictionary of actions to set values. </summary>",
                "\n\tprivate static readonly",
                "\n\tDictionary<string, Dictionary<string, Action<object, object>>> _set =",
                "\n\t\tnew Dictionary<string, Dictionary<string, Action<object, object>>> {"
            ));
            content = string.Empty;
            for (int i = 0; i < registeredTypes.Count; i++) {

                subcontent = string.Empty;
                propertiesInfo = registeredTypes [i].GetProperties (
                    BindingFlags.Public | BindingFlags.Instance
                );
                propertiesToAdd = 0;
                foreach (PropertyInfo propertyInfo in propertiesInfo)
                    if (
                        propertyInfo.CanRead &&
                        propertyInfo.CanWrite &&
                        !propertyInfo.IsDefined (typeof (ObsoleteAttribute), true)
                    )
                        propertiesToAdd++;
                propertiesAdded = 0;

                //  Writing Properties.
                for (int j = 0; j < propertiesInfo.Length; j++) {
                    if (
                        propertiesInfo [j].CanRead &&
                        propertiesInfo [j].CanWrite &&
                        !propertiesInfo [j].IsDefined (typeof (ObsoleteAttribute), true)
                    ) {
                        subcontent += System.String.Concat (
                            "\n\t\t\t\t{\"",
                            propertiesInfo [j].Name,
                            "\", (component, value) => (component as ",
                            registeredTypes [i].ToString (),
                            ").",
                            propertiesInfo [j].Name,
                            " = (",
                            propertiesInfo [j].PropertyType,
                            ")value}"
                        );
                        propertiesAdded++;
                        if (propertiesAdded < propertiesToAdd)
                            subcontent += ",";
                    }
                }

                //  Writing Region Component.
                content += System.String.Concat (
                    "\t\t{",
                    "\n\t\t\t#region ", registeredTypes [i].ToString (),
                    "\n\t\t\t\"", registeredTypes [i].ToString (), "\",",
                    "\n\t\t\tnew Dictionary<string, Action<object, object>>{",
                    subcontent,
                    "\n\t\t\t}",
                    "\n\t\t\t#endregion",
                    "\n\t\t}"
                );
                if (i < registeredTypes.Count - 1)
                    content += ",\n";
            }
            writer.Write (System.String.Concat (content, "\n\t};"));

            #endregion


            #region Get Dictionary

            writer.WriteLine (System.String.Concat (
                "\n\n\t/// <summary> Dictionary of functions to return values. </summary>",
                "\n\tprivate static readonly",
                "\n\tDictionary<string, Dictionary<string, Func<object, object>>> _get =",
                "\n\t\tnew Dictionary<string, Dictionary<string, Func<object, object>>> {"
            ));

            content = string.Empty;
            for (int i = 0; i < registeredTypes.Count; i++) {

                subcontent = string.Empty;
                propertiesInfo = registeredTypes [i].GetProperties (
                    BindingFlags.Public | BindingFlags.Instance
                );
                propertiesToAdd = 0;
                foreach (PropertyInfo propertyInfo in propertiesInfo)
                    if (
                        propertyInfo.CanRead &&
                        propertyInfo.CanWrite &&
                        !propertyInfo.IsDefined (typeof (ObsoleteAttribute), true)
                    )
                        propertiesToAdd++;
                propertiesAdded = 0;

                //  Writing Properties.
                for (int j = 0; j < propertiesInfo.Length; j++) {
                    if (
                        propertiesInfo [j].CanRead &&
                        propertiesInfo [j].CanWrite &&
                        !propertiesInfo [j].IsDefined (typeof (ObsoleteAttribute), true)
                    ) {
                        subcontent += System.String.Concat (
                            "\n\t\t\t\t{\"",
                            propertiesInfo [j].Name,
                            "\", (component) => (component as ",
                            registeredTypes [i].ToString (),
                            ").",
                            propertiesInfo [j].Name,
                            "}"
                        );
                        propertiesAdded++;
                        if (propertiesAdded < propertiesToAdd)
                            subcontent += ",";
                    }
                }

                //  Writing Region Component.
                content += System.String.Concat (
                    "\t\t{",
                    "\n\t\t\t#region ", registeredTypes [i].ToString (),
                    "\n\t\t\t\"", registeredTypes [i].ToString (), "\",",
                    "\n\t\t\tnew Dictionary<string, Func<object, object>>{",
                    subcontent,
                    "\n\t\t\t}",
                    "\n\t\t\t#endregion",
                    "\n\t\t}"
                );
                if (i < registeredTypes.Count - 1)
                    content += ",\n";
            }
            writer.Write (System.String.Concat (content, "\n\t};"));

            #endregion


            #region Accessors and Methods

            writer.WriteLine (System.String.Concat (
                "\n\n\t/// <summary>  The array of registered types. </summary>",
                "\n\t/// <value>The array.</value>",
                "\n\tpublic static Type[] Array {",
                "\n\t\tget { return _typesList.ToArray (); }",
                "\n\t}",
                "\n\t",
                "\n\t/// <summary> Contains the component. </summary>",
                "\n\t/// <returns><c>true</c>, if component is contained.</returns>",
                "\n\t/// <param name=\"component\">Component.</param>",
                "\n\tpublic static bool ContainsComponent(string component){",
                "\n\t\treturn",
                "\n\t\t_set.ContainsKey (component) &&",
                "\n\t\t_get.ContainsKey (component);",
                "\n\t}",
                "\n\t",
                "\n\t/// <summary> Contains the property. </summary>",
                "\n\t/// <returns><c>true</c>, if property is contained.</returns>",
                "\n\t/// <param name=\"component\">Component.</param>",
                "\n\t/// <param name=\"property\">Property name.</param>",
                "\n\tpublic static bool ContainsProperty(string component, string property) {",
                "\n\t\treturn",
                "\n\t\t\t_set[component].ContainsKey (property) &&",
                "\n\t\t\t_get[component].ContainsKey (property);",
                "\n\t}",
                "\n\t",
                "\n\t/// <summary> Gets the value. </summary>",
                "\n\t/// <returns>The value.</returns>",
                "\n\t/// <param name=\"component\">Component.</param>",
                "\n\t/// <param name=\"property\">Property name.</param>",
                "\n\tpublic static object GetValue(object component, string property) {",
                "\n\t\ttry {",
                "\n\t\t\treturn _get[component.GetType ().ToString ()][property] (component);",
                "\n\t\t}",
                "\n\t\tcatch(Exception exception){",
                "\n\t\t\tUnityEngine.Debug.LogError (exception);",
                "\n\t\t\treturn null;",
                "\n\t\t}",
                "\n\t}",
                "\n\t",
                "\n\t/// <summary> Sets the value. </summary>",
                "\n\t/// <param name=\"component\">Component.</param>",
                "\n\t/// <param name=\"property\">Property name.</param>",
                "\n\t/// <param name=\"value\">Value.</param>",
                "\n\tpublic static void SetValue (object component, string property, object value) {",
                "\n\t\ttry {",
                "\n\t\t\t_set[component.GetType ().ToString ()][property] (component, value);",
                "\n\t\t}",
                "\n\t\tcatch (Exception exception) {",
                "\n\t\t\tUnityEngine.Debug.LogError (exception);",
                "\n\t\t}",
                "\n\t}"
            ));
            #endregion


            //  End Class.
            writer.Write ("}");


            //  Closing writer.
            writer.Close ();
            writer = null;

            //  Recompiling assets.
            UnityEditor.AssetDatabase.Refresh ();
        }
    }
    #endif
}