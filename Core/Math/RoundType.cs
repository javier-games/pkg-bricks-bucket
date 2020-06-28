namespace BricksBucket.Core.Math 
{
    /// <!-- RoundType -->
    /// 
    /// <summary>
    /// Defines the different approximation types.
    /// </summary>
    ///
    /// <seealso cref="FloatExtensions"/>
    /// <seealso cref="DoubleExtensions"/>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    public enum RoundType
    {
        /// <summary>
        /// Rounded to the nearest integer.
        /// </summary>
        Round = 0,
        
        /// <summary>
        /// Smallest integer greater or equal.
        /// </summary>
        Ceil = 1,
        
        /// <summary>
        /// Grater integer smallest or equal.
        /// </summary>
        Floor = 2
    }
}
