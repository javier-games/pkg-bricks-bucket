namespace BricksBucket.Utils
{
    /// <summary>
    ///
    /// LogLayer Enum.
    ///
    /// <para>
    /// Defines the different logs layer to be filtered.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public enum LogLayer
    {
        Debug = 0,      //  Layer for regular debug content.
        Physics = 1,    //  Layer for game physics.
        Graphics = 2,   //  Layer for 2D and 3D graphics or shaders.
        Logistics = 3,  //  Layer for logistics systems.
        Interface = 4,  //  Layer for UI/UX related data.
        Mechanics = 5,  //  Layer for game mechanics and dynamics.
        Services = 6,   //  Layer for external services implementations.
        DataBase = 7,   //  Layer for data base info.
        Network = 8,    //  Layer for network info.
        Internal = 9,   //  Layer for BrickBucket Scripts.
    };

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
    public enum Compiler
    {
        CSharp = 0,     //  Global define symbols for C# scripts.
        Editor = 1,     //  Global define symbols for Editor scripts.
        Platform = 2    //  Global define symbols per platform.
    }
}