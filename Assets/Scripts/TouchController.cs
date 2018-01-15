using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    private List<GameObject> sphereList;

    public int Layer;
    int layerMask;

    public GameObject portada;
    public Text message;

    // Use this for initialization
    void Start()
    {
        sphereList = new List<GameObject>();
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
                        message.text= "Importando informacion de " + nombre + "...";
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
        Description = Description.Replace(" ", "%20");
        Debug.Log(Description);
        // Pull down the JSON from our web-service
        WWW w = new WWW("https://serverpmi.tr3sco.net/api/KPIs?Description=" + Description);
        yield return w;
        EliminarSphere();
        print("Waiting for sphere definitions\n");

        // Add a wait to make sure we have the definitions
        yield return new WaitForSeconds(1f);
        print("Received sphere definitions\n");
        print(w.text);
        // Extract the spheres from our JSON results
        ExtractSpheres(w.text);

    }

    void ExtractSpheres(string json)
    {
        string json2 = "{\"valores\":" + json + "}";
        var kpis = KPICollection.CreateFromJSON(json2).valores.OrderByDescending(h => h.YTD);
        var kpiMasAlto = kpis.FirstOrDefault();
        kpiMasAlto.porcentaje = 100;

        foreach (var item in kpis)
        {
            item.porcentaje = (item.YTD * 100) / kpiMasAlto.YTD;
        }


        message.text = "Conversion de datos a graficas..." ;
        float x = 0, y = -0.5f, z = 3, r = 0.03f;
        x = -(0.06f * ((kpis.Count() / 2)));
        int columna = 0;
        foreach (var item in kpis)
        {
            columna++;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
			GameObject text = new GameObject();


			TextMesh t = text.AddComponent<TextMesh>();
			t.text = item.Description;
			t.fontSize = 30;

            float float_YTD = 0.01f * Convert.ToSingle(item.porcentaje);
            sphere.name = float_YTD.ToString();

            sphere.transform.position = new Vector3(x, y + (float_YTD / 2), z);
			t.transform.localPosition = new Vector3(x, (y + ((float_YTD / 2))+ float_YTD) + 0.02f, z);


            x = x + (2 * (r + 0.002f));
            float d = 2 * r;


			t.transform.localEulerAngles += new Vector3(90, 0, 0);


            sphere.transform.localScale = new Vector3(d, d + float_YTD, d);

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
        }

        message.text = "Grafica creada.";

    }


    public void EliminarSphere()
    {
        foreach (var item in sphereList)
        {
            Destroy(item);
        }

    }

}
