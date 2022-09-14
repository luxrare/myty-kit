using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WeightedSum2DAdapterV2 : NativeAdapter
{

    public ParametricTemplate face;
    public List<string> xParamNames;
    public List<string> yParamNames;
    public List<float> weights;
    public MYTYController controller;

    public float stabilizeTime = 0.1f;
    float m_elapsed = 0;

    void Update()
    {
        if (xParamNames.Count != yParamNames.Count) return;
        if (xParamNames.Count != weights.Count) return;
        var input = controller as IVec2Input;
        if (input == null) return;

        m_elapsed += Time.deltaTime;
        if (m_elapsed < stabilizeTime)
        {
            input.SetInput(GetStabilizedVec2());
            return;
        }

        m_elapsed = 0;

        var weightedSum = Vector2.zero;
        for (int i = 0; i < weights.Count; i++)
        {
            weightedSum += weights[i] * new Vector2(face.GetValue(xParamNames[i]), face.GetValue(yParamNames[i]));
        }

        Stabilize(weightedSum);
        input.SetInput(GetStabilizedVec2());
    }
}