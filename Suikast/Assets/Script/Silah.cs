using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Silah : MonoBehaviour
{
    [Header("AYARLAR")]
    float AtesEtmeSikligi_1;
    public float AtesEtmeSikligi_2;
    public float Menzil;
    int ToplamMermiSay�s� = 30;
    int SarjorKapesitesi = 5;
    int KalanMermi;
    float DarbeGucu = 25;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;
    [Header("Sesler")]
    public AudioSource[] Sesler;

    [Header("Efektler")]
    public ParticleSystem[] Efektler;

    //public ParticleSystem Mermi�zi;
    //public ParticleSystem KanEfekti;

    [Header("GENEL �SLEMLER")]
    public Camera BenimKameram;
    public Animator KarekterinAnimatoru;

    // Start is called before the first frame update
    void Start()
    {
        KalanMermi = SarjorKapesitesi;
        ToplamMermi_Text.text = ToplamMermiSay�s�.ToString();
        KalanMermi_Text.text = SarjorKapesitesi.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadKontrol();

        }
        if (KarekterinAnimatoru.GetBool("Reload"))
        {
            Reload�slemTeknikFonksiyon();
        }



        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time > AtesEtmeSikligi_1 && KalanMermi != 0)
            {
                AtesEt();
                AtesEtmeSikligi_1 = Time.time + AtesEtmeSikligi_2;
            }
            if (KalanMermi == 0)
            {
                Sesler[1].Play();
            }

        }
    }
    void AtesEt()
    {
        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();
        Sesler[0].Play();
        Efektler[0].Play();
        KarekterinAnimatoru.Play("Egilme Ates");

        RaycastHit hit;

        if (Physics.Raycast(BenimKameram.transform.position, BenimKameram.transform.forward, out hit, Menzil))
        {

            Instantiate(Efektler[1], hit.point, Quaternion.LookRotation(hit.normal));

        }
    }

    void ReloadKontrol()
    {
        if (KalanMermi < SarjorKapesitesi && ToplamMermiSay�s� != 0)
        {
            KarekterinAnimatoru.Play("sarjordegistir");
            if (!Sesler[2].isPlaying)
            {
                Sesler[2].Play();
            }



        }
    }
    void Reload�slemTeknikFonksiyon()
    {
        if (KalanMermi == 0)
        {
            if (ToplamMermiSay�s� <= SarjorKapesitesi)
            {
                KalanMermi = ToplamMermiSay�s�;
                ToplamMermiSay�s� = 0;
            }
            else
            {
                ToplamMermiSay�s� -= SarjorKapesitesi;
                KalanMermi = SarjorKapesitesi;

            }


        }
        else
        {
            if (ToplamMermiSay�s� <= SarjorKapesitesi)
            {
                int OlusanDeger = KalanMermi + ToplamMermiSay�s�;

                if (OlusanDeger > SarjorKapesitesi)
                {
                    KalanMermi = SarjorKapesitesi;
                    ToplamMermiSay�s� = OlusanDeger - SarjorKapesitesi;
                }
                else
                {
                    KalanMermi += ToplamMermiSay�s�;
                    ToplamMermiSay�s� = 0;
                }
            }
            else
            {

                int MevcutMermimiz = SarjorKapesitesi - KalanMermi;
                ToplamMermiSay�s� -= MevcutMermimiz;
                KalanMermi = SarjorKapesitesi;
            }

        }

        ToplamMermi_Text.text = ToplamMermiSay�s�.ToString();
        KalanMermi_Text.text = SarjorKapesitesi.ToString();
        KarekterinAnimatoru.SetBool("Reload", false);
    }
}
