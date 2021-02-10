using UnityEngine;

namespace ObjectSystemScripts {

	[CreateAssetMenu(fileName = "New Card Object", menuName = "Object System/Objects/Card")]
	public class CardObject : Object {

		// Define o tipo do objeto como carta
		private void Awake() {

			type = ObjectType.Card;
		}

	}

}