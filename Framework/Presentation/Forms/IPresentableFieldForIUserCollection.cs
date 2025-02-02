﻿/*********************************************************************
 * Umanu Framework / (C) Umanu Team / http://www.umanu.de/           *
 *                                                                   *
 * This program is free software: you can redistribute it and/or     *
 * modify it under the terms of the GNU Lesser General Public        *
 * License as published by the Free Software Foundation, either      *
 * version 3 of the License, or (at your option) any later version.  *
 *                                                                   *
 * This program is distributed in the hope that it will be useful,   *
 * but WITHOUT ANY WARRANTY; without even the implied warranty of    *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the     *
 * GNU Lesser General Public License for more details.               *
 *                                                                   *
 * You should have received a copy of the GNU Lesser General Public  *
 * License along with this program.                                  *
 * If not, see <http://www.gnu.org/licenses/>.                       *
 *********************************************************************/

namespace Framework.Presentation.Forms {

    using Framework.Persistence;
    using Framework.Persistence.Directories;

    /// <summary>
    /// Interface for fields of type list of IUser to be presented.
    /// </summary>
    public interface IPresentableFieldForIUserCollection : IPresentableFieldForCollection<IUser> {

        /// <summary>
        /// Directory to use for user resolval.
        /// </summary>
        UserDirectory UserDirectory { get; set; }

        /// <summary>
        /// Sorts the list by a given field.
        /// </summary>
        /// <param name="fieldKey">key of field to order list by</param>
        void Sort(string fieldKey);

    }

}