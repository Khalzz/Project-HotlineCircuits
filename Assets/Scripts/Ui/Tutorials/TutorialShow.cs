using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    public class TutorialShow : MonoBehaviour
{
    public GameObject tutorialMessage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        tutorialMessage.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialMessage.SetActive(false);
    }
}
