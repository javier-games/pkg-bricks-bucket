using System.Globalization;
using NUnit.Framework;
using UnityEngine;

namespace BricksBucket.Localization.Tests
{
    public class StandardsTestSuit
    {
        /// <summary>
        /// Test for the ISO 15924 standard related methods.
        /// This method checks for:
        /// <list type="bullet">
        /// <item>
        /// <see cref="StandardUtils.GetName(ISO15924)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetCode(ISO15924)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetISO15924(string)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetDirection(ISO15924)"/>
        /// </item>
        /// </list>
        /// </summary>
        [Test]
        public void Iso15924Test()
        {
            // Test Name for a non ISO 15924 enum member.
            const ISO15924 fakeValue = (ISO15924) (-1);
            var name = StandardUtils.GetName (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for not enum member (-1): " + name
            );
            
            // Test Code for a non ISO 15924 enum member.
            var code = StandardUtils.GetCode (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for not enum member (-1): " + code
            );
            
            // Test Parse for not valid ISO 15924.
            Assert.IsTrue (
                StandardUtils.GetISO15924 ("Invalid Code") == ISO15924.NONE,
                "Wrong parse method for not valid string."
            );
            
            // Test Parse for Lower Case code.
            Assert.IsTrue (
                // ReSharper disable once StringLiteralTypo
                StandardUtils.GetISO15924 ("latn") == ISO15924.LATN,
                // ReSharper disable once StringLiteralTypo
                "Wrong parse method for \"latn\"."
            );
            
            // Test Parse for mixed Case code.
            Assert.IsTrue (
                StandardUtils.GetISO15924 ("lATn") == ISO15924.LATN,
                "Wrong parse method for \"lATn\"."
            );
            
            // Test Direction for a non ISO 15924 enum member:
            Assert.IsTrue (
                StandardUtils.GetDirection (fakeValue) ==
                StandardUtils.ScriptDirection.NONE,
                "Wrong direction for a non enum member (-1)."
            );
            
            // Test for all elements in ISO 15924 Enum.
            var values = System.Enum.GetValues (typeof (ISO15924));
            for (int i = 0; i < values.Length; i++)
            {
                var iso = (ISO15924)values.GetValue (i);
                
                // Test Name.
                name = StandardUtils.GetName (iso);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    iso + " has not a name."
                );
                
                // Test Code.
                code = StandardUtils.GetCode (iso);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    iso + " has not code."
                );
                
                // Test Parse.
                Assert.IsTrue(
                    iso == StandardUtils.GetISO15924 (code),
                    iso + " wrong parse method."
                );
            }
        }
        
        /// <summary>
        /// Test for the ISO 639 standard related Methods.
        /// This method checks for:
        /// <list type="bullet">
        /// <item>
        /// <see cref="StandardUtils.GetName(ISO639)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetCode(ISO639)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetISO639(string)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetISO15924(ISO639)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetPluralForm(ISO639)"/>
        /// </item>
        /// </list>
        /// </summary>
        [Test]
        public void Iso639Test()
        {
            // Test Name for a non ISO 639 enum member.
            const ISO639 fakeValue = (ISO639) (-1);
            var name = StandardUtils.GetName (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for not enum member (-1): " + name
            );
            
            // Test Code for a non ISO 639 enum member.
            var code = StandardUtils.GetCode (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for not enum member (-1): " + code
            );
            
            // Test Parse for not valid ISO 639 enum member.
            Assert.IsTrue (
                StandardUtils.GetISO639 ("Invalid Code") == ISO639.NONE,
                "Wrong parse method for not valid string."
            );
            
            // Test Parse for Lower Case code.
            Assert.IsTrue (
                StandardUtils.GetISO639 ("en") == ISO639.EN,
                "Wrong parse method for \"en\"."
            );
            
            // Test Parse for Mixed Case code.
            Assert.IsTrue (
                StandardUtils.GetISO639 ("eN") == ISO639.EN,
                "Wrong parse method for \"eN\"."
            );
            
            // Test Wrong ISO 15924 for not ISO 639 enum member.
            Assert.IsTrue (
                StandardUtils.GetISO15924 (fakeValue) == ISO15924.NONE,
                "Wrong ISO 15924 for not enum member (-1)."
            );

            // Test for all elements in ISO 639 Enum.
            var values = System.Enum.GetValues (typeof (ISO639));
            for (int i = 0; i < values.Length; i++)
            {
                var iso = (ISO639)values.GetValue (i);
                
                // Test Name.
                name = StandardUtils.GetName (iso);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    iso + " has not a name."
                );
                
                // Test Code.
                code = StandardUtils.GetCode (iso);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    iso + " has not code."
                );
                
                // Test Parse.
                Assert.IsTrue(
                    iso == StandardUtils.GetISO639 (code),
                    iso + " wrong parse method."
                );

                // Test Plural Form.
                var pluralForm = StandardUtils.GetPluralForm (iso);
                if (pluralForm != null)
                {
                    Assert.IsTrue (
                        pluralForm.Evaluate (0) >= 0,
                        "Wrong plural form option value."
                    );
                }
            }
        }

        /// <summary>
        /// Test for the UN M.49 standard related Methods.
        /// This method checks for:
        /// <list type="bullet">
        /// <item>
        /// <see cref="StandardUtils.GetName(UNM49)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetCode(UNM49)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetUNM49(string)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetISO3166(UNM49)"/>
        /// </item>
        /// </list>
        /// </summary>
        [Test]
        public void UnM49Test ()
        {
            const UNM49 fakeValue = (UNM49) (-1);
            
            // Test Name for a non UN M.49 enum member.
            var name = StandardUtils.GetName (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for not enum member (-1): " + name
            );
            
            // Test Code for a non UN M.49 enum member.
            var code = StandardUtils.GetCode (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for not enum member (-1): " + code
            );
            
            // Test Parse for not valid string.
            Assert.IsTrue (
                StandardUtils.GetUNM49 ("Invalid Code") == UNM49.NONE,
                "Wrong parse method for not valid string."
            );
            
            // Test Parse for not valid numeric code.
            Assert.IsTrue (
                StandardUtils.GetUNM49 ("-1") == UNM49.NONE,
                "Wrong parse method for not valid string."
            );

            // Test Parse for existing code.
            Assert.IsTrue (
                StandardUtils.GetUNM49 ("001") == UNM49.WORLD,
                "Wrong parse method for \"World\"."
            );
            
            // Test Parse for existing code with out format.
            Assert.IsTrue (
                StandardUtils.GetUNM49 ("1") == UNM49.WORLD,
                "Wrong parse method for \"World\"."
            );
            
            // Test Getting ISO 3166 from a non UN M.49 enum member.
            Assert.IsTrue (
                StandardUtils.GetISO3166 (fakeValue) == ISO3166.NONE,
                "Wrong ISO 3166 from invalid UN M.49 value (-1)."
            );
            
            // Test Format for code.
            Assert.IsTrue (
                StandardUtils.GetCode (UNM49.WORLD).Length == 3,
                "Wrong parse method for \"World\"."
            );

            // Test for all elements in ISO 15924 Enum.
            var values = System.Enum.GetValues (typeof (UNM49));
            for (int i = 0; i < values.Length; i++)
            {
                var value = (UNM49)values.GetValue (i);
                
                // Test Name.
                name = StandardUtils.GetName (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    value + " has not a name."
                );
                
                // Test Code.
                code = StandardUtils.GetCode (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    value + " has not code."
                );
                
                // Test Parse.
                Assert.IsTrue (
                    value == StandardUtils.GetUNM49 (code),
                    value + " wrong parse method."
                );
            }
        }
        

        /// <summary>
        /// Test for the ISO 3166 standard related Methods.
        /// This method checks for:
        /// <list type="bullet">
        /// <item>
        /// <see cref="StandardUtils.GetName(ISO3166)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetCode(ISO3166)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetISO3166(string)"/>
        /// </item>
        /// <item>
        /// <see cref="StandardUtils.GetUNM49(ISO3166)"/>
        /// </item>
        /// </list>
        /// </summary>
        [Test]
        public void Iso3166Test ()
        {
            const ISO3166 wrongValue = (ISO3166) (-1);
            
            // Test Name for a non ISO 3166 enum member.
            var name = StandardUtils.GetName (wrongValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for not enum member (-1): " + name
            );
            
            // Test Code for a non ISO 3166 enum member.
            var code = StandardUtils.GetCode (wrongValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for not enum member (-1): " + code
            );
            
            // Test Parse for not valid ISO 3166 enum member.
            Assert.IsTrue (
                StandardUtils.GetISO3166 ("Invalid Code") == ISO3166.NONE,
                "Wrong parse method for not valid string."
            );
            
            // Test Parse for Lower Case code.
            Assert.IsTrue (
                StandardUtils.GetISO3166 ("mx") == ISO3166.MX,
                "Wrong parse method for \"mx\"."
            );
            
            // Test Parse for Mixed Case code.
            Assert.IsTrue (
                StandardUtils.GetISO3166 ("mX") == ISO3166.MX,
                "Wrong parse method for \"mX\"."
            );
            
            // Test Wrong ISO 15924 for not ISO 3166 enum member.
            Assert.IsTrue (
                StandardUtils.GetUNM49 (wrongValue) == UNM49.NONE,
                "Wrong UN M.49 for not enum member (-1)."
            );


            // Test for all elements in ISO 3166 Enum.
            var values = System.Enum.GetValues (typeof (ISO3166));
            for (int i = 0; i < values.Length; i++)
            {
                var value = (ISO3166)values.GetValue (i);
                
                // Test UN M.49 value.
                // Every ISO 3166 MUST have a UN M.49 value.
                if(value != ISO3166.NONE)
                    Assert.IsTrue (
                        StandardUtils.GetUNM49 (value) != UNM49.NONE,
                        value + " has not an UN M.49 standard value."
                    );
                
                // Test Name.
                name = StandardUtils.GetName (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    value + " has not a name."
                );
                
                // Test Code.
                code = StandardUtils.GetCode (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    value + " has not code."
                );
                
                // Test Parse.
                Assert.IsTrue (
                    value == StandardUtils.GetISO3166 (code),
                    value + " wrong parse method."
                );
            }
        }


        [Test]
        public void LcidTest ()
        {
            const LCID fakeValue = (LCID) (-1);
            
            // Test Name for a non LCID enum member.
            var name = StandardUtils.GetName (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (name),
                "Invalid code for a non enum member (-1): " + name
            );
            
            // Test Code for a non LCID enum member.
            var code = StandardUtils.GetCode (fakeValue);
            Assert.IsTrue (
                string.IsNullOrEmpty (code),
                "Invalid code for a non enum member (-1): " + code
            );
            
            // Test Parse from invalid code.
            Assert.IsTrue (
                StandardUtils.GetLCID("Invalid Code") == LCID.NONE,
                "Wrong parse method from invalid code."
            );
            
            // Test Parse from invalid code.
            Assert.IsTrue (
                StandardUtils.GetLCID("Invalid Code") == LCID.NONE,
                "Wrong parse method from invalid code."
            );
            
            // Test Parse from valid low case code.
            Assert.IsTrue (
                StandardUtils.GetLCID("es-mx") == LCID.ES_MX,
                "Wrong parse method for lower case code."
            );
            
            // Test Parse from valid upper case code.
            Assert.IsTrue (
                StandardUtils.GetLCID("ES-MX") == LCID.ES_MX,
                "Wrong parse method from upper case code."
            );
            
            // Test Parse from valid upper case code.
            Assert.IsTrue (
                StandardUtils.GetLCID("es-MX") == LCID.ES_MX,
                "Wrong parse method from regular code."
            );
            
            // Test get ISO 639 from a non LCID enum member.
            Assert.IsTrue (
                StandardUtils.GetISO639 (fakeValue) == ISO639.NONE,
                "Wrong ISO 639 from a non LCID value."
            );
            
            // Test get UN M.49 from a non LCID enum member.
            Assert.IsTrue (
                StandardUtils.GetUNM49 (fakeValue) == UNM49.NONE,
                "Wrong UN M.49 from a non LCID value."
            );
            
            // Test get ISO 639 from a non LCID enum member.
            Assert.IsTrue (
                StandardUtils.GetISO3166 (fakeValue) == ISO3166.NONE,
                "Wrong ISO ISO 3166 from a non LCID value."
            );
            
            // Test get Culture Info for non enum member-
            Assert.IsTrue (
                StandardUtils.GetCultureInfo (fakeValue) == null,
                "Wrong get culture method for non enum members."
            );
            
            const ISO639 fakeLanguage = (ISO639) (-1);
            const UNM49 fakeRegion = (UNM49) (-1);
            const ISO15924 fakeScript = (ISO15924) (-1);
            
            // Test get LCID from non enum members.
            Assert.IsTrue (
                StandardUtils.GetLCID (fakeLanguage) == LCID.NONE,
                "Wrong LCID for a non ISO 639 enum member."
            );
            Assert.IsTrue (
                StandardUtils.GetLCID (fakeLanguage, fakeRegion) == LCID.NONE,
                "Wrong LCID for a non UN M.49 enum member."
            );
            Assert.IsTrue (
                StandardUtils.GetLCID (fakeLanguage, UNM49.NONE, fakeScript) ==
                LCID.NONE,
                "Wrong LCID for a non ISO 15924 enum member."
            );
            Assert.IsTrue (
                StandardUtils.GetLCID (fakeLanguage, fakeRegion, fakeScript) ==
                LCID.NONE,
                "Wrong LCID for a non enum members."
            );
            
            // Test for all elements in LCID Enum.
            var values = System.Enum.GetValues (typeof (LCID));
            for (int i = 0; i < values.Length; i++)
            {
                var value = (LCID)values.GetValue (i);
                
                // Test Name.
                name = StandardUtils.GetName (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (name),
                    value + " has not a name."
                );
                
                // Test Code.
                code = StandardUtils.GetCode (value);
                Assert.IsFalse (
                    string.IsNullOrEmpty (code),
                    value + " has not code."
                );

                if (value == LCID.NONE || value == LCID.INVARIANT) continue;
                // Test Culture Info.
                var cultureInfo = StandardUtils.GetCultureInfo (value);
                    
                //	IMPORTANT: 
                //	Exceptions for string codes.
                if (value == LCID.ZH_HANS) continue;
                    
                Assert.IsTrue (
                    StandardUtils.GetLCID (cultureInfo.Name) == value,
                    value + " different from " + cultureInfo.Name
                );
            }

            var cultures = System.Globalization.CultureInfo.GetCultures (
                CultureTypes.AllCultures
            );
            for (int i = 0; i < cultures.Length; i++)
            {
                if(cultures[i].Name == "zh-Hans") continue;
                
                // Not found in LCID Version.
                if(cultures[i].Name == "zh-CHS") continue;
                if(cultures[i].Name == "zh-CHT") continue;
                
                // TODO: Fix this on sheets.
                if(cultures[i].Name == "tzm-Latn") continue;
                if(cultures[i].Name == "ha-Latn") continue;
                
                var lcid = StandardUtils.GetLCID (cultures[i].Name);

                Assert.IsTrue (
                    StandardUtils.GetCultureInfo (lcid).Name ==
                    cultures[i].Name,
                    "Not good " + StandardUtils.GetCultureInfo (lcid).Name + " " + cultures[i].Name
                );
            }
        }
    }
}
