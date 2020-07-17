using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// 親オブジェクトの位置のみリセットするエディタ拡張
	/// </summary>
	internal static class ParentPositionResetter
	{
		[MenuItem( "CONTEXT/Transform/Reset Position Only Parent" )]
		private static void Reset( MenuCommand command )
		{
			var parent     = ( Transform ) command.context;
			var tempParent = new GameObject();
			var children   = parent.Cast<Transform>().ToArray();

			var parentAndChildren = children
					.Prepend( parent )
					.OfType<Object>()
					.ToArray()
				;

			Undo.RecordObjects( parentAndChildren, "Reset Position Only Parent" );

			foreach ( var child in children )
			{
				child.parent = tempParent.transform;
			}

			parent.position = Vector3.zero;

			foreach ( var child in children )
			{
				child.parent = parent;
			}

			Object.DestroyImmediate( tempParent );
		}
	}
}