using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnStateEnter, updateOnStateExit;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    public float valueOnEnter, valueOnExit;
    //OnStateEnter được gọi trước khi OnstateEnter được gọi ở bất kỳ trạng thái nào trong máy trạng thái này
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }

    }
    // OnStateExit được gọi trước khi OnstateExit được gọi trên bất kỳ trạng thái nào trong máy trạng thái
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }
    //OnStateMachineEnter được gọi khi vào máy trạng thái thông qua Nút nhập của nó
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)

            animator.SetFloat(floatName, valueOnEnter);

    }
    //StateMachinExit nó được gọi khi thoát khỏi máy trạng thái thông qua nút Thoát

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }


}
