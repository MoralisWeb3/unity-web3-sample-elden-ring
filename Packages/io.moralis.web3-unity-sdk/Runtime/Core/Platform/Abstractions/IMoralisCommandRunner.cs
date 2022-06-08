
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Services.Models;

namespace MoralisUnity.Platform.Abstractions
{
    public interface IMoralisCommandRunner
    {
        /// <summary>
        /// Executes <see cref="MoralisCommand"/> and convert the result into Dictionary.
        /// </summary>
        /// <param name="command">The command to be run.</param>
        /// <param name="uploadProgress">Upload progress callback.</param>
        /// <param name="downloadProgress">Download progress callback.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns></returns>
        UniTask<Tuple<HttpStatusCode, string>> RunCommandAsync(MoralisCommand command, IProgress<IDataTransferLevel> uploadProgress = null, IProgress<IDataTransferLevel> downloadProgress = null, CancellationToken cancellationToken = default);
    }
}
