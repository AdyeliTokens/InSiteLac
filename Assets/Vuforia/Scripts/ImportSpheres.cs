using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Lean.Touch;

public class ImportSpheres : MonoBehaviour
{
    private List<GameObject> sphereList;
    public GameObject content;
    

    public IEnumerator DownloadSpheres()
    {
        // Pull down the JSON from our web-service
        WWW w = new WWW("https://serverpmi.tr3sco.net/api/KPIs");
        yield return w;

        print("Waiting for sphere definitions\n");

        // Add a wait to make sure we have the definitions
        yield return new WaitForSeconds(1f);
        print("Received sphere definitions\n");
        print(w.text);
        // Extract the spheres from our JSON results
        ExtractSpheres(w.text);

    }


    void Start()
    {
        sphereList = new List<GameObject>();
        //print("Started sphere import...\n");
        //StartCoroutine(DownloadSpheres());
    }

    public void Iniciar()
    {
        print("Started sphere import...\n");
        StartCoroutine(DownloadSpheres());
    }



    void ExtractSpheres(string json)
    {
        string json2 = "{\"valores\":" + json + "}";

        var items = KPICollection.CreateFromJSON(json2);

        //float x = 0, y = -0.01f, z = 0, r = 0.03f;
        float x = content.gameObject.transform.position.x;
        float y = content.gameObject.transform.position.y;
        float z = content.gameObject.transform.position.z;
        float r = 0.69f;
        //x = -(0.06f * ((items.valores.Count / 2)));



        int columna = 0;
        foreach (var item in items.valores)
        {
            columna++;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sphere.name = item.Description;
            sphere.transform.SetParent(content.gameObject.transform, false);
            sphere.transform.position = new Vector3(x, y, z);
            sphere.AddComponent<LeanSelectable>();
            sphere.AddComponent<LeanSelectableSpriteRendererColor>(); 



            x = x + (r + 0.2f);
            

            sphere.transform.localScale = new Vector3(r, 0.009f, r);

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

    void Update()

    {

    }

}