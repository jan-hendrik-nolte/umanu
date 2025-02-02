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

namespace Framework.BusinessApplications.Workflows {

    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the status of a workflow step.
    /// </summary>
    public enum WorkflowStepStatus {

        /// <summary>
        /// Workflow step is initial.
        /// </summary>
        [EnumMemberAttribute]
        Initial = 0,

        /// <summary>
        /// Workflow step is active.
        /// </summary>
        [EnumMemberAttribute]
        Active = 1,

        /// <summary>
        /// Workflow step is completed.
        /// </summary>
        [EnumMemberAttribute]
        Completed = 2

    }

}