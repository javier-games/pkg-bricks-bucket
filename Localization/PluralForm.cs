using System.Collections.Generic;
using BricksBucket.Localization.Internal;

namespace BricksBucket.Localization
{
	/// <!-- PluralForm -->
	/// 
	/// <summary>
	/// Part of each different language the plural form is one of the values of
	/// the grammatical category of number. This class contains the
	/// <see cref="Count" /> of different cases of plural forms a language has
	/// and the method <see cref="Evaluate" /> that returns the case in which a
	/// number is.
	/// </summary>
	/// 
	/// <seealso cref="BricksBucket.Localization.CustomPluralForm" />
	/// <seealso cref="Iso639"/>
	/// 
	/// <!-- Note: The code of the members of the enum have been generated with
	/// the following table: https://bit.ly/bb-localization-iso3166 -->
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	public class PluralForm : IPluralForm
	{
		
		#region Fields

		/// <summary>
		/// Delegate to evaluate the plural form.
		/// </summary>
		/// <param name="n">Plural number to evaluate.</param>
		public delegate int Evaluator (int n);

		/// <summary>
		/// Evaluator Delegate for de plural form.
		/// </summary>
		private Evaluator _evaluator;

		/// <summary>
		/// Code of the default plural form.
		/// </summary>
		internal static readonly int Default = 0x000;

		#endregion


		#region Properties

		/// <summary>
		/// Text representative of the rule used to choose the plural option.
		/// </summary>
		/// <returns>String value with the text rule.</returns>
		public string Rule { get; }

		/// <inheritdoc cref="IPluralForm.Count" />
		public int Count { get; }

		/// <inheritdoc cref="IPluralForm.Evaluate(int)" />
		public int Evaluate (int n)
		{
			return _evaluator (n);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Private Constructor to avoid the creation of new instances.
		/// </summary>
		private PluralForm () { }

		/// <summary>
		/// Constructor with Int and Func Signature for the dictionary.
		/// </summary>
		/// <param name="count">Count of plural forms.</param>
		/// <param name="evaluator">Delegate that implements the rule for the
		/// plural form.</param>
		/// <param name="rule"> String representation of the rule used to
		/// define the case.</param>
		internal PluralForm (int count, Evaluator evaluator, string rule)
		{
			_evaluator = evaluator;
			Count = count;
			Rule = rule;
		}

		/// <summary>
		/// Gets a pre-defined <seealso cref="PluralForm" /> for the
		/// given language.
		/// </summary>
		/// <param name="code">Plural form code to look for.</param>
		/// <returns>Whether a form for the language has been found.</returns>
		public static PluralForm GetForm (int code) =>
			PluralForms.ContainsKey (code) ? PluralForms[code] : null;

		/// <summary>
		/// Gets the count of defined plural forms.
		/// </summary>
		/// <returns>Count of defined plural forms.</returns>
		public static int DefinedPluralFormsCount () => PluralForms.Count;
		
		#endregion
		
		
		#region Static Read Only
		
		/// <summary>
		/// Dictionary that contains existing defined rules and count of its
		/// cases in plural forms.
		/// </summary>
		private static readonly Dictionary<int, PluralForm> PluralForms =
			new Dictionary<int, PluralForm>
			{
				{
					0x000, new PluralForm (2,
						n => n != 1 ? 1 : 0,
						"n != 1 ? 1 : 0")
				},
				{
					0x001, new PluralForm (1, n => 0,
						"0")
				},
				{
					0x002, new PluralForm (3,
						n => n % 10 == 1 && n % 100 != 11 ? 0 : n % 10 >= 2 &&
							n % 10 <= 4 &&
							(n % 100 < 10 || n % 100 >= 20) ? 1 : 2,
						"n % 10 == 1 && n % 100 != 11 ? 0 : n % 10 >= 2 && n " +
						"% 10 <= 4 && (n % 100 < 10 || n % 100 >= 20) ? 1 : 2")
				},
				{
					0x003, new PluralForm (3,
						n => n == 1 ? 0 : n >= 2 && n <= 4 ? 1 : 2,
						"n == 1 ? 0 : n >= 2 && n <= 4 ? 1 : 2")
				},
				{
					0x004, new PluralForm (2,
						n => n == 1 || n % 10 == 1 ? 0 : 1,
						"n == 1 || n % 10 == 1 ? 0 : 1")
				},
				{
					0x005, new PluralForm (2,
						n => n % 10 != 1 || n % 100 == 11 ? 1 : 0,
						"n % 10 != 1 || n % 100 == 11 ? 1 : 0")
				},
				{
					0x006, new PluralForm (3,
						n => n % 10 == 1 && n % 100 != 11 ? 0 : n != 0 ? 1 : 2,
						"n % 10 == 1 && n % 100 != 11 ? 0 : n != 0 ? 1 : 2")
				},
				{
					0x007, new PluralForm (3,
						n => n % 10 == 1 && n % 100 != 11 ? 0
							: n % 10 >= 2 && (n % 100 < 10 || n % 100 >= 20) ? 1
							: 2,
						"n % 10 == 1 && n % 100 != 11 ? 0 : n % 10 >= 2 &&" +
						" (n % 100 < 10 || n % 100 >= 20) ? 1 : 2")
				},
				{
					0x008, new PluralForm (3,
						n => n == 1 ? 0 : n == 0 || n % 100 > 0 && n % 100 < 20
							? 1
							: 2,
						"n == 1 ? 0 : (n == 0 || (n % 100 > 0 && n % " +
						"100 < 20)) ? 1 : 2")
				},
				{
					0x009, new PluralForm (3,
						n => n == 1 ? 0 : n % 10 >= 2 && n % 10 <= 4 &&
							(n % 100 < 10 || n % 100 >= 20) ? 1 : 2,
						"n == 1 ? 0 : n % 10 >= 2 && n % 10 <= 4 &&" +
						" (n % 100 < 10 || n % 100 >= 20) ? 1 : 2")
				},
				{
					0x00A, new PluralForm (4,
						n => n % 100 == 1 ? 0 : n % 100 == 2 ? 1
							: n % 100 == 3 || n % 100 == 4 ? 2 : 3,
						"n % 100 == 1 ? 0 : n % 100 == 2 ? 1 : n " +
						"% 100 == 3 || n % 100 == 4 ? 2 : 3")
				},
				{
					0x00B, new PluralForm (4,
						n => n == 1 ? 0 : n == 0 || n % 100 > 1 && n % 100 < 11
							? 1
							: n % 100 > 10 && n % 100 < 20 ? 2 : 3,
						"n == 1 ? 0 : n == 0 || (n % 100 > 1 && n % 100 " +
						"< 11) ? 1 : (n % 100 > 10 && n % 100 < 20) ? 2 : 3")
				},
				{
					0x00C, new PluralForm (4,
						n => n == 1 || n == 11 ? 0 : n == 2 || n == 12 ? 1
							: n > 2 && n < 20
								? 2 : 3,
						"n == 1 || n == 11 ? 0 : n == 2 || n == 12 ? 1 : " +
						"n > 2 && n < 20 ? 2 : 3")
				},
				{
					0x00D, new PluralForm (4,
						n => n == 1 ? 0 : n == 2 ? 1 : n != 8 && n != 11 ? 2
							: 3,
						"n == 1 ? 0 : n == 2 ? 1 : n != 8 && n != 11 ? 2 : 3")
				},
				{
					0x00E, new PluralForm (3,
						n => n == 1 ? 0 : n == 2 ? 1 : n == 3 ? 2 : 3,
						"n == 1 ? 0 : n == 2 ? 1 : n == 3 ? 2 : 3")
				},
				{
					0x00F, new PluralForm (5,
						n => n == 1 ? 0 : n == 2 ? 1 : n > 2 && n < 7 ? 2
							: n > 6 && n < 11
								? 3 : 4,
						"n == 1 ? 0 : n == 2 ? 1 : n > 2 && n < 7 ? 2 : n >" +
						" 6 && n < 11 ? 3 : 4")
				},
				{
					0x010, new PluralForm (6,
						n => n == 0 ? 0 : n == 1 ? 1 : n == 2 ? 2
							: n % 100 >= 3 && n % 100 <= 10 ? 3 : n % 100 >= 11
								? 4
								: 5,
						"n == 0 ? 0 : n == 1 ? 1 : n == 2 ? 2 : n % 100 >=" +
						" 3 && n % 100 <= 10 ? 3 : n % 100 >= 11 ? 4 : 5")
				}
			};

		#endregion
	}
}