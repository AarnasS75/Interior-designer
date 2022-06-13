using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;

    public GameObject colrPicker;

    public GameObject pendingObject;

    [Header("Grid")]
    public float gridSize;
    public bool gridOn;

    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;

    RaycastHit hit;
    public float ofsetY;

    EditObject editObject;
    private void Awake()
    {
        editObject = GetComponent<EditObject>();
    }
    private void Start()
    {
        gridOn = false;
        colrPicker.SetActive(false);
    }
    private void Update()
    {
        if (pendingObject)
        {
            MoveObject();

            if (Input.GetMouseButtonDown(0) && editObject.canPlace)
            {
                PlaceObject();
            }
        }
    }
    void PlaceObject()
    {
        editObject.selectedObject = null;
        editObject.Select(pendingObject);
        pendingObject = null;
    }
    void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 500000.0f, 1 << 3))
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(hit.point.x),
                    RoundToNearestGrid(hit.point.y) + ofsetY,
                    RoundToNearestGrid(hit.point.z)
                    );
            }
            else
            {
                pendingObject.transform.position = new Vector3(hit.point.x, hit.point.y + ofsetY, hit.point.z);
            }
        }
    }
    // On UI Button Press
    public void SelectObject(int index)
    {
        if (!editObject.selectedObject)
        {
            editObject.EditMode = true;
            pendingObject = Instantiate(objects[index]);
            ofsetY = pendingObject.GetComponent<BoxCollider>().size.y / 2;
        }
    }

    #region Grid
    public void ToggleGrid()
    {
        if (gridOn)
        {
            targetButton.sprite = buttonSprites[1];
            gridOn = false;
        }
        else
        {
            targetButton.sprite = buttonSprites[0];
            gridOn = true;
        }
    }
    public float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
    #endregion
}
