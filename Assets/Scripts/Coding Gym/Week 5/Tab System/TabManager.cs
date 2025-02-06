using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    private AudioSource source;

    public GameObject panel1, panel2, panel3;

    public AudioClip clip;

    int currentTab = -1;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tab1Pressed()
    {
        if (currentTab == 1) return;
        currentTab = 1;

        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);

        source.PlayOneShot(clip);
    }

    public void Tab2Pressed()
    {
        if (currentTab == 2) return;
        currentTab = 2;

        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);

        source.PlayOneShot(clip);

    }

    public void Tab3Pressed()
    {
        if (currentTab == 3) return;
        currentTab = 3;

        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);

        source.PlayOneShot(clip);

    }
}
