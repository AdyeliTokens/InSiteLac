using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Lean.Touch
{
	// This script will tell you which direction you swiped in
	public class LeanSwipeDirection8 : MonoBehaviour
	{
		public GameObject Portada;
		public GameObject Arbol;

		public GameObject CO2;
		public GameObject Energy;
		public GameObject LTI;
		public GameObject TRI;
		public GameObject Water;
		public GameObject VQI;
		public GameObject DM;
		public GameObject TDY;
		public GameObject CPQI;
		public GameObject Diversity;
		public GameObject MTC;
		public GameObject Market;
		public GameObject Open1;
		public GameObject Open2;


	
		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerSwipe += OnFingerSwipe;

		}
	
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerSwipe -= OnFingerSwipe;
		}
	
		public void OnFingerSwipe(LeanFinger finger)
		{
			
				// Store the swipe delta in a temp variable
				var swipe = finger.SwipeScreenDelta;
				var left  = new Vector2(-1.0f,  0.0f);
				var right = new Vector2( 1.0f,  0.0f);
				var down  = new Vector2( 0.0f, -1.0f);
				var up    = new Vector2( 0.0f,  1.0f);
			
			if (SwipedInThisDirection(swipe, left) == true)
			{
				if(CO2.activeSelf){

					CO2.SetActive (false);
					Energy.SetActive (true);

				}else if(Energy.activeSelf){

					Energy.SetActive (false);
					LTI.SetActive (true);

				}else if(LTI.activeSelf){

					LTI.SetActive (false);
					TRI.SetActive (true);

				}else if(TRI.activeSelf){

					TRI.SetActive (false);
					Water.SetActive (true);

				}else if(Water.activeSelf){

					Water.SetActive (false);
					VQI.SetActive (true);

				}else if(VQI.activeSelf){

					VQI.SetActive (false);
					DM.SetActive (true);

				}else if(DM.activeSelf){

					DM.SetActive (false);
					TDY.SetActive (true);

				}else if(TDY.activeSelf){

					TDY.SetActive (false);
					CPQI.SetActive (true);

				}else if(CPQI.activeSelf){
						
					CPQI.SetActive (false);
					Diversity.SetActive (true);

				}else if(Diversity.activeSelf){

					Diversity.SetActive (false);
					MTC.SetActive (true);

				}else if(MTC.activeSelf){

					MTC.SetActive (false);
					Market.SetActive (true);

				}else if(Market.activeSelf){

					Market.SetActive (false);
					Open1.SetActive (true);

				}else if(Open1.activeSelf){

					Open1.SetActive (false);
					Open2.SetActive (true);

				}else if(Open2.activeSelf){

					Open2.SetActive (false);
					CO2.SetActive (true);

				}

 

			}
			
			if (SwipedInThisDirection(swipe, right) == true)
			{
				if(Open2.activeSelf){

					Open2.SetActive (false);
					Open1.SetActive (true);

				}else if(Open1.activeSelf){

					Open1.SetActive (false);
					Market.SetActive (true);

				}else if(Market.activeSelf){

					Market.SetActive (false);
					MTC.SetActive (true);

				}else if(MTC.activeSelf){

					MTC.SetActive (false);
					Diversity.SetActive (true);

				}else if(Diversity.activeSelf){

					Diversity.SetActive (false);
					CPQI.SetActive (true);

				}else if(CPQI.activeSelf){

					CPQI.SetActive (false);
					TDY.SetActive (true);

				}else if(TDY.activeSelf){

					TDY.SetActive (false);
					DM.SetActive (true);

				}else if(DM.activeSelf){

					DM.SetActive (false);
					VQI.SetActive (true);

				}else if(VQI.activeSelf){

					VQI.SetActive (false);
					Water.SetActive (true);

				}else if(Water.activeSelf){

					Water.SetActive (false);
					TRI.SetActive (true);

				}else if(TRI.activeSelf){

					TRI.SetActive (false);
					LTI.SetActive (true);

				}else if(LTI.activeSelf){

					LTI.SetActive (false);
					Energy.SetActive (true);

				}else if(Energy.activeSelf){

					Energy.SetActive (false);
					CO2.SetActive (true);

				}else if(CO2.activeSelf){

					CO2.SetActive (false);
					Open2.SetActive (true);

				}



			}
			
				if (SwipedInThisDirection(swipe, down) == true)
				{
				
				CO2.SetActive (false);
				Energy.SetActive (false);
				LTI.SetActive (false);
				TRI.SetActive (false);
				Water.SetActive (false);
				VQI.SetActive (false);
				DM.SetActive (false);
				TDY.SetActive (false);
				CPQI.SetActive (false);
				Diversity.SetActive (false);
				MTC.SetActive (false);
				Market.SetActive (false);
				Open1.SetActive (false);
				Open2.SetActive (false);

				Portada.SetActive (true);
				Arbol.SetActive (true);
				}
			
				if (SwipedInThisDirection(swipe, up) == true)
				{
				
				CO2.SetActive (false);
				Energy.SetActive (false);
				LTI.SetActive (false);
				TRI.SetActive (false);
				Water.SetActive (false);
				VQI.SetActive (false);
				DM.SetActive (false);
				TDY.SetActive (false);
				CPQI.SetActive (false);
				Diversity.SetActive (false);
				MTC.SetActive (false);
				Market.SetActive (false);
				Open1.SetActive (false);
				Open2.SetActive (false);

				Portada.SetActive (true);
				Arbol.SetActive (true);
				}

				if (SwipedInThisDirection(swipe, left + up) == true)
				{
					
				}

				if (SwipedInThisDirection(swipe, left + down) == true)
				{
					
				}

				if (SwipedInThisDirection(swipe, right + up) == true)
				{
					
				}

				if (SwipedInThisDirection(swipe, right + down) == true)
				{
					
				}

		}

		private bool SwipedInThisDirection(Vector2 swipe, Vector2 direction)
		{
			// Find the normalized dot product between the swipe and our desired angle (this will return the acos between the vectors)
			var dot = Vector2.Dot(swipe.normalized, direction.normalized);

			// With 8 directions, each direction takes up 45 degrees (360/8), but we're comparing against dot product, so we need to halve it
			var limit = Mathf.Cos(22.5f * Mathf.Deg2Rad);

			// Return true if this swipe is within the limit of this direction
			return dot >= limit;
		}
	}
}