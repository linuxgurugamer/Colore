﻿// ---------------------------------------------------------------------------------------
// <copyright file="Custom.cs" company="Corale">
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

namespace Corale.Colore.Razer.Mousepad.Effects
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;
    using UnityEngine;

    /// <summary>
    /// Custom effect for mouse pad.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Custom : IEquatable<Custom>, IEquatable<IList<Color>>
    {
        /// <summary>
        /// Colors for the LEDs.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxLeds)]
        private readonly Color[] _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct with
        /// a default color to apply to every LED.
        /// </summary>
        /// <param name="color">The color to set every LED to initially.</param>
        public Custom(Color color)
        {
            _colors = new Color[Constants.MaxLeds];

            for (var i = 0; i < _colors.Length; i++)
                _colors[i] = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentException">Thrown if the colors list supplied is of an incorrect size.</exception>
        public Custom(IList<Color> colors)
        {
            if (colors.Count != Constants.MaxLeds)
            {
                throw new ArgumentException(
                    "Invalid length of color list, expected " + Constants.MaxLeds + " but received " + colors.Count,
                    nameof(colors));
            }

            _colors = new Color[Constants.MaxLeds];

            for (var i = 0; i < _colors.Length; i++)
                _colors[i] = colors[i];
        }

        /// <summary>
        /// Gets or sets LEDs in the custom array.
        /// </summary>
        /// <param name="led">Index of the LED to access.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        public Color this[int led]
        {
            get
            {
                if (led < 0 || led >= Constants.MaxLeds)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(led),
                        led,
                        "Attempted to access an LED that was out of range.");
                }

                return _colors[led];
            }

            set
            {
                if (led < 0 || led >= Constants.MaxLeds)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(led),
                        led,
                        "Attempted to access an LED that was out of range.");
                }

                _colors[led] = value;
            }
        }

        /// <summary>
        /// Compares an instance of <see cref="Custom" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="Custom" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(Custom left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="Custom" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="Custom" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(Custom left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Create a new empty <see cref="Custom" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="Custom" /> filled with the color black.</returns>
        [PublicAPI]
        public static Custom Create()
        {
            return new Custom(Color.black);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return _colors?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Sets all the LED indices to the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set the LEDs to.</param>
        [PublicAPI]
        public void Set(Color color)
        {
            for (var i = 0; i < Constants.MaxLeds; i++)
                _colors[i] = color;
        }

        /// <summary>
        /// Clears the colors in this <see cref="Custom" /> struct (sets to <see cref="Color.black" />).
        /// </summary>
        [PublicAPI]
        public void Clear()
        {
            Set(Color.black);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> and this instance are the same type
        /// and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (obj is Custom)
                return Equals((Custom)obj);

            var list = obj as IList<Color>;
            return list != null && Equals(list);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Custom other)
        {
            for (var i = 0; i < Constants.MaxLeds; i++)
            {
                if (this[i] != other[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Indicates whether the current object is equal to an
        /// instance of <see cref="IList{Color}" />.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(IList<Color> other)
        {
            if (other == null || other.Count != Constants.MaxLeds)
                return false;

            for (var i = 0; i < Constants.MaxLeds; i++)
            {
                if (this[i] != other[i])
                    return false;
            }

            return true;
        }
    }
}
