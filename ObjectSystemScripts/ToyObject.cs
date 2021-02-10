using UnityEngine;

namespace ObjectSystemScripts {

	[CreateAssetMenu(fileName = "New Toy Object", menuName = "Object System/Objects/Toy")]
	public class ToyObject : Object {

		// Define o tipo do objeto como carta
		private void Awake() {

			type = ObjectType.Toy;
		}

	}

}