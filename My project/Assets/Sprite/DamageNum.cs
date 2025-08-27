using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    public Text damageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    public void SetDamageNum(int damage)
    {
        damageText.text = "-" + damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
