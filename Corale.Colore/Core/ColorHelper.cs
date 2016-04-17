using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Corale.Colore.Core
{
    class ColorHelper
    {
		private ColorHelper() {
		}

		public static Color uintToColor(uint colorCode) {
			Color32 myReturn = new Color32();

			myReturn.a = (byte) ((colorCode >> 24) & 0xFF);
			myReturn.r = (byte) ((colorCode >> 16) & 0xFF);
			myReturn.g = (byte) ((colorCode >> 8) & 0xFF);
			myReturn.b = (byte) ((colorCode >> 0) & 0xFF);

			return myReturn;
		}

		public static uint colorToUint(Color color) {
			Color32 c = color;
			uint myReturn = ((uint) (c.a << 24)) | ((uint) (c.r << 16)) | ((uint) (c.g << 8)) | ((uint) c.b);
			return myReturn;
		}
    }
}
