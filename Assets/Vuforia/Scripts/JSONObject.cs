using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONObject : MonoBehaviour
{
    private string json;

    public JSONObject(string json)
    {
        this.json = json;
    }

    public IEnumerable<JSONObject> list { get; set; }

    void Start()
    {

    }
    
    void Update()
    {

    }
}
