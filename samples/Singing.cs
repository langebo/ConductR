using System.Runtime.CompilerServices;

namespace ConductR.Samples;

public record Sing(int Syllables);

public class SingingHandler : IStreamHandler<Sing, string>
{
    private static readonly string[] syllables = new[] { "do", "re", "mi", "fa", "so", "la", "ti", "do" };

    public async IAsyncEnumerable<string> HandleAsync(Sing query, [EnumeratorCancellation] CancellationToken token = default)
    {
        var rnd = new Random();
        for (var n = 0; n < query.Syllables; n++)
        {
            await Task.Delay(rnd.Next(50, 500), token);
            yield return syllables[rnd.Next(0, syllables.Length)];
        }
    }
}