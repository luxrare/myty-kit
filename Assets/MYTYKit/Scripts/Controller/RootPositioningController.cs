using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RootPositioningController : MYTYController, IVec3Input
{
    public GameObject targetObject;
    public Vector3 displacement;

    private Vector3 m_initPos;

    public override void PostprocessAfterLoad(Dictionary<GameObject, GameObject> objMap)
    {
        targetObject = objMap[targetObject];
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            var so = new SerializedObject(this);
            so.FindProperty("targetObject").objectReferenceValue = targetObject;
            so.ApplyModifiedProperties();
        }
#endif
    }

    public override void PrepareToSave()
    {
#if UNITY_EDITOR
        targetObject = PrefabUtility.GetCorrespondingObjectFromSource(targetObject);    
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        if (targetObject == null) return;
        m_initPos = targetObject.transform.position;

    }

    private void LateUpdate()
    {
        if (targetObject == null) return;
       
        targetObject.transform.position = m_initPos + displacement;
    }

    public void SetInput(Vector3 val)
    {
        displacement = val;
    }
}
