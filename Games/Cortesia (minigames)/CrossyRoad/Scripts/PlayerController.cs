using UnityEngine;

namespace Games.CrossyRoad.Scripts {

	public class PlayerController : MonoBehaviour {

		// Ativa os controles por toque na tela
		public bool enableTouchControls;
	
		[HideInInspector]
		public bool touchAnVehicle;
	
		// Armazena a direção do toque na tela
		private Vector3 dir;

		// Start is called before the first frame update
		private void Start() {

			touchAnVehicle = false;
		}

		// Update is called once per frame
		private void Update() {

			if (enableTouchControls) {

				if (Input.touchCount > 0) {

					var touch = Input.GetTouch(0);
				
					// Detecta o ponto onde o toque foi detectado e o armazena a posição do local em questão
					var touchPos = Camera.main.ScreenToWorldPoint(touch.position);

					// var newPos = new Vector3(touchPos.x, touchPos.y, 1f);

					// Verifica qual é a direção do toque
					CheckDirection(transform.position, touchPos);

					// Movimenta o jogador para a direção definida em dir
					transform.position = Vector3.MoveTowards(transform.position, transform.position += dir, .5f);
				}
			}
		}

		//-----------------------------------------------------------
		//							Touch
		//-----------------------------------------------------------

		// Verifica qual é a direção do toque
		private void CheckDirection(Vector3 _currentPos, Vector3 _nextPos) {

			// cima
			if (_currentPos.y < _nextPos.y) {

				dir = Vector3.up * 1.5f;

			} // baixo
			else if (_currentPos.y > _nextPos.y) {

				dir = Vector3.down * 1.5f;
			}

			// Esquerda
			if (_currentPos.x > _nextPos.x) {

				dir = Vector3.left;

			} // Direita
			else if (_currentPos.x < _nextPos.x) {

				dir = Vector3.right;
			}
		}

		//-----------------------------------------------------------
		//							Botões
		//-----------------------------------------------------------
	
		public void MoveUp() {

			transform.position += Vector3.up * 1.5f;
		}

		public void MoveRight() {

			transform.position += Vector3.right;
		}

		public void MoveLeft() {

			transform.position += Vector3.left;
		}

		//-----------------------------------------------------------
		//							Colisão
		//-----------------------------------------------------------
	
		private void OnTriggerEnter2D(Collider2D other) {

			if (other.CompareTag("Vehicle")) {

				touchAnVehicle = true;
			}
		}

	}

}