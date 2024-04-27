using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float dx = 3f, dy = 2f;
    public float xOffset = 0f, yOffset = 0f;
    public float cameraSpeed = 50f;

    void FixedUpdate()
    {
        var dxy = player.position + new Vector3(xOffset, yOffset, 0) - transform.position;
        var dx = Math.Max(Math.Abs(dxy.x) - this.dx, 0f) * Math.Sign(dxy.x) / 4 * Time.fixedDeltaTime;
        var dy = Math.Max(Math.Abs(dxy.y) - this.dy, 0f) * Math.Sign(dxy.y) / 4 * Time.fixedDeltaTime;
        transform.position += new Vector3(dx * cameraSpeed, dy * cameraSpeed, 0);
    }
}