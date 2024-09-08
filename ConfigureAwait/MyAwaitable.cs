using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Fody;

public struct MyCustomAwaitable
{
    private readonly Task _task;

    public MyCustomAwaitable(Task task)
    {
        _task = task;
    }

    public MyCusomAwaiter GetAwaiter()
    {
        return new MyCusomAwaiter(_task);
    }

    // TODO: Add copies for ConfiguredTaskAwaiter, ValueTaskAwaiter, ConfiguredValueTaskAwaiter and their generic versions
    public struct MyCusomAwaiter : INotifyCompletion
    {
        private readonly TaskAwaiter _awaiter;

        public MyCusomAwaiter(Task task)
        {
            _awaiter = task.GetAwaiter();
        }

        public bool IsCompleted => false;

        public void OnCompleted(Action continuation)
        {
            _awaiter.OnCompleted(continuation);
        }

        public void GetResult()
        {
            _awaiter.GetResult();
        }
    }
}

public static class MyTaskExtensions
{
    public static MyCustomAwaitable CreateCustomAwaitable(this Task task)
    {
        return new MyCustomAwaitable(task);
    }
}
