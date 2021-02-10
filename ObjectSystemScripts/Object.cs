using UnityEngine;

namespace ObjectSystemScripts {
	
	public abstract class Object : ScriptableObject {

		public string objName;
		public int id;
		public ObjectType type;
		public State state;
		public Sprite commonImage;
		public Sprite hiddenImage;
	
		[TextArea(15,10)]
		public string description;
	}

	public enum ObjectType {
		
		Toy,
		Card
	}

	public enum State {

		Hidden,
		Common
	}
}