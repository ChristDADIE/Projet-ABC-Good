using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    public string enemyName;



    public float baseHealth;
    public float baseSpeed;
    public float baseAttack;
    public float baseAttackSpeed;
    public float baseRange;
    [System.NonSerialized]
    public float health;
    [System.NonSerialized]
    public float speed;
    [System.NonSerialized]
    public float attack;
    [System.NonSerialized]
    public float attackSpeed;
    [System.NonSerialized]
    public float range;
    Vector3 objective;

    

    protected LevelManager level;




    [System.NonSerialized]
    public bool isDead;
    void Start()
    {
        isDead = false;

    }

    virtual public void Setup(LevelManager level, float factor, Vector3 position)
    {
        isDead = false;
        this.level = level;
        health = baseHealth * factor;
        speed = baseSpeed * factor;
        transform.position = position;
        float size = Mathf.Sqrt(factor);
        RectTransform rt = (RectTransform)transform;
        rt.localScale = new Vector3(size, size, size);
        cooldown = 0;
    }

    public void Damage(float amount, string type)
    {
        health -= amount;
        if (health <= 0)
            Death();
    }

    virtual protected void Death()
    {
        isDead = true;
    }

    virtual protected void Animation()
    {

    }

    virtual protected void Behavior()
    {
        if ((transform.position - objective).magnitude < baseRange)
        {
            cooldown += Time.fixedDeltaTime;
            if (cooldown > 1 / attackSpeed)
            {
                // deal damages
                cooldown -= 1 / attackSpeed;
            }
        }
        else
        {
            transform.position += (objective - transform.position).normalized * speed * Time.fixedDeltaTime;
            transform.forward = objective - transform.position;
        }
    }

    public virtual void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerAttack pa))
        {
            Damage damage = pa.Touched();
            for (int i = 0; i != damage.damageTypes.Length; ++i)
                Damage(damage.damageAmounts[i], damage.damageTypes[i]);
        }
    }

    private void Update()
    {
        Animation();
    }



    float cooldown;
    void FixedUpdate()
    {
        Behavior();
    }
}