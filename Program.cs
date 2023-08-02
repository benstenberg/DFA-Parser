using System;
using System.Text.RegularExpressions;

class Driver
{
    private static String dfaStr = "";
    private static List<string> tests = new List<string>();

    static void Main(string[] args)
    {
        // Args: testingStrings... -f "dfaRepresentation"
        parseArgs(args);

        // Generate DFA (remove whitespace for parsing)
        Dfa d;
        try
        {
            d = new Dfa(Regex.Replace(dfaStr, "\\s+", ""));
        }
        catch (Exception)
        {
            Console.WriteLine("Unable to build DFA. See -help for more information on correct syntax.");
            return;
        }

        // Test input
        testInput(d);
    }

    static void parseArgs(string[] args)
    {
        if (!args.Contains("-d") || args.Contains("-help") || args.Length < 3)
        {
            help();
            return;
        }
        else
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-d")
                {
                    continue;
                }
                else if (i > 0 && args[i - 1] == "-d")
                {
                    dfaStr = args[i];
                }
                else
                {
                    tests.Add(args[i]);
                }
            }
        }
    }

    static void testInput(Dfa d)
    {
        foreach (string str in tests)
        {
            if (d.isValid(str))
            {
                Console.WriteLine("The input string " + str + " is valid.");
            }
            else
            {
                Console.WriteLine("The input string " + str + " is invalid.");
            }
        }
    }

    static void help()
    {
        Console.WriteLine("DFA String Tester\n" +
        "    Please provide input string(s) to test, and a DFA representation in the form of: \n" +
        "    s1(s2:x, s3:y, ...), s2*(s1:z, s3:x, ...), ...\n" +
        "      s1,s2,... are state identifiers and may be any string.\n" +
        "      x,y,z... are tokens in the language. \n" +
        "      The * character after a state identifier marks the state as a final/accept state. \n" +
        "      Transitions are listed in parentheses after the state identifier in the form of s2:x, meaning transition to state s2 on an 'x'.\n" +
        "    Example: s0*(s0:0, s1:1), s1(s0:1, s2:0), s2(s1:0, s2:1)\n" +
        "\n" +
        "\n" +
        "Troubleshooting:\n" +
        "    Arguments: DFA representation should be preceded with -d flag. All other strings will be tested as input. \n" +
        "    It is up to the user to provide a valid DFA. Finite State Machines in other forms may behave unexpectedly.\n" +
        "    Use -help to see this message again."
        );
    }
}

