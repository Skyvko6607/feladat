class Program
{
    private const string OPENING_BRACKETS = "{[(<";
    private const string CLOSING_BRACKETS = "}])>";

    public static void Main(string[] args)
    {
        string userInput = "{x{}[{]9[}xY zG]{i4{}";
        string input = "";
        for (int i = 0; i < userInput.Length; i++)
        {
            if (OPENING_BRACKETS.Contains(userInput[i]) || CLOSING_BRACKETS.Contains(userInput[i]))
                input += userInput[i];
        }

        char[] charArray = input.ToCharArray();
        
        int[] foundInputs = new int[input.Length];


        int retry = 5;
        while (!IsFullyFound(foundInputs))
        {
            retry--;
            for (int i = 0; i < input.Length; i++)
            {
                if (foundInputs[i] == 1) continue;
                int nextIndex = GetNextIndex(foundInputs, i + 1);
                if (nextIndex == -1) continue;
                if (nextIndex >= input.Length) continue;
                if (!OPENING_BRACKETS.Contains(charArray[i])) continue;
                if (CLOSING_BRACKETS.IndexOf(charArray[nextIndex]) != OPENING_BRACKETS.IndexOf(charArray[i])) continue;
                foundInputs[i] = 1;
                foundInputs[nextIndex] = 1;
                retry = 5;
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (foundInputs[i] == 1) continue;
                if (retry <= 0 && i % 2 != (retry % 2)) continue;
                if (CLOSING_BRACKETS.Contains(charArray[i]))
                    charArray[i] = OPENING_BRACKETS[CLOSING_BRACKETS.IndexOf(charArray[i])];
                else charArray[i] = CLOSING_BRACKETS[OPENING_BRACKETS.IndexOf(charArray[i])];
            }
        }

        bool changed = HasFlipped(input.ToCharArray(), charArray);
        Console.WriteLine("Cserélve lett? " + changed);
    }

    private static bool HasFlipped(char[] original, char[] flipped) {
        for (int i = 0; i < original.Length; i++) {
            if (flipped[i] != original[i]) return true;
        }

        return false;
    }

    private static bool IsFullyFound(int[] foundInputs)
    {
        for (int i = 0; i < foundInputs.Length; i++)
        {
            if (foundInputs[i] != 1) return false;
        }

        return true;
    }
    
    private static int GetNextIndex(int[] foundInputs, int i)
    {
        while (i < foundInputs.Length)
        {
            if (foundInputs[i] != 1) return i;
            i++;
        }

        return -1;
    }
}