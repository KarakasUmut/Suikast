using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    [Header("DÝGER AYARLAR")]
    NavMeshAgent navMesh;
    Animator animatorum;
    GameObject Hedef;

    [Header("GENEL AYARLAR")]
    float AtesEtmeMenzilDeger = 7;
    float SuphelenmeMenzilDeger = 10;
    Vector3 BaslangýcNoktasý;
    bool AtesEdilsinMi = false;
    bool Suphelenme = false;

    [Header("DEVRÝYE AYARLARI")]
    public GameObject[] DevriyeNoktalarý_1;
    public GameObject[] DevriyeNoktalarý_2;
    public GameObject[] DevriyeNoktalarý_3;
    public bool DevriyeVarmi;
    Coroutine DevriyeAt;
    
   

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animatorum = GetComponent<Animator>();
        BaslangýcNoktasý = transform.position;
        

    }

    IEnumerator DevriyeTeknikÝslem()
    {
        animatorum.SetBool("Yurume", true);
        int toplamnokta = DevriyeNoktalarý_1.Length-1;
        int baslangýcdeger = 0;
        
        while (true && !DevriyeVarmi) 
        {
            if (Vector3.Distance(transform.position, DevriyeNoktalarý_1[baslangýcdeger].transform.position) <= 1f) 
            {
                if (toplamnokta>baslangýcdeger)
                {
                    ++baslangýcdeger;
                    navMesh.SetDestination(DevriyeNoktalarý_1[baslangýcdeger].transform.position);
                }
                else
                {
                    navMesh.stoppingDistance = 1;
                    navMesh.SetDestination(BaslangýcNoktasý);
                    if (navMesh.remainingDistance <= 1)
                    {
                        animatorum.SetBool("Yurume", false);
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        DevriyeVarmi = false;
                        StopCoroutine(DevriyeAt);
                    }
                }
                
            }
            else
            {
                if (toplamnokta > baslangýcdeger)
                {
                    navMesh.SetDestination(DevriyeNoktalarý_1[baslangýcdeger].transform.position);
                }
            }
            
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (DevriyeVarmi)
        {
            DevriyeAt = StartCoroutine(DevriyeTeknikÝslem());
        }
        SuphelenmeMenzil();
        AtesEtmeMenzil();
        
    }


    void AtesEtmeMenzil()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AtesEtmeMenzilDeger);
       

        foreach (var objeler in hitColliders)
        {
            if (objeler.gameObject.CompareTag("Player"))
            {
                animatorum.SetBool("Yurume", false);
                navMesh.isStopped = true;
                animatorum.SetBool("AtesEt", true);
               
            }
            else
            {
                animatorum.SetBool("AtesEt", false);

            }
        }
    }
    void SuphelenmeMenzil()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SuphelenmeMenzilDeger);
        

        foreach (var objeler in hitColliders)
        {
            if (objeler.gameObject.CompareTag("Player"))
            {
                Suphelenme = true;
                animatorum.SetBool("Yurume", true);
                Hedef = objeler.gameObject;

                navMesh.SetDestination(Hedef.transform.position);
            }
            else
            {
                Suphelenme = false;
                Hedef = null;
                if (transform.position != BaslangýcNoktasý)
                {
                    navMesh.stoppingDistance = 1;
                    navMesh.SetDestination(BaslangýcNoktasý);
                    if (navMesh.remainingDistance <= 1)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }


            }
        }
    }


    void Update()
    {
       
    }
}
