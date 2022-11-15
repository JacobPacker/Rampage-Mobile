using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform target;
    public Vector3 target_Offset;
    public GameObject gui;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        if (target)
        {
            float oldZ = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, target.position + target_Offset, 0.05f);
            transform.position = new Vector3( transform.position.x, transform.position.y, oldZ );
        }

    }
}
