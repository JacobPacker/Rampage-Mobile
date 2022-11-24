using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator anim;
        Collision col;
        public SpriteHelper sh;
        public LayerMask platformLayerMask;

        public bool onPlatform;

        public bool jumpFlag, jumpButtonPressed, jumpButtonReleased;
        public bool shootButtonPressed, shootButtonReleased;
        public bool upButtonPressed, downButtonPressed;
        public bool leftButtonPressed, rightButtonPressed;

        public float fall = 10f;
        public float jumpGravity = 0.6f;
        public float initialJumpVel = 10f;
        public float xv, yv;

        Dir lastDir;
        public Dir currentDir;

        public float runSpeed = 6;

        LevelManager lm;

        // variables holding the different player states
        public StandingState standingState;
        public WalkingState walkingState;
        public JumpingState jumpingState;
        public AttackState attackState;
        public AttackUpState attackUpState;
        public JumpAttackState jumpAttackState;
        public JumpAttackUpState jumpAttackUpState;

        public StateMachine sm;

        [SerializeField] private MobileButton jumpButton;
        [SerializeField] private MobileButton attackButton;
        [SerializeField] private MobileJoystick joystick;

        private void Awake()
        {
            sh = gameObject.AddComponent<SpriteHelper>();
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            col = gameObject.AddComponent<Collision>();
            lm = LevelManager.lm;
            sm = gameObject.AddComponent<StateMachine>();

            // add new states here
            standingState = new StandingState(this, sm);
            walkingState = new WalkingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            attackState = new AttackState(this, sm);
            attackUpState = new AttackUpState(this, sm);
            jumpAttackState = new JumpAttackState(this, sm);
            jumpAttackUpState = new JumpAttackUpState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(standingState);

            
            sh.SetSpriteXDirection(Dir.Right);

        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();

            //output debug info to the canvas
            string s;
            s = string.Format("onplat={0} jumpFlag={1}\nlast state={2}\ncurrent state={3}", onPlatform, jumpFlag, sm.LastState, sm.CurrentState);
            UIscript.ui.DrawText(s);

            s = string.Format("current dir2={0} lastdir={1} yv={2}", currentDir, lastDir, yv);
            UIscript.ui.DrawText(s);

            s = string.Format("shoot button={0} ", shootButtonPressed);
            UIscript.ui.DrawText(s);

            // Press R to reset the player's position
            DebugPlayer();
            //isGrounded();

          
        }

        void FixedUpdate()
        {
            
            // this is called for all states
            col.CheckTileCollisionPlatform(platformLayerMask, 0.38f, 0.4f, 0.11f);
            onPlatform = col.PlatformHit();
            col.ShowDebugCollisionPoints();

            sm.CurrentState.PhysicsUpdate();
            rb.velocity = new Vector2(xv, yv);
        }

       

        public void CheckForLand()
        {
            Vector2 pos = rb.position;


            // check for landing on a platform

            if ((yv <= 1) && (onPlatform == true))
            {
                yv = 0;
                rb.velocity = new Vector2(0, 0);

                jumpFlag = false;

                // round to 0.5
                pos.y = Mathf.Round(pos.y);

                //pos.y = (Mathf.Round(pos.y * 2)) / 2;

                //print("Landed! y was=" + transform.position.y + "  and is now " + pos.y);

                sm.ChangeState(standingState);

                rb.transform.position = pos;
            }
            
        }



        public void CheckForStand()
        {
            
            if (onPlatform == true)
            {

                if (joystick.x == 0f) // key held down
                {
                    if (joystick.y == 0f) // joystick moved
                    {
                        sm.ChangeState(standingState);
                    }
                }

            }
            
            // check for changing direction
            if (currentDir != lastDir)
            {
                // player has changed direction
                //sm.ChangeState(standingState);
            }
        }


        public void SetWalkState()
        {
            if (onPlatform)
            {
                if (!attackButton.isPressing)
                {
                    if (joystick.x > 0.1f)
                    {
                        sm.ChangeState(walkingState);
                    }

                    if (joystick.x < -0.1f)
                    {
                        sm.ChangeState(walkingState);
                    }
                }
            }
        }

        public void SetMoveDirectionAndVelocity()
        {
            if (joystick.x < -0.1f)
            {
                currentDir = Dir.Left;
                xv = -runSpeed;
                gameObject.transform.localScale = new Vector3(-3, 3, 1);
            }
            else if (joystick.x > 0.1f)
            {
                currentDir = Dir.Right;
                xv = runSpeed;
                gameObject.transform.localScale = new Vector3(3, 3, 1);
            }
            else
            {
                xv = 0;
            }
        }

        public void SetJumpState()
        {
            if (Input.GetKey("space") == true || jumpButton.isPressing)
            {
                if (!onPlatform)
                {
                    return;
                }
                else
                {
                    sm.ChangeState(jumpingState);
                    yv = 10f;
                    //yv = initialJumpVel;
                }
            }
        }

        public void SetJumpsState()
        {
            if (Input.GetKey("space") == true || jumpButton.isPressing)
            {
                if (!onPlatform)
                {
                    return;
                }
                else
                {
                    sm.ChangeState(jumpingState);
                    yv = 10f;
                    //yv = initialJumpVel;
                }
            }
        }

        public void SetAttackState()
        {
            if (attackButton.isPressing)
            {
                if (!onPlatform)
                {
                    return;
                }
                else
                {
                    sm.ChangeState(attackState);
                }
            }
        }

        public void SetAttackUpState()
        {
            if(attackButton.isPressing && joystick.y > 0.5f)
            {
                if (!onPlatform)
                {
                    return;
                }
                else
                {
                    sm.ChangeState(attackUpState);
                }
            }
        }

        public void SetJumpAttackState()
        {
            if (attackButton.isPressing)
            {
                if (!onPlatform)
                {
                    sm.ChangeState(jumpAttackState);
                }
                else
                {
                    return ;
                }
            }
        }

        public void SetJumpAttackUpState()
        {
            if (attackButton.isPressing && joystick.y > 0.5f)
            {
                if (!onPlatform)
                {
                    sm.ChangeState(jumpAttackUpState);
                }
                else
                {
                    return;
                }
            }
        }

        public void ReadInputKeys()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                leftButtonPressed = true;
            }
            else
            {
                leftButtonPressed = false;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rightButtonPressed = true;
            }
            else
            {
                rightButtonPressed = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                jumpButtonPressed = true;
                jumpButtonReleased = false;
            }
            else
            {
                jumpButtonPressed= false;
                jumpButtonReleased= true;
            }

            if (Input.GetKey(KeyCode.LeftControl) && (shootButtonReleased == true))
            {
                shootButtonReleased = false;
                shootButtonPressed = true;
            }
            else
            {
                shootButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.LeftControl) == false)
            {
                shootButtonReleased = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                jumpButtonPressed = true;
            }
            else
            {
                jumpButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                upButtonPressed = true;
            }
            else
            {
                upButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                downButtonPressed = true;
            }
            else
            {
                downButtonPressed = false;
            }

        }

        public void DoFall()
        {
            if (!onPlatform)
            {
                if( yv > -4 )
                    yv = -fall;
            }
            else
            {
                yv = 0;
            }
        }

        public void DoJump()
        {
            if (yv > -5)
            {
                yv -= 0.6f; 
            }

        }



        void DebugPlayer()
        {
            // reset player position with "R" key
            if (Input.GetKeyDown("r"))
            {
                gameObject.transform.position = new Vector2(-7, 4);
                rb.velocity = new Vector2(0, 0);
                xv = yv = 0;
            }

        }


        // executed from attack anim event
        public void ChangeStateToStand()
        {
            sm.ChangeState(standingState);
        }

    }

}