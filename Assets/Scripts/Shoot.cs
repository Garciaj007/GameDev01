using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float maxdistance;
    public LayerMask mask;

    private Camera mainCam;

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera not found...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") == 1)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, maxdistance, mask)){
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
}
