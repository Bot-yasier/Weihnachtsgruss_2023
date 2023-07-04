using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentGenerator : MonoBehaviour
{
    public GameObject boxesParent;  // Reference to the parent object of the boxes
    public GameObject decosParent;  // Reference to the parent object of the decos

    private List<GameObject> boxes = new List<GameObject>();
    private List<GameObject> decos = new List<GameObject>();

    private void Start()
    {
        // Populate the lists with the box and deco objects
        foreach (Transform child in boxesParent.transform)
        {
            boxes.Add(child.gameObject);
        }

        foreach (Transform child in decosParent.transform)
        {
            decos.Add(child.gameObject);
        }

        GenerateRandomPresent();
    }

    private void GenerateRandomPresent()
    {
        // Disable all box and deco objects
        DisableAllObjects(boxes);
        DisableAllObjects(decos);

        // Randomly select one box and one deco object
        int randomBoxIndex = Random.Range(0, boxes.Count);
        int randomDecoIndex = Random.Range(0, decos.Count);

        // Enable the selected box and deco objects
        boxes[randomBoxIndex].SetActive(true);
        decos[randomDecoIndex].SetActive(true);

        // Check if the selected box and deco have the same color
        string boxColor = GetColorFromObject(boxes[randomBoxIndex]);
        string decoColor = GetColorFromObject(decos[randomDecoIndex]);

        // If the colors match, regenerate the present
        if (boxColor == decoColor)
        {
            GenerateRandomPresent();
        }
    }

    private void DisableAllObjects(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    private string GetColorFromObject(GameObject obj)
    {
        // Extract the color from the object's name
        string[] nameParts = obj.name.Split('-');
        return nameParts[1];
    }
}
