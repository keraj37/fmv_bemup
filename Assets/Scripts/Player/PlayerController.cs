using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AnimName
{
    IDLE,
    PUNCH,
    KICK,
    HIT
}

[System.Serializable]
public class SoundCombo
{
    public AudioClip[] sounds;
}

[System.Serializable]
public class Anims
{
    public AnimName name;
    public KeyCode triggerKey;
    public AnimPlayer anim;
    public SoundCombo[] sounds;
}

public class PlayerController : MonoBehaviour
{
    public float regeneration = 0.2f;
    public float consumptionMin = 0.1f;
    public float consumptionMax = 0.25f;

    public float maxX;
    public float minX;

    public KeyCode right;
    public KeyCode left;

    public float speed = 1f;

    public Anims[] anims;

    public LifeBar bar;
    public LifeBar barPower;

    private float power = 1f;
    public float life = 100f;

    public SoundCombo[] tiredSounds;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        DisableAnims();
        StartIdle();
        bar.maxLife = life;
        bar.Life = life;

        barPower.maxLife = power;
        barPower.Life = power;
    }

    private void StartIdle()
    {
        anims.First(x => x.name == AnimName.IDLE).anim.Active = true;
    }

    private void DisableAnims()
    {
        foreach (var i in anims)
            i.anim.Active = false;
    }

    public void Hit(float lifeTaken)
    {
        DisableAnims();
        anims.First(x => x.name == AnimName.HIT).anim.Trigger(() => StartIdle());
        life -= lifeTaken;
        UpdateBars();
    }

    private void UpdateBars()
    {
        if (life < 0f)
            life = 0f;

        bar.Life = life;

        if (power < 0f)
            power = 0f;

        barPower.Life = power;
    }

    void Update()
    {
        if (Input.GetKey(right))
        {
            this.transform.Translate(Vector3.right * speed);

            if (this.transform.position.x > maxX)
            {
                this.transform.position = new Vector3(maxX, this.transform.position.y, this.transform.position.z);
            }
        }
        else if (Input.GetKey(left))
        {
            this.transform.Translate(Vector3.left * speed);

            if (this.transform.position.x < minX)
            {
                this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
            }
        }

        power += regeneration * Time.deltaTime;

        foreach (var i in anims)
        {
            if (i.anim.animType != AnimType.TRIGGER)
                continue;

            if (Input.GetKeyDown(i.triggerKey))
            {
                power -= Random.Range(consumptionMin, consumptionMax);

                if (power <= 0f)
                {
                    SoundManager.PlaySound(tiredSounds);
                }
                else
                {
                    DisableAnims();
                    i.anim.Trigger(() => StartIdle());
                    SoundManager.PlaySound(i.sounds);
                }
            }
        }

        UpdateBars();
    }
}
