using System.Threading;
using System.Threading.Tasks;

namespace GodotUtil;

public static class UtilAsync
{
    private const int MS_FREQUENCY = 25;

    /// <summary>Blocks until condition is true or timeout occurs</summary>
    /// <param name="condition">The break condition</param>
    /// <param name="frequency">The frequency at which the condition will be checked</param>
    /// <returns></returns>
    public static async Task WaitUntil(Func<bool> condition, int frequency = MS_FREQUENCY)
    {
        await Task.Run(WaitTask);
        async Task WaitTask()
        {
            while (!condition())
                await Task.Delay(frequency);
        }
    }

    /// <summary>Blocks until condition is true or task is canceled</summary>
    /// <param name="token">Cancellation token</param>
    /// <param name="condition">The condition that will perpetuate the block</param>
    /// <param name="frequency">The delay at which the condition will be polled, in milliseconds</param>
    public static async Task WaitUntil(Func<bool> condition, CancellationToken token, int frequency = MS_FREQUENCY)
    {
        try
        {
            while (!condition())
                await Task.Delay(frequency, token).ConfigureAwait(true);
        }
        catch (TaskCanceledException)
        {
            // ignore: Task.Delay throws this exception when ct.IsCancellationRequested = true
            // In this case, we only want to stop polling and finish this async Task.
        }
    }
}
