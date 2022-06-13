using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class RoomObject : MonoBehaviour
{
    public GameObject editMenu;

    BuildingManager buildingManager;
    EditObject editObject;

    [HideInInspector] public Color color;
    Renderer rend;

    Outline outline;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        outline = GetComponent<Outline>();
        editObject = GameObject.FindGameObjectWithTag("ObjectController").GetComponent<EditObject>();
        buildingManager = GameObject.FindGameObjectWithTag("ObjectController").GetComponent<BuildingManager>();
    }
    private void Start()
    {
        outline.OutlineWidth = 4.5f;
        outline.OutlineColor = new Color(1f, 0f, 0.5f, 1f);
        outline.enabled = false;

        editMenu.SetActive(false);
        editObject.canPlace = true;
        color = rend.material.color;
    }
    public void EnableEditMenu()
    {
        editMenu.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (!buildingManager.pendingObject && !editObject.EditMode)
        {
            outline.enabled = true;
        }
    }
    private void OnMouseExit()
    {
        if (!buildingManager.pendingObject && !editObject.EditMode)
        {
            outline.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.transform.CompareTag("RoomObject") || other.transform.CompareTag("Wall")) && gameObject.Equals(buildingManager.pendingObject))
        {
            editObject.canPlace = false;
            rend.material.color = new Color(1f, 0f, 0f, 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.transform.CompareTag("RoomObject") || other.transform.CompareTag("Wall")) && gameObject.Equals(buildingManager.pendingObject))
        {
            editObject.canPlace = true;
            rend.material.color = color;
        }
    }
}
