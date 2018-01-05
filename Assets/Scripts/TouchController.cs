using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {
	
	public int Layer;
	int layerMask;


	public GameObject portada;


	public GameObject panelInfo;
	public GameObject pCO2;
	public GameObject pEnergy;
	public GameObject pLTI;
	public GameObject pTRI;
	public GameObject pWater;
	public GameObject pVQI;
	public GameObject pDM;
	public GameObject pTDY;
	public GameObject pCPQI;
	public GameObject pDiversity;
	public GameObject pMTC;
	public GameObject pMarket;
	public GameObject pOpen1;
	public GameObject pOpen2;

	public GameObject arbol;
	public GameObject open;
	public GameObject lti;
	public GameObject vqi;
	public GameObject cpqi;
	public GameObject diversity;
	public GameObject tri;
	public GameObject dim;
	public GameObject tabacco;
	public GameObject leadeship;
	public GameObject smoke;
	public GameObject market;


	// Use this for initialization
	void Start () {
		layerMask = 1<<Layer;



	}

	private void ocultarVR(){
		
	}
	private void mostrarVR(){
		
	}


	void Update () {
	
		
		
		RaycastHit hit = new RaycastHit();
	        for (int i = 0; i < Input.touchCount; ++i) {
	            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
	            // Construct a ray from the current touch coordinates
	            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
	            if (Physics.Raycast(ray, out hit, layerMask)) {

					switch(hit.transform.gameObject.name){
					case "co2Touch":
						pCO2.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "ltiTouch":
						pLTI.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "triTouch":
						pTRI.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "waterTouch":
						pWater.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "vqiTouch":
						pVQI.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "dmTouch":
						pDM.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "tabaccoTouch":
						pTDY.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "cpqiTouch":
						pCPQI.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "diversityTouch":
						pDiversity.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "mtcTouch":
						pMTC.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "marketTouch":
						pMarket.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "open1Touch":
						pOpen1.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					case "open2Touch":
						pOpen2.SetActive (true);

						arbol.SetActive (false);
						portada.SetActive (false);
						break;
					
					
					

					}
					Debug.Log("Touch event is called "+ hit.transform.gameObject);
              	}
			}
		}
	}
}
