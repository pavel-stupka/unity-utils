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
            switch (m_PhysicalUnit)
            {
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