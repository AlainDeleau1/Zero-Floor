using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource shotgunPickUpSound, shootSound, reloadSound, outOfAmmoSound, enemyAttackSound, enemyDamagedSound, enemyDeadSound, playerDamageSound, playerDamagedSound, rifleShotSound, riflePickUpSound, explosionSound, rifleReloadSound, deathEnemySound, moaningEnemySound, AWPShootSound, AWPReloadSound, kamikazeDeathSound;

    public void ShotgunPickUpSound()
    {
        shotgunPickUpSound.PlayOneShot(shotgunPickUpSound.clip);
    }

    public void ShootSound()
    {
        shootSound.PlayOneShot(shootSound.clip);
    }

    public void ReloadSound()
    {
        reloadSound.PlayOneShot(reloadSound.clip);
    }

    public void OutOfAmmoSound()
    {
        outOfAmmoSound.PlayOneShot(outOfAmmoSound.clip);
    }

    public void EnemyAttackSound()
    {
        enemyAttackSound.PlayOneShot(enemyAttackSound.clip);
    }

    public void EnemyDamagedSound()
    {
        enemyDamagedSound.PlayOneShot(enemyDamagedSound.clip);
    }

    public void EnemyDeadSound()
    {
        enemyDeadSound.PlayOneShot(enemyDeadSound.clip);
    }

    public void PlayerAttackSound()
    {
        playerDamageSound.PlayOneShot(playerDamageSound.clip);
    }

    public void PlayerDamagedSound()
    {
        playerDamagedSound.PlayOneShot(playerDamagedSound.clip);
    }

    public void RifleShotSound()
    {
        rifleShotSound.PlayOneShot(rifleShotSound.clip);
    }

    public void RiflePickUpSound()
    {
        riflePickUpSound.PlayOneShot(riflePickUpSound.clip);
    }
    public void ExplosionDefeat()
    {
        explosionSound.PlayOneShot(explosionSound.clip);
    }

    public void RifleReloadSound()
    {
        rifleReloadSound.PlayOneShot(rifleReloadSound.clip);
    }

    public void _AWPShootSound()
    {
        AWPShootSound.PlayOneShot(AWPShootSound.clip);
    }

    public void _AWPReloadSound()
    {
        AWPReloadSound.PlayOneShot(AWPReloadSound.clip);
    }
    public void KamikazeDeathSound()
    {
        kamikazeDeathSound.PlayOneShot(kamikazeDeathSound.clip);
    }

    public AudioSource[] GetAudioSources()
    {
        return GetComponentsInChildren<AudioSource>();
    }

}
