using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float money = 0;
    public int month = 1;
    [SerializeField] private float monthDuration;
    private float _monthTimer = 0f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _monthTimer += Time.deltaTime;
        if (_monthTimer > monthDuration)
        {
            _monthTimer = 0;
            month = 1 + ((month) % 12);
        }
    }
}
