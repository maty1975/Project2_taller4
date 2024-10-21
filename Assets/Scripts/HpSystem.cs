using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HpSystem : MonoBehaviour
{
    public int hp;
    
    public UnityEvent onHit, onMuere;
    public bool destruirAlMorir;
    [ContextMenu("DAÑAR")]
    public void TakeDamage(int damage)
    {
        hp -= damage;
        CheckHP();
    }

    void CheckHP()
    {
        if (hp > 0)
            Hit();
        else
            Muere();
    }

    void Hit()
    {
        onHit.Invoke();
    }
    void Muere()
    {
        onMuere.Invoke();
        if (destruirAlMorir) Destroy(gameObject);
    }


}
