using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip playSound;
    public float volume = 10f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;
    // hen gio am thanh bi tri hoan
    public float playDelay = 0.25f;
    private float timeSinceEntered = 0; //thời gian kể từ khi nhập
    private bool hasDelaySoundPlayed = false; //đã phát âm thanh trễ
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnEnter)
        {
            AudioSource.PlayClipAtPoint(playSound, animator.gameObject.transform.position, volume);
        }
        timeSinceEntered = 0f;
        hasDelaySoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playAfterDelay && !hasDelaySoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            if(timeSinceEntered> playDelay)
            {
                AudioSource.PlayClipAtPoint(playSound, animator.gameObject.transform.position, volume);
                hasDelaySoundPlayed = true;
            }
        
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(playSound, animator.gameObject.transform.position, volume);
        }
    }

 
}
