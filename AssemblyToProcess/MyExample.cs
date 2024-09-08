using Fody;

[ConfigureAwait(false)]
public class MyExample
{
    public async Task AsyncMethodExample()
    {
        await Task.Delay(1);
    }
}
