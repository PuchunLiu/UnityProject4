using Unity.Cinemachine;
using UnityEngine;

public class CinemaShake : MonoBehaviour
{
    public static CinemaShake instance;
    public CinemachineBasicMultiChannelPerlin cp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            cp.AmplitudeGain = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShakeCamera()
    {
        CancelInvoke("ShakeEnd");
        cp.AmplitudeGain = 3.0f;
        Invoke("ShakeEnd", 0.2f);
    }

    public void ShakeEnd()
    {
        cp.AmplitudeGain = 0f;
    }
}
