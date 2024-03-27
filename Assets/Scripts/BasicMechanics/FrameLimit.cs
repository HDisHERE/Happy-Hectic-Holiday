using UnityEngine;

public class FrameLimit : MonoBehaviour
{
    public enum limitType
    {
        noLimit=-1,
        limit30=30,
        limit60=60,
        lmit120=120
    }

    public limitType frameLimit;

    private void Awake()
    {
        Application.targetFrameRate = (int)frameLimit;
    }
}
