using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEditor;

namespace BricksBucket.Utils
{
    /// <summary>
    ///
    /// Compiler Enum.
    ///
    /// <para>
    /// Defines the different compiler types to Defined Symbols
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityDefineManager by @karl-.
    /// https://github.com/karl-/UnityDefineManager
    /// </para>
    ///
    /// </summary>
    public static class ScriptingDefineUtils
	{

        #region Constants

        /*
         *  Constants from unity.
         *  Do not modify this.
         *  http://forum.unity3d.com/threads/93901-global-define/page2
         *
         */
        const string _csharp_path = "Assets/csc.rsp";
        const string _editor_path = "Assets/gmcs.rsp";

        const string _assets_folder = "Assets";
        const string _script_extension = "*.cs";
        const string _meta_extension = ".meta";
        const string _define_declaration = "-define:";
        const string _semicolon_string = ";";
        const char _semicolon_char = ';';

        #endregion



        #region Public Static Methods

        /// <summary> Get the defined symbols. </summary>
        /// <param name="compiler"></param>
        /// <returns> Array of defined symbols. </returns>
        public static string[] GetDefines(Compiler compiler)
		{
			if (compiler == Compiler.CSharp)
				return ParseRspFile(_csharp_path);
			if(compiler == Compiler.Editor)
				return ParseRspFile(_editor_path);

			return null;
		}

        /// <summary> Modifies the current defined symbols. </summary>
        /// <param name="compiler"></param>
        /// <param name="defs"></param>
		public static void SetDefines(Compiler compiler, string[] defs)
		{
			switch (compiler)
			{
				case Compiler.CSharp:
					WriteDefines(_csharp_path, defs);
					break;

				case Compiler.Editor:
					WriteDefines(_editor_path, defs);
					break;
			}

			string first = Directory.GetFiles(
                path: _assets_folder,
                searchPattern: _script_extension,
                searchOption: SearchOption.AllDirectories
            ).FirstOrDefault();

			if(!string.IsNullOrEmpty(first))
				AssetDatabase.ImportAsset(first);
		}

        /// <summary> Parse defined symbols from main file. </summary>
        /// <param name="path"></param>
        /// <returns></returns>
		public static string[] ParseRspFile(string path)
		{
			if (!File.Exists(path))
				return new string[0];

			string[] lines = File.ReadAllLines(path);
			List<string> defs = new List<string>();

			foreach (string line in lines)
				if (line.StartsWith(
                    value: _define_declaration,
                    comparisonType: System.StringComparison.Ordinal))
					defs.AddRange(
                        line.Replace(
                            oldValue: _define_declaration,
                            newValue: string.Empty
                        ).Split(_semicolon_char)
                    );

			return defs.ToArray();
		}

        /// <summary> Write current defines to the main file. </summary>
        /// <param name="path"></param>
        /// <param name="defs"></param>
		public static void WriteDefines(string path, string[] defs)
		{
			if (defs == null || (defs.Length < 1 && File.Exists(path)))
			{
				File.Delete(path);
				File.Delete(path + _meta_extension);
				AssetDatabase.Refresh();
				return;
			}

			StringBuilder sb = new StringBuilder();

			sb.Append(_define_declaration);

			for (int i = 0; i < defs.Length; i++)
			{
				sb.Append(defs[i]);
				if (i < defs.Length - 1)
                    sb.Append(_semicolon_string);
			}

			using (StreamWriter writer = new StreamWriter(path, false))
			{
				writer.Write(sb);
			}
		}

        #endregion
    }
}
