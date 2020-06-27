namespace BricksBucket.Core 
{
    /// <summary>
    ///
    /// Round Approximation Type Enum.
    ///
    /// <para>
    /// Defines the different approximation types.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public enum RoundType
    {
        Round = 0,      //  Rounded to the nearest integer.
        Ceil = 1,       //  Smallest integer greater or equal.
        Floor = 2       //  Grater integer smallest or equal.
    }
}
