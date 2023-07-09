using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private GameObject _progressGameObject;
    
    private IHasProgress _progessCounter;
    
    private void Start()
    {
        _progessCounter = _progressGameObject.GetComponent<IHasProgress>();
        
        if (_progessCounter == null)
        {
            Debug.LogError("Game object" + _progressGameObject + "has not IHasProgress");
        }
        
        _progessCounter.OnProgressChanged += ProgressCounter_OnProgressChanged;
        _barImage.fillAmount = 0f;
        
        gameObject.SetActive(false);
    }
    
    private void ProgressCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e.progressNormalized;
       
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            gameObject.SetActive(false);
        } 
        else 
        {
            gameObject.SetActive(true);
        }
    }
}
