using UnityEngine;
using UnityEngine.Splines;

public class Runner : MonoBehaviour
{
    public float speed = 5f;
    public int level = 1;
    private bool isBoosting = false;
    public SplineAnimate splineAnimate { get; private set; }

    void Awake()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        if (splineAnimate == null)
        {
            splineAnimate = gameObject.AddComponent<SplineAnimate>();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartBoost();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            EndBoost();
        }
    }

    public void StartRunner(SplineContainer splineContainer, int characterLevel)
    {
        level = characterLevel;

        if (splineAnimate == null)
        {
            splineAnimate = gameObject.AddComponent<SplineAnimate>();
        }

        splineAnimate.Container = splineContainer;
        splineAnimate.Duration = speed;
        splineAnimate.Play();
    }
    private void StartBoost()
    {
        if (!isBoosting)
        {
            isBoosting = true;
            float progress = splineAnimate.NormalizedTime;

            splineAnimate.Pause();
            splineAnimate.Duration = 3f; 
            splineAnimate.NormalizedTime = progress;
            splineAnimate.Play();
        }
    }

    private void EndBoost()
    {
        if (isBoosting)
        {
            isBoosting = false;

            float progress = splineAnimate.NormalizedTime;

            splineAnimate.Pause();
            splineAnimate.Duration = speed; 
            splineAnimate.NormalizedTime = progress;
            splineAnimate.Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            GameManager.Instance.AddScore(level);
        }
    }
}
