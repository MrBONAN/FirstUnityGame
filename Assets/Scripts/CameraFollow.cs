using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float dx = 3f, dy = 2f;

    void FixedUpdate()
    {
        var dxy = player.position - transform.position;
        var dx = Math.Max(Math.Abs(dxy.x) - this.dx, 0f) * Math.Sign(dxy.x) / 4;
        var dy = Math.Max(Math.Abs(dxy.y) - this.dy, 0f) * Math.Sign(dxy.y) / 4;
        transform.position = transform.position + new Vector3(dx, dy, 0);
    }
}