using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuildings : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<BuildingSegments>(out BuildingSegments segments))
        {
            segments.TakeDamage(1); 
        }
    }
}
