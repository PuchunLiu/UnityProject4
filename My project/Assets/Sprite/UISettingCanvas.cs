using UnityEngine;
using UnityEngine.UI;

public class UISettingCanvas : MonoBehaviour
{
    public Slider volice;
    public Text voliceText;

    private void Update()
    {
        //OnSliderChange();
    }

    public void OnSliderChange()
    {
        SoundManager.instance.SetVolice(volice.value);
        voliceText.text = (volice.value * 100).ToString("F0") + "%";
    }
}
