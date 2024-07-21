using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BenimKutuphanem
{
    public class Animasyon
    {

        float MaxSpeedClass;
        float MaxinputXClass;

       

        public float YonuDisariCikar()
        {
            return MaxinputXClass;
        }

        // public void Sol_Hareket(Animator anim,string AnaParametreAd�,string KontrolParametreAd�,float yurume,float kosma,float ilerisol,float gerisol)
        public void Sol_Hareket(Animator anim, string AnaParametreAd�, string KontrolParametreAd�, List<float> ParametreDegerleri)
        {
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool(KontrolParametreAd�, true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[1]);

                }
                else if (Input.GetKey(KeyCode.W))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[3]);
                }
                else
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[0]);
                }
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetFloat(AnaParametreAd�, 0f);
                anim.SetBool(KontrolParametreAd�, false);

            }
        }
        public void Egilme_Hareket(Animator anim, string AnaParametreAd�,List<float> ParametreDegerleri)
        {
            if (Input.GetKey(KeyCode.C))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[1]);

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[3]);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[4]);
                }
                else
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[0]);
                }
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                anim.SetFloat(AnaParametreAd�, 0f);
            }
        }
        public void Sag_Hareket(Animator anim, string AnaParametreAd�, string KontrolParametreAd�, List<float> ParametreDegerleri)
        {
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool(KontrolParametreAd�, true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[1]);

                }
                else if (Input.GetKey(KeyCode.W))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[3]);
                }
                else
                {
                    anim.SetFloat(AnaParametreAd�, ParametreDegerleri[0]);
                }
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetFloat(AnaParametreAd�, 0f);
                anim.SetBool(KontrolParametreAd�, false);

            }
        }
        public void Geri_Hareket(Animator anim, string AnaParametreAd�)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool(AnaParametreAd�, true);


            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool(AnaParametreAd�, false);
            }
        }
        public void Karekter_hareket(Animator anim, string hizdegeri,float maksimumuzunluk, float TamHiz, float YurumeHizi)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MaxSpeedClass = TamHiz;


            }
            else if (Input.GetKey(KeyCode.W))
            {
                MaxSpeedClass = YurumeHizi;
                MaxinputXClass = 1f;

            }
            else
            {
                MaxSpeedClass = 0f;
                MaxinputXClass = 0f;
            }
            anim.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(MaxinputXClass, 0, 0), MaxSpeedClass).magnitude, maksimumuzunluk, Time.deltaTime * 10);
        }
        public void Karekter_Rotation(Camera Maincam ,float rotationSpeed,GameObject karekter)
        {

            Vector3 CamOfset = Maincam.transform.forward;
            CamOfset.y = 0;
            karekter.transform.forward = Vector3.Slerp(karekter.transform.forward, CamOfset, Time.deltaTime * rotationSpeed);
        }
        
        public List<float> ParametreOlustur(float[] deger)
        {
            List<float> Sol_Yon_Parametreleri = new List<float>();

            foreach (float item in deger)
            {
                Sol_Yon_Parametreleri.Add(item);
            }
            return Sol_Yon_Parametreleri;
        }
    }
}

