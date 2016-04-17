﻿// ---------------------------------------------------------------------------------------
// <copyright file="Static.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Razer.Mouse.Effects
{
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;
    using UnityEngine;

    /// <summary>
    /// Describes the static effect type.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Static
    {
        /// <summary>
        /// The LED on which to apply the color.
        /// </summary>
        [UsedImplicitly]
        public readonly Led Led;

        /// <summary>
        /// The color to apply.
        /// </summary>
        [UsedImplicitly]
        public readonly Color Color;

        /// <summary>
        /// Initializes a new instance of the <see cref="Static" /> struct.
        /// </summary>
        /// <param name="led">The <see cref="Led" /> on which to apply the color.</param>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        public Static(Led led, Color color)
        {
            Led = led;
            Color = color;
        }
    }
}
