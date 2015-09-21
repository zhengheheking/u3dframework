using UnityEngine;
using System.Collections;

public class DestoryTimed : MonoBehaviour
{
    private float m_CreatTime;
    public float m_LiveTime;
    // Use this for initialization
    void Start()
    {
        m_CreatTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > m_CreatTime + m_LiveTime)
        {
            Destroy(gameObject);
        }
    }
}
