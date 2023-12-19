using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int id;
    [SerializeField]
    TextAsset[] levels;

    string[] enemynames;
    public Enemy[] enemies;
    public float SpawnDistance = 15;
    List<Enemy> currentEnemies;

    void Start()
    {
        enemynames = new string[enemies.Length];
        for (int i = 0; i != enemies.Length; ++i)
        {
            enemynames[i] = enemies[i].enemyName;
        }
        currentEnemies = new List<Enemy>();
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public void SetupAssets()
    {

    }

    public class Data
    {
        public string intro { get; set; }
        public List<Round> rounds { get; set; }
    }

    public class Round
    {
        public string type { get; set; }
        public int nb_pool { get; set; }
        public List<string> enemies { get; set; }
        public List<int> number { get; set; }
        public List<double> frequency { get; set; }
        public List<int> factor { get; set; }
        public string condition { get; set; }
        public int time { get; set; }
        public int? nb { get; set; }
    }
    int phase;
    Data data;
    float time;
    List<float> cooldown;
    int nb;
    int enemykilled;

    bool active;
    public void StartLevel()
    {
        SetupAssets();
        active = true;
        data = JsonConvert.DeserializeObject<Data>(levels[Id].text);
        phase = 0;
        ResetVariables();
    }

    void KillAll() // kill all enemies
    {
        foreach (Enemy enemy in currentEnemies)
        {
            Destroy(enemy);
        }
        currentEnemies = new List<Enemy>();
    }

    void ResetVariables()
    {
        time = 0;
        nb = 0;
        if (data.rounds.Count > phase)
        {
            cooldown = new List<float>();
            for (int enemyId = 0; enemyId != data.rounds[phase].enemies.Count; ++enemyId)
            {
                cooldown.Add(0);
            }
        }
        enemykilled = 0;
    }

    void SpawnEnemies(string type, int number, int factor)
    {
        int index = 0;
        while (index < enemynames.Length && enemynames[index] != type)
            index += 1;
        if (index == enemynames.Length)
        {
            Debug.Log("Nom d'ennemi non trouv�: " + type);
        }
        float theta = Random.Range(0, Mathf.PI * 2);
        for (int i = 0; i != number; ++i)
        {
            Enemy enemy = Instantiate<Enemy>(enemies[index]);
            
            Vector2 pos = new Vector2(Mathf.Sin(theta)+Random.Range(-0.2f,0.2f),Mathf.Cos(theta)+Random.Range(-0.2f, 0.2f)) * SpawnDistance;
            enemy.Setup(this, factor, new Vector3(pos.x, 0, pos.y));
            currentEnemies.Add(enemy);
        }
    }

    public void AddEnemies(Enemy enemy)
    {
        currentEnemies.Add(enemy);
    }

    bool AreAllEnemiesDead()
    {
        return currentEnemies.Count == 0;
    }

    void LevelUpdate() // called approximately one time per fixed update
    {
        if (phase >= data.rounds.Count)
        {
            active = false;
            GetComponent<MainManager>().LevelEnded();
            return;
        }
        time += Time.fixedDeltaTime;
        for (int enemyId = 0; enemyId != data.rounds[phase].enemies.Count; ++enemyId)
        {
            cooldown[enemyId] += Time.fixedDeltaTime;
        }

        if (data.rounds[phase].condition == "time")
        {
            if (time > data.rounds[phase].time)
            {
                phase += 1;
                ResetVariables();
                KillAll();
                FixedUpdate();
                return;
            }
        }

        switch (data.rounds[phase].type)
        {
            case "pool":
                if (nb < data.rounds[phase].nb_pool)
                {
                    for (int enemyId = 0; enemyId != data.rounds[phase].enemies.Count; ++enemyId)
                    {
                        if (cooldown[enemyId] > data.rounds[phase].frequency[enemyId])
                        {
                            cooldown[enemyId] = 0;
                            SpawnEnemies(data.rounds[phase].enemies[enemyId], data.rounds[phase].number[enemyId], data.rounds[phase].factor[enemyId]);
                            nb += 1;
                        }
                    }
                }

                if (nb >= data.rounds[phase].nb_pool)
                {
                    if (AreAllEnemiesDead())
                    {
                        phase += 1;
                        ResetVariables();
                        KillAll();
                        FixedUpdate();
                        return;
                    }
                }
                break;
            case "wave":

                for (int enemyId = 0; enemyId != data.rounds[phase].enemies.Count; ++enemyId)
                {
                    if (cooldown[enemyId] > data.rounds[phase].frequency[enemyId])
                    {
                        cooldown[enemyId] = 0;
                        SpawnEnemies(data.rounds[phase].enemies[enemyId], data.rounds[phase].number[enemyId], data.rounds[phase].factor[enemyId]);
                    }
                }
                if (enemykilled > data.rounds[phase].nb)
                {
                    phase += 1;
                    ResetVariables();
                    KillAll();
                    FixedUpdate();
                    return;
                }
                break;
            case "boss":
                if (time == 0)
                {
                    for (int enemyId = 0; enemyId != data.rounds[phase].enemies.Count; ++enemyId)
                    {
                        SpawnEnemies(data.rounds[phase].enemies[enemyId], data.rounds[phase].number[enemyId], data.rounds[phase].factor[enemyId]);
                    }
                }
                if (enemykilled > data.rounds[phase].nb)
                {
                    phase += 1;
                    ResetVariables();
                    KillAll();
                    FixedUpdate();
                    return;
                }
                break;
        }
    }



    void FixedUpdate()
    {
        List<Enemy> deads = new();
        foreach (Enemy enemy in currentEnemies) // remove dead enemies
        {
            if (enemy.isDead)
            {
                deads.Add(enemy);
            }
        }

        foreach(Enemy enemy in deads)
        {
            currentEnemies.Remove(enemy);
            Destroy(enemy);
        }


        if (active)
            LevelUpdate();
    }
}