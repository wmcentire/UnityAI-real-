using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateAgent.States
{
    internal class FleeState : State
    {
        public FleeState(global::StateAgent owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            owner.navigation.targetNode = null;
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate()
        {
            if (owner.enemySeen)
            {
                Vector3 direction = (owner.transform.position - owner.perceived[0].transform.position).normalized;
                owner.movement.MoveTowards(owner.transform.position + direction * 5);
            }
        }
    }
}
