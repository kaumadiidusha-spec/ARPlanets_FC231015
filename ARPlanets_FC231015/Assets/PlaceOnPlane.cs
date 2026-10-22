
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;

    private GameObject placedObject;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (!TryGetTap(out Vector2 tapPosition))
            return;

        if (raycastManager.Raycast(
            tapPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (placedObject == null)
            {
                placedObject = Instantiate(
                    objectToPlace, hitPose.position, hitPose.rotation);
            }
            else
            {
                placedObject.transform.position = hitPose.position;
            }
        }
    }

    bool TryGetTap(out Vector2 position)
    {
        position = Vector2.zero;

        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            position = Input.GetTouch(0).position;
            return true;
        }

        if (Input.touchCount == 0 &&
            Input.GetMouseButtonDown(0))
        {
            position = Input.mousePosition;
            return true;
        }

        return false;
    }
}

