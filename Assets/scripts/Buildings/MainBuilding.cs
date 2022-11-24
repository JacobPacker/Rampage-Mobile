using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    public Animator anim;
    public GameObject[] buildingSegs;

    [HideInInspector]
    public bool destroyed = false;

    int numOfDestroyedSegs;
    int numOfSegs;
    int maxDestroyedSegs;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("BuildingIdle");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDestroy();
    }

    public void CheckForDestroy()
    {
        print("nos" + numOfSegs);
        numOfSegs = 0;
        numOfDestroyedSegs = 0;
        // Check how many segments are destroyed
        for (int i = 0; i < buildingSegs.Length; i++)
        {
            if (buildingSegs[i].GetComponent<BuildingSegments>().destroyed == true)
            {
                numOfDestroyedSegs++;
            }
            numOfSegs++;
        }

        maxDestroyedSegs = numOfSegs / 2;

        if (numOfDestroyedSegs >= maxDestroyedSegs)
        {
            Debug.Log("Building destroyed");
            anim.Play("BuildingCollapse");
        }
    }
}
