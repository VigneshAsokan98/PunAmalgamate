using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviour
{
    public Transform Head;
    public Transform RightHand;
    public Transform LeftHand;

    private PhotonView photonView;
    
    Transform HeadOrigin;
    Transform LeftHandOrigin;
    Transform RightHandOrigin;


    public Animator leftHandAnimator;
    public Animator rightHandAnimator;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            XROrigin rig = FindObjectOfType<XROrigin>();
            HeadOrigin = rig.transform.Find("Camera Offset/Main Camera");
            LeftHandOrigin = rig.transform.Find("Camera Offset/LeftHand Controller");
            RightHandOrigin = rig.transform.Find("Camera Offset/RightHand Controller");
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Head.gameObject.SetActive(false);
            LeftHand.gameObject.SetActive(false);
            RightHand.gameObject.SetActive(false);

            MapPosition(Head, HeadOrigin);
            MapPosition(LeftHand, LeftHandOrigin);
            MapPosition(RightHand, RightHandOrigin);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
    void MapPosition(Transform target, Transform OriginTransform)
    {
        target.position = OriginTransform.position;
        target.rotation = OriginTransform.rotation;
    }
}
