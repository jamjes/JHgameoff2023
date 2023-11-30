using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera _cam;
    private float mainLens;
    private float zoomLens;
    public float zoomForce = .1f;
    private bool zoomIn = false, zoomOut = false;

    private void Start()
    {
        mainLens = _cam.m_Lens.OrthographicSize;
        zoomLens = mainLens - 6;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && (!zoomIn && !zoomOut))
        {
            zoomIn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && (!zoomIn && !zoomOut))
        {
            zoomOut = true;
        }

        if (zoomIn && _cam.m_Lens.OrthographicSize > zoomLens)
        {
            _cam.m_Lens.OrthographicSize -= zoomForce;
        }
        else if (zoomIn && _cam.m_Lens.OrthographicSize <= zoomLens)
        {
            zoomIn = false;
        }

        if (zoomOut && _cam.m_Lens.OrthographicSize < mainLens)
        {
            _cam.m_Lens.OrthographicSize += zoomForce;
        }
        else if (zoomOut && _cam.m_Lens.OrthographicSize >= mainLens)
        {
            zoomOut = false;
        }
    }


    private void OnEnable()
    {
        PlayerCollisionHandler.OnQTEEnter += ZoomIn;
        PlayerCollisionHandler.OnShrinkExit += ZoomOut;
        PlayerQTEController.OnQTELoss += ZoomOut;
        PlayerShrinkController.OnShrinkEnd += ZoomOut;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnQTEEnter -= ZoomIn;
        PlayerCollisionHandler.OnShrinkExit -= ZoomOut;
        PlayerQTEController.OnQTELoss -= ZoomOut;
        PlayerShrinkController.OnShrinkEnd -= ZoomOut;
    }

    public void ZoomIn()
    {
        //_cam.m_Lens.OrthographicSize = zoomLens;
        zoomIn = true;
    }

    public void ZoomOut()
    {
        //_cam.m_Lens.OrthographicSize = mainLens;
        zoomOut = true;
    }

    public void ResetCam()
    {
        //_cam.m_Lens.OrthographicSize = mainLens;
        zoomOut = true; 
    }
}
