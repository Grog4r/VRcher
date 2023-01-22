using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Projectile : XRGrabInteractable
{
    public float launchForce = 1000.0f;
    public Transform tip = null;

    private bool inAir = false;
    private bool inTarget = false;
    private GameObject attachedTo = null;
    private Vector3 targetOffset = Vector3.zero;

    private Vector3 lastPosition = Vector3.zero;

    private Rigidbody rigidBody = null;

    public TrailRenderer trailRenderer = null;


    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            lastPosition = tip.position;
        }
        if (inTarget)
        {
            this.transform.position = attachedTo.transform.position + targetOffset;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            attachToTarget(collision.gameObject);

            Target target = collision.gameObject.GetComponent<Target>();
            Vector3 targetCenterPosition = target.transform.position;
            int score = FigureOutScore(targetCenterPosition);
            target.InvokeHitEvent(score);
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            print("Stopping arrow!");
            Stop();
        }
    }

    private void attachToTarget(GameObject target)
    {
        attachedTo = target;
        targetOffset = this.gameObject.transform.position - target.transform.position;
        inTarget = true;
        trailRenderer.enabled = false;
    }

    private int FigureOutScore(Vector3 targetCenterPosition)
    {
        float distanceFromCenter = Vector3.Distance(targetCenterPosition, tip.position);
        int score = (int)Math.Ceiling(10 - (distanceFromCenter * 2 * 10));
        score = score > 0 ? score : 1;

        return score;
    }


    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
    }

    public void Release(float pullForce)
    {
        // Set required bools for arrow in air
        inAir = true;
        SetPhysics(true);

        //colliders[0].enabled = false;
        interactionLayers = 1 << LayerMask.NameToLayer("Ignore");

        // Launch the arrow
        Vector3 launchVector = transform.forward * launchForce * pullForce;
        rigidBody.AddForce(launchVector);

        // Start coroutine for rotating the arrow
        StartCoroutine(RotateWithVelocity());

        // Set the lastPosition for the first time
        lastPosition = tip.position;

    }

    private void SetPhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;
        rigidBody.useGravity = usePhysics;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();

        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    public new void OnSelectEntering(XRBaseInteractor interactor)
    {
        base.OnSelectEntering(interactor);
    }

    public new void OnSelectExiting(XRBaseInteractor interactor)
    {
        base.OnSelectExiting(interactor);
    }
}
