namespace GrainAbstaction
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        ValueTask<string> SayHello(string greeting);
    }
}
