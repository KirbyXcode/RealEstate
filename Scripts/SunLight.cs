using UnityEngine;
using System.Collections;

public class SunLight : MonoBehaviour 
{
    private float degree;
    private Light mLight;
    private float nightIntensity = 0;
    private float dayIntensity = 1;
    public float smooth = 1.5f;
    public bool turnDay = false;
    public bool turnNight = false;

    void Start()
    {
        mLight = GetComponent<Light>();
    }

    void IntensityLerp(bool isSwitchToDay)
    {
        if (isSwitchToDay)
            mLight.intensity = Mathf.Lerp(mLight.intensity, dayIntensity, Time.deltaTime * smooth);
        else
            mLight.intensity = Mathf.Lerp(mLight.intensity, nightIntensity, Time.deltaTime * smooth);
    }

    public void Night()
    {
        StartCoroutine(NightSwitch());
    }

	IEnumerator NightSwitch()
    {
        float day = 24.0f;
        float now = 0.0f;
        float dayDegree = 60.0f;
        while (degree < 180) 
        {
            now = (now + Time.deltaTime) % day;
            degree = dayDegree + now / day * 360.0f;
            transform.rotation = Quaternion.Euler(degree, 0, 0);
            yield return new WaitForFixedUpdate();
        }
    }

    public void Day()
    {
        StartCoroutine(DaySwitch());
    }

    IEnumerator DaySwitch()
    {
        float day = 24.0f;
        float now = 0.0f;
        float nightDegree = 180.0f;
        while (degree > 60)
        {
            now = (now - Time.deltaTime) % day;
            degree = nightDegree + now / day * 360.0f;
            transform.rotation = Quaternion.Euler(degree, 0, 0);
            yield return new WaitForFixedUpdate();
        }
    }

    void Update()
    {
        if (turnDay)
        {
            IntensityLerp(true);
            if (Mathf.Abs(mLight.intensity - dayIntensity) < 0.01f)
            {
                mLight.intensity = 1;
                turnDay = false;
            }
        }
        if(turnNight)
        {
            IntensityLerp(false);
            if (Mathf.Abs(mLight.intensity - nightIntensity) < 0.01f)
            {
                mLight.intensity = 0;
                turnNight = false;
            }
        }
    }
}
