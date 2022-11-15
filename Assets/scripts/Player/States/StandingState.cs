
using UnityEngine;
namespace Player
{
    public class StandingState : State
    {
        // constructor
        public StandingState(PlayerScript player, StateMachine sm) : base(player, sm) { }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_stand", 0, 0);
            player.xv = player.yv = 0;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("stand", false );
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.SetWalkState();
            player.SetJumpState();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}