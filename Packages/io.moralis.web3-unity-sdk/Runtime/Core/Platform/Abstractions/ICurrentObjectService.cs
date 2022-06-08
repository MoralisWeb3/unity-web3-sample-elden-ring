﻿using System.Threading;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Objects;

namespace MoralisUnity.Platform.Abstractions
{
    public interface ICurrentObjectService <T, TUser> where T : MoralisObject where TUser : MoralisUser
    {
        /// <summary>
        /// Persists current <see cref="MoralisObject"/>.
        /// </summary>
        /// <param name="obj"><see cref="MoralisObject"/> to be persisted.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        UniTask SetAsync(T obj, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the persisted current <see cref="MoralisObject"/>.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        UniTask<T> GetAsync(IServiceHub<TUser> serviceHub, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a <see cref="Task"/> that resolves to <code>true</code> if current
        /// <see cref="MoralisObject"/> exists.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        UniTask<bool> ExistsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns <code>true</code> if the given <see cref="MoralisObject"/> is the persisted current
        /// <see cref="MoralisObject"/>.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>true if <code>obj</code> is the current persisted <see cref="MoralisObject"/>.</returns>
        bool IsCurrent(T obj);

        /// <summary>
        /// Nullifies the current <see cref="MoralisObject"/> from memory.
        /// </summary>
        void ClearFromMemory();

        /// <summary>
        /// Clears current <see cref="MoralisObject"/> from disk.
        /// </summary>
        UniTask ClearFromDiskAsync();
    }
}
