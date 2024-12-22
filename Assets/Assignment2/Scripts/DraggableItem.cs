using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public string category;
    public string[] animalCategories;
    public Bucket blueBucket;
    public Bucket redBucket;
    public string animalDescription;
    public GameObject detailPanel;
    public TMP_Text animalText;

    private void Start()
    {
        foreach (var c in animalCategories)
        {
            if (c == blueBucket.category)
            {
                category = c.ToString();
            }
            else if (c == redBucket.category)
            {
                category = c.ToString();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsFirstSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Use eventData.position for both mouse and touch input
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
    }

    public void OpenDetailPanel()
    {
        animalText.text = animalDescription;
        detailPanel.SetActive(true);
    }
    
}
