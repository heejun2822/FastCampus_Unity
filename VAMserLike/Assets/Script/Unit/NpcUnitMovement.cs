using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcUnitMovement : UnitMovementBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveToMyPc();
    }

    private void MoveToMyPc()
    {
        Vector3 ITargetDirection = GameDataManager.aInstance.GetMyPcObject().transform.position - transform.position;
        Vector3 IDirect = ITargetDirection.normalized;

        transform.position += IDirect * mSpeed * Time.deltaTime;
        if (IDirect != Vector3.zero)
        {
            mRotationTransform.rotation = Quaternion.RotateTowards(mRotationTransform.rotation,
                                                                    Quaternion.LookRotation(IDirect, Vector3.up),
                                                                    mRotationSpeed * Time.deltaTime);
        }
    }
}
