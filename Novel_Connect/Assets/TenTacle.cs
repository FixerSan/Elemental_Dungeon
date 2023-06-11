using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenTacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRenderer;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    void Awake()
    {
        lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
    }

    private void Update()
    {
        segmentPoses[0] = targetDir.position;

        for (int i = 0; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed);
        }
        lineRenderer.SetPositions(segmentPoses);

    }
}
