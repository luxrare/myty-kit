using System;
using System.Collections.Generic;
using UnityEngine;

namespace MYTYKit.Controllers
{
    [Serializable]
    public class RS2DPivot
    {
        public string name;
        public Vector2 position;
        public List<RiggingEntity> riggingState;
    }

    public class RiggedSprite2DNearstController : BoneController, IVec2Input
    {
        public Vector2 controlPosition;
        public float xScale = 1.0f;
        public float yScale = 1.0f;
        public Vector2 bottomLeft = new Vector2(0, 0);
        public Vector2 topRight = new Vector2(1, 1);
        public List<RS2DPivot> pivots = new();

        List<RiggingEntity> diffBuffer;

        public void Update()
        {
            Debug.Log("update");
            var interpList = CalcInterpolate();
            if (interpList == null) return;
            diffBuffer = CalcDiff(orgRig, interpList);

        }


        public override void ApplyDiff()
        {
            if (diffBuffer == null || diffBuffer.Count == 0) return;
            AccumulatePose(diffBuffer);
        }


        protected override List<RiggingEntity> CalcInterpolate()
        {

            if (orgRig == null || orgRig.Count == 0) return null;

            var u = Math.Abs(controlPosition.x) / xScale;
            var v = Math.Abs(controlPosition.y) / yScale;

            var pos = new Vector2(u, v);
            int selected = -1;
            var minDist = float.MaxValue;
            for (var index = 0; index < pivots.Count; index++)
            {
                var dist = (pos - pivots[index].position).magnitude;
                if (dist < minDist)
                {
                    selected = index;
                    minDist = dist;
                }

            }

            Debug.Log("selected : " + selected);
            if (selected < 0) return null;

            return pivots[selected].riggingState;
        }

        public void SetInput(Vector2 val)
        {
            controlPosition = val;
        }

    }
}