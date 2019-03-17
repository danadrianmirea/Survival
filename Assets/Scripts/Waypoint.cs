﻿// Copyright (c) 2019 JensenJ
// NAME: FacePlayer
// PURPOSE: Make game objects face the camera/player

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour
{
    //Marker Widget settings
    private TextMeshProUGUI widgetDistanceMesh;
    private TextMeshProUGUI widgetNameMesh;
    private GameObject widgetBackgroundPanel;
    private Image widgetWaypointColor;

    //Marker manager settings
    private GameObject managerContentPanel;
    private TextMeshProUGUI managerLocationMesh;
    private TextMeshProUGUI managerNameMesh;
    private Image managerWaypointColor;

    private Transform objectToFace = null;
    private Transform playerPos;
    private PlayerController pc = null;
    private float distanceFromPlayer = 10.0f;

    [SerializeField] private GameObject markerManagerPrefab = null;
    [SerializeField] public int MarkerID = 0;

    //Setup
    void Awake()
    {
        objectToFace = transform.root.GetChild(3).transform;
        playerPos = objectToFace;
        pc = objectToFace.GetComponent<PlayerController>();

        widgetBackgroundPanel = transform.GetChild(0).GetChild(0).gameObject;
        widgetDistanceMesh = widgetBackgroundPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        widgetNameMesh = widgetBackgroundPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        widgetWaypointColor = widgetBackgroundPanel.transform.GetChild(0).GetComponent<Image>();

        managerContentPanel = transform.root.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;

    }

    //Function that is called when other functions, e.g. player controller changes waypoint settings.
    public void SetWaypointSettings(int index, bool m_bIsEnabled, string m_name, Vector3 m_location, Color m_color)
    {
        MarkerID = index;
        widgetBackgroundPanel.SetActive(m_bIsEnabled);
        widgetWaypointColor.color = m_color;
        transform.position = m_location;
        widgetNameMesh.text = m_name;

        GameObject markerManager = Instantiate(markerManagerPrefab, transform.position, Quaternion.identity, managerContentPanel.transform);
        managerWaypointColor = markerManager.transform.GetChild(0).GetComponent<Image>();
        managerWaypointColor.color = m_color;

        managerNameMesh = markerManager.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        managerNameMesh.text = m_name + "(enabled=" + m_bIsEnabled.ToString() + ")";

        managerLocationMesh = markerManager.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        managerLocationMesh.text = Mathf.Round(m_location.x).ToString() + ", " + Mathf.Round(m_location.y).ToString() + ", " + Mathf.Round(m_location.z).ToString();

    }

    //Removes waypoint by destroying it.
    public void RemoveWaypoint()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.bHasDeployedDrone)
        {
            objectToFace = playerPos.GetChild(3);
        }
        else
        {
            objectToFace = playerPos;
        }
        
        distanceFromPlayer = Mathf.Round(Vector3.Distance(objectToFace.position, transform.position));
        widgetDistanceMesh.text = distanceFromPlayer.ToString() + "m";

        if(objectToFace != null)
        {
            transform.LookAt(objectToFace);
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        else
        {
            Debug.LogError("Object to face is null");
        }
    }
}