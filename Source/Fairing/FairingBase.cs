using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSTUTools
{
	public class FairingBase
	{
		public GameObject root;
		public FairingPanel[] panels;
		public GameObject editorColliders;
		
		public FairingBase(GameObject root, GameObject[] panelsGOs)
		{
			this.root = root;
			this.panels = new FairingPanel[panelsGOs.Length];
			for (int i = 0; i < panelsGOs.Length; i++)
			{
				this.panels[i] = new FairingPanel(panelsGOs[i]);
			}
		}
		
		//sets opacity of base only, via ksp bumped-specular shader _Opacity value
		public void setBaseOpacity(float val)
		{
			root.renderer.material.SetFloat("_Opacity", val);
			root.renderer.material.renderQueue = val>=1f? 2000 : 3000;
		}
		
		//sets opacity of panels only, via ksp bumped-specular shader _Opacity value
		public void setPanelOpacity(float val)
		{
			int length = panels.Length;
			for(int i = 0; i < length; i++)
			{
				panels[i].panel.renderer.material.SetFloat("_Opacity", val);
				panels[i].panel.renderer.material.renderQueue = val>=1f? 2000 : 3000;
			}
		}
		
		//sets the material of the root renderer and any panels
		public void setMaterial(Material mat)
		{
			if(root.renderer!=null)
			{
				root.renderer.material = mat;
			}
			int length = panels.Length;
			for(int i = 0; i < length; i++)
			{
				panels[i].panel.renderer.material = mat;
			}
		}
		
		//sets panel rotations around the x-axis, used for deploying panels
		public void setPanelRotations(float rotation)
		{
			//set local rotation around x axis
			int length = panels.Length;
			for(int i = 0; i < length; i++)
			{	
				panels[i].setRotation(rotation);
			}
		}
		
		//enable or disable colliders for individual fairing panels
		public void enablePanelColliders(bool enable, bool convex)
		{
			int length = panels.Length;
			for(int i = 0; i < length; i++)
			{	
				panels[i].enableCollider(enable, convex);
			}
		}
		
		public void enableRootColliders(bool enable, bool convex)
		{
			MeshCollider mc = root.GetComponent<MeshCollider>();
			if(mc==null){mc=root.AddComponent<MeshCollider>();}
			mc.enabled = enable;
			mc.convex = convex;
		}

	}
	
}

