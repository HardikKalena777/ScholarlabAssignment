using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class AnimalCards : MonoBehaviour
{
    public Sprite[] animalArray;
    public GameObject[] cardArray;
    public List<Sprite> animalList;

   
    private void Start()
    {
        animalList = new List<Sprite>(animalArray);

        foreach (var c in cardArray)
        {
            Image cardImage = c.GetComponent<Image>();

            Sprite randomAnimalSprite = animalArray[animalArray.Length - 1];

            cardImage.sprite = randomAnimalSprite;

            animalList.Remove(randomAnimalSprite);
            
        }
    }
}
