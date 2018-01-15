using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Lean.Touch;
using UnityEngine.UI;

public class ImportSpheres : MonoBehaviour
{
    
    public List<GameObject> sphereList;
    private List<TextMesh> titleList;

    public Text message;
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
        titleList = new List<TextMesh>();
        //print("Started sphere import...\n");
        //StartCoroutine(DownloadSpheres());
    }

    public void Iniciar()
    {
        message.text = "Importando KPI para mostrar...";
        StartCoroutine(DownloadSpheres());
    }



    void ExtractSpheres(string json)
    {
        string json2 = "{\"valores\":" + json + "}";
        message.text = "Creando KPI...";
        var items = KPICollection.CreateFromJSON(json2);

        //float x = 0, y = -0.01f, z = 0, r = 0.03f;

        //x = -(0.06f * ((items.valores.Count / 2)));



        int columna = 0;
        foreach (var item in items.valores)
        {
            columna++;

			float x =  UnityEngine.Random.Range(content.gameObject.transform.position.x - 3, content.gameObject.transform.position.x + 3);
			float y = UnityEngine.Random.Range(content.gameObject.transform.position.y -4, content.gameObject.transform.position.y + 4);
			float z = content.gameObject.transform.position.z -1;
			float r = 0.8f;

			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            GameObject text = new GameObject();
            TextMesh t = text.AddComponent<TextMesh>();


            sphere.name = item.Description;
            t.name = "KPI_" + item.YTD;

            sphere.transform.SetParent(content.gameObject.transform, false);
            t.transform.SetParent(sphere.gameObject.transform, false);

            sphere.transform.position = new Vector3(x, y, z);
            t.transform.localPosition = new Vector3( -1, 0 , 0);
            t.transform.localScale = new Vector3(.1f, .1f, 1);
            t.transform.localEulerAngles = new Vector3(90, 270, 90);


            t.text = item.Description;
            t.fontSize = 30;
            t.fontStyle = FontStyle.Bold;
            t.characterSize = 2;



            LeanSelectable selectable = sphere.AddComponent<LeanSelectable>();
            LeanTranslateSmooth translade = sphere.AddComponent<LeanTranslateSmooth>();
            translade.RequiredSelectable = selectable;
            translade.Dampening = 10;
            //sphere.AddComponent<Floater>();


            //sphere.AddComponent<LeanTranslate>();

            //sphere.AddComponent<LeanSelectableSpriteRendererColor>();






            sphere.transform.localScale = new Vector3(r, 0.009f, r);

            Color col = UnityEngine.Color.white;
            sphere.GetComponent<Renderer>().material.color = col;



            sphereList.Add(sphere);

            message.text = "Selecciona un KPI para continuar";
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