using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace FreakingAwesome.Data
{
    /// <summary>
    /// Extentions for async operations where the task appears in the both operands
    /// </summary>
    public static class AsyncResultExtensionsBothOperands
    {
        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> resultTask, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            K value = await func(result.Value);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result> resultTask, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> resultTask, Func<T, Task<Result<K>>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return await func(result.Value);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result> resultTask, Func<Task<Result<T>>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> resultTask, Func<Task<Result<K>>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return result;

            var next = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            if (next.IsFailure)
                return Result.Fail<T>(next.Error);

            return result;
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> resultTask, Func<Task<Result>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return result;

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> EnsureAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!await predicate(result.Value).ConfigureAwait(continueOnCapturedContext))
                return Result.Fail<T>(errorMessage);

            return Result.Ok(result.Value);
        }

        public static async Task<Result> EnsureAsync(this Task<Result> resultTask, Func<Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
                return Result.Fail(errorMessage);

            return Result.Ok();
        }

        public static async Task<Result<K>> MapAsync<T, K>(this Task<Result<T>> resultTask, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            K value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> MapAsync<T>(this Task<Result> resultTask, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result<T>> resultTask, Func<T, Task> action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> resultTask, Func<Task> action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<T> OnBothAsync<T>(this Task<Result> resultTask, Func<Result, Task<T>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<K> OnBothAsync<T, K>(this Task<Result<T>> resultTask, Func<Result<T>, Task<K>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnFailureAsync<T>(this Task<Result<T>> resultTask, Func<Task> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnFailureAsync(this Task<Result> resultTask, Func<Task> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<T>> OnFailureAsync<T>(this Task<Result<T>> resultTask, Func<IEnumerable<ValidationError>, Task> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public async static Task<Result<T>> EnsureAsync<T>(this Result<T> self, Func<T, Task<bool>> predicate, string field, string error) =>
            self.IsFailure || await predicate(self.Value)
                ? self
                : Result.Fail<T>(field, error);

        [DebuggerStepThrough]
        public async static Task<Result> UpcastAsync<T>(this Task<Result<T>> self) =>
            (await self).Upcast();
    }
}
