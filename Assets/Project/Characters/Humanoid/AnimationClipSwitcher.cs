using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEditor;
public class AnimationClipSwitcher
{
    private Animator animator;
    private AnimatorOverrideController overrideController;
    private AnimationClipOverrides clipOverrides;



    public AnimationClipSwitcher(Animator animator)
    {
        this.animator = animator;
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        clipOverrides = new AnimationClipOverrides(overrideController.overridesCount);
        overrideController.GetOverrides(clipOverrides);
    }

    public void SwitchClipForState(String state, AnimationClip clip)
    {
        clipOverrides[state] = clip;
        //Debug.Log(state + ": " + clipOverrides[state] +  ": " + clip);
        overrideController.ApplyOverrides(clipOverrides);
    }

    public void PrintCurrentState()
    {
        foreach (AnimationClip clip in overrideController.animationClips)
        {
             //Debug.Log(clip.name);
        }
       // Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

    }

    private class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                {
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);

                }
            }
        }
    }
}
