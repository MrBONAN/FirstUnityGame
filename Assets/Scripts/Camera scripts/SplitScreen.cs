using System;
using UnityEngine;

public class SplitScrin : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    public Transform player1;
    public Transform player2;

    public float cameraSpeed = 10f;
    public float convergenceSpeed = 3f;
    public float dWidth, dHeight;
    public float size = 5f;
    private float zPos = -10f;

    public float xMaxPlayersDistance = 4f;
    public float yMaxPlayersDistance = 3f;


    public void Start()
    {
        camera1.rect = new Rect(0, 0, 0.5f, 1);
        camera2.rect = new Rect(0.5f, 0, 0.5f, 1);
        camera1.transform.position = new Vector3(-size / 2, 0, zPos);
        camera2.transform.position = new Vector3(size / 2, 0, zPos);
        dWidth = size / 2;
        dHeight = size / 2;
    }


    public void FixedUpdate()
    {
        var playersDistance = player1.position - player2.position;
        if (Math.Abs(playersDistance.x) < xMaxPlayersDistance &&
            Math.Abs(playersDistance.y) < yMaxPlayersDistance)
            MoveConnectedCameras();
        else
            MoveSplitedCameras();
    }

    private void MoveConnectedCameras()
    {
        var cp1 = camera1.transform.position;
        var cp2 = camera2.transform.position;
        var playersCenter = (player1.position + player2.position) / 2;
        var camerasCenter = (cp1 + cp2 - new Vector3(0, 0, 2 * zPos)) / 2;

        var targetPos1 = new Vector3(playersCenter.x - dWidth, playersCenter.y);
        var targetPos2 = new Vector3(playersCenter.x + dWidth, playersCenter.y);
        
        if ((playersCenter - camerasCenter).magnitude < size)
        {
            camera1.transform.position = targetPos1;
            camera2.transform.position = targetPos2;
        }
        else
        {
            camera1.transform.position += GetCameraMoveDirection(camera1, targetPos1) * convergenceSpeed;
            camera2.transform.position += GetCameraMoveDirection(camera2, targetPos2) * convergenceSpeed;
        }
    }

    private void MoveSplitedCameras()
    {
        camera1.transform.position += GetCameraMoveDirection(camera1, player1.position) * cameraSpeed;
        camera2.transform.position += GetCameraMoveDirection(camera2, player2.position) * cameraSpeed;
    }

    private Vector3 GetCameraMoveDirection(Camera camera, Vector3 target)
    {
        var dxy = target - camera.transform.position;
        var dx = Math.Max(Math.Abs(dxy.x) - dWidth, 0f) * Math.Sign(dxy.x) / 4 * Time.fixedDeltaTime;
        var dy = Math.Max(Math.Abs(dxy.y) - dHeight, 0f) * Math.Sign(dxy.y) / 4 * Time.fixedDeltaTime;
        return new Vector3(dx, dy);
    }
}