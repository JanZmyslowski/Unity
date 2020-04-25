using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    //[SerializeField]
    //private AudioSource audioSource;

    //[SerializeField]
    //private Slider audioSlider;
    // Start is called before the first frame update

    //[RangeAttribute(0f, 1f)]
    ///[SerializeField]
    ///private float startVolume;

    private static DontDestroy instance;

    void Start()
    {
        // audioSlider.value = startVolume;
        //audioSlider.value = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.volume = audioSlider.value;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

}

