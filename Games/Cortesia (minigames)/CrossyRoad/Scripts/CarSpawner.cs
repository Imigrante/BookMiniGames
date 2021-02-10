using System;
using Unity.Mathematics;
using UnityEngine;

namespace Games.CrossyRoad.Scripts {

	public class CarSpawner : MonoBehaviour {

		public GameObject car;
		public GameObject carParent;

		[Header("Street Size Builder")]

		// Define o centro da área onde as pistas ficarão
		public Vector3 areaCenter;

		// Define a largura da área onde as pistas ficarão
		[Space(10)] public float areaWidth;

		// Define a altura da área onde as pistas ficarão
		public float areaHeight;

		// Define a quantidade de linhas
		[Space(10)] public int rowQuantity;

		// Define o espaço entre as linhas
		public float spaceBtwRow;

		[Header("Streets Data")]

		// Armazena o intervalo inicial de tempo entre a criação dos veículos
		public float[] initialVehicleSpawnTime = new float[5];
	
		// Armazena o intervalo de tempo atual entre a criação dos veículos
		public float[] currentlyVehiclesSpawnTime;
	
		// Armazena os dados das pistas construídas pelo spawner
		public StreetInfo[] streets;

		// Start is called before the first frame update
		private void Start() {

			// Iniciando os arrays
			streets = new StreetInfo[rowQuantity];
			currentlyVehiclesSpawnTime = new float[initialVehicleSpawnTime.Length];

			// Passa os intervalos iniciais para o array que irá armazenar o tempo atual
			for (var i = 0; i < initialVehicleSpawnTime.Length; i++) {

				currentlyVehiclesSpawnTime[i] = initialVehicleSpawnTime[i];
			}
		
			// Define a altura da linha
			var rowHeight = (areaHeight - spaceBtwRow * (rowQuantity + 1)) / rowQuantity;

			// Define o centro da primeira linha a ser adicionada ao array
			var nextAreaCenter = new Vector3(0f, areaHeight / 2 - rowHeight, 0f);

			// Cria as pistas e armazena suas informações no array
			for (var i = 0; i < rowQuantity; i++) {

				streets[i] = new StreetInfo(nextAreaCenter);

				nextAreaCenter = new Vector3(0f, nextAreaCenter.y - rowHeight - spaceBtwRow, 0f);
			}

			// // Gera os primeiros carros
			// foreach (var street in streets) {
			//
			// 	street.SpawnCar(car, carParent, areaWidth);
			// }
		}

		// Update is called once per frame
		private void Update() {

			// Diminui o intervalo de tempo para cada pista
			for (var i = 0; i < currentlyVehiclesSpawnTime.Length; i++) {

				currentlyVehiclesSpawnTime[i] -= Time.deltaTime;

				// Se o tempo chegar a zero, cria um carro e restaura o valor inicial do tempo
				if (currentlyVehiclesSpawnTime[i] <= 0f) {
				
					streets[i].SpawnCar(car, carParent, areaWidth);

					currentlyVehiclesSpawnTime[i] = initialVehicleSpawnTime[i];
				}
			}
		}

		// Desenha a pista na tela (apenas no editor)
		private void OnDrawGizmosSelected() {

			// Desenha os limites do conjunto de pistas
			Gizmos.DrawWireCube(areaCenter, new Vector3(areaWidth, areaHeight, 0f));

			// Define a altura da linha
			var rowHeight = (areaHeight - spaceBtwRow * (rowQuantity + 1)) / rowQuantity;

			// Define o centro da primeira linha a ser adicionada ao array
			var nextAreaCenter = new Vector3(0f, areaHeight / 2 - rowHeight, 0f);

			// Desenha as pistas
			for (int i = 0; i < rowQuantity; i++) {

				Gizmos.DrawWireCube(nextAreaCenter, new Vector3(areaWidth, rowHeight, 0f));
				nextAreaCenter = new Vector3(0f, nextAreaCenter.y - rowHeight - spaceBtwRow, 0f);
			}
		}

	}

	[Serializable]
	public class StreetInfo {

		// Centro da estrada
		public Vector3 Pos;

		/// <summary>
		/// Direita = 1, Esquerda = -1
		/// </summary>
		[Space(10)] public int Direction;

		// Construtor
		public StreetInfo(Vector3 pos) {
		
			Pos = pos;
			SetStreetDir();
		}

		// Cria um carro
		public void SpawnCar(GameObject _vehicle, GameObject _carParent, float _width) {

			var xPos = Direction == 1 ? -_width / 2 : _width / 2;
			var vehiclePos = new Vector3(xPos, Pos.y, -1f);

			GameObject vehicle = GameObject.Instantiate(_vehicle, vehiclePos, Quaternion.identity, _carParent.transform);

			SetCarDir(vehicle.GetComponent<CarBehaviour>());
		}

		// Gera um número aleatório que define a direção dos carros naquela pista
		public void SetStreetDir() {

			var n = UnityEngine.Random.Range(1, 100);

			Direction = n >= 1 && n <= 50 ? 1 : -1;
		}

		// Define a direção do carro
		public void SetCarDir(CarBehaviour _car) {

			_car.direction = Direction;
		}

	}

}