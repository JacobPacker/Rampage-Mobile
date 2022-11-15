using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

bool platformHit, wallLeftHit, wallRightHit;
RaycastHit2D l1_hit, l2_hit, r1_hit, r2_hit, m1_hit, m2_hit, lw_hit, rw_hit;
Vector2 l1,l2,r1,r2, m1,m2;
Vector2 l1_end,l2_end,r1_end,r2_end, m1_end, m2_end, lw, lw_end, rw, rw_end;

    public void CheckTileCollisionPlatform( LayerMask layermask, float xoffs, float yoffs1, float yoffs2 )
    {
    


        float lineLength = 0.16f;

        // Collision points
        // XXXXXXXXXXXXXXXXXXXXXX
        // XXXXXXXXXXXXXXXXXXXXXX
        // XX XXXXXXXXXXXXXXXX XX
        // XX XXXXXXXXXXXXXXXX XX
        // XX L1 XXX M1 XXX R1 XX
        // XX L2 XXX M2 XXX R2 XX


        // need to adjust values these when on balloon
        //float xo = 0.38f;
        //float yo1 = 0.45f;
        //float yo2 = 0.1f;


        // set up l1
        l1.x = transform.position.x - xoffs;
        l1.y = transform.position.y + yoffs1;
        l1_end=l1;
        l1_end.x -= lineLength;


        //set up l2
        l2.x = l1.x;
        l2.y = transform.position.y - yoffs2;
        l2_end = l2;
        l2_end.x -= lineLength;

        //set up r1
        r1.x = transform.position.x + xoffs;
        r1.y = transform.position.y + yoffs1;
        r1_end = r1;
        r1_end.x += lineLength;

        //set up r2
        r2.x = r1.x;
        r2.y = l2.y;
        r2_end = r2;
        r2_end.x += lineLength;

        //set up m1 (mid 1)
        m1.x = transform.position.x - lineLength;
        m1.y = transform.position.y + yoffs1;
        m1_end = m1;
        m1_end.x += lineLength*2;

        //set up m2 (mid 2)
        m2.y = transform.position.y - yoffs2;
        m2.x = m1.x;
        m2_end = m2;
        m2_end.x = m1_end.x;






        // do a raycast at all 4 points
        l1_hit = Physics2D.Linecast(l1,l1_end, layermask.value);
        l2_hit = Physics2D.Linecast(l2,l2_end, layermask.value);
        r1_hit = Physics2D.Linecast(r1,r1_end, layermask);
        r2_hit = Physics2D.Linecast(r2,r2_end, layermask);
        m1_hit = Physics2D.Linecast(m1,m1_end, layermask);
        m2_hit = Physics2D.Linecast(m2,m2_end, layermask);


        

        // check if player is on a platform
        if ((l1_hit==false && l2_hit==true ) || (r1_hit==false && r2_hit==true ) || (m1_hit==false && m2_hit==true) )
        {
            platformHit=true;
        }
        else
        {
            platformHit=false;
        }

    }

    public void CheckTileCollisionSides( LayerMask layerMask, float xoffs, float yoffs1, float yoffs2 )
    {
        // Collision points
        // XXXXXXXXXXXXXXXXXX
        // XXXXXXXXXXXXXXXXXX
        // LW XXXXXXXXXXXX RW
        // LW XXXXXXXXXXXX RW
        // LW XXXXXXXXXXXX RW
        // LW XXXXXXXXXXXX RW

        //int layerMaskValue = layerMask.value;

        //these aren't used when on a balloon
        //set up left wall
        lw = transform.position;
        lw.x -= xoffs;
        lw.y += yoffs1;
        lw_end = lw;
        lw_end.y = transform.position.y + yoffs2;

        //set up right wall
        rw = transform.position;
        rw.x += xoffs;
        rw.y += yoffs1;
        rw_end = rw;
        rw_end.y = lw_end.y;

        lw_hit = Physics2D.Linecast(lw,lw_end, layerMask);
        rw_hit = Physics2D.Linecast(rw,rw_end, layerMask);




    }

    public void ShowDebugCollisionPoints()
    {
        Debug.DrawLine( l1,l1_end, l1_hit?Color.red:Color.white );
        Debug.DrawLine( l2,l2_end, l2_hit?Color.red:Color.white );
        Debug.DrawLine( r1,r1_end, r1_hit?Color.red:Color.white );
        Debug.DrawLine( r2,r2_end, r2_hit?Color.red:Color.white );

        Debug.DrawLine( m1,m1_end, m1_hit?Color.red:Color.white );
        Debug.DrawLine( m2,m2_end, m2_hit?Color.red:Color.white );

        Debug.DrawLine( lw,lw_end, lw_hit?Color.blue:Color.green );
        Debug.DrawLine( rw,rw_end, rw_hit?Color.red:Color.white );

    }

    public bool PlatformHit()
    {
        return platformHit;
    }


    public bool LeftSideHit()
    {
        return lw_hit;
    }

    public bool RightSideHit()
    {
        return rw_hit;
    }

    // is right side of object touching a platform?
    public bool CheckPlatformRight()
    {
        
        return r2_hit;
        
    }

    // is left side of object touching a platform?
    public bool CheckPlatformLeft()
    {
        return l2_hit;
    }


}
