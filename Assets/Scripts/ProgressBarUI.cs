using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private CuttingCounter _cuttingCounter;
    
    private void Start()
    {
        _cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        _barImage.fillAmount = 0f;
        
        gameObject.SetActive(false);
    }
    
    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
