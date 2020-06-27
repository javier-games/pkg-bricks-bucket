using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Web
{
	[CustomEditor(typeof(WebAPI))]
	// ReSharper disable once InconsistentNaming
	public class WebAPIEditor : OdinEditor
	{

		private Service _serviceToAdd;
		
		private Vector2 _scrollPos;

		private void Awake ()
		{
			_serviceToAdd = new Service ();
		}

		public override void OnInspectorGUI()
		{
			var tree = this.Tree;
			var api = this.target as WebAPI;

			if (api == null) return;
 
			InspectorUtilities.BeginDrawPropertyTree(tree, true);
			
			SirenixEditorGUI.Title ("Host", "", TextAlignment.Left, true);
			tree.GetPropertyAtPath ("_productionHost").Draw();
			tree.GetPropertyAtPath ("_developmentHost").Draw();
			tree.GetPropertyAtPath ("_overrideHost").Draw();
			tree.GetPropertyAtPath ("_activeHost").Draw();
			EditorGUILayout.Space();
			
			SirenixEditorGUI.Title ("Services", "", TextAlignment.Left, true);
			

			tree.GetPropertyAtPath ("_service").Draw();

			//	Preview
			//_serviceToAdd = Service.DrawField (_serviceToAdd, api, ref _scrollPos);
			
			
 
			InspectorUtilities.EndDrawPropertyTree(tree);
		}
		
	}
}