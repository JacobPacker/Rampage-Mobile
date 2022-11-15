using UnityEngine;
namespace Player
{
    public class JumpingState : State
    {
        // constructor
        public JumpingState(PlayerScript player, StateMachine sm) : base(player, sm) { }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_jump_up", 0, 0);
            player.xv = player.yv = 0;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("stand", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.CheckForStand();


            player.SetWalkState();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}