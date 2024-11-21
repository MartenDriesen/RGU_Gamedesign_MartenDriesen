using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructions : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public TextMeshProUGUI buttonText;

    private bool areObjectsActive = true;

    void Start()
    {
        object1.SetActive(true);
        object2.SetActive(true);
        buttonText.text = "Hide Instructions";
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        areObjectsActive = !areObjectsActive;
        
        object1.SetActive(areObjectsActive);
        object2.SetActive(areObjectsActive);
        
        buttonText.text = areObjectsActive ? "Hide Instructions" : "Show Instructions";
    }
}
