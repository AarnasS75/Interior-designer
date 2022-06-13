using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HSVPickerExamples;

public class EditObject : MonoBehaviour
{
    BuildingManager buildingManager;
    public GameObject selectedObject;

    public bool EditMode;

    public bool canPlace;
    private void Awake()
    {
        buildingManager = GetComponent<BuildingManager>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("RoomObject") && !EditMode && !buildingManager.pendingObject)
                {
                    Select(hit.collider.gameObject);
                }
            }
        }
    }
    public void Select(GameObject obj)
    {
        if (obj == selectedObject) return;
        if (selectedObject != null)
        {
            Deselect();
        }
        obj.GetComponent<Outline>().enabled = false;
        selectedObject = obj;
        buildingManager.ofsetY = obj.GetComponent<BoxCollider>().size.y / 2;
        obj.GetComponent<RoomObject>().EnableEditMenu();
        EditMode = true;
    }
    public void Deselect()
    {
        EditMode = false;
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject.GetComponent<RoomObject>().editMenu.SetActive(false);
        selectedObject = null;
    }
    public void Move()
    {
        selectedObject.GetComponent<RoomObject>().editMenu.SetActive(false);
        buildingManager.pendingObject = selectedObject;
    }
    public void Delete(GameObject objToDestory)
    {
        EditMode = false;
        objToDestory = selectedObject;
        Deselect();
        Destroy(objToDestory);
    }
    public void SaveColor(GameObject obj)
    {
        obj.GetComponent<RoomObject>().color = obj.GetComponent<Renderer>().material.color;
    }
    public void CancelColor(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = obj.GetComponent<RoomObject>().color;
    }
}
