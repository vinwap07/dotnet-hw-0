using System.Text;

var word = Console.ReadLine();
if (string.IsNullOrEmpty(word))
{
    throw new ArgumentNullException(nameof(word));
}

Console.WriteLine(ReverseWord(word));

var sentence = Console.ReadLine();
if (string.IsNullOrEmpty(sentence))
{
    throw new ArgumentNullException(nameof(sentence));
}

Console.WriteLine(ReverseSentence(sentence));

static string ReverseWord(string word)
{
    // return word.ToArray().Reverse().ToString(); - слишком изи да

    var reversedWord = new StringBuilder();
    for (int i = word.Length - 1; i >= 0; i--)
    {
        reversedWord.Append(word[i]);
    }
    return reversedWord.ToString();
}

static string ReverseSentence(string sentence)
{
    var words = sentence.Split(' ');
    var reversedSentence = new StringBuilder();
    foreach (var word in words)
    {
        var wordToReverse = word;
        var anotherSymbols = new StringBuilder();
        for (var i = word.Length - 1; i >= 0; i--)
        {
            if (char.IsLetter(word[i]))
            {
                wordToReverse = word[..(i + 1)];
                break;
            }

            anotherSymbols.Append(word[i]);
        }

        reversedSentence.Append(ReverseWord(wordToReverse));
        reversedSentence.Append(ReverseWord(anotherSymbols.ToString()));
        reversedSentence.Append(' ');
    }

    return reversedSentence.ToString()[..reversedSentence.Length];
}