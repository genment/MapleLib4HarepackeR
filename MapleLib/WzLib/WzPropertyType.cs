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

namespace MapleLib.WzLib
{
	public enum WzPropertyType
	{
		#region Regular
		Null,
		Short,
		Int,
        Long,
		Float,
		Double,
		String,
		#endregion

		#region Extended
		SubProperty,
		Canvas,
		Vector,
		Convex,
		Sound,
		Raw,
		UOL,
		Lua,
		#endregion

		#region Png
		PNG
		#endregion
	}
}