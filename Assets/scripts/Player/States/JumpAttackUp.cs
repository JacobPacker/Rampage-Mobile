using UnityEngine;
namespace Player
{
    public class JumpAttackUpState : State
    {
        public JumpAttackUpState(PlayerScript player, StateMachine sm) : base(player, sm) { }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("JumpAttackUp", 0, 0);
            //player.xv = player.yv = 0;
        }

        public override void Exit()
        {
            base.Exit();

            //player.anim.SetBool("run", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            //player.CheckForStand();


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            player.CheckForLand();
        }
    }
}