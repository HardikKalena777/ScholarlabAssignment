using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> animals = new List<GameObject>();
    public GameObject gameOverPanel;
    public TMP_Text scoreCount;
    public int currentScore;
    public TMP_Text finalScore;
    public GameObject detailPanel;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if(animals.Count == 0)
        {
            finalScore.text = currentScore.ToString();
            gameOverPanel.SetActive(true);
        }
    }

    public void CloseDetailPanel()
    {
        detailPanel.SetActive(false);
    }
}
