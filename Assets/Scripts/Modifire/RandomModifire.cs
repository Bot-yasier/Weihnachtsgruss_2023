using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RandomModifire : MonoBehaviour
{
    public List<Sprite> images;

    void Start()
    {
    }

    public void AssignImages()
    {
        
        if (images.Count < 3)
        {
            Debug.LogError("Es müssen mindestens drei Bilder im Inspector festgelegt werden.");
            return;
        }


        List<Sprite> randomizedImages = new List<Sprite>(images);
        randomizedImages.Shuffle();


        for (int i = 0; i < 3; i++)
        {
            GameObject gameobject = GameObject.Find("Modifire" + (i + 1));
            if (gameobject != null)
            {
                Image imageComponent = gameobject.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = randomizedImages[i];
                }
            }
        }
    }
}