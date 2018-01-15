using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    private List<GameObject> sphereList;
	private List<TextMesh> titleList;

	private TextMesh title;

    public int Layer;
    int layerMask;

    public GameObject portada;
    public Text message;

    // Use this for initialization
    void Start()
    {
        sphereList = new List<GameObject>();
		titleList = new List<TextMesh>();
		title = new TextMesh();

        layerMask = 1 << Layer;

    }

    void Update()
    {

        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit, layerMask))
                {
                    string nombre = hit.transform.gameObject.name;
                    if (nombre != "arbol")
                    {
						EliminarSphere();
                        message.text = "Importando informacion de " + nombre + "...";
                        StartCoroutine(DownloadSpheres(nombre));
                    }
                    else
                    {
                        EliminarSphere();
                    }
                    Debug.Log("Touch event is called: " + nombre + "...");
                }
            }
        }
    }

    public IEnumerator DownloadSpheres(string Description)
    {

        string descriptionSinEspacios = Description.Replace(" ", "%20");
        
        // Pull down the JSON from our web-service
        WWW w = new WWW("https://serverpmi.tr3sco.net/api/KPIs?Description=" + descriptionSinEspacios);
        yield return w;
		message.text = "Esperando informacion de " + Description + "... ";
		// Add a wait to make sure we have the definitions
        yield return new WaitForSeconds(1f);
        print("Received sphere definitions\n");
        print(w.text);
        // Extract the spheres from our JSON results
        ExtractSpheres(Description, w.text);

    }

    void ExtractSpheres(string description, string json)
    {
        string json2 = "{\"valores\":" + json + "}";
        var kpis = KPICollection.CreateFromJSON(json2).valores.OrderByDescending(h => h.YTD);
        var kpiMasAlto = kpis.FirstOrDefault();
        kpiMasAlto.porcentaje = 100;

        foreach (var item in kpis)
        {
            item.porcentaje = (item.YTD * 100) / kpiMasAlto.YTD;
        }

        float x = 0, y = -0.5f, z = 3, r = 0.03f;
        x = -(0.06f * ((kpis.Count() / 2)));
        int columna = 0;

        GameObject titulo = new GameObject();
        TextMesh tGrafica = titulo.AddComponent<TextMesh>();
        tGrafica.name = "titulo_"+ description;
        tGrafica.text = description;
        tGrafica.fontSize = 30;
        tGrafica.fontStyle = FontStyle.Bold;
        tGrafica.characterSize = 2;
        tGrafica.transform.localPosition = new Vector3(x, y + 1.3f , z);
        tGrafica.transform.localEulerAngles = new Vector3(0, 0, 0);
        tGrafica.transform.localScale = new Vector3(0.02f, 0.02f, 1);
        tGrafica.anchor = TextAnchor.UpperCenter;
        


        message.text = "Conversion de datos a graficas...";
        
        foreach (var item in kpis)
        {
            columna++;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject text = new GameObject();
            TextMesh t = text.AddComponent<TextMesh>();
			float rounded = (float)(Math.Round(item.YTD, 2));
			t.text = rounded.ToString();
            t.fontSize = 30;



            float float_YTD = 0.005f * Convert.ToSingle(item.porcentaje);
            sphere.name = float_YTD.ToString();
			t.name = "text_" + item.YTD; 
            

            sphere.transform.position = new Vector3(x , y + (float_YTD / 2), z);
            t.transform.localPosition = new Vector3(x - 0.05f , (y +  float_YTD) + 0.20f, z);


            x = x + (6 * (r + 0.010f));
            float d = 6 * r;


            t.transform.localEulerAngles = new Vector3(0, 0, 45);

            sphere.transform.localScale = new Vector3(d, d + float_YTD, d);
            t.transform.localScale = new Vector3(0.02f,0.02f, 1);

            UnityEngine.Color col = UnityEngine.Color.white;
            switch (columna)
            {
                case 1:
                    col = UnityEngine.Color.red;
                    break;
                case 2:
                    col = UnityEngine.Color.yellow;
                    break;
                case 3:
                    col = UnityEngine.Color.green;
                    break;
                case 4:
                    col = UnityEngine.Color.cyan;
                    break;
                case 5:
                    col = UnityEngine.Color.magenta;
                    break;
                case 6:
                    col = UnityEngine.Color.blue;
                    break;
                case 7:
                    col = UnityEngine.Color.grey;
                    columna = 0;
                    break;
            }

            sphere.GetComponent<Renderer>().material.color = col;

            sphereList.Add(sphere);
			titleList.Add (t);
			title = tGrafica;
        }

        message.text = "Grafica creada.";

    }


    public void EliminarSphere()
    {
        foreach (var item in sphereList)
        {
            Destroy(item);
        }
		foreach (var item in titleList)
		{
			Destroy(item);
		}
		Destroy(title);
    }

}
