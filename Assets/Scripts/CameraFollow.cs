using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float dx = 3f, dy = 2f;

    void FixedUpdate()
    {
        var dxy = player.position - transform.position;
        var dx = Math.Max(Math.Abs(dxy.x) - this.dx, 0f) * Math.Sign(dxy.x);
        var dy = Math.Max(Math.Abs(dxy.y) - this.dy, 0f) * Math.Sign(dxy.y);
        transform.position = new Vector3(transform.position.x + dx, transform.position.y + dy, transform.position.z);
    }
}