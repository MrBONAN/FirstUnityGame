using System;
using UnityEngine;

public class SplitScrin : MonoBehaviour
{
    public bool splitScreen = true;

    public Camera camera1;
    public Camera camera2;

    public Transform player1;
    public Transform player2;

    public float cameraSpeed = 15f;
    public float convergenceSpeed = 5f;

    public float halfWidth, halfHeight; // половина ширины и высота камеры в юнитах
    public float orthographicSize = 5f;
    private float zPos = -10f;

    // максимальная дистанция между игроками в неразделённом экране
    public float xMaxPlayersDistance, yMaxPlayersDistance;

    // размеры "внутреннего окна" у камеры для игрока
    public float dWidth, dHeight;
    public float xOffset = 0f, yOffset = 0f;


    public void Start()
    {
        if (splitScreen)
            InitializeSplitModeSettings();
        else
            InitializeMonoCameraSettings();
    }

    private void InitializeSplitModeSettings()
    {
        camera1.rect = new Rect(0, 0, 0.5f, 1);
        camera2.rect = new Rect(0.5f, 0, 0.5f, 1);

        halfHeight = orthographicSize / 2;
        halfWidth = orthographicSize * camera1.pixelWidth / camera1.pixelHeight;
        xMaxPlayersDistance = halfWidth * 2.5f;
        yMaxPlayersDistance = halfHeight * 2;
        dWidth = halfWidth / 3;
        dHeight = halfHeight - 1;


        camera1.orthographicSize = orthographicSize;
        camera2.orthographicSize = orthographicSize;

        var playerCenter = (player1.position + player2.position) / 2;
        camera1.transform.position = playerCenter + new Vector3(-halfWidth, 0, zPos);
        camera2.transform.position = playerCenter + new Vector3(halfWidth, 0, zPos);
    }

    private void InitializeMonoCameraSettings()
    {
        camera1.rect = new Rect(0, 0, 1, 1);

        halfHeight = orthographicSize;
        halfWidth = orthographicSize * camera1.pixelWidth / camera1.pixelHeight;
        dWidth = halfWidth / 2;
        dHeight = halfHeight / 2;
        yOffset = -1;

        camera1.orthographicSize = orthographicSize;
        camera1.transform.position = player1.position + new Vector3(0, 0, zPos);

        camera2.enabled = false;
    }


    public void FixedUpdate()
    {
        if (!splitScreen)
        {
            camera1.transform.position += GetCameraMoveDirection(camera1, player1.position) * cameraSpeed;
            return;
        }

        var playersDistance = player1.position - player2.position;
        if (Math.Abs(playersDistance.x) < xMaxPlayersDistance &&
            Math.Abs(playersDistance.y) < yMaxPlayersDistance)
            MoveConnectedCameras();
        else
            MoveSplitedCameras();
    }

    private void MoveConnectedCameras()
    {
        var playersCenter = (GetShiftedPlayerPosition(player1.position) +
                             GetShiftedPlayerPosition(player2.position)) / 2;

        var targetPos1 = new Vector3(playersCenter.x - halfWidth, playersCenter.y, zPos);
        var targetPos2 = new Vector3(playersCenter.x + halfWidth, playersCenter.y, zPos);

        camera1.transform.position +=
            (targetPos1 - camera1.transform.position) * (Time.fixedDeltaTime * convergenceSpeed);
        camera2.transform.position +=
            (targetPos2 - camera2.transform.position) * (Time.fixedDeltaTime * convergenceSpeed);
    }

    private void MoveSplitedCameras()
    {
        var target1 = GetShiftedPlayerPosition(player1.position);
        var target2 = GetShiftedPlayerPosition(player2.position);
        if ((camera1.transform.position - target1).sqrMagnitude > (camera1.transform.position - target2).sqrMagnitude)
            (target1, target2) = (target2, target1);

        camera1.transform.position += GetCameraMoveDirection(camera1, target1) * cameraSpeed;
        camera2.transform.position += GetCameraMoveDirection(camera2, target2) * cameraSpeed;
    }

    private Vector3 GetCameraMoveDirection(Camera camera, Vector3 target)
    {
        var dxy = target + new Vector3(xOffset, yOffset) - camera.transform.position;
        var dx = Math.Max(Math.Abs(dxy.x) - dWidth, 0f) * Math.Sign(dxy.x) / 4 * Time.fixedDeltaTime;
        var dy = Math.Max(Math.Abs(dxy.y) - dHeight, 0f) * Math.Sign(dxy.y) / 4 * Time.fixedDeltaTime;
        return new Vector3(dx, dy);
    }

    private Vector3 GetShiftedPlayerPosition(Vector3 position) => new Vector3(xOffset, yOffset) + position;
}