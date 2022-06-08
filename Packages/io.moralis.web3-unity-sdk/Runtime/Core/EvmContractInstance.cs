﻿/**
 *           Module: EvmContractInstance.cs
 *  Descriptiontion: Class that wraps specifica copontract / chain instnace
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
using MoralisUnity;
using System;
#if !UNITY_WEBGL
using Nethereum.Contracts;
#endif

namespace MoralisUnity
{
    /// <summary>
    /// Defines specific copontract / chain instnace
    /// </summary>
    public class EvmContractInstance
    {
        /// <summary>
        /// Contract address on this chain.
        /// </summary>
        public string ContractAddress { get; set; }

#if !UNITY_WEBGL
        /// <summary>
        /// Contract Instance derived from ABI
        /// </summary>
        public Contract ContractInstance { get; set; }

#endif
        /// <summary>
        /// Evm Chain information.
        /// </summary>
        public ChainEntry ChainInfo { get; set; }
    }
}
