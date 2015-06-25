/*
The MIT License (MIT)

Copyright (c) 2015 Pavel Stupka

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.UI {

	// Canvas scaler layout that allows forcing custom screen DPI instead of the default Screen.dpi.
	// Use ForcedDpi property to set / get your custom screen DPI.
	public class DpiCanvasScaler : CanvasScaler {

		// DPI that will be used instead of the default Screen.dpi if greater than 0.
		private float forcedDpi = 0; 

		//--------------------------------------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------------------------------------
		//	PUBLIC
		//--------------------------------------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------------------------------------
		
		// Property setter / getter for DPI that will be used instead of the default Screen.dpi if greater than 0.
		public float ForcedDpi {
			get {
				return forcedDpi;
			}
			set {
				if (forcedDpi != value) {
					forcedDpi = value;
					Handle();	
				}				 
			}
		}
		
		//--------------------------------------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------------------------------------
		//	PROTECTED
		//--------------------------------------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------------------------------------
		
		protected override void HandleConstantPhysicalSize() {
            
			float currentDpi = Screen.dpi;
			
			// Shall we use the forced DPI?
			if (forcedDpi > 0) {
				currentDpi = forcedDpi;
			}			
			
            float dpi = (currentDpi == 0 ? m_FallbackScreenDPI : currentDpi);
            float targetDPI = 1;
            switch (m_PhysicalUnit) {
                case Unit.Centimeters: targetDPI = 2.54f; break;
                case Unit.Millimeters: targetDPI = 25.4f; break;
                case Unit.Inches:      targetDPI =     1; break;
                case Unit.Points:      targetDPI =    72; break;
                case Unit.Picas:       targetDPI =     6; break;
            }

            SetScaleFactor(dpi / targetDPI);
            SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit * targetDPI / m_DefaultSpriteDPI);
        }
		
		//--------------------------------------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------------------------------------
	}
}