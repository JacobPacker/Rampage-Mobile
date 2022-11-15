using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float deathTimer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    SpriteHelper sh;
    public Dir playerDir;
    public Dir weaponDir;

    // Start is called before the first frame update


    public UnityEngine.Sprite[] weaponSprites;

    private void Awake()
    {
        sh = this.AddComponent<SpriteHelper>();
        

    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        deathTimer = 45 * Time.fixedDeltaTime;

        //

        if (weaponDir == Dir.Forward)
        {
            rb.velocity = new Vector2(playerDir == Dir.Left ? -16 : 16, 0);
            sr.sprite = weaponSprites[0];


        }
        else if (weaponDir == Dir.Down)
        {
            rb.velocity = new Vector2(0, -16);
            sr.sprite = weaponSprites[1];


        }
        else
        {
            rb.velocity = new Vector2(0, 16);
            sr.sprite = weaponSprites[2];
        }

        sh.SetSpriteXDirection(playerDir);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //sprite.SetSpriteXDirection(dir);
        deathTimer -= Time.fixedDeltaTime;
        if (deathTimer < 0)
        {
            Destroy(gameObject);
        }
            
        
    }
}
