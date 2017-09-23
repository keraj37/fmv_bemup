using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController player2;

    public SoundCombo[] startFightSounds;
    public SoundCombo[] endFightSounds;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        player1.onHitTry += x => TryHit(player1, x);
        player2.onHitTry += x => TryHit(player2, x);
    }

    public void Switch(bool isOn)
    {
        player1.gameObject.SetActive(isOn);
        player2.gameObject.SetActive(isOn);

        if(isOn)
        {
            player1.Reset();
            player2.Reset();

            SoundManager.PlaySound(startFightSounds);
        }
    }

    public void End(PlayerController wonPlayer)
    {
        player1.active = false;
        player2.active = false;

        SoundManager.PlaySound(endFightSounds);
        UIManager.ShowGameOver(wonPlayer);
    }

    private void TryHit(PlayerController player, Anims anim)
    {
        if (player1.transform.position.x > player2.transform.position.x)
        {
            if (Vector3.Distance(player1.transform.position, player2.transform.position) <= anim.anim.hitDistance)
            {
                if (player == player1)
                {
                    if (player2.Hit(anim.hitAmount * Random.Range(0.5f, 1.5f)))
                    {
                        End(player1);
                    }
                }
                else
                {
                    if (player1.Hit(anim.hitAmount * Random.Range(0.5f, 1.5f)))
                    {
                        End(player2);
                    }
                }
            }
        }
    }
}
