namespace BricksBucket.Localization
{
	// ISO 3166 Part.
	// By Javier García | @jvrgms | 2020
	public static partial class Standards
	{
		/// <summary>
		/// Gets the name of the given ISO 3166 enum value.
		/// </summary>
		/// <param name="country">ISO 3166 to look for its name.</param>
		/// <returns>Empty if the ISO 3166 value has not a name.</returns>
		public static string GetName (Iso3166 country) =>
			GetName ((Unm49) (int) country);

		/// <summary>
		/// Gets the code of the given ISO 3166 enum value.
		/// </summary>
		/// <param name="country">ISO 3166 to look for its code.</param>
		/// <returns>Empty if the ISO 3166 value has not a code.</returns>
		public static string GetCode (Iso3166 country) =>
			System.Enum.IsDefined (typeof (Iso3166), country)
				? country.ToString ()
				: string.Empty;
		
		/// <summary>
		/// Gets the ISO 3166 enum value for the given string code.
		/// </summary>
		/// <param name="code">String value of the code of an ISO 3166.</param>
		/// <returns><value>Iso3166.NONE</value> if a code is not found.
		/// </returns>
		public static Iso3166 GetIso3166 (string code) =>
			System.Enum.TryParse (code, true, out Iso3166 country)
				? country
				: Iso3166.NONE;
		
		/// <summary>
		/// Gets the UN M49 enum value for the given ISO 3166 code.
		/// </summary>
		/// <param name="country">ISO 3166 to look for its region.</param>
		/// <returns><value>Unm49.NONE</value> if a code is not found.
		/// </returns>
		public static Unm49 GetUnm49 (Iso3166 country) =>
			GetUnm49 ((int) country);
	}
}