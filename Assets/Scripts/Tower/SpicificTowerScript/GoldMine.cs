using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : BaseTower {

    public int moneyGain;
    public float moneyGainTime;
    public IngameCurrency currency;

    public void Update()
    {
        InfoPopUp();
        if (placed)
        {
            CheckIfAttacked();
            currency = GameObject.FindWithTag("Manager").GetComponent<IngameCurrency>();
            StartCoroutine(MoneyGainTimer());
            placed = false;
        }
    }

    public IEnumerator MoneyGainTimer()
    {
        currency.currency += moneyGain;
        currency.CurrencyChange();
        yield return new WaitForSeconds(moneyGainTime);
        StartCoroutine(MoneyGainTimer());
    }
}
