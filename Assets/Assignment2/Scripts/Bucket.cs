using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bucket : MonoBehaviour, IDropHandler
{
    public string category;
    
    public string[] bucketCategories;
    public TMP_Text categoryText;
    
    private void Start()
    {
        category = bucketCategories[Random.Range(0, bucketCategories.Length)];
        categoryText.text = category;
        GameManager.Instance.scoreCount.text = GameManager.Instance.currentScore.ToString();
        
    }


    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if(dropped.GetComponent<DraggableItem>().category == category)
        {
            Debug.Log("ScoreIncreased");
            GameManager.Instance.currentScore++;
            GameManager.Instance.scoreCount.text = GameManager.Instance.currentScore.ToString();
            Destroy(dropped);
        }
        else
        {
            Debug.Log("ScoreDecreased");
            GameManager.Instance.scoreCount.text = "Error";
            Destroy(dropped);
        }
        if(GameManager.Instance.animals.Contains(dropped))
        {
            GameManager.Instance.animals.Remove(dropped);
        }

    }

}
