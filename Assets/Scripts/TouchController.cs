using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TouchController : MonoBehaviour
{
    private List<GameObject> sphereList;

    public int Layer;
    int layerMask;

    public GameObject portada;

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

                    print("Started sphere import...\n");
                    StartCoroutine(DownloadSpheres(nombre));



                    if (nombre != "arbol")
                    {
                        //arbol.SetActive(false);
                        //portada.SetActive(false);
                    }
                    else
                    {
                        //arbol.SetActive(true);
                        //portada.SetActive(true);

                    }


                    Debug.Log("Touch event is called: " + nombre);
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
        var items = KPICollection.CreateFromJSON(json2);
        print("Conversion");
        float x = 0, y = -0.5f, z = 3, r = 0.03f;
        x = -(0.06f * ((items.valores.Count / 2)));
        int columna = 0;
        foreach (var item in items.valores)
        {
            columna++;
            // Gather center coordinates, radius and level

            int level = 1;



            Vector3 v = new Vector3(x, y, z);


            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);

            float float_YTD = Convert.ToSingle(item.YTD);
            sphere.transform.position = new Vector3(x, y + (float_YTD / 2), z);
            x = x + (2 * (r + 0.002f));
            float d = 2 * r;

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



    }


    public void EliminarSphere()
    {
        foreach (var item in sphereList)
        {
            Destroy(item);
        }

    }

}
