using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYTYKit.MotionTemplate.Mediapipe.Model
{
    public class MPRightWrist : MPJointModel
    {


        private void LateUpdate()
        {
            if (rawPoints == null) return;
            var pinkey = rawPoints[0] - rawPoints[17];
            lookAt = rawPoints[17] - rawPoints[5];

            up = -Vector3.Cross(pinkey, lookAt);
            lookAt.Normalize();
            up.Normalize();
            UpdateTemplate();
        }

    }
}