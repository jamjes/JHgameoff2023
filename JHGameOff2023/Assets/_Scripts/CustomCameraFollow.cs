using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera _cam;
    private float mainLens;
    private float zoomLens;
    public float zoomForce = .03f;
    private bool zoomIn = false, zoomOut = false;


    private void Start()
    {
        mainLens = _cam.m_Lens.OrthographicSize;
        zoomLens = mainLens - 2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !zoomIn)
        {
            zoomIn = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !zoomOut)
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
        PlayerQTEController.OnQTELoss += ZoomOut;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnQTEEnter -= ZoomIn;
        PlayerQTEController.OnQTELoss -= ZoomOut;
    }

    private void ZoomIn()
    {
        //_cam.m_Lens.OrthographicSize = zoomLens;
        zoomIn = true;
    }

    private void ZoomOut()
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
