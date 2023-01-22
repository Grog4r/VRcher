using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{

    public Transform barrel = null;
    public GameObject projectilePrefab = null;
    GameObject arrows = null;

    private XRGrabInteractable interactable = null;

    public float pullForce = 10.0f;

    private Vector3 resetPosition = Vector3.zero;
    private Quaternion resetRotation = Quaternion.identity;

    void Start()
    {
        resetPosition = this.transform.position;
        resetRotation = this.transform.rotation;
        arrows = GameObject.Find("Arrows");
    }

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();_
    }

    public void ResetWeapon() {
        print("Weapon " + this.name + " reset.");
        this.transform.position = resetPosition;
        this.transform.rotation = resetRotation;
    }

    private void OnEnable()
    {
        interactable.onActivate.AddListener(Fire);
    }

    private void OnDisable()
    {
        interactable.onActivate.RemoveListener(Fire);
    }

    private void Fire(XRBaseInteractor interactor)
    {
        CreateProjectile();
        SendToScoreBoards();
    }

    private void CreateProjectile()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        projectileObject.transform.parent = arrows.transform;

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Release(pullForce);
    }

    private void SendToScoreBoards() {
        ScoreBoard.AddArrow();
    }
}
