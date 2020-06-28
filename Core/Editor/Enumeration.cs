namespace BricksBucket 
{
    /// <summary>
    ///
    /// Compiler Enum.
    ///
    /// <para>
    /// Defines the different compiler types to Defined Symbols.
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
    public enum Compiler
    {
        CSharp = 0,     //  Global define symbols for C# scripts.
        Editor = 1,     //  Global define symbols for Editor scripts.
        Platform = 2    //  Global define symbols per platform.
    }
}
