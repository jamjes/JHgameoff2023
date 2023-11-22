using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera _cam;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnQTEEnter += ZoomIn;
        //PlayerQTEController.OnQTEWin += ZoomOut;
        PlayerQTEController.OnQTELoss += ZoomOut;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnQTEEnter -= ZoomIn;
        //PlayerQTEController.OnQTEWin -= ZoomOut;
        PlayerQTEController.OnQTELoss -= ZoomOut;
    }

    private void ZoomIn()
    {
        _cam.m_Lens.OrthographicSize = 6;
    }

    private void ZoomOut()
    {
        _cam.m_Lens.OrthographicSize = 10;
    }

    public void ResetCam()
    {
        _cam.m_Lens.OrthographicSize = 10;
    }
}
