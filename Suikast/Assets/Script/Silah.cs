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
    int ToplamMermiSayısı = 30;
    int SarjorKapesitesi = 5;
    int KalanMermi;
    float DarbeGucu = 25;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;
    [Header("Sesler")]
    public AudioSource[] Sesler;

    [Header("Efektler")]
    public ParticleSystem[] Efektler;

    //public ParticleSystem Mermiİzi;
    //public ParticleSystem KanEfekti;

    [Header("GENEL İSLEMLER")]
    public Camera BenimKameram;
    public Animator KarekterinAnimatoru;

    // Start is called before the first frame update
    void Start()
    {
        KalanMermi = SarjorKapesitesi;
        ToplamMermi_Text.text = ToplamMermiSayısı.ToString();
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
            ReloadİslemTeknikFonksiyon();
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
        if (KalanMermi < SarjorKapesitesi && ToplamMermiSayısı != 0)
        {
            KarekterinAnimatoru.Play("sarjordegistir");
            if (!Sesler[2].isPlaying)
            {
                Sesler[2].Play();
            }



        }
    }
    void ReloadİslemTeknikFonksiyon()
    {
        if (KalanMermi == 0)
        {
            if (ToplamMermiSayısı <= SarjorKapesitesi)
            {
                KalanMermi = ToplamMermiSayısı;
                ToplamMermiSayısı = 0;
            }
            else
            {
                ToplamMermiSayısı -= SarjorKapesitesi;
                KalanMermi = SarjorKapesitesi;

            }


        }
        else
        {
            if (ToplamMermiSayısı <= SarjorKapesitesi)
            {
                int OlusanDeger = KalanMermi + ToplamMermiSayısı;

                if (OlusanDeger > SarjorKapesitesi)
                {
                    KalanMermi = SarjorKapesitesi;
                    ToplamMermiSayısı = OlusanDeger - SarjorKapesitesi;
                }
                else
                {
                    KalanMermi += ToplamMermiSayısı;
                    ToplamMermiSayısı = 0;
                }
            }
            else
            {

                int MevcutMermimiz = SarjorKapesitesi - KalanMermi;
                ToplamMermiSayısı -= MevcutMermimiz;
                KalanMermi = SarjorKapesitesi;
            }

        }

        ToplamMermi_Text.text = ToplamMermiSayısı.ToString();
        KalanMermi_Text.text = SarjorKapesitesi.ToString();
        KarekterinAnimatoru.SetBool("Reload", false);
    }
}
