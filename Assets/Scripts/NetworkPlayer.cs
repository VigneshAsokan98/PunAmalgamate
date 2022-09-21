using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public Transform Head;
    public Transform RightHand;
    public Transform LeftHand;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Head.gameObject.SetActive(false);
            RightHand.gameObject.SetActive(false);
            LeftHand.gameObject.SetActive(false);

            MapPosition(Head, XRNode.Head);
            MapPosition(RightHand, XRNode.RightHand);
            MapPosition(LeftHand, XRNode.LeftHand);
        }
    }
    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}
