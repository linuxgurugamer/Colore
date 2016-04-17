// <copyright file="ColorHelper.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using UnityEngine;

    /// <summary>
    /// Helper class allowing easy color-uint-conversion.
    /// </summary>
    internal class ColorHelper
    {
        /// <summary>
        /// Fetches a color from the given uint color code.
        /// </summary>
        /// <param name="colorCode">32bit ARGB color uint representation (0xAABBGGRR)</param>
        /// <returns>the corresponding unity color object</returns>
        public static Color UintToColor(uint colorCode)
        {
            Color32 myReturn = default(Color32);

            myReturn.a = (byte)((colorCode >> 24) & 0xFF);
            myReturn.b = (byte)((colorCode >> 16) & 0xFF);
            myReturn.g = (byte)((colorCode >> 8) & 0xFF);
            myReturn.r = (byte)((colorCode >> 0) & 0xFF);

            return myReturn;
        }

        /// <summary>
        /// Fetches the uint color code from the given unity color.
        /// </summary>
        /// <param name="color">The color to translate into a uint code</param>
        /// <returns>32bit ARGB uint representation of the color (0xAABBGGRR)</returns>
        public static uint ColorToUint(Color color)
        {
            Color32 c = color;
            uint myReturn = ((uint)(c.a << 24)) | ((uint)(c.b << 16)) | ((uint)(c.g << 8)) | ((uint)c.r);
            return myReturn;
        }
    }
}
