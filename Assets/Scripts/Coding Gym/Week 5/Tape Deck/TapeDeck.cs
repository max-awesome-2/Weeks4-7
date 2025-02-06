using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapeDeck : MonoBehaviour
{

    AudioSource source;
    public TMP_Dropdown dropdown;
    public Slider playTimer;
    float t;

    public List<AudioClip> clips;


    private int lastPlaying = -1;

    public List<Transform> tapeWheels;
    float currentWheelZRotation = 0;

    public float wheelRotationSpeed = 250;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying)
        {
            currentWheelZRotation += wheelRotationSpeed * Time.deltaTime;

            foreach (Transform t in tapeWheels)
            {
                t.transform.rotation = Quaternion.Euler(0, 0, currentWheelZRotation);
            }

            t += Time.deltaTime;
            playTimer.value = t;
        }
    }

    public void OnPlayButton()
    {
        print("source time: " + source.time);
        print("source playing: " + source.isPlaying);

        int dropdownOption = dropdown.value;

        if (!source.isPlaying)
        {
            if (source.time > 0.01f && source.time < playTimer.maxValue - 0.01f && dropdownOption == lastPlaying)
            {
                // source must be paused - now unpause
                source.UnPause();
            } // on song changed
            else 
            {
                lastPlaying = dropdownOption;

                source.Stop();
                source.clip = clips[dropdown.value];
                source.Play();
                playTimer.maxValue = source.clip.length;

                t = 0;
            }
        }
       


    }

    public void OnStopButton()
    {
        if (source.isPlaying)
        {
            source.Pause();
        }
    }

}
