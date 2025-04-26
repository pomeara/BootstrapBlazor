// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Generic throttle dispatcher class
/// </summary>
public class ThrottleDispatcher(ThrottleOptions options)
{
    private readonly object _locker = new();
    private Task? _lastTask;
    private DateTime? _invokeTime;
    private bool _busy;

    /// <summary>
    /// Determines whether to wait
    /// </summary>
    /// <returns></returns>
    protected virtual bool ShouldWait() => _busy || _invokeTime.HasValue && (DateTime.UtcNow - _invokeTime.Value) < options.Interval;

    /// <summary>
    /// Asynchronous throttling method
    /// </summary>
    /// <param name="function">Asynchronous callback method</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task ThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default) => InternalThrottleAsync(() => Task.Run(function), cancellationToken);

    /// <summary>
    /// Synchronous throttling method
    /// </summary>
    /// <param name="action">Synchronous callback method</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public void Throttle(Action action, CancellationToken cancellationToken = default)
    {
        var task = InternalThrottleAsync(() => Task.Run(() =>
        {
            action();
            return Task.CompletedTask;
        }, cancellationToken), cancellationToken);
        Wait();
        return;

        [ExcludeFromCodeCoverage]
        void Wait()
        {
            try
            {
                task.Wait(cancellationToken);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is not null)
                {
                    throw ex.InnerException;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Task instance
    /// </summary>
    protected Task LastTask => _lastTask ?? Task.CompletedTask;

    /// <summary>
    /// Asynchronous throttling method
    /// </summary>
    /// <param name="function">Asynchronous callback method</param>
    /// <param name="cancellationToken">Cancellation token</param>
    private Task InternalThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default)
    {
        if (ShouldWait())
        {
            return LastTask;
        }

        lock (_locker)
        {
            if (ShouldWait())
            {
                return LastTask;
            }

            _busy = true;
            _invokeTime = DateTime.UtcNow;
            _lastTask = function();
            _lastTask.ContinueWith(_ =>
            {
                if (options.DelayAfterExecution)
                {
                    _invokeTime = DateTime.UtcNow;
                }
                _busy = false;
            }, cancellationToken);

            if (options.ResetIntervalOnException)
            {
                _lastTask.ContinueWith((_, _) =>
                {
                    _lastTask = null;
                    _invokeTime = null;
                }, cancellationToken, TaskContinuationOptions.OnlyOnFaulted);
            }
            return LastTask;
        }
    }
}
