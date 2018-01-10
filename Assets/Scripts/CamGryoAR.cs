using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamGryoAR : MonoBehaviour
{

    public GameObject webPlane;
    GameObject camParent;


    // Use this for initialization
    void Start()
    {
        camParent = new GameObject("CamParent");
        camParent.transform.position = transform.position;
        transform.parent = camParent.transform;
        camParent.transform.Rotate(Vector3.right, 90);
        Input.gyro.enabled = true;

        WebCamTexture webcamTexture = new WebCamTexture();
        webPlane.GetComponent<MeshRenderer>().material.mainTexture = webcamTexture;

    }

    void Update()
    {
        Quaternion rotFix = new Quaternion(Input.gyro.attitude.x,
            Input.gyro.attitude.y,
            -Input.gyro.attitude.z,
            -Input.gyro.attitude.w);

        transform.localRotation = rotFix;
    }
}
