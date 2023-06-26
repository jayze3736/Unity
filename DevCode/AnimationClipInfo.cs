using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace jsh
{
    public class AnimationClipInfo
    {
        public static AnimationClip FindAnimationClip(Animator animator, string clipName)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            foreach (var clip in clips)
            {

                if (clip.name == clipName)
                {
                    return clip;
                }


            }

            return null;


        }



    }

}
