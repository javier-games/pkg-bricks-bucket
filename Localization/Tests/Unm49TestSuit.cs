using NUnit.Framework;

namespace BricksBucket.Localization.Tests
{
    /// <!-- Unm49TestSuit -->
    /// 
    /// <summary>
    /// Test Suit for the UN M49 standard related Methods.
    /// </summary>
    /// 
    /// <seealso cref="BricksBucket.Localization.Unm49"/>
    /// <seealso cref="Standards"/>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
	public class Unm49TestSuit
	{
        /// <summary>
        /// Tests the response to a not valid values in the following methods.
        /// <list type="bullet">
        /// <item><see cref="Standards.GetName(Unm49)"/></item>
        /// <item><see cref="Standards.GetCode(Unm49)"/></item>
        /// <item><see cref="Standards.GetUnm49(string)"/></item>
        /// <item><see cref="Standards.GetIso3166(Unm49)"/></item>
        /// </list>
        /// </summary>
        [Test]
        public void FakeValuesTest ()
        {
            const Unm49 fakeValue = (Unm49) (-1);
            
            // Test Name for a non UN M.49 enum member.
            var name = Standards.GetName (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for not enum member (-1): " + name
            );
            
            // Test Code for a non UN M.49 enum member.
            var code = Standards.GetCode (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for not enum member (-1): " + code
            );
            
            // Test Parse for not valid string.
            Assert.IsTrue (
                Standards.GetUnm49 ("Invalid Code") == Unm49.NONE,
                "Wrong parse method for not valid string."
            );
            
            // Test Parse for not valid numeric code.
            Assert.IsTrue (
                Standards.GetUnm49 ("-1") == Unm49.NONE,
                "Wrong parse method for not valid string."
            );

            // Test Parse for existing code.
            Assert.IsTrue (
                Standards.GetUnm49 ("001") == Unm49.WORLD,
                "Wrong parse method for \"World\"."
            );
            
            // Test Parse for existing code with out format.
            Assert.IsTrue (
                Standards.GetUnm49 ("1") == Unm49.WORLD,
                "Wrong parse method for \"World\"."
            );
            
            // Test Getting ISO 3166 from a non UN M.49 enum member.
            Assert.IsTrue (
                Standards.GetIso3166 (fakeValue) == Iso3166.NONE,
                "Wrong ISO 3166 from invalid UN M.49 value (-1)."
            );
            
            // Test Format for code.
            Assert.IsTrue (
                Standards.GetCode (Unm49.WORLD).Length == 3,
                "Wrong parse method for \"World\"."
            );
        }
        
        /// <summary>
        /// Tests each enum member from the <see cref="Unm49">UN M49</see>
        /// enum for the following methods.
        /// <list type="bullet">
        /// <item><see cref="Standards.GetName(Unm49)"/></item>
        /// <item><see cref="Standards.GetCode(Unm49)"/></item>
        /// <item><see cref="Standards.GetUnm49(string)"/></item>
        /// </list>
        /// </summary>
        [Test]
        public void Unm49Test ()
        {
            // Test for all elements in ISO 15924 Enum.
            var values = System.Enum.GetValues (typeof (Unm49));
            for (int i = 0; i < values.Length; i++)
            {
                var value = (Unm49)values.GetValue (i);
                
                // Test Name.
                var name = Standards.GetName (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    value + " has not a name."
                );
                
                // Test Code.
                var code = Standards.GetCode (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    value + " has not code."
                );
                
                // Test Parse.
                Assert.IsTrue (
                    value == Standards.GetUnm49 (code),
                    value + " wrong parse method."
                );
            }
        }
	}
}