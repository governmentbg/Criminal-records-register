using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BNBFileProcessor.Helpers
{
    public static class AsyncTaskOperationHelper
    {

        public static IEnumerable<Task<T>> Interleaved<T>(IEnumerable<Task<T>> tasks)
        {
            var inputTasks = tasks.ToList();
            var sources = (from _ in Enumerable.Range(0, inputTasks.Count)
                           select new TaskCompletionSource<T>()).ToList();
            int nextTaskIndex = -1;
            foreach (var inputTask in inputTasks)
            {
                inputTask.ContinueWith(completed =>
                {
                    var source = sources[Interlocked.Increment(ref nextTaskIndex)];
                    if (completed.IsFaulted)
                        source.TrySetException(completed.Exception.InnerExceptions);
                    else if (completed.IsCanceled)
                        source.TrySetCanceled();
                    else
                        source.TrySetResult(completed.Result);
                }, CancellationToken.None,
                   TaskContinuationOptions.ExecuteSynchronously,
                   TaskScheduler.Default);
            }
            return from source in sources
                   select source.Task;
        }

        public static Task<T[]> WhenAllOrFirstException<T>(IEnumerable<Task<T>> tasks)
        {
            var inputs = tasks.ToList();
            var ce = new CountdownEvent(inputs.Count);
            var tcs = new TaskCompletionSource<T[]>();

            Action<Task> onCompleted = (Task completed) =>
            {
                if (completed.IsFaulted)
                    tcs.TrySetException(completed.Exception.InnerExceptions);
                if (ce.Signal() && !tcs.Task.IsCompleted)
                    tcs.TrySetResult(inputs.Select(t => t.Result).ToArray());
            };

            foreach (var t in inputs) t.ContinueWith(onCompleted);
            return tcs.Task;
        }

        public static async Task<T> RetryOnFault<T>(Func<Task<T>> function, int maxTries, Func<Task> retryWhen)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try { return await function().ConfigureAwait(false); }
                catch { if (i == maxTries - 1) throw; }
                await retryWhen().ConfigureAwait(false);
            }
            return default(T);
        }

        public static async Task RetryOnFault(Func<Task> function, int maxTries, Func<Task> retryWhen)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try { await function().ConfigureAwait(false); return; }
                catch { if (i == maxTries - 1) throw; }
                await retryWhen().ConfigureAwait(false);
            }
        }

        public static T RetryOnFault<T>(Func<T> function, int maxTries)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try { return function(); }
                catch { if (i == maxTries - 1) throw; }
            }
            return default(T);
        }

        public static async Task<T> RetryOnFault<T>(Func<Task<T>> function, int maxTries)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try { return await function().ConfigureAwait(false); }
                catch { if (i == maxTries - 1) throw; }
            }
            return default(T);
        }

        public async static Task ThrottlingTaskExecuting<T>(IEnumerable<Task<T>> tasks, Func<Task<T>> function)
        {
            const int CONCURRENCY_LEVEL = 15;
            int nextIndex = 0;
            var inputTasks = tasks.ToList();
            var imageTasks = new List<Func<Task<T>>>();

            while (nextIndex < CONCURRENCY_LEVEL && nextIndex < inputTasks.Count)
            {
                //imageTasks.Add(GetBitmapAsync(inputTasks[nextIndex]));
                nextIndex++;
            }

            while (imageTasks.Count > 0)
            {
                //try
                //{
                //    Task<Bitmap> imageTask = await Task.WhenAny(imageTasks);
                //    imageTasks.Remove(imageTask);

                //    Bitmap image = await imageTask;
                //    panel.AddImage(image);
                //}
                //catch (Exception exc) { Log(exc); }

                //if (nextIndex < urls.Length)
                //{
                //    imageTasks.Add(GetBitmapAsync(urls[nextIndex]));
                //    nextIndex++;
                //}
            }
        }

    }
}
