using UnityEngine;

namespace Games.CrossyRoad.Scripts {

	public class CarBehaviour : MonoBehaviour {

		public float speed;

		/// <summary>
		/// Direita = 1, Esquerda = -1
		/// </summary>
		public int direction;

		private CarSpawner carSpawner;

		private void Start() {

			carSpawner = GameObject.Find("GameManager").GetComponent<CarSpawner>();
		}

		// Update is called once per frame
		private void Update() {

			// Move para uma determinada direção. A variável direction define se o objeto vai
			// ou para a esquerda ou para a direita.
			transform.Translate(Vector3.right * (speed * direction));
		
			// Verifica se é hora de explodir
			CanSelfDestroy();
		}

		private void CanSelfDestroy() {

			if (transform.position.x > carSpawner.areaWidth / 2) {

				Destroy(gameObject);
			}
			else if (transform.position.x < -carSpawner.areaWidth / 2) {

				Destroy(gameObject);
			}
		}

	}

}