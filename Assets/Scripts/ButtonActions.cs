using UnityEngine;
using HSVPickerExamples;

public class ButtonActions : MonoBehaviour
{
    ColorPickerTester colorPickerTester;
    
    EditObject editObject;

    private void Start()
    {
        colorPickerTester = GameObject.FindGameObjectWithTag("ObjectController").GetComponent<ColorPickerTester>();
        editObject = GameObject.FindGameObjectWithTag("ObjectController").GetComponent<EditObject>();
    }
    public void ChangeColor(GameObject obj)
    {
        colorPickerTester.renderer = obj.GetComponent<Renderer>();
        colorPickerTester.picker.CurrentColor = obj.GetComponent<Renderer>().material.color;
        GameManager.Instance.colorPicker.SetActive(true);
    }
    public void AcceptColor(GameObject obj)
    {
        editObject.SaveColor(obj);
        colorPickerTester.renderer = null;
        colorPickerTester.picker.CurrentColor = obj.GetComponent<Renderer>().material.color;
        GameManager.Instance.colorPicker.SetActive(false);
    }
    public void CancelColor(GameObject obj)
    {
        editObject.CancelColor(obj);
        colorPickerTester.picker.CurrentColor = obj.GetComponent<Renderer>().material.color;
        colorPickerTester.renderer = null;
        GameManager.Instance.colorPicker.SetActive(false);
    }
    public void DeleteObject(GameObject obj)
    {
        editObject.Delete(obj);
    }
    public void MoveObject()
    {
        editObject.Move();
    }
    public void CancelSelection()
    {
        editObject.Deselect();
    }
}
