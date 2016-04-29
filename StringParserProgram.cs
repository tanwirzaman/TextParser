using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StringParser
{
    class StringParserProgram
    {
        static void Main(string[] args)
        {
            string str = "";
            string result = "";
            StringParser strParser = new StringParser();
            
            //UNIT TEST CASES
            str = "An Automotive_system evaluation.";
            result = strParser.parseString(str);
            Debug.Assert(result=="A0n A6e_s4m e7n.");

            str = "An automotive_system a!!!";
            result = strParser.parseString(str);
            Debug.Assert(result == "A0n a6e_s4m a!!!");
            
            str = "";
            result = strParser.parseString(str);
            Debug.Assert(result == "");
            
            str = "a";
            result = strParser.parseString(str);
            Debug.Assert(result == "a");
            
            str = "An";
            result = strParser.parseString(str);
            Debug.Assert(result == "A0n");

            str = "The";
            result = strParser.parseString(str);
            Debug.Assert(result == "T1e");
            
            str = "An&^a_system#$a";
            result = strParser.parseString(str);
            Debug.Assert(result == "A0n&^a_s4m#$a"); 
            
            str = "A1234Z";
            result = strParser.parseString(str);
            Debug.Assert(result == "A1234Z");
            
            str = "A1234ABCDZ";
            result = strParser.parseString(str);
            Debug.Assert(result == "A1234A3Z");
            
            str = "A1234AabcdefgZ";
            result = strParser.parseString(str);
            Debug.Assert(result == "A1234A7Z");
            
            str = "A1234ADDDDDDDDZ";
            result = strParser.parseString(str);
            Debug.Assert(result == "A1234A1Z");
            
            str = "A1234AabcdefghijklmnopqrstuvwxyzZ";
            result = strParser.parseString(str);
            Debug.Assert(result == "A1234A26Z");
            
            str = "A    B    C";
            result = strParser.parseString(str);
            Debug.Assert(result == "A    B    C");
            
            str = "A    B    C";
            result = strParser.parseString(str);
            Debug.Assert(result == "A    B    C");
            
            str = "A123456789Z";
            result = strParser.parseString(str);
            Debug.Assert(result == "A123456789Z");
            
            str = " Another#$$$new@###String____Computttter ";
            result = strParser.parseString(str);
            Debug.Assert(result == " A5r#$$$n1w@###S4g____C6r ");
            
            str = "aaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbccccccccccccccccccccccccccccccccccccccccc";
            result = strParser.parseString(str);
            Debug.Assert(result == "a3c");
            
            str = "aaaaaaaaaaaaaaaa bbbbbbbbbbbbbbbbbbbbbbbbb ccccccccccccccccccccccccccccccccccccccccc";
            result = strParser.parseString(str);
            Debug.Assert(result == "a1a b1b c1c");
            
            str = "aaaaaaaaaaaaaaaa@#bbbbbbbbbbbbbbbbbbbbbbbbb%&ccccccccccccccccccccccccccccccccccccccccc";
            result = strParser.parseString(str);
            Debug.Assert(result == "a1a@#b1b%&c1c");

            str = "This123 is an automotive@system test!!!!";
            result = strParser.parseString(str);
            Debug.Assert(result == "T2s123 i0s a0n a6e@s4m t2t!!!!");
        }

        
    }

    class StringParser
    {
        public StringParser() { }

        /// <summary>
        /// This method parses the input string by detecting words delimited by non alphabetic characters.
        /// It uses the stringModifier method to modify the words and finally returns the output.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Final Output string</returns>
        public string parseString(string str)
        {
            StringBuilder result = new StringBuilder();
            //If input is empty return empty string
            if (str.Length == 0)
                return "";

            StringBuilder sbWord = new StringBuilder();
            foreach (char c in str)
            {
                if (IsAlphabet(c))
                    sbWord.Append(c);
                else
                {
                    if (sbWord.Length != 0)
                    {
                        result.Append(stringModifier(sbWord));
                        sbWord.Clear();
                    }
                    result.Append(c);

                }

            }
            //Final word in string
            if (sbWord.Length > 0)
            {
                result.Append(stringModifier(sbWord));
                sbWord.Clear();
            }

            return result.ToString();

        }

        /// <summary>
        /// This method takes a word as input and modifies it according to the requirements.
        /// </summary>
        /// <param name="sbWord"></param>
        /// <returns>Modified word</returns>
        private StringBuilder stringModifier(StringBuilder sbWord)
        {
            StringBuilder modWord = new StringBuilder();
            HashSet<char> set = new HashSet<char>();
            string strWord = sbWord.ToString();
            int letterCount = 0;
            if (sbWord.Length == 1)
                return sbWord;
            else if (sbWord.Length == 2)
            {
                modWord.Append(strWord[0]);
                modWord.Append("0");
                modWord.Append(strWord[1]);
            }
            else
            {
                modWord.Append(strWord[0]);
                for (int i = 1; i < strWord.Length - 1; i++)
                {
                    char c = strWord[i];
                    if (!set.Contains(c))
                    {
                        letterCount++;
                        set.Add(c);
                    }
                }
                modWord.Append("" + letterCount);
                modWord.Append(strWord[strWord.Length - 1]);
            }

            return modWord;

        }

        private bool IsAlphabet(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }
    }
}
