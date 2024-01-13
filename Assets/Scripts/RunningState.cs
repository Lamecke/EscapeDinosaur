using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
   

    public override Vector3 ProcessMotion() {
        Vector3 motion = Vector3.zero;
        motion.x = playerController.SnapToLane();
        motion.y = -1.0f;
        motion.z = playerController.baseRunSpeed;

        return motion;

    }
    public override void Transition() {

        if (InputManager.Instance.SwipeRight) {
            playerController.ChangeLane(1);
        
        }
        if (InputManager.Instance.SwipeLeft) {
            playerController.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeUp && playerController.isGrounded) { }

    }
}
