using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;
    //FEEDBACK: ukoliko je potreban tip varijable koji omogucuje vise stanja koristiti enumerator
    [SerializeField] //Trippleshot = 0, Speed = 1, Shield = 2
    private int _powerUpID; //ID broj (0,1,2...) koji smo dodjelili objektu direktno u Unityu, na trippleshot smo stavili 0, a na speed 1
    [SerializeField]
    

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
            
           
            if(player != null)
            {
                switch (_powerUpID)//switch PETLJA prima vrijednost od POWERUPID od onog objekta s kojim smo kolajdali
                {
                    case 0: //u slucaju da je PowerUPID jednak 0
                        player.TrippleShotActive();// izvršava se aktivacija TrippleshotPowerUp
                        break;//ovo moramo imat, govori programu da kada je izvršena naredba iznad, da nas izbaci iz petlje, kako ju nebi slucajno poceo ponavljat
                    case 1:
                        player.StartPowerUpSpeed();
                            break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default: //ukoliko niti jedan od gore navedenih slucajeva nije istinit ili ispunjen, po defaultu izbaci nas iz switch petlje
                        break;
                }

            }
       
        }

   
    }

 

}
