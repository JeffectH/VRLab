using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadInput;
    public Transform cameraTransform;
    private CapsuleCollider capsuleColider;

    private void Start()
    {
        capsuleColider = GetComponent<CapsuleCollider>(); 
    }
    private void FixedUpdate()
    {
        Vector3 movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(touchpadInput.axis.x, 0, touchpadInput.axis.y));
        transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * 2, Vector3.up);

        float distanceFromFloor = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
        capsuleColider.height = Mathf.Max(capsuleColider.radius, distanceFromFloor);

        capsuleColider.center = cameraTransform.localPosition - 0.5f * distanceFromFloor * Vector3.up;
    }
}
