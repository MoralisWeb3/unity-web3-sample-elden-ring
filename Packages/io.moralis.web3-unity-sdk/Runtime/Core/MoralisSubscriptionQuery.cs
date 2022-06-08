﻿/**
 *           Module: MoralisSubscriptionQuery.cs
 *  Descriptiontion: Wraps subscription processes to facilitate automation
 *                   of pause / unpause handling.
 *           Author: Moralis Web3 Technology AB, 559307-5988 - David B. Goodrich 
 *  
 *  MIT License
 *  
 *  Copyright (c) 2021 Moralis Web3 Technology AB, 559307-5988
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.
 */
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Services.ClientServices;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Platform.Queries;

namespace MoralisUnity
{
    /// <summary>
    /// Provides a wrapper around the query subscription process to facilitate automated
    /// subscribe and suspension processes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MoralisSubscriptionQuery<T> : ISubscriptionQuery where T : MoralisObject
    {
        private MoralisLiveQueryClient<T> subscription;

        /// <summary>
        /// The client event handlers used to react to this subscription.
        /// </summary>
        public MoralisLiveQueryCallbacks<T> Callbacks { get; private set; }

        /// <summary>
        /// Query against which the subscription is made.
        /// </summary>
        public MoralisQuery<T> Query { get; private set; }

        /// <summary>
        /// Indicates if a connection the server was established.
        /// </summary>
        public bool Connected { get; private set; }

        /// <summary>
        /// Indicates if a subscription for the query has been established.
        /// </summary>
        public bool Subscribed { get; private set; }

        /// <summary>
        /// Key name used to identify this subscription and included in any 
        /// error logs generated.
        /// </summary>
        public string SubscriptionName { get; private set; }

        public MoralisSubscriptionQuery(string keyName, MoralisQuery<T> q, MoralisLiveQueryCallbacks<T> c)
        {
            Query = q;
            Callbacks = c;
            SubscriptionName = keyName;

            // Internally track connection state
            Callbacks.OnConnectedEvent += (() => { Connected = true; });
            // Internally track subscription state
            Callbacks.OnSubscribedEvent += ((requestId) => { Subscribed = true; });
            Callbacks.OnUnsubscribedEvent += ((requestId) => { Subscribed = false; });

            // Create initial subscription.
            subscription = Query.Subscribe<T>(Callbacks);
        }

        /// <summary>
        /// Attempts to re-establish a previous subscriptions. If the 
        /// subscription is already active, unsubscribe is called.
        /// Subscription is then re-created.
        /// </summary>
        /// <returns></returns>
        public async UniTask RenewSubscription()
        {
            // Make sure the subscription is not active.
            if (Subscribed)
            {
                await Unsubscribe();
            }

            // Re-establish the subscription.
            subscription = Query.Subscribe<T>(Callbacks);
        }

        /// <summary>
        /// Unsubscribes from and disposes of the subscription.
        /// </summary>
        /// <returns></returns>
        public async UniTask Unsubscribe()
        {
            if (Subscribed)
            {
                await UniTask.Run(() =>
                {
                    // Try to close down the subscription properly
                    subscription.Unsubscribe();
                
                    subscription.Dispose();
                    subscription = null;
                    Subscribed = false;
                });

            }
        }
    }
}
