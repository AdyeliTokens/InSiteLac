using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImportSpheres : MonoBehaviour
{
    public GameObject texto;

    IEnumerator DownloadSpheres()
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

        print("Started sphere import...\n");
        StartCoroutine(DownloadSpheres());

    }



    void ExtractSpheres(string json)
    {
        string json2 = "{\"valores\":" + json + "}";
        var items = KPICollection.CreateFromJSON(json2);
        print("Conversion");
        float x = -0.9f, y = 0, z = 2, r = 0.03f;
        foreach (var item in items.valores)
        {

            // Gather center coordinates, radius and level
            
            int level = 1;

            

            Vector3 v = new Vector3(x , y, z);

            
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
            float float_YTD = Convert.ToSingle(item.YTD);
            sphere.transform.position = new Vector3(x, y + (float_YTD / 2), z);
            x = x + (2 * (r + 0.002f));
            float d = 2 * r;

            sphere.transform.localScale = new Vector3(d, d + float_YTD, d);

            UnityEngine.Color col = UnityEngine.Color.white;
            switch (level)
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

                    break;

            }

            sphere.GetComponent<Renderer>().material.color = col;



        }


    }



    void Update()

    {

    }

}
