using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateSlider(float value)
    {
        if(slider == null)
        {
            slider = GetComponent<Slider>();
        }
        if (value <= 0)
        {
            gameObject.SetActive(false);
            slider.value = 0;
        }
        else if(value >= 1)
        {
            gameObject.SetActive(false);
            slider.value = 1;
        }
        else
        {
            if(gameObject.activeSelf == false)
                gameObject.SetActive(true);
            slider.value = value;
        }
    }

}
