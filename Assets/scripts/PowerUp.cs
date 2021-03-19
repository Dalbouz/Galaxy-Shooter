using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField] //Trippleshot = 0, Speed = 1, Shield = 2
    private int _powerUpID; //ID broj (0,1,2...) koji smo dodjelili objektu direktno u Unityu, na trippleshot smo stavili 0, a na speed 1
    [SerializeField]
    private AudioClip _clipSound;

    private UIManager _uiManager;

    public GameObject[] SpeedUpSpawns;

    

    

    
    void Start()
    {

        transform.position = new Vector3(Random.Range(-9, 9), 7, transform.position.z);
      
            

    }

   
   /// <summary>
   /// Update metoda !!
   /// </summary>
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.5f)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            player player = other.transform.GetComponent<player>(); //dohvacanje skripte player koja se nalazi na PLAYERU (this object)
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            
            AudioSource.PlayClipAtPoint(_clipSound, transform.position,2.0f); // Funkcionira slicno kao instantiate naredba, nije mu potrebna AudioSource Kompomenta, na ovaj nacin instancira određeni zvuk (clip), na određenoj poziciji
           
            if(player != null)
            {
                switch (_powerUpID)//switch PETLJA prima vrijednost od POWERUPID od onog objekta s kojim smo kolajdali
                {
                    case 0: //u slucaju da je PowerUPID jednak 0
                        player.TrippleShotActive();// izvršava se aktivacija TrippleshotPowerUp
                        _uiManager.DisplayPowerUps(_powerUpID);
                        break;//ovo moramo imat, govori programu da kada je izvršena naredba iznad, da nas izbaci iz petlje, kako ju nebi slucajno poceo ponavljat
                    case 1:
                        player.StartPowerUpSpeed();
                        _uiManager.DisplayPowerUps(_powerUpID);
                        break;
                    case 2:
                        player.ShieldActive();
                        _uiManager.DisplayPowerUps(_powerUpID);
                        break;
                    case 3:
                        player.StartSpeedShotRoutine();
                        _uiManager.DisplayPowerUps(_powerUpID);
                        break;
                    case 4:
                        player.RepairEngine();
                        break;
                    case 5:
                        player.HealUp();
                        
                        break;
                    default: //ukoliko niti jedan od gore navedenih slucajeva nije istinit ili ispunjen, po defaultu izbaci nas iz switch petlje
                        break;
                }

            }
       
        }

   
    }

 

}
