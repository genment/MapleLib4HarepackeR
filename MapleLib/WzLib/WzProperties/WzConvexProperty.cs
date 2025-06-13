﻿/*  MapleLib - A general-purpose MapleStory library
 * Copyright (C) 2009, 2010, 2015 Snow and haha01haha01
   
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MapleLib.WzLib.Util;

namespace MapleLib.WzLib.WzProperties
{
	/// <summary>
	/// A property that contains several WzExtendedPropertys
	/// </summary>
    public class WzConvexProperty : WzExtended, IPropertyContainer
	{
		#region Fields
		internal WzPropertyCollection properties;
		internal string name;
		internal WzObject parent;
		//internal WzImage imgParent;
		#endregion

		#region Inherited Members
        public override void SetValue(object value)
        {
            throw new NotImplementedException();
        }

        public override WzImageProperty DeepClone()
        {
            WzConvexProperty clone = new WzConvexProperty(name);
            foreach (WzImageProperty prop in properties)
                clone.AddProperty(prop.DeepClone());
            return clone;
        }

    	/// <summary>
		/// The parent of the object
		/// </summary>
		public override WzObject Parent { get { return parent; } internal set { parent = value; } }
		/*/// <summary>
		/// The image that this property is contained in
		/// </summary>
		public override WzImage ParentImage { get { return imgParent; } internal set { imgParent = value; } }*/
		/// <summary>
		/// The WzPropertyType of the property
		/// </summary>
		public override WzPropertyType PropertyType { get { return WzPropertyType.Convex; } }
		/// <summary>
		/// The properties contained in the property
		/// </summary>
		public override WzPropertyCollection WzProperties
		{
			get
			{
                return properties; //properties.ConvertAll<IWzImageProperty>(new Converter<IExtended, IWzImageProperty>(delegate(IExtended source) { return (IWzImageProperty)source; }));
			}
		}
		/// <summary>
		/// The name of this property
		/// </summary>
		public override string Name { get { return name; } set { name = value; } }
		/// <summary>
		/// Gets a wz property by it's name
		/// </summary>
		/// <param name="name">The name of the property</param>
		/// <returns>The wz property with the specified name</returns>
		public override WzImageProperty this[string name]
		{
			get
			{
                string nameLower = name.ToLower();
                foreach (WzImageProperty iwp in properties)
					if (iwp.Name.ToLower() == nameLower)
						return iwp;
				//throw new KeyNotFoundException("A wz property with the specified name was not found");
				return null;
			}
		}

        public WzImageProperty GetProperty(string name)
        {
			string nameLower = name.ToLower();
            foreach (WzImageProperty iwp in properties)
                if (iwp.Name.ToLower() == nameLower)
                    return iwp;
            return null;
        }

		/// <summary>
		/// Gets a wz property by a path name
		/// </summary>
		/// <param name="path">path to property</param>
		/// <returns>the wz property with the specified name</returns>
		public override WzImageProperty GetFromPath(string path)
		{
			string[] segments = path.Split(new char[1] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
			if (segments[0] == "..")
			{
				return ((WzImageProperty)Parent)[path.Substring(name.IndexOf('/') + 1)];
			}
			WzImageProperty ret = this;
			foreach (string segment in segments)
			{
                ret = ret.WzProperties.FirstOrDefault(iwp => iwp.Name == segment);
                if (ret == null)
                {
                    break;
                }
            }
			return ret;
		}
		public override void WriteValue(WzBinaryWriter writer)
		{
            List<WzExtended> extendedProps = new List<WzExtended>(properties.Count);
            foreach (WzImageProperty prop in properties) 
				if (prop is WzExtended extended) 
					extendedProps.Add(extended);
			writer.WriteStringValue("Shape2D#Convex2D", WzImage.WzImageHeaderByte_WithoutOffset, WzImage.WzImageHeaderByte_WithOffset);
            writer.WriteCompressedInt(extendedProps.Count);

			foreach (WzImageProperty imgProperty in properties) 
			{
				imgProperty.WriteValue(writer);
			}
		}
		public override void ExportXml(StreamWriter writer, int level)
		{
			writer.WriteLine(XmlUtil.Indentation(level) + XmlUtil.OpenNamedTag("WzConvex", this.Name, true));
			WzImageProperty.DumpPropertyList(writer, level, WzProperties);
			writer.WriteLine(XmlUtil.Indentation(level) + XmlUtil.CloseTag("WzConvex"));
		}
		public override void Dispose()
		{
			name = null;
            foreach (WzImageProperty exProp in properties)
				exProp.Dispose();
			properties.Clear();
			properties = null;
		}
		#endregion

		#region Custom Members
		/// <summary>
		/// Creates a blank WzConvexProperty
		/// </summary>
		public WzConvexProperty() {
            this.properties = new WzPropertyCollection(this);
        }
		/// <summary>
		/// Creates a WzConvexProperty with the specified name
		/// </summary>
		/// <param name="name">The name of the property</param>
		public WzConvexProperty(string name)
		{
			this.name = name;

            this.properties = new WzPropertyCollection(this);
        }
		/// <summary>
		/// Adds a WzExtendedProperty to the list of properties
		/// </summary>
		/// <param name="prop">The property to add</param>
        public void AddProperty(WzImageProperty prop)
		{
            if (!(prop is WzExtended))
                throw new Exception("Property is not IExtended");
            prop.Parent = this;
            properties.Add((WzExtended)prop);
		}

        public void AddProperties(WzPropertyCollection properties)
        {
			foreach (WzImageProperty property in properties)
			{
				AddProperty(property);
			}
        }

		/// <summary>
		/// Remove a property by the property object
		/// </summary>
		/// <param name="prop"></param>
        public void RemoveProperty(WzImageProperty prop)
        {
            prop.Parent = null;
            properties.Remove(prop);
        }

        /// <summary>
        /// Remove a property by its name
        /// </summary>
        /// <param name="name">Name of Property</param>
        public void RemoveProperty(string propertyName)
        {
            WzImageProperty prop = this[propertyName];
            if (prop != null)
            {
                RemoveProperty(prop);
            }
        }

        public void ClearProperties()
		{
            foreach (WzImageProperty prop in properties) prop.Parent = null;
			properties.Clear();
		}

		#endregion
	}
}