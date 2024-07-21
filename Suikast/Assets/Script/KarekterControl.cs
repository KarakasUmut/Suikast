using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BenimKutuphanem;

public class KarekterControl : MonoBehaviour
{
    float inputX;
    public Transform karekter;
    Animator anim;
    Vector3 mevcutyon;
    Camera Maincam;
    float maksimumuzunluk = 1;
    float rotationSpeed = 10;
    float MaxSpeed;
    Animasyon animasyon = new Animasyon();


    float[] Sol_Yon_Parametreleri = { 0.12f , 0.34f, 0.63f , 0.92f };
    float[] Sag_Yon_Parametreleri = { 0.12f, 0.34f, 0.63f, 0.92f };
    float[] Egilme_Yon_Parametreleri = { 0.2f, 0.35f, 0.40f, 0.45f, 1f };

    void Start()
    {
    
        anim = GetComponent<Animator>();
        Maincam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        animasyon.Karekter_hareket(anim,"speed",maksimumuzunluk, 1, 0.2f);

        animasyon.Karekter_Rotation(Maincam, rotationSpeed,gameObject);

       animasyon.Sol_Hareket(anim,  "Sol Hareket", "Sol_Aktifmi", animasyon.ParametreOlustur(Sol_Yon_Parametreleri));
       animasyon.Sag_Hareket(anim, "Sag Hareket", "Sag_Aktifmi", animasyon.ParametreOlustur(Sag_Yon_Parametreleri));
       animasyon.Geri_Hareket(anim, "Geri yuru");
       animasyon.Egilme_Hareket(anim, "Egilme Hareket", animasyon.ParametreOlustur(Egilme_Yon_Parametreleri));

           
    }




}
