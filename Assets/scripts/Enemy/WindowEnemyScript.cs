using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEnemyScript : MonoBehaviour
{
    public int time;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        time = Random.Range(1, 10);

        yield return new WaitForSeconds(time);

        anim.Play("Spawn");
    }
}
