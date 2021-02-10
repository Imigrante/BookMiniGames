using System.Collections.Generic;
using System.Linq;
using ObjectSystemScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Games.ShadowGame.Scripts {

	public class GameManager : MonoBehaviour {

		// Base de dados dos briquedos
		public ObjectDatabase toyDatabase;

		// Imagem que irá mostrar a silhueta de um brinquedo
		public Image silhouetteImage;

		// Painel onde serão inseridas as opções
		public GridLayoutGroup optionsPanel;

		// Prefab da opção a ser criada pelo gerenciador
		public GameObject optionToSpawn;

		[Header("Options' parameters")] [Range(3, 8)]

		// Nº de alternativas a serem geradas na tela
		public int numberOfOptions;

		// Define se a alternativa correta estará entre as opções geradas
		public bool generateCorrectOption;

		// Armazena o ID da alternativa correta
		private int _correctOptionID;

		// Armazena todos as alternativas que estão na tela
		private Dictionary<int, GameObject> _optionsDisplayed = new Dictionary<int, GameObject>();

		private void Start() {

			ChooseSilhouette();
			GenerateOptions();
		}

		public void WinCondition() {

			foreach (var option in _optionsDisplayed.Where(option => option.Value.GetComponent<Option>().chosen)) {
				print(option.Key == _correctOptionID ? "ganhou" : "errou");
			}
		}

		// Ajusta o espaço entre as opções com base na quantidade de alternativas
		private void SetOptionsPanelSpacing() {

			optionsPanel.spacing = numberOfOptions == 8 ? new Vector2(10f, 10f) : new Vector2(50f, 50f);
		}

		// Escolhe aleatoriamente uma das silhuetas presentes na base de dados
		public void ChooseSilhouette() {

			// Gera um nº aleatório e o armazena como o ID da alternativa correta
			// Esse nº é utilizado para selecionar o brinquedo escolhido
			_correctOptionID = Random.Range(0, toyDatabase.objects.Length);

			// Troca a sprite na tela pela silhueta armazenada no brinquedo escolhido
			silhouetteImage.sprite = toyDatabase.getToy[_correctOptionID].hiddenImage;
		}

		// Gera as alternativas que estarão na tela
		public void GenerateOptions() {

			// Ajusta o espaço entre as opções com base na quantidade de alternativas
			SetOptionsPanelSpacing();

			// Verifica se existe alguma opção na tela
			if (_optionsDisplayed.Count != 0) {

				// Destrói as opções que estão na tela
				foreach (var n in _optionsDisplayed) {

					Destroy(n.Value);
				}
			}

			// Parent do optionsPanel
			var optionsObj = optionsPanel.gameObject;

			// Lista com os números não sorteados
			var openNumbers = new List<int>();

			// Adiciona os valores que serão utilizados no sorteio à uma lista
			for (int i = 0; i < toyDatabase.objects.Length; i++) {

				openNumbers.Add(i);
			}

			// Cria as opções com base na quantidade definida em numberOfOptions
			for (int i = 0; i < numberOfOptions; i++) {

				// Opção que está sendo construída
				GameObject option;

				// Verifica se é a última repetição do laço
				if (i == numberOfOptions - 1) {

					// Verifica se obrigatoriamente a alternativa correta deve estar entre as opções
					if (generateCorrectOption) {

						// Verifica se a alternativa correta já foi gerada
						if (openNumbers.Contains(_correctOptionID)) {

							// Cria a alternativa na tela e a deixa como child do painel de opções
							option = Instantiate(optionToSpawn, optionsObj.transform);

							// Altera a sprite da alternativa criada
							option.GetComponent<Image>().sprite = toyDatabase.getToy[_correctOptionID].commonImage;

							// Adiciona a alternativa criada à lista de opções que estão sendo exibidas na tela
							_optionsDisplayed.Add(_correctOptionID, option);

							// Vaza da função
							return;
						}
					}
				}

				var random = new System.Random();

				// Sorteia um nº com base no limite da lista dos nºs não sorteados
				var number = openNumbers[random.Next(openNumbers.Count)];

				// Remove o nº sorteado da lista dos nºs não sorteados
				openNumbers.Remove(number);

				// Cria a alternativa na tela e a deixa como child do painel de opções
				option = Instantiate(optionToSpawn, optionsObj.transform);

				// Altera a sprite da alternativa criada
				option.GetComponent<Image>().sprite = toyDatabase.getToy[number].commonImage;

				// Adiciona a alternativa criada à lista de opções que estão sendo exibidas na tela
				_optionsDisplayed.Add(number, option);

				// print(i + " : " + number);
			}
		}

	}

}