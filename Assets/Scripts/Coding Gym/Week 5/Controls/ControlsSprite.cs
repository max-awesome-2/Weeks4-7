using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSprite : MonoBehaviour
{
    private SpriteRenderer ren;

    private AudioSource source;
    public AudioClip clip;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        ren.color = Random.ColorHSV();

        source.PlayOneShot(clip);
    }

    public void OnSliderValueChanged()
    {
        transform.rotation = Quaternion.Euler(0, 0, slider.value);


        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);
        }

    }
}
