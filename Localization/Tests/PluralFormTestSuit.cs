using NUnit.Framework;

namespace BricksBucket.Localization.Tests
{
    /// <!-- PluralFormTestSuit -->
    /// 
    /// <summary>
    /// Test Suit for the Plural Forms related Methods.
    /// </summary>
    /// 
    /// <seealso cref="BricksBucket.Localization.PluralForm"/>
    /// <seealso cref="Standards"/>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    public class PluralFormTestSuit
    {
        /// <summary>
        /// Test the properties for each defined plural form.
        /// </summary>
        [Test]
        public void PluralFormTest ()
        {
            for (int i = 0; i < PluralForm.DefinedPluralFormsCount (); i++)
            {
                var form = PluralForm.GetForm (i);
                Assert.IsNotNull (form, "Missing form " + i);
                
                Assert.IsFalse (
                    string.IsNullOrEmpty (form.Rule) ,
                    "Missing rule on form " + i
                );

                Assert.GreaterOrEqual (
                    form.Count, 0,
                    "Wrong Count in form " + i
                );
                
                Assert.GreaterOrEqual (
                    form.Evaluate (0), 0,
                    "Wrong Evaluate method in form " + i
                );
            }

            for (int i = 0; i < 10; i++)
            {
                UnityEngine.Debug.Log (PluralForm.GetForm (3).Evaluate (i));
            }
        }
    }
}
