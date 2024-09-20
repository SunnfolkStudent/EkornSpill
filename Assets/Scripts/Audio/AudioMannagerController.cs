using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMannagerController : MonoBehaviour
{
    [Header("Music Source")] [SerializeField]
    AudioSource musicSource;

    [Header("Audio Clips")]
    
    

    //Time of a tab
    public float musicTimer = 1.90476f;

    //Waiting time
    public float relativTimer;

    //Active Timer
    public float timer = 0.0f;

    public static AudioMannagerController instance;

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

//Check if its main stage to start from beggining
    private void Start()
    {
        
    }

    //constant check and changing of music
    private void Update()
    {
        //timmer
        timer += Time.deltaTime;
    }
}


