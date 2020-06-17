using NUnit.Framework;

namespace BricksBucket.Localization.Tests
{
	/// <!-- Iso15924TestSuit -->
	/// 
	/// <summary>
	/// Test Suit for the ISO 15924 standard related Methods.
	/// </summary>
	/// 
	/// <seealso cref="BricksBucket.Localization.Iso15924"/>
	/// <seealso cref="Standards"/>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	public class Iso15924TestSuit
	{
		
        /// <summary>
        /// Tests the response to a not valid values in the following methods.
        /// <list type="bullet">
        /// <item><see cref="Standards.GetName(Iso15924)"/></item>
        /// <item><see cref="Standards.GetCode(Iso15924)"/></item>
        /// <item><see cref="Standards.GetIso15924(string)"/></item>
        /// <item><see cref="Standards.GetDirection(Iso15924)"/></item>
        /// </list>
        /// </summary>
        [Test]
        public void FakeValuesTest()
        {
            // Test Name for a non ISO 15924 enum member.
            const Iso15924 fakeValue = (Iso15924) (-1);
            var name = Standards.GetName (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for not enum member (-1): " + name
            );
            
            // Test Code for a non ISO 15924 enum member.
            var code = Standards.GetCode (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for not enum member (-1): " + code
            );
            
            // Test Parse for not valid ISO 15924.
            Assert.IsTrue (
                Standards.GetIso15924 ("Invalid Code") == Iso15924.NONE,
                "Wrong parse method for not valid string."
            );
            
            // Test Parse for Lower Case code.
            Assert.IsTrue (
                // ReSharper disable once StringLiteralTypo
                Standards.GetIso15924 ("latn") == Iso15924.LATN,
                // ReSharper disable once StringLiteralTypo
                "Wrong parse method for \"latn\"."
            );
            
            // Test Parse for mixed Case code.
            Assert.IsTrue (
                Standards.GetIso15924 ("lATn") == Iso15924.LATN,
                "Wrong parse method for \"lATn\"."
            );
            
            // Test Direction for a non ISO 15924 enum member:
            Assert.IsTrue (
                Standards.GetDirection (fakeValue) ==
                Standards.ScriptDirection.NONE,
                "Wrong direction for a non enum member (-1)."
            );
        }
        
        /// <summary>
        /// Tests each enum member from the <see cref="Iso15924">ISO 15924
        /// </see> enum for the following methods.
        /// <list type="bullet">
        /// <item><see cref="Standards.GetName(Iso15924)"/></item>
        /// <item><see cref="Standards.GetCode(Iso15924)"/></item>
        /// <item><see cref="Standards.GetIso15924(string)"/></item>
        /// </list>
        /// </summary>
        [Test]
        public void Iso15924EnumTest()
        {
            var values = System.Enum.GetValues (typeof (Iso15924));
            for (int i = 0; i < values.Length; i++)
            {
                var iso = (Iso15924)values.GetValue (i);
                
                // Test Name.
                var name = Standards.GetName (iso);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    iso + " has not a name."
                );
                
                // Test Code.
                var code = Standards.GetCode (iso);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    iso + " has not code."
                );
                
                // Test Parse.
                Assert.IsTrue(
                    iso == Standards.GetIso15924 (code),
                    iso + " wrong parse method."
                );
            }
        }
	}
}