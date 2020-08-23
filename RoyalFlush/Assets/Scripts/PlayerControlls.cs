﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 movement;

    private Rigidbody2D rb;

    public bool isMelee;

    public Weapon weapon = null;

    public Vector2 mousePos;

    public int gold;
    public int hp;
    public int maxHP;

    public Inventory i;

    public int[] seed;

    // Start is called before the first frame update
    void Start()
    {
        seed = new int[9];
        maxHP = 10;
        hp = 10;
        gold = 0;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        if (weapon != null)
        {
            weapon.InstantiateWeapon(this);
            weapon = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButton(0) && (weapon != null))
        {
            weapon.Attack(mousePos);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            weapon.LevelUp();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * movement * Time.fixedDeltaTime);
    }

    public void Pickup(string item)
    {
        i.Pickup(item);
    }

    public void LoadFromSave(PlayerData data)
    {
        seed = data.seed;
        gold = data.gold;
        hp = data.health;
        maxHP = data.maxHealth;
        gameObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.dropsLeft = data.dropsLeft;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.cardsLeft = data.cardsLeft;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.minCardsLeft = data.minCardsLeft;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.cardsDropped = data.cardsDropped;

    }
}
