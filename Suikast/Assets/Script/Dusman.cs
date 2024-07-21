using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    [Header("D�GER AYARLAR")]
    NavMeshAgent navMesh;
    Animator animatorum;
    GameObject Hedef;

    [Header("GENEL AYARLAR")]
    float AtesEtmeMenzilDeger = 7;
    float SuphelenmeMenzilDeger = 10;
    Vector3 Baslang�cNoktas�;
    bool AtesEdilsinMi = false;
    bool Suphelenme = false;

    [Header("DEVR�YE AYARLARI")]
    public GameObject[] DevriyeNoktalar�_1;
    public GameObject[] DevriyeNoktalar�_2;
    public GameObject[] DevriyeNoktalar�_3;
    public bool DevriyeVarmi;
    Coroutine DevriyeAt;
    
   

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animatorum = GetComponent<Animator>();
        Baslang�cNoktas� = transform.position;
        

    }

    IEnumerator DevriyeTeknik�slem()
    {
        animatorum.SetBool("Yurume", true);
        int toplamnokta = DevriyeNoktalar�_1.Length-1;
        int baslang�cdeger = 0;
        
        while (true && !DevriyeVarmi) 
        {
            if (Vector3.Distance(transform.position, DevriyeNoktalar�_1[baslang�cdeger].transform.position) <= 1f) 
            {
                if (toplamnokta>baslang�cdeger)
                {
                    ++baslang�cdeger;
                    navMesh.SetDestination(DevriyeNoktalar�_1[baslang�cdeger].transform.position);
                }
                else
                {
                    navMesh.stoppingDistance = 1;
                    navMesh.SetDestination(Baslang�cNoktas�);
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
                if (toplamnokta > baslang�cdeger)
                {
                    navMesh.SetDestination(DevriyeNoktalar�_1[baslang�cdeger].transform.position);
                }
            }
            
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (DevriyeVarmi)
        {
            DevriyeAt = StartCoroutine(DevriyeTeknik�slem());
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
                if (transform.position != Baslang�cNoktas�)
                {
                    navMesh.stoppingDistance = 1;
                    navMesh.SetDestination(Baslang�cNoktas�);
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
