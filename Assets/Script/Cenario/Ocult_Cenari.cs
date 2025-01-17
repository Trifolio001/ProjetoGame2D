using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocult_Cenari : MonoBehaviour
{


    public string compareTag = "Player";


    public SpriteRenderer spriteRendererOcult5;
    public SpriteRenderer spriteRendererOcult6;


    public bool Visible;
   

    private int selectedPoint;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            PassaLista();
        }
    }



    void Start()
    {
        // Obt�m o componente SpriteRenderer
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // M�todo para alterar a transpar�ncia

        //meshRender.material.SetColor("_Color", Color.Lerp(Color.white, Color.black, influenceColor));
        // Exemplo: Aumenta a transpar�ncia com base no tempo
        //float alpha = Mathf.PingPong(Time.time, 1.0f);
        //transparencyController.SetTransparency(alpha);
    

    /*public void Lista()
    {
        Foreach(nomedachamadadalistaouVAR namevariavel in nomedalista)
        {
            Passa a informa��o da �vari�vel� em toda a lista �list�
        }

    }

    */ 


    private void PassaLista()
    {

        //for (int i = 0; i < ListBoxCollider.Count; i++)
        {
            //ListBoxColliderSetup SetBoxCollider = ListBoxCollider[(i)];
            //SpriteRenderer spriteRenderer1 = Instantiate(SetBoxCollider.spriteRendererOcult);

            if (Visible == false)
            {
                if (spriteRendererOcult5.color.a > 0.6)
                {
                    StartCoroutine(Opera��odeTempoSumir(spriteRendererOcult5));
                    StartCoroutine(Opera��odeTempoSumir(spriteRendererOcult6));
                }
            }
            else
            {
                if (spriteRendererOcult5.color.a < 0.4)
                {
                    StartCoroutine(Opera��odeTempoAparecer(spriteRendererOcult5));
                    StartCoroutine(Opera��odeTempoAparecer(spriteRendererOcult6));
                }
            }
        }

    }

    IEnumerator Opera��odeTempoSumir(SpriteRenderer spriteRendererOcult1)
    {
        float colorA = spriteRendererOcult1.color.a;
        for (float i = colorA; i >= 0; i -= 0.2f)
        {
            yield return new WaitForSeconds(0.1f);
            SetTransparency(i, spriteRendererOcult1);
        }
    }


    IEnumerator Opera��odeTempoAparecer(SpriteRenderer spriteRendererOcult1)
    {

        float colorA = spriteRendererOcult1.color.a;
        for (float i = colorA; i <= 1; i += 0.2f)
        {
            yield return new WaitForSeconds(0.1f);
            SetTransparency(i, spriteRendererOcult1);
        }
    }



    public void SetTransparency(float alpha, SpriteRenderer spriteRendererOcult1)
    {
        Debug.Log("ola " + spriteRendererOcult1 + " = " + alpha);
        alpha = Mathf.Clamp01(alpha);
        Color color = spriteRendererOcult1.color;
        color.a = alpha;
        spriteRendererOcult1.color = color;
    }

}
