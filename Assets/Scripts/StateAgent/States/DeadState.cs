using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateAgent.States
{
    internal class DeadState : State
    {
        public DeadState(global::StateAgent owner) : base(owner)
        {

        }

        public override void OnEnter()
        {
            owner.animator.SetBool("isDead", true);
            owner.movement.Stop();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            if (owner.animationDone)
            {
                GameObject.Destroy(owner.gameObject,1);
            }
        }
    }
}
