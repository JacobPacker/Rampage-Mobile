using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHelper : MonoBehaviour
{

    Dir directionX;
    protected SpriteRenderer sr;


    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
    }

    public void SetSpriteXDirection(Dir dir)
    {
        
        // flipX == true makes sprite face left
        directionX = dir;

        sr.flipX = (dir == Dir.Left);
    }



    public Dir GetDirection()
    {
        return directionX;
    }

    public void Spawn()
    {

    }

    public void FaceOtherObject(GameObject other)
    {
        if (other.transform.position.x < transform.position.x)
        {
            SetSpriteXDirection(Dir.Left);
        }
        else
        {
            SetSpriteXDirection(Dir.Right);
        }
    }




    public void FlipDirection()
    {
        if (GetDirection() == Dir.Left)
        {
            SetSpriteXDirection(Dir.Right);
        }
        else
        {
            SetSpriteXDirection(Dir.Left);
        }
    }

    public void FlipDirectionIfWallHit(Collision col, Rigidbody2D rb)
    {
        Vector2 vel = rb.velocity;
        if (GetDirection() == Dir.Right)
        {

            if ((col.CheckPlatformRight() == false) || col.RightSideHit())
            {
                vel.x = -2;
                FlipDirection();
            }
        }

        if (GetDirection() == Dir.Left)
        {
            if (col.CheckPlatformLeft() == false || col.LeftSideHit())
            {
                FlipDirection();
                vel.x = 2;
            }
        }

        rb.velocity = vel;
    }

    public void PlayAnimAtFrame(Animator anim, string animName, int totalFrames, int frameToPlay)
    {
        anim.Play(animName, 0, (1f / totalFrames) * frameToPlay);
    }

    public void SetDynamic(Rigidbody2D rb)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetKinematic(Rigidbody2D rb)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }


    public void ZeroVelocity(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(0, 0);
    }







}
