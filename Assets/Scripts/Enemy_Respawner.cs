using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Enemy_Respawns : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;// we attach them from Unity directly
    [SerializeField] private float cooldown = 2f;
    [Space]
    [SerializeField] private float cooldownDecreaseRate = .05f;
    [SerializeField] private float cooldownCap = .7f;
    private float timer;
    private Transform player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>().transform;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = cooldown;// Each time cooldown will be tinier and so on
            CreateNewEnemy();
        } // Returns the maximun value between 2 values, this will take care of my cooldown until it reaches his limit (cooldownCap)
        cooldown = Mathf.Max(cooldownCap, cooldown - cooldownDecreaseRate);
    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        Vector3 spawnPoint = respawnPoints[respawnPointIndex].position;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        Enemy enemy = newEnemy.GetComponent<Enemy>();

        bool createdOnTheRight = newEnemy.transform.position.x > player.transform.position.x;

        if (createdOnTheRight)
            enemy.SetFacingDirection(-1); // mirar hacia el player, que está a la izquierda
        else
            enemy.SetFacingDirection(1);  // mirar hacia el player, que está a la derecha
    }
}
