using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;
    public FirstPersonMovement MV;
    // Start is called before the first frame update
    void Start()
    {
        MV = GetComponent<FirstPersonMovement>();
        anim.SetTrigger("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (MV.targetVelocity.x > 0f ||
            MV.targetVelocity.y > 0f ||
            MV.targetVelocity.y < 0f ||
            MV.targetVelocity.x < 0f)
        {
            if (Input.GetKey(MV.runningKey))
            {
                anim.ResetTrigger("walking");
                anim.SetTrigger("running");
                
            }
            else
            {
                anim.ResetTrigger("running");
                anim.SetTrigger("walking");
            }
            

        }
        else
        {
            anim.SetTrigger("idle");
        }

        
    }
}
