using UnityEngine;

namespace Games.CrossyRoad.Scripts {

	public class GameManager : MonoBehaviour {

		public CarSpawner carSpawner;
		public GameObject player;

		// Armazena a altura em que o jogador se encontra
		private float playerYPos;
	
		// Armazena a altura máxima do conjunto de pistas
		private float levelYLimit;
	
		// Update is called once per frame
		private void Update() {

			// Coleta o valor da altura em que o jogador se encontra
			playerYPos = player.transform.position.y;
		
			// Coleta a altura máxima do conjunto de pistas
			levelYLimit = carSpawner.areaHeight / 2;

			WinCondition();
			LoseCondition();
		}

		//-----------------------------------------------------------
		//				Condições de Vitória/Derrota
		//-----------------------------------------------------------

		private void WinCondition() {

			if (playerYPos > levelYLimit) {

				print("Ganhou");
			}
		}

		private void LoseCondition() {

			if (player.GetComponent<PlayerController>().touchAnVehicle) {

				print("Me atropelaram aqui ô!");
			}
		}

	}

}