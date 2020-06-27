namespace BricksBucket.Localization.Internal
{
	/// <!-- IPluralForm -->
	///
	/// <summary>
	/// Interface for a plural forms.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	internal interface IPluralForm
	{
		/// <summary>
		/// Number of plural forms.
		/// </summary>
		/// <returns>Number of different cases.</returns>
		int Count { get; }
        
		/// <summary>
		/// Evaluates the given number to return the index of its
		/// right option.
		/// </summary>
		/// <param name="n">Integer number ideally greater or equals to
		/// zero. </param>
		/// <returns>Index of the option to use between Inclusive Zero and
		/// Exclusive <see cref="Count"/>.</returns>
		int Evaluate (int n);
	}
}