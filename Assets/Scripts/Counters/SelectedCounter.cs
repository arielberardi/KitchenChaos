using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjects;
    
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }
    
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)   
    {
        if (e.selectedCounter == _baseCounter)
        {
            foreach (GameObject visualGameObject in _visualGameObjects)
            {
                visualGameObject.SetActive(true);
            }
        }
        else 
        {
            foreach (GameObject visualGameObject in _visualGameObjects)
            {
                visualGameObject.SetActive(false);
            }
        }
    }
}
