using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

[RequireComponent(typeof(Animator))]
public class AnimationEventDispatcher : MonoBehaviour
{

    public delegate void MyAnimationEvent(string name);

    MyAnimationEvent OnAnimationStart;
    MyAnimationEvent OnAnimationComplete;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        List<string> completedAnimationClipName = new List<string>();
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];

            if (!completedAnimationClipName.Contains(clip.name))
            {

                Debug.Log("Register animation event~");
                completedAnimationClipName.Add(clip.name);
                AnimationEvent animationStartEvent = new AnimationEvent();
                animationStartEvent.time = 0;
                animationStartEvent.functionName = "AnimationStartHandler";
                animationStartEvent.stringParameter = clip.name;

                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = clip.name;

                clip.AddEvent(animationStartEvent);
                clip.AddEvent(animationEndEvent);
            }

        }
    }

    public void registerAnimationStartEvent(MyAnimationEvent handler)
    {
        OnAnimationStart += handler;
    }

    public void registerAnimationCompleteEvent(MyAnimationEvent handler)
    {
        OnAnimationComplete += handler;
    }

    public void clearAllEvents()
    {
        OnAnimationStart = null;
        OnAnimationComplete = null;
    }

    void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} animation start.");
        OnAnimationStart?.Invoke(name);
    }
    void AnimationCompleteHandler(string name)
    {
        Debug.Log($"{name} animation complete.");
        OnAnimationComplete?.Invoke(name);
    }
}