using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float redColorDuration = 1;
    private SpriteRenderer sr;
    public float timer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        timer = timer - Time.deltaTime;
    }
    [ContextMenu("Update timer")]
    private void UpdateTimer()
    {
        timer = redColorDuration;
        if (timer < 0)
        {

        }
    }
    public void TakeDamage()
    {
        sr.color = Color.red;
        timer = redColorDuration;// From 5 to 0 seconds
        //Invoke(nameof(TurnWhite), redColorDuration);//red color will last 1 second and then it will turn white
    }

    public void TurnWhite()
    {
        sr.color = Color.white;
    }
}
