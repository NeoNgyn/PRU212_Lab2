using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    public Transform scoreListContainer;
    public Transform scoreListTemplate;
    public float templateHeight = 100f;

    private void Awake()
    {
        

        scoreListTemplate.gameObject.SetActive(false);

        for(int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(scoreListTemplate, scoreListContainer);
            RectTransform entryRecTransfrom = entryTransform.GetComponent<RectTransform>();
            entryRecTransfrom.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryRecTransfrom.gameObject.SetActive(true);       
            
        }
    }
}
