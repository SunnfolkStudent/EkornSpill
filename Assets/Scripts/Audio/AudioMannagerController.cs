using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMannagerController : MonoBehaviour
{
    [Header("Music Source")] [SerializeField]
    AudioSource musicSource;

    [Header("Music Clips")]
    public AudioClip intro;
    public AudioClip loop;

    private float introValue = 0;
    
    //Waiting time
    public float relativTimer;

    //Active Timer
    public float timer = 0.0f;

    public static AudioMannagerController instance;

    /*
    //Never Die
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }
*/

    //Check if its main stage to start from beggining
    private void Start()
    {
        relativTimer = 0.5f;
    }

    //constant check and changing of music
    private void Update()
    {
        if (introValue == 2) return;
        //timmer
        timer += Time.deltaTime;

        if (timer >= relativTimer && introValue == 0)
        {
            musicSource.clip = intro;
            musicSource.Play();
            timer = 0f;
            relativTimer = 28.8f;
            introValue = 1;
            Debug.Log("playing intro");
        }
        else if (timer >= relativTimer && introValue == 1)
        {
            musicSource.clip = loop;
            musicSource.Play();
            introValue = 2;
            Debug.Log("Playing loop");
        }
    }
}


