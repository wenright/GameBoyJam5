using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopController : MonoBehaviour {

	public Text moneyText;
	public Text pageTitleText;
	public Text costText;
	public Image[] levels;

	private int currentPage = 0;
	private int numPages = 6;

	private int currentCost;

	public void Start () {
		LoadPage();
	}

	public void Purchase () {
		if (UpgradesController.money >= currentCost) {
			UpgradesController.money -= currentCost;
			Upgrade();
		} else {
			print("Not enough money!");
		}

		LoadPage();
	}

	public void PageRight () {
		currentPage++;

		if (currentPage >= numPages) {
			currentPage = 0;
		}

		LoadPage();
	}

	public void PageLeft () {
		currentPage--;

		if (currentPage < 0) {
			currentPage = numPages - 1;
		}

		LoadPage();
	}

	private void LoadPage () {
		print(currentPage);

		moneyText.text = "$" + UpgradesController.money;

		switch (currentPage) {
			case 0:
				LoadPage("Damage", UpgradesController.damageLevel, UpgradesController.damageCost);
				break;
			case 1:
				LoadPage("Accuracy", UpgradesController.accuracyLevel, UpgradesController.accuracyCost);
				break;
			case 2:
				LoadPage("FireRate", UpgradesController.fireRateLevel, UpgradesController.fireRateCost);
				break;
			case 3:
				LoadPage("Ammo & Reload", UpgradesController.clipReloadLevel, UpgradesController.clipReloadCost);
				break;
			case 4:
				LoadPage("HP", UpgradesController.hpLevel, UpgradesController.hpCost);
				break;
			case 5:
				LoadPage("Flares", UpgradesController.flaresLevel, UpgradesController.flaresCost);
				break;
			default:
				break;
		}
	}

	private void LoadPage (string title, int level, int cost) {
		pageTitleText.text = title;
		SetLevel(level);
		currentCost = cost;
		costText.text = "$" + cost;
	}

	private void SetLevel (int level) {
		for (int i = 0; i < levels.Length; i++) {
			if (i < level) {
				levels[i].color = Color.white;
			} else {
				levels[i].color = Color.grey;
			}
		}
	}

	private void Upgrade () {
		switch (currentPage) {
			case 0:
				UpgradesController.damageLevel++;
				UpgradesController.damage += 20;
				UpgradesController.explosionRadius += 5;
				UpgradesController.damageCost *= 2;
				break;
			case 1:
				UpgradesController.accuracyLevel++;
				UpgradesController.accuracy *= 0.9f;
				UpgradesController.accuracyCost *= 2;
				break;
			case 2:
				UpgradesController.fireRateLevel++;
				UpgradesController.fireRate *= 0.8f;
				UpgradesController.fireRateCost *= 2;
				break;
			case 3:
				UpgradesController.clipReloadLevel++;
				UpgradesController.clipSize += 10;
				UpgradesController.reloadTime -= 0.7f;
				UpgradesController.clipReloadCost *= 2;
				break;
			case 4:
				UpgradesController.hpLevel++;
				UpgradesController.hp += 2000;
				UpgradesController.hpCost *= 2;
				break;
			case 5:
				UpgradesController.flaresLevel++;
				UpgradesController.flares += 50;
				UpgradesController.flaresCost *= 2;
				break;
			default:
				break;
		}
	}
}
