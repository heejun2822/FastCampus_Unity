using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPcUnitMovement : UnitMovementBase
{
    // Start is called before the first frame update
    void Start()
    {
        GameControl.aInstance.aOnMoving += HandleMoving;
        GameControl.aInstance.aOnMoveStart += HandleMoveStart;
        GameControl.aInstance.aOnMoveEnd += HandleMoveEnd;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameControl.aInstance.aOnMoving -= HandleMoving;
        GameControl.aInstance.aOnMoveStart -= HandleMoveStart;
        GameControl.aInstance.aOnMoveEnd -= HandleMoveEnd;
    }

    private void HandleMoving(Vector3 pDirect)
    {
        // 이동
        transform.position += pDirect * mSpeed * Time.deltaTime;
        // 회전
        mRotationTransform.rotation = Quaternion.RotateTowards(mRotationTransform.rotation,
                                                                Quaternion.LookRotation(pDirect),
                                                                mRotationSpeed * Time.deltaTime);
    }

    private void HandleMoveStart()
    {
        if (mAnimator != null)
        {
            mAnimator.CrossFade("Run", 0.1f);
        }
    }
    private void HandleMoveEnd()
    {
        if (mAnimator != null)
        {
            mAnimator.CrossFade("Idle", 0.1f);
        }
    }
}
