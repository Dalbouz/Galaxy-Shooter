using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyParent;
    [SerializeField]
    private float _timeToSpawnEnemy = 5.0f;
    [SerializeField]
    private GameObject[] powerUpSpawner; //array (mozemo spremiti vise objekta u njega, u Unityu zadajemo velicinu arraya i dodajemo objekte), numeracija pocije od 0 i ide do N-1
    [SerializeField]
    





    private GameObject _clonePowerUp;
    private GameObject _cloneEnemy;
   // private GameObject _cloneTrippleShotPowerUp;
  //  private GameObject _cloneSpeedPowerUp;

  //  public GameObject[] SpeedUpList;

    

    

    private bool _stopSpawn = false; // varijabla koja odreduje kada trebamo prestat spawnat neprijatelje
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); // pokretanje Korutine
        StartCoroutine(SpawnPowerUpRoutine());        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator se koristi kada radimo nekakvim loop-ama, i on nam dopusta da koristimo funkciju Yield(yield funkcija nam omogucava da zamrznemo kod na neko vrijeme i onda ga ponovno poceno pokretat nakon isteka vremena)
    IEnumerator SpawnEnemyRoutine()
    {
        

            while (_stopSpawn == false) // ako je while(true) to je neogranicena petlja, ako imamo ovako uvjet, onda ga mozemo mijenjat i zaustaviti petlju, tj. spawnanje objekta
            {
                int RandomNumbersOfEnemys = Random.Range(0, 3);//vraca broj izmedu 0 i 2
            for (int i = 0; i <= RandomNumbersOfEnemys; i++) // ako je i=0 spawn 1 Enemy, i=1 spawn 2Enemy, i=2 Spawn 3 Enemy
            {
                
                Vector3 PosToSpawn = new Vector3(Random.Range(-9.6f, 9.6f), 8, 0); //Random pozicija spawnanja svakog Enemy Clona
                _cloneEnemy = Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity); // spawnanje objekta, i spremljeno u varijablu tipa "GAMEOBJECT"
                _cloneEnemy.transform.parent = _enemyParent.transform; //određujemo da ce novo stvoreni klon biti djete unutar _enemyParent(empty), parent je tip "transfrom" zato moramo kada ulazimo unutar parenta koristiti .transform
            }
                 
                yield return new WaitForSeconds(_timeToSpawnEnemy); // ovo ce cekati 5 sekundi, i onda ce pocet izvrsavat dalje petlju
            }

            Destroy(GameObject.FindWithTag("EnemyContainer")); // uništavanje cijelog parenta 
        
        }

    IEnumerator SpawnPowerUpRoutine()
    {

        while (_stopSpawn == false)
        {
            float TimeToSpawn = Random.Range(5, 20); //random vrijeme za spawnanje
            Vector3 PosToSpawn = new Vector3(Random.Range(-9.6f, 9.6f), 8, 0);
            int RandomNumberForList = Random.Range(0, 3);
            yield return new WaitForSeconds(TimeToSpawn);
            _clonePowerUp = Instantiate(powerUpSpawner[RandomNumberForList], PosToSpawn, Quaternion.identity); //prvo pricekamo radnom vrijeme i onda pocnemo sa instanciranjem
            //powerUpSpawner[RandomNumberForList] - buduci da smo prefabove spremili unutar Unitya (Trippleshot - 0, Speed -1,shield -2)
            //uzima random broj izmedu 0 i 3, i vraca vrijednost, tj vraca GameObject Prefab koji je postavljen na određenu vrijednost
        }
    }

   



        /*if (SpeedUpList == null)
            SpeedUpList = GameObject.FindGameObjectsWithTag("SpeedPowerUp");
        foreach (GameObject speed in SpeedUpList)
        {
            GameObject.FindWithTag("SpeedPowerUp").GetComponent<BoxCollider2D>().isTrigger = false;
        }*/



        // Metoda koja se poziva unutar player skripte, mjenja nasu vrijednost od _stopSpawn i na taj nacin u While petlji uvjek ce biti jednak False i prestat ce se izvrsavat, to se dogodi kada nam je life = 0
        public void OnPlayerDeath()
        {
            _stopSpawn = true;
        }

    }

