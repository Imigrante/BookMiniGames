using System.Collections.Generic;
using UnityEngine;

namespace ObjectSystemScripts {

	[CreateAssetMenu(fileName = "New Object Database", menuName = "Object System/Objects/Database")]
	public class ObjectDatabase : ScriptableObject, ISerializationCallbackReceiver {

		public Object[] objects;
		public Dictionary<int, Object> getToy = new Dictionary<int, Object>();

		public void OnAfterDeserialize() {

			// Define o ID de cada objeto, com base em sua posição no Array
			for (int i = 0; i < objects.Length; i++) {

				objects[i].id = i;
				getToy.Add(i, objects[i]);
			}
		}
		
		public void OnBeforeSerialize() {

			getToy = new Dictionary<int, Object>();
		}
	}

}