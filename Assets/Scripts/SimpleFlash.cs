using System.Collections;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;  // Material to use for flashing effect
    [SerializeField] private float duration;          // Duration of each flash
    [SerializeField] private int flashTimes;          // Number of times to flash

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    private int originalFlashTimes;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        originalFlashTimes = flashTimes;
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashTimes; i++)
        {
            // Set material to flashMaterial and wait for 'duration' time
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(duration);

            // Reset to the original material and wait for the next flash cycle
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(duration);
        }

        // Reset flash routine and flashTimes
        flashRoutine = null;
        flashTimes = originalFlashTimes;
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public float GetFlashDuration()
    {
        return duration * flashTimes * 2; // Times 2 because of the on and off duration
    }
}
