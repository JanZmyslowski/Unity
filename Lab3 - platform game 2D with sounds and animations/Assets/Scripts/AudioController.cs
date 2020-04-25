using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private Slider audioSlider;

    private AudioSource music;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
            audioSlider.value = music.volume;
        }
        catch (System.NullReferenceException)
        {
                        
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //music = GameObject.FindGameObjectWithTag("Music");
        try
        {
            if (music != null)
            {
                music.volume = audioSlider.value;
            }
        }
        catch (System.NullReferenceException)
        {

        }
        
    }

    private void Awake()
    {
        if (music != null)
        {
            music.volume = audioSlider.value;
        }        
    }
}
