using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementBase : MonoBehaviour
{
    public float mSpeed = 5.0f;
    public Transform mRotationTransform;
    public float mRotationSpeed = 400.0f;
    public Animator mAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Height 계산
        Vector3 INowPosition = transform.position + new Vector3(0, 100, 0);
        Vector3 IDirection = new Vector3(0, -1, 0);
        RaycastHit IHit;
        int layermask = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(INowPosition, IDirection, out IHit, 200, layermask))
        {
            float IHeight = IHit.point.y;
            Vector3 INewPos = transform.position;
            INewPos.y = IHeight;
            transform.position = INewPos;
        }
    }
}
